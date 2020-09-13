using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleShoppingCart.Model
{
    public class OrderModel
    {
        // Properties of the Order
        public int Id { get; set; }
        public string OrderName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}
