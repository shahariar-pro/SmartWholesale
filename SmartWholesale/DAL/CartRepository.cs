using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SmartWholesale.Models;

namespace SmartWholesale.DAL
{
    public class CartRepository
    {
        public List<CartItem> GetCartItems(int userId)
        {
            List<CartItem> items = new List<CartItem>();
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                string query = "SELECT c.*, i.IBrand + ' ' + i.IModelNo as ItemName, i.IPrice FROM Cart c JOIN Items i ON c.IId = i.IId WHERE c.UId = @UId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UId", userId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(new CartItem
                    {
                        CartId = Convert.ToInt32(reader["CartId"]),
                        UId = Convert.ToInt32(reader["UId"]),
                        IId = Convert.ToInt32(reader["IId"]),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        ItemName = reader["ItemName"].ToString(),
                        Price = Convert.ToDecimal(reader["IPrice"])
                    });
                }
            }
            return items;
        }

        public bool AddToCart(int userId, int itemId, int quantity)
        {
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                string query = "INSERT INTO Cart (UId, IId, Quantity) VALUES (@UId, @IId, @Qty)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UId", userId);
                cmd.Parameters.AddWithValue("@IId", itemId);
                cmd.Parameters.AddWithValue("@Qty", quantity);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool RemoveFromCart(int cartId)
        {
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                string query = "DELETE FROM Cart WHERE CartId = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", cartId);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool ClearCart(int userId)
        {
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                string query = "DELETE FROM Cart WHERE UId = @UId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UId", userId);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
