using LabExpressDesktop.Reporting;
using SalesMngmt.Configs;
using SalesMngmt.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesMngmt
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Signin());
            //Application.Run(new Prod(1));
        }
    }
}
