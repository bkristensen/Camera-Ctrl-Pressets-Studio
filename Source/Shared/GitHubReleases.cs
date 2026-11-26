using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CameraControlStudio
{
    internal static class GitHubReleases
    {
        internal const string Repository = "bkristensen/Camera-Ctrl-Pressets-Studio";
        const string ReleasePath = "/" + Repository + "/releases/tag/";
        const string Endpoint = "https://api.github.com/repos/" + Repository + "/releases?per_page=100";
        const string VersionPattern = @"(?<version>[0-9]+\.[0-9]+\.[0-9]+)(?:-(?<prerelease>[0-9A-Za-z-]+(?:\.[0-9A-Za-z-]+)*))?(?:\+[0-9A-Za-z-]+(?:\.[0-9A-Za-z-]+)*)?";
        static readonly Regex VersionTag = new Regex("^v?" + VersionPattern + "$", RegexOptions.IgnoreCase);
        static readonly Regex VersionInTitle = new Regex(@"(?<![0-9A-Za-z.])v?" + VersionPattern + @"(?![0-9A-Za-z.+-])", RegexOptions.IgnoreCase);
        static readonly HttpClient Client = CreateClient();

        internal sealed class AvailableRelease
        {
            internal AvailableRelease(string version, string tag, Uri url, DateTimeOffset? publishedAt)
            {
                Version = version;
                Tag = tag;
                Url = url;
                PublishedAt = publishedAt;
            }

            internal string Version { get; }
            internal string Tag { get; }
            internal Uri Url { get; }
            internal DateTimeOffset? PublishedAt { get; }

            internal bool IsNewerThan(string installedVersion)
            {
                return TryReadVersion(VersionTag.Match(installedVersion ?? ""), out var installed, out var installedPrerelease)
                    && TryReadVersion(VersionTag.Match(Version), out var available, out var availablePrerelease)
                    && CompareVersions(available, availablePrerelease, installed, installedPrerelease) > 0;
            }
        }

        [DataContract]
        sealed class Release
        {
            [DataMember(Name = "tag_name")]
            public string Tag { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "html_url")]
            public string Url { get; set; }

            [DataMember(Name = "draft")]
            public bool Draft { get; set; }

            [DataMember(Name = "published_at")]
            public string PublishedAt { get; set; }
        }

        static HttpClient CreateClient()
        {
            var client = new HttpClient(new HttpClientHandler { UseCookies = false })
            {
                Timeout = TimeSpan.FromSeconds(5)
            };
            client.DefaultRequestHeaders.UserAgent.ParseAdd("CameraControlStudio");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github+json");
            return client;
        }

        internal static Task<AvailableRelease> CheckLatestAsync() => FetchAsync(FindLatest);

        internal static Task<AvailableRelease> CheckAsync(string installedVersion) =>
            FetchAsync(payload => FindNewer(installedVersion, payload));

        static async Task<AvailableRelease> FetchAsync(Func<Stream, AvailableRelease> readRelease)
        {
            try
            {
                using (var response = await Client.GetAsync(Endpoint).ConfigureAwait(false))
                {
                    if (!response.IsSuccessStatusCode)
                        return null;
                    using (var payload = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        return readRelease(payload);
                }
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is OperationCanceledException
                || ex is IOException || ex is SerializationException)
            {
                // Connection or API failures leave the footer showing the installed version.
                return null;
            }
        }

        internal static AvailableRelease FindNewer(string installedVersion, Stream payload)
        {
            var release = FindLatest(payload);
            return release?.IsNewerThan(installedVersion) == true ? release : null;
        }

        internal static AvailableRelease FindLatest(Stream payload)
        {
            Version latest = null;
            string latestPrerelease = "";
            var releases = (Release[])new DataContractJsonSerializer(typeof(Release[])).ReadObject(payload);
            AvailableRelease latestRelease = null;
            foreach (var release in releases ?? Array.Empty<Release>())
            {
                if (release == null || release.Draft || !Uri.TryCreate(release.Url, UriKind.Absolute, out var url)
                    || url.Scheme != Uri.UriSchemeHttps || url.Host != "github.com" || !url.IsDefaultPort
                    || url.UserInfo.Length != 0 || !url.AbsolutePath.StartsWith(ReleasePath, StringComparison.OrdinalIgnoreCase)
                    || url.AbsolutePath.Length == ReleasePath.Length
                    || string.IsNullOrEmpty(release.Tag) || release.Tag == "." || release.Tag == "..")
                    continue;

                var versionMatch = VersionTag.Match(release.Tag ?? "");
                // The first published beta uses the tag "Beta" and puts its version in the title.
                if (!versionMatch.Success)
                    versionMatch = VersionInTitle.Match(release.Name ?? "");

                if (!TryReadVersion(versionMatch, out var version, out var prerelease))
                    continue;

                if (latest != null && CompareVersions(version, prerelease, latest, latestPrerelease) <= 0)
                    continue;

                latest = version;
                latestPrerelease = prerelease;
                string displayVersion = prerelease.Length == 0 ? version.ToString() : version + "-" + prerelease;
                DateTimeOffset? publishedAt = DateTimeOffset.TryParse(release.PublishedAt, CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal, out var date) ? date : (DateTimeOffset?)null;
                latestRelease = new AvailableRelease(displayVersion, release.Tag, url, publishedAt);
            }
            return latestRelease;
        }

        static bool TryReadVersion(Match match, out Version version, out string prerelease)
        {
            prerelease = match.Groups["prerelease"].Value;
            return Version.TryParse(match.Groups["version"].Value, out version);
        }

        static int CompareVersions(Version left, string leftPrerelease, Version right, string rightPrerelease)
        {
            int order = left.CompareTo(right);
            return order == 0 ? ComparePrerelease(leftPrerelease, rightPrerelease) : order;
        }

        static int ComparePrerelease(string left, string right)
        {
            // A final release has higher precedence than every prerelease of the same version.
            if (left.Length == 0 || right.Length == 0)
                return right.Length.CompareTo(left.Length);

            string[] a = left.Split('.');
            string[] b = right.Split('.');
            for (int index = 0; index < Math.Min(a.Length, b.Length); index++)
            {
                string x = a[index];
                string y = b[index];
                bool xNumeric = x.All(char.IsDigit);
                bool yNumeric = y.All(char.IsDigit);
                if (xNumeric != yNumeric)
                    return xNumeric ? -1 : 1;
                if (xNumeric)
                {
                    int lengthOrder = x.Length.CompareTo(y.Length);
                    if (lengthOrder != 0)
                        return lengthOrder;
                }

                int order = string.CompareOrdinal(x, y);
                if (order != 0)
                    return order;
            }
            return a.Length.CompareTo(b.Length);
        }
    }
}
