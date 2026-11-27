using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public partial class GuideViewer
    {
        const string DailyGuide = "DAILY-USE.md";
        const string SetupGuide = "SETUP.md";
        const string CombinedGuide = "USER-GUIDE.md";
        const string IssuesGuide = "ISSUES.md";
        const string BugGuide = "Report a bug.md";
        const string FeatureGuide = "Request a feature.md";
        const string RawHost = "raw.githubusercontent.com";
        static readonly HttpClient GuideClient = new HttpClient(new HttpClientHandler
        {
            AllowAutoRedirect = false,
            UseCookies = false
        })
        {
            Timeout = TimeSpan.FromSeconds(8),
            MaxResponseContentBufferSize = 2 * 1024 * 1024
        };

        readonly Dictionary<Uri, string> onlineDocuments = new Dictionary<Uri, string>();
        Task<GitHubReleases.AvailableRelease> releaseCheck;
        GitHubReleases.AvailableRelease latestRelease;
        CancellationTokenSource guideRequest;
        string selectedGuide;
        string selectedAnchor;
        bool selectedSetupSection;
        bool selectingLinkedLanguage;
        bool printing;

        void InitializeGuideSelection()
        {
            guideLanguage.Items.AddRange(UiText.LanguageNames.Skip(1).Cast<object>().ToArray());
            guideLanguage.SelectedIndex = Array.IndexOf(UiText.LanguageCodes, UiText.Language) - 1;
            guideLanguage.SelectedIndexChanged += async (sender, args) =>
            {
                if (selectingLinkedLanguage)
                    return;
                selectedAnchor = null;
                await ShowSelectedGuide();
            };
            useOnlineGuides.CheckedChanged += async (sender, args) =>
            {
                if (AppUpdates.Channel == UpdateChannel.GitHub && useOnlineGuides.Checked
                    && releaseCheck?.IsCompleted == true && latestRelease == null)
                    releaseCheck = GitHubReleases.CheckLatestAsync();
                await ShowSelectedGuide();
            };
            Disposed += (sender, args) => guideRequest?.Cancel();
        }

        void BindGuideLink(LinkLabel link, string file) =>
            BindLink(link, () => SelectGuide(file, setupSection: file == SetupGuide));

        async void SelectGuide(string file, bool setupSection = false, string anchor = null)
        {
            selectedGuide = file;
            selectedSetupSection = setupSection;
            selectedAnchor = anchor;
            await ShowSelectedGuide();
        }

        async Task StartGuides()
        {
            releaseCheck = AppUpdates.Channel == UpdateChannel.GitHub
                ? GitHubReleases.CheckLatestAsync() : Task.FromResult<GitHubReleases.AvailableRelease>(null);
            SelectGuide(openSetup ? SetupGuide : DailyGuide, setupSection: openSetup);

            latestRelease = await releaseCheck;
            if (!IsDisposed && !printing && !useOnlineGuides.Checked && HasNewerRelease)
                await ShowSelectedGuide();
        }

        string OnlineGuideTag => AppUpdates.Channel == UpdateChannel.GitHub
            ? latestRelease?.Tag : InstalledVersionTag;

        bool HasNewerRelease => latestRelease?.IsNewerThan(DiagnosticLog.AppVersion) == true;

        static IEnumerable<string> GuidePaths(string guide, string language)
        {
            if (guide.Contains("/"))
            {
                yield return guide;
                yield break;
            }

            foreach (string code in new[] { language, "en" }.Distinct())
            {
                yield return code + "/" + guide;
                if (guide == DailyGuide || guide == SetupGuide)
                    yield return code + "/" + CombinedGuide;
                if (guide == CombinedGuide)
                    yield return code + "/" + DailyGuide;
            }
            yield return guide;
        }

        void OpenLinkedGuide(string path, string anchor)
        {
            if (path.Split('/').Any(part => part.Length == 0 || part == "." || part == ".."
                || part.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0))
                return;
            int slash = path.IndexOf('/');
            int language = slash < 0 ? -1 : Array.IndexOf(UiText.LanguageCodes, path.Substring(0, slash));
            if (language > 0)
            {
                selectingLinkedLanguage = true;
                guideLanguage.SelectedIndex = language - 1;
                selectingLinkedLanguage = false;
                path = path.Substring(slash + 1);
            }
            SelectGuide(path, anchor: anchor);
        }

        async Task ShowSelectedGuide()
        {
            if (selectedGuide == null || guideLanguage.SelectedIndex < 0 || printing || IsDisposed)
                return;

            guideRequest?.Cancel();
            print.Enabled = false;
            using (var request = new CancellationTokenSource())
            {
                guideRequest = request;
                try
                {
                    string language = UiText.LanguageCodes[guideLanguage.SelectedIndex + 1];
                    var paths = GuidePaths(selectedGuide, language).ToArray();
                    bool wantsOnline = useOnlineGuides.Checked;
                    string guide = null;
                    string markdown = null;
                    Uri onlineRoot = null;
                    if (wantsOnline)
                    {
                        latestRelease = await releaseCheck;
                        request.Token.ThrowIfCancellationRequested();
                        string guideTag = OnlineGuideTag;
                        if (guideTag != null)
                        {
                            var candidateRoot = new Uri("https://" + RawHost + "/" + GitHubReleases.Repository
                                + "/refs/tags/" + Uri.EscapeDataString(guideTag) + GuideDirectory);
                            try
                            {
                                foreach (string path in paths)
                                {
                                    var onlinePath = new Uri(candidateRoot, path);
                                    if (!candidateRoot.IsBaseOf(onlinePath))
                                        continue;
                                    markdown = await ReadOnlineGuide(onlinePath, request.Token);
                                    if (markdown == null)
                                        continue;
                                    guide = path;
                                    onlineRoot = candidateRoot;
                                    break;
                                }
                            }
                            catch (Exception ex) when (ex is HttpRequestException || ex is OperationCanceledException || ex is IOException)
                            {
                                request.Token.ThrowIfCancellationRequested();
                            }
                        }
                    }

                    request.Token.ThrowIfCancellationRequested();
                    if (IsDisposed)
                        return;
                    if (markdown == null)
                    {
                        guide = paths.FirstOrDefault(path => File.Exists(Path.Combine(root, path)));
                        if (guide != null)
                            markdown = File.ReadAllText(Path.Combine(root, guide));
                    }
                    bool printDocument = markdown != null;
                    links.Clear();
                    RenderGuide(guide ?? selectedGuide, markdown ?? "", onlineRoot,
                        GuideBanner(wantsOnline, onlineRoot != null, printDocument,
                            language != "en" && guide?.StartsWith("en/", StringComparison.Ordinal) == true),
                        selectedSetupSection && Path.GetFileName(guide) == CombinedGuide, selectedAnchor,
                        printDocument);
                }
                catch (OperationCanceledException) when (request.IsCancellationRequested) { }
                catch (Exception ex) { ShowGuideError(ex); }
                finally
                {
                    if (guideRequest == request)
                        guideRequest = null;
                }
            }
        }

        async Task<string> ReadOnlineGuide(Uri url, CancellationToken cancellation)
        {
            if (onlineDocuments.TryGetValue(url, out var markdown))
                return markdown;

            using (var response = await GuideClient.GetAsync(url, cancellation))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;
                response.EnsureSuccessStatusCode();
                markdown = await response.Content.ReadAsStringAsync();
                cancellation.ThrowIfCancellationRequested();
                if (string.IsNullOrWhiteSpace(markdown))
                    return null;
                onlineDocuments[url] = markdown;
                return markdown;
            }
        }

        string GuideBanner(bool wantsOnline, bool online, bool available, bool englishFallback)
        {
            if (!wantsOnline && !HasNewerRelease && available && !englishFallback)
                return "";

            string message = null;
            if (!available)
                message = "This guide is unavailable.";
            else if (online)
                message = AppUpdates.Channel == UpdateChannel.GitHub
                    ? "This online guide is for the latest release." : "This online guide is for the installed version.";
            else if (wantsOnline)
                message = "The online guide is unavailable. Showing the local guide.";
            else if (HasNewerRelease)
                message = "You are viewing a local guide. A newer release is available.";
            string html = "<div class='guide-banner'>";
            if (message != null)
                html += WebUtility.HtmlEncode(UiText.Translate(message, message));
            if (englishFallback)
                html += (message == null ? "" : "<br>") + WebUtility.HtmlEncode(
                    UiText.Translate("This guide is shown in English.", "This guide is shown in English."));
            if (latestRelease != null && (wantsOnline || HasNewerRelease))
            {
                string versions = string.Format(UiText.Translate("Installed: {0} · Online: {1}", "Installed: {0} · Online: {1}"),
                    DiagnosticLog.DisplayAppVersion, latestRelease.Version);
                string releaseLink = RegisterGuideLink(latestRelease.Url);
                html += "<br>" + WebUtility.HtmlEncode(versions) + " · <a href='" + WebUtility.HtmlEncode(releaseLink) + "'>"
                    + WebUtility.HtmlEncode(UiText.Translate("View latest release", "View latest release")) + "</a>";
            }
            return html + "</div>";
        }
    }
}
