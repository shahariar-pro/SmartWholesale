using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SmartWholesale.Models;

namespace SmartWholesale.DAL
{
    public class ReviewRepository
    {
        public bool AddReview(Review review)
        {
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                string query = "INSERT INTO Reviews (UId, IId, Rating, Comment, ReviewDate) VALUES (@UId, @IId, @Rating, @Comment, @Date)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UId", review.UId);
                cmd.Parameters.AddWithValue("@IId", review.IId);
                cmd.Parameters.AddWithValue("@Rating", review.Rating);
                cmd.Parameters.AddWithValue("@Comment", review.Comment);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Review> GetAllReviews()
        {
            List<Review> reviews = new List<Review>();
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                string query = "SELECT r.*, u.UName, i.IBrand + ' ' + i.IModelNo as ItemName FROM Reviews r " +
                               "JOIN Users u ON r.UId = u.UId " +
                               "JOIN Items i ON r.IId = i.IId";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    reviews.Add(new Review
                    {
                        ReviewId = Convert.ToInt32(reader["ReviewId"]),
                        UId = Convert.ToInt32(reader["UId"]),
                        IId = Convert.ToInt32(reader["IId"]),
                        Rating = Convert.ToInt32(reader["Rating"]),
                        Comment = reader["Comment"].ToString() ?? "",
                        ReviewDate = Convert.ToDateTime(reader["ReviewDate"]),
                        UserName = reader["UName"].ToString() ?? ""
                    });
                }
            }
            return reviews;
        }

        public List<int> GetOrderedItemIds(int userId)
        {
            List<int> itemIds = new List<int>();
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                string query = "SELECT DISTINCT bi.IId FROM BillItems bi " +
                               "JOIN Bills b ON bi.BillId = b.BillId " +
                               "WHERE b.CustomerId = @UId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UId", userId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    itemIds.Add(Convert.ToInt32(reader["IId"]));
                }
            }
            return itemIds;
        }
    }
}
