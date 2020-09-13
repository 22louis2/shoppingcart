using System;
using System.Collections.Generic;
using System.Text;
using SimpleShoppingCart.Model;

namespace SimpleShoppingCart.Controller
{
    public interface IProductController
    {
        // Methods the ProductController Must Implement
        public void AddProduct(string name, decimal cost);
        public void RemoveProduct(int id);
        public void EditProduct(int id, string name, decimal cost);
        public List<ProductModel> GetAllProduct();
        public List<ProductModel> GetAllProductByOffSet(int offset, int span);
        public List<ProductModel> FilterProductByPrice(decimal cost);
        public List<ProductModel> FilterProductByProductName(string name);
    }
}
