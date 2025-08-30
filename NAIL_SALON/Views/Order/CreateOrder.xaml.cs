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
using System.Windows.Shapes;
using System.ComponentModel;

namespace NAIL_SALON.Views.Order
{
    /// <summary>
    /// Interaction logic for CreateOrder.xaml
    /// </summary>
    public partial class CreateOrder : Window
    {
        private OrderViewModel _vm;
        private SessionModel _user;
        public CreateOrder(OrderViewModel order, SessionModel user)
        {
            InitializeComponent();
            _vm = order;
            this.DataContext = order;
            this.Loaded += CreateOrder_Loaded;
        }
        private void CreateOrder_Loaded(object sender, RoutedEventArgs e)
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
