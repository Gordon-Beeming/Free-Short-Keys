using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Free_Short_Keys
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var processCurrent = Process.GetCurrentProcess();
            foreach (var process in Process.GetProcessesByName(processCurrent.ProcessName))
            {
                if (process.Id != processCurrent.Id)
                {
                    process.Kill();
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
