using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Core;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace CameraControlStudio
{
    public partial class GuideViewer
    {
        static async Task SaveGuidePdf(CoreWebView2 web, CoreWebView2PrintSettings settings, string path)
        {
            // Keep the existing file intact until the PDF and its annotations are ready.
            string staged = Path.Combine(Path.GetDirectoryName(path), Guid.NewGuid().ToString("N") + ".tmp");
            try
            {
                using (var buffer = new MemoryStream())
                {
                    using (var pdf = await web.PrintToPdfStreamAsync(settings))
                        await pdf.CopyToAsync(buffer);
                    buffer.Position = 0;
                    using (var document = PdfReader.Open(buffer, PdfDocumentOpenMode.Modify))
                    {
                        foreach (var page in document.Pages)
                            LinkPrintedMargins(page);
                        document.Save(staged);
                    }
                }
                if (File.Exists(path))
                    File.Replace(staged, path, null);
                else
                    File.Move(staged, path);
            }
            finally
            {
                if (File.Exists(staged))
                    File.Delete(staged);
            }
        }

        static void LinkPrintedMargins(PdfPage page)
        {
            double left = PrintLeftMargin;
            double right = page.Width.Point - PrintRightMargin;
            double width = right - left;
            double headerLine = page.Height.Point - PrintVerticalMargin;
            double footerLine = PrintVerticalMargin;

            page.AddWebLink(LinkArea(left, headerLine + 1, left + width * PrintTitleWidth / 100, headerLine + 36), OnlineGuidesUrl);
            if (InstalledReleaseUrl is string versionLink)
                page.AddWebLink(LinkArea(right - width * PrintVersionWidth / 100, headerLine + 1, right, headerLine + 22), versionLink);
            page.AddWebLink(LinkArea(left, footerLine - 24, left + width * PrintProjectWidth / 100, footerLine - 1), ProjectUrl);
        }

        static PdfRectangle LinkArea(double left, double bottom, double right, double top) =>
            new PdfRectangle(new XPoint(left, bottom), new XPoint(right, top));
    }
}
