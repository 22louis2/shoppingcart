using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleShoppingCart.Controller;
using SimpleShoppingCart.Helper;

namespace SimpleShoppingCart
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Dependency Injection Performed
            DISocket.PlugSocket();
            IProductController resProd = DISocket.ISocket;
            ICartController resCart = DISocket.IAdapter;
            SQLConnection connect = DISocket.Connect;

            // Passing the Dependency into the Form
            Application.Run(new ShoppingCartUI(resProd, resCart, connect));
        }
    }
}
