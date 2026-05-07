using System;
using System.Data.SqlClient;

namespace SmartWholesale.DAL
{
    public class DatabaseConnection
    {
        private static DatabaseConnection? _instance;
        private string _connectionString;

        private DatabaseConnection()
        {
            _connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=SmartWholesaleDB;Trusted_Connection=True;";
        }

        public static DatabaseConnection Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DatabaseConnection();
                return _instance;
            }
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}