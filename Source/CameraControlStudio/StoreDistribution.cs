using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CameraControlStudio
{
    internal static class StoreDistribution
    {
        const string PolicyUrl = "https://raw.githubusercontent.com/" + GitHubReleases.Repository + "/main/.github/update-channel.txt";
        const string StoreSearchUrl = "https://storeedgefd.dsx.mp.microsoft.com/v9.0/manifestSearch";
        const string ListingTitle = "Camera Control Studio";
        const string ListingPublisher = "Brian Kristensen";
        const string StoreFirst = "store-first";
        static readonly Regex ProductId = new Regex(@"\A[A-Z0-9]{12}\z");
        static readonly HttpClient Client = new HttpClient(new HttpClientHandler
        {
            AllowAutoRedirect = false,
            UseCookies = false
        })
        {
            Timeout = TimeSpan.FromSeconds(4),
            MaxResponseContentBufferSize = 64 * 1024
        };

        internal static async Task<Uri> FindPreferredStorePageAsync()
        {
            try
            {
                using (var policy = await Client.GetAsync(PolicyUrl).ConfigureAwait(false))
                {
                    if (!policy.IsSuccessStatusCode ||
                        !string.Equals((await policy.Content.ReadAsStringAsync().ConfigureAwait(false)).Trim(), StoreFirst,
                            StringComparison.OrdinalIgnoreCase))
                        return null;
                }

                string url = StoreSearchUrl + "?Market=" + Uri.EscapeDataString(RegionInfo.CurrentRegion.TwoLetterISORegionName);
                const string search = "{\"Filters\":[{\"PackageMatchField\":\"PackageName\","
                    + "\"RequestMatch\":{\"KeyWord\":\"" + ListingTitle + "\",\"MatchType\":\"Exact\"}}],\"MaximumResults\":10}";
                using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    request.Headers.Add("Version", "1.1.0");
                    request.Content = new StringContent(search, Encoding.UTF8, "application/json");
                    using (var response = await Client.SendAsync(request).ConfigureAwait(false))
                    {
                        if (!response.IsSuccessStatusCode)
                            return null;
                        using (var payload = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                            return FindStorePage(payload);
                    }
                }
            }
            catch (Exception error) when (error is HttpRequestException || error is OperationCanceledException
                || error is IOException || error is SerializationException)
            {
                // An unavailable policy or catalog must not block GitHub downloads.
                return null;
            }
        }

        internal static Uri FindStorePage(Stream payload)
        {
            var results = (SearchResult)new DataContractJsonSerializer(typeof(SearchResult)).ReadObject(payload);
            var matches = (results?.Data ?? Array.Empty<Listing>()).Where(item =>
                item != null && string.Equals(item.Title, ListingTitle, StringComparison.OrdinalIgnoreCase)
                && string.Equals(item.Publisher, ListingPublisher, StringComparison.OrdinalIgnoreCase)
                && ProductId.IsMatch(item.Id ?? "")
                && item.Versions?.Any(version => version?.PackageFamilies?.Any(family => !string.IsNullOrEmpty(family)) == true) == true)
                .Select(item => item.Id).Distinct().Take(2).ToArray();

            return matches.Length == 1 ? new Uri("ms-windows-store://pdp/?ProductId=" + matches[0]) : null;
        }

        [DataContract]
        sealed class SearchResult
        {
            [DataMember] public Listing[] Data { get; set; }
        }

        [DataContract]
        sealed class Listing
        {
            [DataMember(Name = "PackageIdentifier")] public string Id { get; set; }
            [DataMember(Name = "PackageName")] public string Title { get; set; }
            [DataMember] public string Publisher { get; set; }
            [DataMember] public ListingVersion[] Versions { get; set; }
        }

        [DataContract]
        sealed class ListingVersion
        {
            [DataMember(Name = "PackageFamilyNames")] public string[] PackageFamilies { get; set; }
        }
    }
}
