using DataAccessLayer.Models;
using InventorySystem.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataAccessLayer.Interface;

namespace InventorySystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       // IShipmentRepository repository;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ShipmentViewModel();
        }
    }
}