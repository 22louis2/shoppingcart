using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleShoppingCart.Model
{
    public class CartModel
    {
        // Property of the Cart
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateOfOrder { get; set; }
    }
}
