using DataAccessLayer.Interface;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Utility.DbConnection;
using Utility.ExceptionHandler;


namespace DataAccessLayer.Repository
{
    public class ShipmentRepository : IShipmentRepository
    {
       
        private int count=0;

        public ShipmentRepository()
        {
            
        }

        // Create
        public int AddShipment(Shipment shipment)
        {
            try
            {
                using (SqlConnection conn = ConnectionManager.GetConnection())
                {
                    //conn.Open();
                    SqlCommand cmd = new SqlCommand("InsertShipment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ItemName", shipment.ItemName);
                    cmd.Parameters.AddWithValue("@Quantity", shipment.Quantity);
                    cmd.Parameters.AddWithValue("@Destination", shipment.Destination);
                    cmd.Parameters.AddWithValue("@CreatedBy", shipment.CurrentUser);


                    count = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex);
                ErrorHandler.GetErrorMessage(ex);
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return count;
        }

        // Read
        public ObservableCollection<Shipment> GetAllShipments()
        {
            ObservableCollection<Shipment> shipments = new ObservableCollection<Shipment>();
            try
            {
                using (SqlConnection conn = ConnectionManager.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("DisplayShipment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        shipments.Add(new Shipment
                        {
                            ShipmentId = Convert.ToInt32(reader["ShipmentId"]),
                            ItemName = reader["ItemName"].ToString(),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            Destination = reader["Destination"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex);
                ErrorHandler.GetErrorMessage(ex);
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return shipments;
        }

        // Update
        public int UpdateShipment(Shipment shipment)
        {
            try
            {
                using (SqlConnection conn = ConnectionManager.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("UpdateShipment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ShipmentId", shipment.ShipmentId);
                    cmd.Parameters.AddWithValue("@ItemName", shipment.ItemName);
                    cmd.Parameters.AddWithValue("@Quantity", shipment.Quantity);
                    cmd.Parameters.AddWithValue("@Destination", shipment.Destination);
                    cmd.Parameters.AddWithValue("@ModifiedBy", shipment.CurrentUser);


                    count = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex);
                ErrorHandler.GetErrorMessage(ex);
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return count;
        }

        // Delete
        public int DeleteShipment(int shipmentId)
        {
            try
            {
                using (SqlConnection conn = ConnectionManager.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("DeleteShipment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ShipmentId", shipmentId);


                    count = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex);
                ErrorHandler.GetErrorMessage(ex);
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return count;
        }

        public void Dispose()
        {
            ConnectionManager.CloseConnection();
        }
    }
}
