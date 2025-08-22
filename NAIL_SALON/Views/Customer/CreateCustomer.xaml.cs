using NAIL_SALON.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace NAIL_SALON.Views.Customer
{
    /// <summary>
    /// Interaction logic for CreateCustomer.xaml
    /// </summary>
    public partial class CreateCustomer : Window
    {
        private CustomerViewModel _vm;
        public CreateCustomer(CustomerViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            this.DataContext = _vm;
            this.Loaded += Vm_CreateCustomerLoaded;
        }

        public void Vm_CreateCustomerLoaded(object sender, RoutedEventArgs e)
        {
            _vm.PropertyChanged += Vm_PropertyChanged;
        }

        public void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(CustomerViewModel.IsCreateSuccess))
            {
                if(_vm.IsCreateSuccess)
                {
                    this.Close();
                }
            }
        }
        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    
    }
}
