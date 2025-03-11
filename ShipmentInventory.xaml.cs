using InventorySystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataAccessLayer.Models;
using System.Xml.Linq;

namespace InventorySystem.Views
{
    /// <summary>
    /// Interaction logic for ShipmentInventory.xaml
    /// </summary>
    public partial class ShipmentInventory : UserControl
    {
        public ShipmentInventory()
        {
            
        }

        private void ShipmentGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ShipmentGrid.SelectedItem is Shipment selectedItem)
            {
                ItemNameTxtBox.Text = selectedItem.ItemName;
                

                QuantityTxtBox.Text = (Convert.ToInt32(selectedItem.Quantity)).ToString();

                DestinationTxtBox.Text = selectedItem.Destination;
                var viewModel = DataContext as ShipmentViewModel;
                if (viewModel != null)
                {
                    viewModel.Shipment.ShipmentId = selectedItem.ShipmentId;
                }

            }
        }


        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Allow only numeric input
            e.Handled = !IsTextAllowed(e.Text);
        }
        private static bool IsTextAllowed(string text)
        {
            // Regex to match only numeric input 
            return Regex.IsMatch(text, @"^[0-9]+$");
        }

        
    }
}

