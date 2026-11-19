using System;
using System.Threading;
using System.Windows.Forms;
namespace CameraControlStudio {
    static class Program {
        [STAThread]
        static void Main(string[] args) {
            UiText.Initialize(args);
            foreach (var arg in args)
            if (arg.StartsWith("--capture-docs=")) {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(arg.Substring(15)));
                return;
            }
            bool created;
            using (var mutex = new Mutex(true, "Local\\CameraControlStudio", out created)) {
                if (!created) {
                    MessageBox.Show(UiText.T("The application is already running.", "Programmet kører allerede."));
                    return;
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }
}
