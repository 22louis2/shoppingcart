using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SimpleShoppingCart.Helper
{
    public class SQLConnection
    {
        string conString = @"Data Source=.\MSSQLSERVER03;Initial Catalog=Cart;Integrated Security=True";
        SqlConnection conn;

        public void OpenConnection()
        {
            conn = new SqlConnection(conString);
            conn.Open();
        }

        public void CloseConnection()
        {
            conn.Close();
        }

        public void ExecuteQueries(string query)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
        }

        public SqlDataReader DataReader(string query)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        public object ShowDataInGridView(string query)
        {
            SqlDataAdapter dr = new SqlDataAdapter(query, conString);
            DataSet ds = new DataSet();
            dr.Fill(ds);
            object dataum = ds.Tables[0];
            return dataum;
        }
    }
}
