using SimpleShoppingCart.Model;
using System;
using System.Collections.Generic;
using System.Text;
using SimpleShoppingCart.Helper;

namespace SimpleShoppingCart.Controller
{
    public class CartController : ICartController
    {
        // Connecting To SQLConnection Class
        SQLConnection conn = new SQLConnection();

        // Method to add to cart
        public void AddCart(int id, int quantity)
        {
            // Open the connection to database
            conn.OpenConnection();

            // SQL Query to insert into my cart table
            string query = $"INSERT INTO Cart (ProductId, Quantity) VALUES ({id}, {quantity})";
            conn.ExecuteQueries(query);

            // Close the connection to database
            conn.CloseConnection();
        }

        // Method to remove from cart
        public void RemoveCart(int id)
        {
            // open the connection to database
            conn.OpenConnection();

            // Query my database by deleting where the Id matches
            string query = $"DELETE FROM Cart WHERE Id={id}";
            conn.ExecuteQueries(query);

            // Close the connection to database
            conn.CloseConnection();
        }

        // Method to Clear the Cart
        public void ClearCart()
        {
            // Open the connection to database
            conn.OpenConnection();

            // Method to query and delete everything from the cart
            string query = $"DELETE FROM Cart";
            conn.ExecuteQueries(query);

            // Close the connection to database
            conn.CloseConnection();
        }

        // Method to Get Everything from the Cart table
        public List<OrderModel> GetAllCart()
        {
            // Creating a List data type
            List<OrderModel> orders = new List<OrderModel>();
            // Open the connection to database
            conn.OpenConnection();

            // Query the Cart table while using Inner Join to the Product table
            string query = $"SELECT Cart.Id, Cart.Quantity, Product.ProductName, Product.CostPrice FROM Cart inner join" +
                $" Product ON Cart.ProductId = Product.ProductId ";
            var result = conn.DataReader(query);

            // Loop through each data information found
            while(result.Read())
            {
                orders.Add(
                    new OrderModel
                    {
                        Id = (int)result[0],
                        Quantity = (int)result[1],
                        OrderName = (string)result[2],
                        Price = (decimal)result[3],
                        Amount = (decimal)result[3] * (int)result[1]
                    }
                );
            }

            // Close the connection to database
            conn.CloseConnection();
            return orders;
        }
    }
}
