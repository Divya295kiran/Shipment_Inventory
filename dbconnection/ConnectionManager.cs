using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.DbConnection
{
    public static class ConnectionManager
    {
        private static SqlConnection _connection; // Shared connection instance
        private static readonly object LockObj = new object(); // For thread-safety

        // Method to get an open connection
        public static SqlConnection GetConnection()
        {
            
                if (_connection == null || _connection.State == System.Data.ConnectionState.Closed || _connection.State == System.Data.ConnectionState.Broken)
                {
                    lock (LockObj)
                    {
                        if (_connection == null || _connection.State == System.Data.ConnectionState.Closed || _connection.State == System.Data.ConnectionState.Broken)
                        {
                            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentDb"]?.ConnectionString);
                            _connection.Open();
                        }
                    }
                }
            
              return _connection;
        }

        // Method to close the connection
        public static void CloseConnection()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null; // Reset the connection instance
            }
        }
    }
}
