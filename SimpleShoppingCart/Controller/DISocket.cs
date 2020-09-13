using System;
using System.Collections.Generic;
using System.Text;
using SimpleShoppingCart.Helper;

namespace SimpleShoppingCart.Controller
{
    public class DISocket
    {
        // Interface property
        public static IProductController ISocket { get; set; }
        public static ICartController IAdapter { get; set; }

        public static SQLConnection Connect { get; set; }
        public static void PlugSocket()
        {
            // Initializing my Various Methods
            ProductController socket = new ProductController();
            CartController adapter = new CartController();
            SQLConnection connect = new SQLConnection();
            // Assigning it to the Interface Property
            ISocket = socket;
            IAdapter = adapter;
            Connect = connect;
        }
    }
}
