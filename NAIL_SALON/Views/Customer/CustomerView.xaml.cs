using NAIL_SALON.ViewModels;
using NAIL_SALON.Views.Employer;
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

namespace NAIL_SALON.Views.Customer
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : UserControl
    {
        public CustomerView()
        {
            InitializeComponent();
            DataContext = new CustomerViewModel();
        }
        public void OpenCreateCustomer(object sender, RoutedEventArgs e)
        {
            var vm = (CustomerViewModel)this.DataContext;
            vm.IsCreateSuccess = false;
            var showDialog = new CreateCustomer(vm)
            {
                Owner = Window.GetWindow(this)
            };
            showDialog.ShowDialog();
        }
    }
}
