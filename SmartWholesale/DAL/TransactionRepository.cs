using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SmartWholesale.Models;

namespace SmartWholesale.DAL
{
    public class TransactionRepository
    {
        public int CreateBill(Bill bill)
        {
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string billQuery = "INSERT INTO Bills (TotalAmount, CustomerId) OUTPUT INSERTED.BillId VALUES (@Amount, @CustId)";
                    SqlCommand billCmd = new SqlCommand(billQuery, conn, trans);
                    billCmd.Parameters.AddWithValue("@Amount", bill.TotalAmount);
                    billCmd.Parameters.AddWithValue("@CustId", bill.CustomerId);
                    
                    int billId = (int)billCmd.ExecuteScalar();

                    foreach (var item in bill.Items)
                    {
                        string itemQuery = "INSERT INTO BillItems (BillId, IId, Quantity, UnitPrice) VALUES (@BillId, @IId, @Qty, @Price)";
                        SqlCommand itemCmd = new SqlCommand(itemQuery, conn, trans);
                        itemCmd.Parameters.AddWithValue("@BillId", billId);
                        itemCmd.Parameters.AddWithValue("@IId", item.IId);
                        itemCmd.Parameters.AddWithValue("@Qty", item.Quantity);
                        itemCmd.Parameters.AddWithValue("@Price", item.UnitPrice);
                        itemCmd.ExecuteNonQuery();

                        // Update Stock
                        string stockQuery = "UPDATE Items SET IStockStatus = IStockStatus - @Qty WHERE IId = @IId";
                        SqlCommand stockCmd = new SqlCommand(stockQuery, conn, trans);
                        stockCmd.Parameters.AddWithValue("@Qty", item.Quantity);
                        stockCmd.Parameters.AddWithValue("@IId", item.IId);
                        stockCmd.ExecuteNonQuery();
                    }

                    trans.Commit();
                    return billId;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public bool RecordTransaction(Transaction t)
        {
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                string query = "INSERT INTO Transactions (BillId, UId, TotalAmount) VALUES (@BillId, @UId, @Amount)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BillId", t.BillId);
                cmd.Parameters.AddWithValue("@UId", t.UId);
                cmd.Parameters.AddWithValue("@Amount", t.TotalAmount);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Transaction> GetAllTransactions()
        {
            List<Transaction> list = new List<Transaction>();
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                string query = "SELECT * FROM Transactions";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Transaction
                    {
                        TId = Convert.ToInt32(reader["TId"]),
                        BillId = Convert.ToInt32(reader["BillId"]),
                        UId = Convert.ToInt32(reader["UId"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        TransactionDate = Convert.ToDateTime(reader["TransactionDate"])
                    });
                }
            }
            return list;
        }
    }
}
