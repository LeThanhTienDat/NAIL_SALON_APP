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
        public OrderView()
        {
            InitializeComponent();
            DataContext = new OrderViewModel();
        }
        public void OpenCreateOrder(object sender, RoutedEventArgs e)
        {
            var vm = (OrderViewModel)this.DataContext;
            
            var showDialog = new Views.Order.CreateOrder(vm)
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
