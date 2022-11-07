using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Layanan_Dokter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           // if (Environment.OSVersion.Version.Major >= 6)
             //   SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
//private static extern bool SetProcessDPIAware();
    }
}
