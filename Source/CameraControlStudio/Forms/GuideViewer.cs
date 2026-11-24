using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Markdig;
using Markdig.Extensions.AutoIdentifiers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Microsoft.Web.WebView2.Core;

namespace CameraControlStudio
{
    public partial class GuideViewer : AppForm
    {
        internal const string ReleasesUrl = "https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases";
        internal const string IssuesUrl = "https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues?q=is%3Aissue";
        internal const string OnlineGuidesUrl = "https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/blob/main/Source/docs/USER-GUIDES.md";
        static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder().DisableHtml().UsePipeTables()
            .UseAutoIdentifiers(AutoIdentifierOptions.GitHub).Build();
        readonly string root = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs");
        const string AssetHost = "guide-assets.invalid";
        const string DocumentUrl = "https://guide-document.invalid/current.html";
        static bool loaderConfigured;
        readonly Dictionary<string, Uri> links = new Dictionary<string, Uri>();
        readonly bool openSetup;
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
            string languageFolder = Path.Combine(root, UiText.Language);
            string combined = Path.Combine(languageFolder, "USER-GUIDE.md");
            bool hasCombined = File.Exists(combined);
            string daily = hasCombined ? combined : Path.Combine(languageFolder, "DAILY-USE.md");
            string settings = hasCombined ? combined : Path.Combine(languageFolder, "SETUP.md");
            if (!File.Exists(daily))
                daily = Path.Combine(root, "en", "DAILY-USE.md");
            if (!File.Exists(settings))
                settings = Path.Combine(root, "en", "SETUP.md");
            BindLink(dailyLink, () => LoadGuide(daily));
            BindLink(setupLink, () => LoadGuide(settings, hasCombined));
            BindGuideLink(developmentLink, Path.Combine(languageFolder, "DEVELOPMENT.md"));
            BindGuideLink(buildReleaseLink, Path.Combine(languageFolder, "BUILD-AND-RELEASE.md"));
            BindGuideLink(translationsLink, Path.Combine(languageFolder, "TRANSLATIONS.md"), useEnglish: true);
            BindGuideLink(githubLink, Path.Combine(languageFolder, "GITHUB.md"), useEnglish: true);
            string issuesGuide = BindGuideLink(issuesLink, Path.Combine(languageFolder, "ISSUES.md"), useEnglish: true);
            BindLink(releasesLink, () => OpenBrowser(ReleasesUrl));
            BindLink(reportIssueLink, () =>
            {
                if (issuesGuide != null)
                    LoadGuide(issuesGuide);
                OpenBrowser(IssuesUrl);
            });
            BindLink(onlineGuidesLink, () => OpenBrowser(OnlineGuidesUrl));
            print.Enabled = false;
            print.Click += async (sender, args) =>
            {
                try { await PrintGuide(); }
                catch (Exception ex) { ShowGuideError(ex); }
            };
            sidebar.Enabled = false;
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
                                print.Enabled = true;
                        }
                        catch (Exception ex) { ShowGuideError(ex); }
                    };
                    sidebar.Enabled = true;
                    LoadGuide(openSetup ? settings : daily, openSetup && hasCombined);
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

        string BindGuideLink(LinkLabel link, string path, bool useEnglish = false)
        {
            if (useEnglish && !File.Exists(path))
                path = Path.Combine(root, "en", Path.GetFileName(path));
            bool exists = File.Exists(path);
            link.Visible = exists;
            if (!exists)
                return null;
            BindLink(link, () => LoadGuide(path));
            return path;
        }

        void BindLink(LinkLabel link, Action action)
        {
            link.RightToLeft = UiText.IsRightToLeft ? RightToLeft.Yes : RightToLeft.No;
            link.LinkClicked += (sender, args) => TryGuideAction(action);
        }

        void LoadGuide(string path, bool openSettingsSection = false, string anchor = null)
        {
            TryGuideAction(() =>
            {
                print.Enabled = false;
                scrollToSettings = openSettingsSection;
                documentAnchor = anchor;
                links.Clear();
                var document = Markdown.Parse(File.ReadAllText(path), Pipeline);
                var heading = document.OfType<HeadingBlock>().FirstOrDefault();
                documentTitle = heading?.Inline == null ? Path.GetFileNameWithoutExtension(path)
                    : string.Concat(heading.Inline.Descendants<LiteralInline>().Select(inline => inline.Content.ToString()));
                foreach (var link in document.Descendants<LinkInline>())
                {
                    var url = link.GetDynamicUrl?.Invoke() ?? link.Url;
                    link.GetDynamicUrl = null;
                    link.Url = ResolveLink(path, url, link.IsImage);
                }
                string language = Path.GetFileName(Path.GetDirectoryName(path));
                if (!UiText.LanguageCodes.Contains(language))
                    language = "en";
                string direction = language == "ar" || language == "ur" ? "rtl" : "ltr";
                documentHtml = Encoding.UTF8.GetBytes("<!doctype html><html lang='" + language + "' dir='" + direction + "'><head>"
                    + "<meta charset='utf-8'><meta http-equiv='Content-Security-Policy' content=\"default-src 'none'; img-src https://"
                    + AssetHost + "; style-src 'unsafe-inline'\">"
                    + "<style>body{font:16px/1.55 Segoe UI,sans-serif;color:#18242f;background:#fff;margin:24px}"
                    + "img{max-width:100%;height:auto} table{border-collapse:collapse;width:100%}"
                    + "th,td{padding:5px 9px;border:1px solid #b9c5d0}th{background:#e9eff4}"
                    + "pre{white-space:pre-wrap}a{color:#075692}@media print{body{margin:0}img{max-height:6in;break-inside:avoid}"
                    + "h1,h2,h3{break-after:avoid}tr{break-inside:avoid}}"
                    + "@page{margin:0.65in;@top-center{content:" + CssString(documentTitle)
                    + ";font:10pt Segoe UI,sans-serif;}@bottom-right{content:"
                    + PageCounter() + ";font:9pt Segoe UI,sans-serif;}}</style>"
                    + "<style id='print-footer'></style></head><body>" + document.ToHtml(Pipeline) + "</body></html>");
                page.CoreWebView2.Navigate(DocumentUrl);
            });
        }

        static string CssString(string value) => "\"" + string.Concat(value.Select(character =>
            "\\" + ((int)character).ToString("X", CultureInfo.InvariantCulture) + " ")) + "\"";

        static string PageCounter()
        {
            var parts = System.Text.RegularExpressions.Regex.Split(UiText.T("Page {0} of {1}", "Side {0} af {1}"), "(\\{[01]\\})");
            return string.Join(" ", parts.Select(part => part == "{0}" ? "counter(page)" : part == "{1}" ? "counter(pages)" : CssString(part)));
        }

        async Task ApplyPrintFooter()
        {
            string version = DiagnosticLog.DisplayAppVersion;
            string date = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string footer = "@page{@bottom-left{content:" + CssString("Camera Control Studio " + version + " · " + date)
                + ";font:9pt Segoe UI,sans-serif;}}";
            await page.CoreWebView2.ExecuteScriptAsync("document.getElementById('print-footer').textContent='"
                + footer.Replace("\\", "\\\\") + "';");
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
            if (printDialog.ShowDialog(this) != DialogResult.OK)
                return;
            print.Enabled = false;
            page.Enabled = false;
            sidebar.Enabled = false;
            try
            {
                await ApplyPrintFooter();
                var printer = printDialog.PrinterSettings;
                var settings = web.Environment.CreatePrintSettings();
                settings.PrinterName = printer.PrinterName;
                settings.Copies = printer.Copies;
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
                if (await web.PrintAsync(settings) != CoreWebView2PrintStatus.Succeeded)
                    throw new IOException("The browser did not complete the print job.");
            }
            finally
            {
                if (!IsDisposed)
                {
                    sidebar.Enabled = true;
                    page.Enabled = true;
                    print.Enabled = true;
                }
            }
        }

        string ResolveLink(string source, string url, bool image)
        {
            // Resolve from an explicit file URI so Windows paths preserve a link's fragment.
            var sourceUri = new Uri(new Uri(source).AbsoluteUri);
            if (!Uri.TryCreate(sourceUri, url, out Uri destination))
                return null;
            if (!image && url.StartsWith("#", StringComparison.Ordinal))
                return url;
            if (destination.IsFile)
            {
                string target = Path.GetFullPath(destination.LocalPath);
                if (!target.StartsWith(root + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) || !File.Exists(target))
                    return null;
                string extension = Path.GetExtension(target).ToLowerInvariant();
                if (image && (extension == ".png" || extension == ".jpg" || extension == ".jpeg"))
                    return "https://" + AssetHost + "/" + new Uri(root + Path.DirectorySeparatorChar).MakeRelativeUri(destination);
                if (image || extension != ".md")
                    return null;
            }
            else if (image || (destination.Scheme != Uri.UriSchemeHttp && destination.Scheme != Uri.UriSchemeHttps))
                return null;
            string address = "https://guide-link.invalid/" + links.Count;
            links.Add(address, destination);
            return address;
        }

        void NavigateLink(object sender, CoreWebView2NavigationStartingEventArgs args)
        {
            var url = new Uri(args.Uri);
            if (url.Scheme == "about" || url.GetLeftPart(UriPartial.Path) == DocumentUrl)
                return;
            args.Cancel = true;
            if (links.TryGetValue(url.AbsoluteUri, out var destination))
            {
                BeginInvoke(new Action(() =>
                {
                    if (destination.IsFile)
                        LoadGuide(destination.LocalPath, anchor: destination.Fragment);
                    else
                        TryGuideAction(() => OpenBrowser(destination.AbsoluteUri));
                }));
            }
            else if (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps)
                TryGuideAction(() => OpenBrowser(url.AbsoluteUri));
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
