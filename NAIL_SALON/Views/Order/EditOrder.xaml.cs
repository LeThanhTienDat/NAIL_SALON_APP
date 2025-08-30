using NAIL_SALON.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NAIL_SALON.ViewModels;
using System.ComponentModel;

namespace NAIL_SALON.Views.Order
{
    /// <summary>
    /// Interaction logic for EditOrder.xaml
    /// </summary>
    public partial class EditOrder : Window
    {
        private OrderModel _order;
        private SessionModel _admin;
        private OrderViewModel _vm;
        public EditOrder(OrderViewModel orderViewModel, OrderModel order, SessionModel admin)
        {
            InitializeComponent();
            this.DataContext = orderViewModel;  
            _vm = orderViewModel;
            _order = order;
            _admin = admin;    
            _vm.ServiceViewModel = new ServiceViewModel();
            _vm.ProductViewModel = new ProductViewModel();
            this.Loaded += EditOrder_Loaded;
        }
        private void EditOrder_Loaded(object sender, RoutedEventArgs e)
        {
            _vm.PropertyChanged += Vm_PropertyChanged;
        }


        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OrderViewModel.IsCreateSuccess))
            {
                if (_vm.IsCreateSuccess == true)
                {
                    this.Close();
                }
            }

        }

        private void NumberOnlyTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        
    }
}
