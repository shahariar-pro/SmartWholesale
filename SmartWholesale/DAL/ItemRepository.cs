using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SmartWholesale.Models;

namespace SmartWholesale.DAL
{
    public class ItemRepository
    {
        public List<Item> GetAllItems()
        {
            List<Item> items = new List<Item>();
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                string query = "SELECT * FROM Items";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(MapItem(reader));
                }
            }
            return items;
        }

        public List<Item> GetItemsByOwner(int ownerId)
        {
            List<Item> items = new List<Item>();
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                string query = "SELECT * FROM Items WHERE OwnerId = @OwnerId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OwnerId", ownerId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(MapItem(reader));
                }
            }
            return items;
        }

        public bool AddItem(Item item)
        {
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                string query = "INSERT INTO Items (IType, IBrand, IModelNo, IPrice, IStockStatus, IMinimumStock, OwnerId) VALUES (@Type, @Brand, @Model, @Price, @Stock, @MinStock, @OwnerId)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Type", item.IType);
                cmd.Parameters.AddWithValue("@Brand", item.IBrand);
                cmd.Parameters.AddWithValue("@Model", item.IModelNo);
                cmd.Parameters.AddWithValue("@Price", item.IPrice);
                cmd.Parameters.AddWithValue("@Stock", item.IStockStatus);
                cmd.Parameters.AddWithValue("@MinStock", item.IMinimumStock);
                cmd.Parameters.AddWithValue("@OwnerId", item.OwnerId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateItem(Item item)
        {
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                string query = "UPDATE Items SET IType=@Type, IBrand=@Brand, IModelNo=@Model, IPrice=@Price, IStockStatus=@Stock, IMinimumStock=@MinStock WHERE IId=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", item.IId);
                cmd.Parameters.AddWithValue("@Type", item.IType);
                cmd.Parameters.AddWithValue("@Brand", item.IBrand);
                cmd.Parameters.AddWithValue("@Model", item.IModelNo);
                cmd.Parameters.AddWithValue("@Price", item.IPrice);
                cmd.Parameters.AddWithValue("@Stock", item.IStockStatus);
                cmd.Parameters.AddWithValue("@MinStock", item.IMinimumStock);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteItem(int id)
        {
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                string query = "DELETE FROM Items WHERE IId = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private Item MapItem(SqlDataReader reader)
        {
            return new Item
            {
                IId = Convert.ToInt32(reader["IId"]),
                IType = reader["IType"].ToString(),
                IBrand = reader["IBrand"].ToString(),
                IModelNo = reader["IModelNo"].ToString(),
                IPrice = Convert.ToDecimal(reader["IPrice"]),
                IStockStatus = Convert.ToInt32(reader["IStockStatus"]),
                IMinimumStock = Convert.ToInt32(reader["IMinimumStock"]),
                OwnerId = Convert.ToInt32(reader["OwnerId"])
            };
        }
    }
}
