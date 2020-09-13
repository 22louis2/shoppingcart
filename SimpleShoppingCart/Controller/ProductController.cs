using SimpleShoppingCart.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SimpleShoppingCart.Helper;
using System.Windows.Forms;

namespace SimpleShoppingCart.Controller
{
    public class ProductController : IProductController
    {
        // Static Data of the class
        //public static int Count = 0;

        // Method to initialize the SQLConnection class
        SQLConnection conn = new SQLConnection();
        // Method to Add Product
        public void AddProduct(string name, decimal cost)
        {
            // open the connection to database
            conn.OpenConnection();

            // Query the table by inserting into it
            string query = $"INSERT INTO Product (ProductName, CostPrice) VALUES ('{name}', {cost})";
            conn.ExecuteQueries(query);

            // Close the connection to database
            conn.CloseConnection();
        }

        public void RemoveProduct(int id)
        {
            // Open the connection to database
            conn.OpenConnection();

            // Query to delete from both the Product and Cart table while referencing its foreign key
            string query = $"DELETE FROM Product WHERE ProductId={id}";
            string query2 = $"DELETE FROM Cart WHERE ProductId={id}";
            conn.ExecuteQueries(query2);
            conn.ExecuteQueries(query);
            // Close the connection to database
            conn.CloseConnection();
        }

        public void EditProduct(int id, string name, decimal cost)
        {
            // open the connection to database
            conn.OpenConnection();

            // Query the table to update the product
            string query = $"UPDATE Product SET ProductName = '{name}', CostPrice = {cost} WHERE ProductId={id}";
            conn.ExecuteQueries(query);

            // Close the connection to database
            conn.CloseConnection();
        }

        public List<ProductModel> GetAllProduct()
        {
            // Created List data type
            List<ProductModel> products = new List<ProductModel>();
            // Open the connection to database
            conn.OpenConnection();
            // Query the table and collect every data available
            string query = $"SELECT * FROM Product";
            var result = conn.DataReader(query);

            // Loop through the result information
            while (result.Read())
            {
                var product = new ProductModel();

                product.ProductId = (int)result[0];
                product.ProductName = (string)result[1];
                product.CostPrice = (decimal)result[2];
                product.DateAdded = (DateTime)result[3];

                products.Add(product);
                
                // Auto increment to know every data available
                //Count++;
            }

            // Close the connection to database
            conn.CloseConnection();
            return products;
        }

        public List<ProductModel> GetAllProductByOffSet(int offset, int span)
        {
            List<ProductModel> products = new List<ProductModel>();
            // Open the connection to database
            conn.OpenConnection();
            string query = $"SELECT * FROM Product ORDER BY ProductId OFFSET {offset} ROWS " +
                $" FETCH NEXT {span} ROWS ONLY";
            var result = conn.DataReader(query);

            while (result.Read())
            {
                var product = new ProductModel();

                product.ProductId = (int)result[0];
                product.ProductName = (string)result[1];
                product.CostPrice = (decimal)result[2];
                product.DateAdded = (DateTime)result[3];

                products.Add(product);
            }

            // Close the connection to database
            conn.CloseConnection();
            return products;
        }

        // Method to filter through by its price
        public List<ProductModel> FilterProductByPrice(decimal cost)
        {
            List<ProductModel> products = new List<ProductModel>();
            // open the connection to database
            conn.OpenConnection();
            string query = $"SELECT * FROM Product WHERE CostPrice <= {cost}";
            var result = conn.DataReader(query);

            while (result.Read())
            {
                var product = new ProductModel();

                product.ProductId = (int)result[0];
                product.ProductName = (string)result[1];
                product.CostPrice = (decimal)result[2];
                product.DateAdded = (DateTime)result[3];

                products.Add(product);
            }

            // Close the connection to database
            conn.CloseConnection();
            return products;
        }

        // Method to filter through the product by its name
        public List<ProductModel> FilterProductByProductName(string name)
        {
            List<ProductModel> products = new List<ProductModel>();
            conn.OpenConnection();
            string query = $"SELECT * FROM Product WHERE ProductName LIKE '%{name}%'";
            var result = conn.DataReader(query);

            while (result.Read())
            {
                var product = new ProductModel();

                product.ProductId = (int)result[0];
                product.ProductName = (string)result[1];
                product.CostPrice = (decimal)result[2];
                product.DateAdded = (DateTime)result[3];

                products.Add(product);
            }

            conn.CloseConnection();
            return products;
        }
    }
}
