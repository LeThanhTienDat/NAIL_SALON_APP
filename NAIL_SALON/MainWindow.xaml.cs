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
using NAIL_SALON.Views.Customer;
using NAIL_SALON.Views.Employer;
using NAIL_SALON.Views.Product;

namespace NAIL_SALON
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void OpenTestDialogCustomer(object sender, RoutedEventArgs e)
        {
            var testDialog = new CreateCustomer()
            {
                Owner = Window.GetWindow(this)
            };
            testDialog.ShowDialog();
        }
        public void OpenTestdialogEmployer(object sender, RoutedEventArgs e)
        {
            var testDialog = new CreateEmployer()
            {
                Owner = Window.GetWindow(this)
            };
            testDialog.ShowDialog();
        }
        public void OpenTestdialogProduct(object sender, RoutedEventArgs e)
        {
            var testDialog = new CreateProduct()
            {
                Owner = Window.GetWindow(this)
            };
            testDialog.ShowDialog();
        }

        public void Customer_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Views.Customer.CustomerView();
        }

    }
}
