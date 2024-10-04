using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clipboard {
    internal static class Program {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //Application.Run(new Form2(true, "[fpffppf"));
            //Application.Run(new Report(true, "test"));
            //Application.Run(new Statistics(true, "test"));
            //Application.Run(new Client("ivanov@example.com"));
            //Application.Run(new ViewingInvoices("ivanov@example.com"));
            //Application.Run(new RegistrationOfServices("ivanov@example.com"));
        }
    }
}
