using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleShoppingCart.Model
{
    public class ProductModel
    {
        // Properties of the Product
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal CostPrice { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
