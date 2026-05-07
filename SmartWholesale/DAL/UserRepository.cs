using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SmartWholesale.Models;

namespace SmartWholesale.DAL
{
    public class UserRepository
    {
        public UserBase? Login(string email, string password)
        {
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                string query = "SELECT * FROM Users WHERE Email = @Email AND UPassword = @Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return MapUser(reader);
                }
            }
            return null;
        }

        public List<UserBase> GetAllUsers()
        {
            List<UserBase> users = new List<UserBase>();
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                string query = "SELECT * FROM Users";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(MapUser(reader));
                }
            }
            return users;
        }

        public bool AddUser(UserBase user)
        {
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                string query = "INSERT INTO Users (UName, UPassword, UPhoneNo, Email, UAddress, Role, Salary) VALUES (@Name, @Pass, @Phone, @Email, @Addr, @Role, @Salary)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", user.UName);
                cmd.Parameters.AddWithValue("@Pass", user.UPassword);
                cmd.Parameters.AddWithValue("@Phone", (object?)user.UPhoneNo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Addr", (object?)user.UAddress ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                
                decimal salary = 0;
                if (user is SuperAdmin sa) salary = sa.Salary;
                else if (user is Admin a) salary = a.Salary;
                else if (user is Manager m) salary = m.Salary;
                
                cmd.Parameters.AddWithValue("@Salary", salary);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateUser(UserBase user)
        {
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                string query = "UPDATE Users SET UName=@Name, UPassword=@Pass, UPhoneNo=@Phone, Email=@Email, UAddress=@Addr, Salary=@Salary WHERE UId=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", user.UId);
                cmd.Parameters.AddWithValue("@Name", user.UName);
                cmd.Parameters.AddWithValue("@Pass", user.UPassword);
                cmd.Parameters.AddWithValue("@Phone", (object?)user.UPhoneNo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Addr", (object?)user.UAddress ?? DBNull.Value);

                decimal salary = 0;
                if (user is SuperAdmin sa) salary = sa.Salary;
                else if (user is Admin a) salary = a.Salary;
                else if (user is Manager m) salary = m.Salary;
                cmd.Parameters.AddWithValue("@Salary", salary);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteUser(int id)
        {
            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                string query = "DELETE FROM Users WHERE UId = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private UserBase MapUser(SqlDataReader reader)
        {
            string role = reader["Role"].ToString() ?? "Customer";
            UserBase user;

            switch (role)
            {
                case "SuperAdmin": user = new SuperAdmin { Salary = Convert.ToDecimal(reader["Salary"]) }; break;
                case "Admin": user = new Admin { Salary = Convert.ToDecimal(reader["Salary"]) }; break;
                case "Manager": user = new Manager { Salary = Convert.ToDecimal(reader["Salary"]) }; break;
                default: user = new Customer(); break;
            }

            user.UId = Convert.ToInt32(reader["UId"]);
            user.UName = reader["UName"].ToString() ?? "";
            user.UPassword = reader["UPassword"].ToString() ?? "";
            user.UPhoneNo = reader["UPhoneNo"].ToString() ?? "";
            user.Email = reader["Email"].ToString() ?? "";
            user.UAddress = reader["UAddress"].ToString() ?? "";
            user.JoiningDate = Convert.ToDateTime(reader["JoiningDate"]);

            return user;
        }
    }
}
