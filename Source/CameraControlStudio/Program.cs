using System;
using System.Threading;
using System.Windows.Forms;

namespace CameraControlStudio
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                DiagnosticLog.Current.Write("CRITICAL", "Unhandled process exception", e.ExceptionObject as Exception);
            try
            {
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                Application.ThreadException += (sender, e) =>
                {
                    var log = DiagnosticLog.Current.Write("CRITICAL", "Unhandled UI exception", e.Exception);
                    try
                    {
                        ErrorDialog.Show(Form.ActiveForm, log ?? DiagnosticLog.Current.FindLatest(), closing: true);
                    }
                    finally
                    {
                        Application.Exit();
                    }
                };
                Run(args);
            }
            catch (Exception ex)
            {
                var log = DiagnosticLog.Current.Write("CRITICAL", "Application startup or shutdown", ex);
                ErrorDialog.Show(null, log ?? DiagnosticLog.Current.FindLatest(), closing: true);
            }
        }

        static void Run(string[] args)
        {
            foreach (var arg in args)
                if (arg.StartsWith("--capture-docs="))
                {
                    UiText.Initialize(args);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm(arg.Substring(15)));
                    return;
                }

            bool created;
            bool restart;
            using (var mutex = new Mutex(true, "Local\\CameraControlStudio", out created))
            {
                if (!created)
                {
                    UiText.Initialize(args);
                    MessageBox.Show(UiText.T("The application is already running.", "Programmet kører allerede."), "",
                        MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, AppForm.MessageDirection);
                    return;
                }

                try
                {
                    DiagnosticLog.Current.Configure(DiagnosticLogSettings.Load());
                }
                catch (Exception ex)
                {
                    DiagnosticLog.Current.Write("WARNING", "Load diagnostic log settings", ex);
                }

                var catalog = new ProfileStore().Read();
                var profile = ProfileStore.ReadSelectedProfile(catalog);
                UiText.Initialize(args, catalog.Profiles[profile].Config.Language);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                using (var form = new MainForm(catalog.Profiles[profile], profile))
                {
                    Application.Run(form);
                    restart = form.RestartRequested;
                }
            }

            if (restart)
                Application.Restart();
        }
    }
}
