
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using DataAccessLayer.Interface;
//using System.Data.SqlClient;

namespace InventorySystem.ViewModels
{
    public class ShipmentViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Shipment> shipmentList = new ObservableCollection<Shipment>();
        private Shipment _shipment;
        private ShipmentRepository shipmentRepository;
        private int count = 0;
        private string currentUser;
        private readonly IShipmentRepository repository;
        
        public ShipmentViewModel()
        {

           
            _shipment = new Shipment();
            currentUser = Environment.UserName;
            
            shipmentRepository = new ShipmentRepository();
            AddCommand = new RelayCommand(AddShipment);
            UpdateCommand = new RelayCommand(UpdateShipment);
            DeleteCommand = new RelayCommand(DeleteShipment);
            LoadData();


        }

        public string CreatedBy => currentUser;
        public string ItemName
        {
            get => _shipment.ItemName;
            set
            {
                if (_shipment.ItemName != value)
                {
                    _shipment.ItemName = value;
                    OnPropertyChanged("ItemName");
                }
            }
        }

        public int Quantity
        {
            get => _shipment.Quantity;
            set
            {
                if (_shipment.Quantity != value)
                {
                    _shipment.Quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }
        public string Destination
        {
            get => _shipment.Destination;
            set
            {
                if (_shipment.Destination != value)
                {
                    _shipment.Destination = value;
                    OnPropertyChanged("Destination");
                }
            }
        }
        
        public ObservableCollection<Shipment> ShipmentList
        {
            get { return shipmentList; }
            set
            {
                shipmentList = value;
                OnPropertyChanged("ShipmentList");
            }
        }

        public Shipment Shipment
        {
            get { return _shipment; }
            set
            {
                _shipment = value;
                OnPropertyChanged("Shipment");
            }
        }



        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        #region SaveOperation
        public void AddShipment()
        {

            if (!ValidatingControl())
            {
                return;
            }

           int count = shipmentRepository.AddShipment(Shipment);
            if (count >= 1)
            {
               
                MessageBox.Show("Shipment saved successfully!", "Save", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearControl();

            }
            //ClearControl();
            LoadData();
        }
        #endregion

        #region UpdateOperation
        public void UpdateShipment()
        {
            if (!ValidatingControl())
            {
                return;
            }
            ShipmentList.Clear();
            if (Shipment.ShipmentId != 0)
            {
                 count = shipmentRepository.UpdateShipment(Shipment);
                if (count >= 1)
                {

                    MessageBox.Show("Shipment Updated successfully!", "Save", MessageBoxButton.OK, MessageBoxImage.Information);
                    Shipment.ShipmentId = 0;
                    ClearControl();
                }
            }
            LoadData();

        }

        #endregion
        #region DeleteOperation
        public void DeleteShipment()
        {
            int count = shipmentRepository.DeleteShipment(Shipment.ShipmentId);
            if (count >= 1)
            {
                MessageBox.Show("Product Deleted successfully!", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearControl();
            }
            LoadData();
        }
        #endregion

        #region  DisplayOperation
        public void LoadData()
        {
            ShipmentList.Clear();
           ShipmentList = shipmentRepository.GetAllShipments();
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region ClearControl

        private void ClearControl()


        {
            Shipment = new Shipment
            {
                ItemName = string.Empty,
            Destination = string.Empty,
                Quantity = 0

            };

           
        }

        #endregion

        #region ValidateOperation
        private bool ValidatingControl()
        {
            if (string.IsNullOrEmpty(Shipment.ItemName))
            {
                MessageBox.Show("Please Enter Item Name", "Shipment", MessageBoxButton.OK);
                return false;
            }
            

            if (Shipment.Quantity <= 0)
            {
                MessageBox.Show("Quantity must be greater than zero", "Shipment", MessageBoxButton.OK);
                return false;
            }


          

            if (string.IsNullOrEmpty(Shipment.Destination))
            {
                MessageBox.Show("Please Enter Destination", "Shipment", MessageBoxButton.OK);
                return false;
            }
           

            return true;
        }

        #endregion


        
    }
}
