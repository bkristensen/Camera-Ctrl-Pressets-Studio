using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Markdig;
using Markdig.Extensions.AutoIdentifiers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Microsoft.Web.WebView2.Core;

namespace CameraControlStudio
{
    public partial class GuideViewer : AppForm
    {
        const string TroubleshootingFile = "TROUBLESHOOTING.md";
        internal const string ReleasesUrl = "https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases";
        internal const string IssuesUrl = "https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues?q=is%3Aissue";
        const string RepositoryFilesUrl = "https://github.com/" + GitHubReleases.Repository + "/blob/";
        const string GuideDirectory = "/Source/docs/";
        const string MainGuidesUrl = RepositoryFilesUrl + "main" + GuideDirectory;
        internal const string OnlineGuidesUrl = MainGuidesUrl + "USER-GUIDES.md";
        static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder().DisableHtml().UsePipeTables()
            .UseAutoIdentifiers(AutoIdentifierOptions.GitHub).Build();
        readonly string root = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs");
        const string AssetHost = "guide-assets.invalid";
        const string DocumentUrl = "https://guide-document.invalid/current.html";
        static bool loaderConfigured;
        readonly Dictionary<string, Uri> links = new Dictionary<string, Uri>();
        readonly bool openSetup;
        static string PrinterPreference => Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData), "CamPressets", "guide-printer.txt");
        Uri renderedOnlineRoot;
        bool printerLoaded;
        bool currentGuidePrintable;
        string documentTitle;
        byte[] documentHtml;
        bool scrollToSettings;
        string documentAnchor;

        public GuideViewer()
        {
            InitializeComponent();
            if (InDesigner)
                return;
            FormText.Translate(this);
            print.AccessibleName = UiText.T("Print", "Udskriv");
            print.Text = "\U0001F5A8  " + print.AccessibleName;
            InitializeGuideSelection();
            BindGuideLink(dailyLink, DailyGuide);
            BindGuideLink(setupLink, SetupGuide);
            BindGuideLink(troubleshootingLink, TroubleshootingFile);
            BindGuideLink(developmentLink, "DEVELOPMENT.md");
            BindGuideLink(buildReleaseLink, "BUILD-AND-RELEASE.md");
            BindGuideLink(translationsLink, "TRANSLATIONS.md");
            BindGuideLink(githubLink, "GITHUB.md");
            BindGuideLink(issuesLink, IssuesGuide);
            BindLink(releasesLink, () => OpenBrowser(ReleasesUrl));
            BindLink(reportIssueLink, () =>
            {
                SelectGuide(IssuesGuide);
                OpenBrowser(IssuesUrl);
            });
            BindLink(onlineGuidesLink, () => OpenBrowser(OnlineGuidesUrl));
            print.Enabled = false;
            print.Click += async (sender, args) =>
            {
                try { await PrintGuide(); }
                catch (Exception ex) { ShowGuideError(ex); }
            };
            Shown += async (sender, args) =>
            {
                try
                {
                    string data = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "CamPressets", "GuideViewer");
                    if (!loaderConfigured)
                    {
                        var architecture = RuntimeInformation.ProcessArchitecture.ToString().ToLowerInvariant();
                        var loader = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "runtimes", "win-" + architecture, "native");
                        CoreWebView2Environment.SetLoaderDllFolderPath(loader);
                        loaderConfigured = true;
                    }
                    var browser = await CoreWebView2Environment.CreateAsync(userDataFolder: data);
                    if (IsDisposed)
                        return;
                    await page.EnsureCoreWebView2Async(browser);
                    if (IsDisposed)
                        return;
                    var web = page.CoreWebView2;
                    web.Settings.IsScriptEnabled = false;
                    web.Settings.AreDefaultContextMenusEnabled = false;
                    web.Settings.AreDevToolsEnabled = false;
                    web.Settings.AreBrowserAcceleratorKeysEnabled = false;
                    web.SetVirtualHostNameToFolderMapping(AssetHost, root, CoreWebView2HostResourceAccessKind.DenyCors);
                    web.AddWebResourceRequestedFilter(DocumentUrl, CoreWebView2WebResourceContext.Document, CoreWebView2WebResourceRequestSourceKinds.Document);
                    web.WebResourceRequested += (o, e) =>
                    {
                        if (e.Request.Uri == DocumentUrl && documentHtml != null)
                            e.Response = web.Environment.CreateWebResourceResponse(new MemoryStream(documentHtml, false), 200, "OK",
                                "Content-Type: text/html; charset=utf-8\r\nCache-Control: no-store");
                    };
                    web.NavigationStarting += NavigateLink;
                    web.NewWindowRequested += (o, e) => e.Handled = true;
                    web.NavigationCompleted += async (o, e) =>
                    {
                        if (!e.IsSuccess || documentTitle == null)
                            return;
                        try
                        {
                            if (!string.IsNullOrEmpty(documentAnchor))
                            {
                                string anchor = Uri.EscapeDataString(Uri.UnescapeDataString(documentAnchor.TrimStart('#')))
                                    .Replace("'", "%27");
                                await web.ExecuteScriptAsync("document.getElementById(decodeURIComponent('" + anchor
                                    + "'))?.scrollIntoView();");
                            }
                            else if (scrollToSettings)
                                await web.ExecuteScriptAsync("document.querySelector('ol > li:nth-child(5)')?.scrollIntoView();");
                            if (!IsDisposed)
                                print.Enabled = currentGuidePrintable && !printing;
                        }
                        catch (Exception ex) { ShowGuideError(ex); }
                    };
                    await StartGuides();
                }
                catch (WebView2RuntimeNotFoundException)
                {
                    ShowMessage(UiText.T("Install Microsoft Edge WebView2 Runtime to view the guides.",
                        "Installér Microsoft Edge WebView2 Runtime for at vise vejledningerne."), Text);
                    Close();
                }
                catch (Exception ex) { ShowGuideError(ex); }
            };
        }

        internal GuideViewer(bool setup) : this()
        {
            openSetup = setup;
        }

        void BindLink(LinkLabel link, Action action)
        {
            link.RightToLeft = UiText.IsRightToLeft ? RightToLeft.Yes : RightToLeft.No;
            link.LinkClicked += (sender, args) =>
            {
                if (releaseCheck != null && !printing)
                    TryGuideAction(action);
            };
        }

        void RenderGuide(string path, string markdown, Uri onlineRoot, string banner, bool openSettingsSection,
            string anchor, bool available)
        {
            print.Enabled = false;
            currentGuidePrintable = available;
            scrollToSettings = openSettingsSection;
            documentAnchor = anchor;
            renderedOnlineRoot = onlineRoot;
            // An explicit file URI preserves fragments on relative Markdown links.
            var source = onlineRoot == null ? new Uri(new Uri(Path.Combine(root, path)).AbsoluteUri) : new Uri(onlineRoot, path);
            var document = ParseGuide(path, markdown);
            var heading = document.OfType<HeadingBlock>().FirstOrDefault();
            documentTitle = heading?.Inline == null ? Path.GetFileNameWithoutExtension(path)
                : string.Concat(heading.Inline.Descendants<LiteralInline>().Select(inline => inline.Content.ToString()));
            foreach (var link in document.Descendants<LinkInline>())
            {
                var url = link.GetDynamicUrl?.Invoke() ?? link.Url;
                link.GetDynamicUrl = null;
                link.Url = ResolveLink(source, url, link.IsImage, onlineRoot);
            }
            string language = path.Split('/').First();
            if (!UiText.LanguageCodes.Contains(language))
                language = "en";
            string direction = language == "ar" || language == "ur" ? "rtl" : "ltr";
            string images = onlineRoot == null ? "https://" + AssetHost : "https://" + RawHost;
            documentHtml = Encoding.UTF8.GetBytes("<!doctype html><html lang='" + language + "' dir='" + direction + "'><head>"
                + "<meta charset='utf-8'><meta name='referrer' content='no-referrer'>"
                + "<meta http-equiv='Content-Security-Policy' content=\"default-src 'none'; img-src " + images
                + "; style-src 'unsafe-inline'\">"
                + "<style>body{font:16px/1.55 Segoe UI,sans-serif;color:#18242f;background:#fff;margin:24px}"
                + ".guide-banner{background:#e9eff4;border:1px solid #b9c5d0;padding:9px 12px;margin-bottom:18px;break-inside:avoid}"
                + "img{max-width:100%;height:auto} table{border-collapse:collapse;width:100%}"
                + "th,td{padding:5px 9px;border:1px solid #b9c5d0}th{background:#e9eff4}"
                + "pre{white-space:pre-wrap}a{color:#075692;overflow-wrap:anywhere}@media print{body{margin:0}img{max-height:6in;break-inside:avoid}p:has(img[src*='/images/qr/']){break-inside:avoid}"
                + "h1,h2,h3{break-after:avoid}tr{break-inside:avoid}}"
                + "</style><style id='print-margins'></style></head><body>" + banner
                + document.ToHtml(Pipeline) + "</body></html>");
            page.CoreWebView2.Navigate(DocumentUrl);
        }

        static MarkdownDocument ParseGuide(string path, string markdown)
        {
            var document = Markdown.Parse(markdown, Pipeline);
            if (Path.GetFileName(path) != TroubleshootingFile)
                return document;

            // Match GitHub's heading anchors, including vowel marks in Indic scripts.
            foreach (var section in document.OfType<HeadingBlock>().Where(block => block.Level == 2 || block.Level == 3))
            {
                var label = string.Concat(section.Inline.Descendants<LiteralInline>().Select(inline => inline.Content.ToString()));
                section.GetAttributes().Id = System.Text.RegularExpressions.Regex.Replace(label.ToLowerInvariant(),
                    @"[^\p{L}\p{M}\p{N} _-]", "").Replace(' ', '-');
            }
            return document;
        }

        static string CssString(string value) => "\"" + string.Concat(value.Select(character =>
            "\\" + ((int)character).ToString("X", CultureInfo.InvariantCulture) + " ")) + "\"";

        const string ProjectUrl = "https://github.com/" + GitHubReleases.Repository;
        static string InstalledReleaseUrl => ReleasesUrl + "/tag/v" + Uri.EscapeDataString(DiagnosticLog.DisplayAppVersion);
        const double PrintVerticalMargin = 50.4; // 0.7 inches, in PDF points
        const double PrintRightMargin = 39.6;    // 0.55 inches
        const double PrintLeftMargin = 64.8;     // 0.9 inches for a binder
        const int PrintTitleWidth = 32;
        const int PrintVersionWidth = 40;
        const int PrintProjectWidth = 78;

        static string PrintMargins(string title, string language, DateTime printedAt)
        {
            var translations = UiText.ReadTranslations(language);
            string Translate(string key) => translations.TryGetValue(key, out var value) ? value : key;
            var culture = CultureInfo.CreateSpecificCulture(language);
            string direction = culture.TextInfo.IsRightToLeft ? "rtl" : "ltr";
            string printed = string.Format(culture, Translate("Printed: {0}"), printedAt.ToString("g", culture));
            string project = string.Format(culture, Translate("Project: {0}"), ProjectUrl);
            var parts = System.Text.RegularExpressions.Regex.Split(Translate("Page {0} of {1}"), "(\\{[01]\\})");
            string pageNumber = string.Join(" ", parts.Select(part =>
                part == "{0}" ? "counter(page)" : part == "{1}" ? "counter(pages)" : CssString(part)));

            string appVersion = CssString("Camera Control Studio - v.: " + DiagnosticLog.DisplayAppVersion);
            const string top = "vertical-align:bottom;padding-bottom:4pt;";
            const string bottom = "vertical-align:top;padding-top:4pt;";
            const string normalFont = "font:9pt Segoe UI,sans-serif;";
            string text = "direction:" + direction + ";" + normalFont;
            return FormattableString.Invariant($"@page{{margin:{PrintVerticalMargin}pt {PrintRightMargin}pt {PrintVerticalMargin}pt {PrintLeftMargin}pt;")
                + "padding:8pt 0;border-top:1pt solid black;border-bottom:1pt solid black;"
                + "@top-left{" + top + $"text-align:left;width:{PrintTitleWidth}%;font:bold 13pt Segoe UI,sans-serif;"
                + "direction:" + direction + ";content:" + CssString(title) + ";}"
                + "@top-center{" + top + text + "text-align:center;width:28%;content:" + CssString(printed) + ";}"
                + "@top-right{" + top + normalFont + $"direction:ltr;text-align:right;width:{PrintVersionWidth}%;content:"
                + appVersion + ";}"
                + "@bottom-left{" + bottom + text + $"text-align:left;width:{PrintProjectWidth}%;content:" + CssString(project) + ";}"
                + "@bottom-right{" + bottom + text + "text-align:right;width:22%;content:" + pageNumber + ";}}";
        }

        Task ApplyPrintMargins()
        {
            string language = UiText.LanguageCodes[guideLanguage.SelectedIndex + 1];
            string css = PrintMargins(documentTitle, language, DateTime.Now);
            return page.CoreWebView2.ExecuteScriptAsync("document.getElementById('print-margins').textContent='"
                + css.Replace("\\", "\\\\") + "';");
        }

        static string PdfFileName(string title)
        {
            var invalidCharacters = Path.GetInvalidFileNameChars();
            string safeTitle = new string(title.Select(c => Array.IndexOf(invalidCharacters, c) >= 0 ? '-' : c).ToArray())
                .Trim(' ', '.');
            var elements = new StringInfo(safeTitle);
            if (elements.LengthInTextElements > 120)
                safeTitle = elements.SubstringByTextElements(0, 120).TrimEnd(' ', '.');
            string appName = "Camera Control Studio " + DiagnosticLog.DisplayAppVersion;
            return (safeTitle.Length == 0 ? appName : safeTitle + " - " + appName) + ".pdf";
        }

        string ChoosePdfPath()
        {
            savePdfDialog.FileName = PdfFileName(documentTitle);
            return savePdfDialog.ShowDialog(this) == DialogResult.OK ? savePdfDialog.FileName : null;
        }

        PrinterSettings LoadGuidePrinter(string preference)
        {
            try
            {
                if (File.Exists(preference))
                {
                    var preferred = new PrinterSettings { PrinterName = File.ReadAllText(preference).Trim() };
                    if (preferred.IsValid)
                        return preferred;
                }
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException) { }
            return new PrinterSettings();
        }

        void RememberGuidePrinter(string preference, string name)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(preference));
                File.WriteAllText(preference, name);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException) { }
        }

        async Task PrintGuide()
        {
            var web = page.CoreWebView2;
            if (CoreWebView2Environment.CompareBrowserVersions(web.Environment.BrowserVersionString, "131.0.0.0") < 0)
            {
                ShowMessage(UiText.T("Update Microsoft Edge WebView2 Runtime to print the guides.",
                    "Opdatér Microsoft Edge WebView2 Runtime for at udskrive vejledningerne."), Text);
                return;
            }
            printing = true;
            try
            {
                if (!printerLoaded || !printDialog.PrinterSettings.IsValid)
                {
                    printDialog.PrinterSettings = LoadGuidePrinter(PrinterPreference);
                    printerLoaded = true;
                }
                if (printDialog.ShowDialog(this) != DialogResult.OK)
                    return;
                var printer = printDialog.PrinterSettings;
                RememberGuidePrinter(PrinterPreference, printer.PrinterName);
                string pdfPath = null;
                if (string.Equals(printer.PrinterName, "Microsoft Print to PDF", StringComparison.OrdinalIgnoreCase))
                {
                    pdfPath = ChoosePdfPath();
                    if (pdfPath == null)
                        return;
                }
                guideRequest?.Cancel();
                print.Enabled = false;
                page.Enabled = false;
                sidebar.Enabled = false;
                await ApplyPrintMargins();
                var settings = web.Environment.CreatePrintSettings();
                if (pdfPath == null)
                {
                    settings.PrinterName = printer.PrinterName;
                    settings.Copies = printer.Copies;
                }
                settings.ShouldPrintHeaderAndFooter = false;
                var paper = printer.DefaultPageSettings.PaperSize;
                if (paper.Width > 0 && paper.Height > 0)
                {
                    settings.MediaSize = CoreWebView2PrintMediaSize.Custom;
                    settings.PageWidth = paper.Width / 100.0;
                    settings.PageHeight = paper.Height / 100.0;
                }
                if (printer.DefaultPageSettings.Landscape)
                    settings.Orientation = CoreWebView2PrintOrientation.Landscape;
                if (pdfPath != null)
                    await SaveGuidePdf(web, settings, pdfPath);
                else if (await web.PrintAsync(settings) != CoreWebView2PrintStatus.Succeeded)
                    throw new IOException("The browser did not complete the print job.");
            }
            catch (OperationCanceledException) { }
            catch (COMException ex) when (ex.HResult == unchecked((int)0x800704C7)) // ERROR_CANCELLED
            {
            }
            finally
            {
                printing = false;
                if (!IsDisposed)
                {
                    sidebar.Enabled = true;
                    page.Enabled = true;
                    print.Enabled = currentGuidePrintable;
                }
            }
        }

        string ResolveLink(Uri source, string url, bool image, Uri onlineRoot)
        {
            if (string.IsNullOrEmpty(url) || !Uri.TryCreate(source, url, out Uri destination))
                return null;
            if (!image && url.StartsWith("#", StringComparison.Ordinal))
                return url;

            string extension = Path.GetExtension(destination.AbsolutePath).ToLowerInvariant();
            bool supportedImage = extension == ".png" || extension == ".jpg" || extension == ".jpeg";
            if (destination.IsFile)
            {
                string target = Path.GetFullPath(destination.LocalPath);
                if (onlineRoot != null || !target.StartsWith(root + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)
                    || !File.Exists(target))
                    return null;
                if (image && supportedImage)
                    return "https://" + AssetHost + "/" + new Uri(root + Path.DirectorySeparatorChar).MakeRelativeUri(destination);
                if (image || extension != ".md")
                    return null;
                var relative = new Uri(root + Path.DirectorySeparatorChar).MakeRelativeUri(destination);
                return RegisterGuideLink(destination, new Uri(new Uri(MainGuidesUrl), relative));
            }
            else if (image)
                return supportedImage && onlineRoot?.IsBaseOf(destination) == true ? destination.AbsoluteUri : null;
            else if (destination.Scheme != Uri.UriSchemeHttp && destination.Scheme != Uri.UriSchemeHttps)
                return null;

            if (extension == ".md" && onlineRoot?.IsBaseOf(destination) == true)
            {
                var guideBase = new Uri(RepositoryFilesUrl + Uri.EscapeDataString(latestRelease.Tag) + GuideDirectory);
                return RegisterGuideLink(destination, new Uri(guideBase, onlineRoot.MakeRelativeUri(destination)));
            }
            return RegisterGuideLink(destination);
        }

        string RegisterGuideLink(Uri destination, Uri publicAddress = null)
        {
            // Chromium uses the href when exporting a PDF. Keep the viewer destination separately.
            string address = (publicAddress ?? destination).AbsoluteUri;
            if (publicAddress != null || !links.ContainsKey(address))
                links[address] = destination;
            return address;
        }

        void NavigateLink(object sender, CoreWebView2NavigationStartingEventArgs args)
        {
            var url = new Uri(args.Uri);
            if (url.Scheme == "about" || url.GetLeftPart(UriPartial.Path) == DocumentUrl)
                return;
            args.Cancel = true;
            if (!links.TryGetValue(url.AbsoluteUri, out var destination))
                return;

            var onlineRoot = renderedOnlineRoot;
            BeginInvoke(new Action(() =>
            {
                if (destination.IsFile)
                    OpenLinkedGuide(destination.LocalPath.Substring(root.Length + 1).Replace('\\', '/'), destination.Fragment);
                else if (onlineRoot?.IsBaseOf(destination) == true
                    && destination.AbsolutePath.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
                    OpenLinkedGuide(Uri.UnescapeDataString(onlineRoot.MakeRelativeUri(
                        new Uri(destination.GetLeftPart(UriPartial.Path))).ToString()), destination.Fragment);
                else
                    TryGuideAction(() => OpenBrowser(destination.AbsoluteUri));
            }));
        }

        void TryGuideAction(Action action)
        {
            try { action(); }
            catch (Exception ex) { ShowGuideError(ex); }
        }

        void ShowGuideError(Exception error)
        {
            var log = DiagnosticLog.Current.Write("ERROR", "Open or print user guide", error);
            if (!IsDisposed)
                ErrorDialog.Show(this, log);
        }

        internal static void OpenBrowser(string url)
        {
            using (Process.Start(new ProcessStartInfo(url) { UseShellExecute = true })) { }
        }
    }
}
