using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Services.Store;

namespace CameraControlStudio
{
    internal enum UpdateChannel
    {
        Unknown,
        GitHub,
        MicrosoftStore
    }

    internal sealed class AppUpdate
    {
        internal AppUpdate(Uri url, string version = null, DateTimeOffset? publishedAt = null)
        {
            Url = url;
            Version = version;
            PublishedAt = publishedAt;
        }

        internal Uri Url { get; }
        internal string Version { get; }
        internal DateTimeOffset? PublishedAt { get; }
    }

    internal static class AppUpdates
    {
        const int InsufficientBuffer = 122;
        const int NoPackage = 15700;
        const int StorePackageOrigin = 3;
        static readonly Uri StoreUpdates = new Uri("ms-windows-store://downloadsandupdates");
        static readonly Lazy<UpdateChannel> InstalledChannel = new Lazy<UpdateChannel>(DetectChannel);

        internal static UpdateChannel Channel => InstalledChannel.Value;
        internal static Uri DownloadUrl => Channel == UpdateChannel.MicrosoftStore ? StoreUpdates
            : Channel == UpdateChannel.GitHub ? new Uri(GitHubActions.ReleasesUrl) : null;

        internal static async Task<AppUpdate> CheckAsync(IntPtr window)
        {
            switch (Channel)
            {
                case UpdateChannel.GitHub:
                    var release = await GitHubReleases.CheckAsync(DiagnosticLog.AppVersion).ConfigureAwait(false);
                    return release == null ? null : new AppUpdate(release.Url, release.Version, release.PublishedAt);
                case UpdateChannel.MicrosoftStore:
                    return await CheckStoreAsync(window);
                default:
                    return null;
            }
        }

        static UpdateChannel DetectChannel()
        {
            uint length = 0;
            int result;
            try
            {
                result = GetCurrentPackageFullName(ref length, null);
            }
            catch (EntryPointNotFoundException)
            {
                // Older Windows versions do not package this desktop app.
                return UpdateChannel.GitHub;
            }

            if (result == NoPackage)
                return UpdateChannel.GitHub;
            if (result != InsufficientBuffer || length == 0)
                return UpdateChannel.Unknown;

            var fullName = new StringBuilder((int)length);
            try
            {
                if (GetCurrentPackageFullName(ref length, fullName) == 0
                    && GetStagedPackageOrigin(fullName.ToString(), out int origin) == 0 && origin == StorePackageOrigin)
                    return UpdateChannel.MicrosoftStore;
            }
            catch (EntryPointNotFoundException)
            {
                // A packaged app with an unverified origin has no automatic update source.
            }
            return UpdateChannel.Unknown;
        }

        static async Task<AppUpdate> CheckStoreAsync(IntPtr window)
        {
            try
            {
                var context = StoreContext.GetDefault();
                ((IInitializeWithWindow)(object)context).Initialize(window);
                string family = Package.Current.Id.FamilyName;
                using (var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(7)))
                {
                    var updates = await context.GetAppAndOptionalStorePackageUpdatesAsync().AsTask(timeout.Token);
                    // Windows reports availability here. The Package may describe the installed package.
                    return updates.Any(update => update.Package.Id.FamilyName == family) ? new AppUpdate(StoreUpdates) : null;
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception error)
            {
                DiagnosticLog.Current.Write("WARNING", "Check Microsoft Store updates", error);
            }

            return null;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        static extern int GetCurrentPackageFullName(ref uint length, StringBuilder fullName);

        [DllImport("kernelbase.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        static extern int GetStagedPackageOrigin(string fullName, out int origin);

        [ComImport]
        [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        interface IInitializeWithWindow
        {
            void Initialize(IntPtr window);
        }
    }
}
