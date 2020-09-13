using System;
using System.Collections.Generic;
using System.Text;
using SimpleShoppingCart.Model;

namespace SimpleShoppingCart.Controller
{
    public interface ICartController
    {
        // methods the CartController class must implement
        public void AddCart(int id, int quantity);

        public void RemoveCart(int id);

        public void ClearCart();

        public List<OrderModel> GetAllCart();
    }
}
