using NAIL_SALON.ViewModels;
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
using NAIL_SALON.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace NAIL_SALON.Views.Order
{
    /// <summary>
    /// Interaction logic for BuyProduct.xaml
    /// </summary>
    public partial class BuyProduct : Window
    {
        private OrderViewModel _vm;
        private OrderModel _currentShowedOrder;
        public BuyProduct(OrderViewModel vm, OrderModel currentOrder )
        {
            InitializeComponent();
            _vm = vm;
            _currentShowedOrder = currentOrder;
            _vm.TempProducts = new ObservableCollection<ServiceProductModel>();
            this.DataContext = _vm;
            _vm.ProductViewModel = new ProductViewModel();
            this.Loaded += BuyProduct_Loaded;
        }

        private void BuyProduct_Loaded(object sender, RoutedEventArgs e)
        {
            _vm.PropertyChanged += Vm_PropertyChanged;
        }


        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OrderViewModel.IsBuyProductSuccess))
            {
                if (_vm.IsBuyProductSuccess == true)
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
