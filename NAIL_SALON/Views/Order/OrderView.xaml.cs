using NAIL_SALON.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NAIL_SALON.Views.Order
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : UserControl
    {
        private SessionModel _user;
        public OrderView(SessionModel user)
        {
            InitializeComponent();
            _user = user;
            DataContext = new OrderViewModel(_user);
        }
        public void OpenCreateOrder(object sender, RoutedEventArgs e)
        {
            var vm = (OrderViewModel)this.DataContext;
            vm.ServiceViewModel = new ServiceViewModel();
            vm.ProductViewModel = new ProductViewModel();
            vm.CustomerViewModel = new CustomerViewModel();
            vm.CurrentShowedOrder = new OrderModel();
            if(vm.ServiceViewModel.CurrentService.DefaultProducts != null)
            {
                vm.ServiceViewModel.CurrentService.DefaultProducts.Clear();
            }
            if(vm.CurrentOrder.OrderDetailsModel != null)
            {
                vm.CurrentOrder.OrderDetailsModel.Clear();
            }
            vm.IsCreateSuccess = false;
            var showDialog = new Views.Order.CreateOrder(vm, _user)
            {
                Owner = Window.GetWindow(this),
            };
            showDialog.ShowDialog();

        }

        private void NumberOnlyTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
        
    }
}
