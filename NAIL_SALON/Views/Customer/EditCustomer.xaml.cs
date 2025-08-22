using NAIL_SALON.Models;
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
    /// Interaction logic for EditCustomer.xaml
    /// </summary>
    public partial class EditCustomer : Window
    {
        private CustomerModel _customer;
        public EditCustomer(CustomerModel customer)
        {
            InitializeComponent();
            _customer = customer;
            DataContext = new CustomerViewModel
            {
                CurrentCustomer = customer,
            };
            this.Loaded += Vm_EditCustomerLoaded;
        }

        public void Vm_EditCustomerLoaded(object sender, RoutedEventArgs e)
        {
            if(this.DataContext is CustomerViewModel vm)
            {
                vm.PropertyChanged += Vm_PropertyChanged;
            }
        }
        public void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(CustomerViewModel.IsCreateSuccess))
            {
                if(sender is CustomerViewModel vm && vm.IsCreateSuccess)
                {
                    this.Close();
                }
            }
        }
        public void Cancel_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    
}
