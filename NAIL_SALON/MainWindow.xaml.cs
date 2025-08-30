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
using NAIL_SALON.Models;
using NAIL_SALON.Views.Customer;
using NAIL_SALON.Views.Employer;
using NAIL_SALON.Views.Product;
using NAIL_SALON.Views.Service;
using NAIL_SALON.Views.Login;
namespace NAIL_SALON
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SessionModel _user;
        
        public MainWindow(SessionModel user)
        {
            InitializeComponent();
            _user = user;
            Authorization();
        }
        private void Authorization()
        {
            if (_user.IsAdmin == false)
            { 
                AdminMenu.Visibility = Visibility.Collapsed;
                EmployerMenu.Visibility = Visibility.Collapsed;
                ServiceMenu.Visibility = Visibility.Collapsed;
                ProductMenu.Visibility = Visibility.Collapsed;
            }
            else
            {
                EmployerProfile.Visibility = Visibility.Collapsed;
            }
        }

        public void Customer_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Views.Customer.CustomerView();
        }
        public void Employer_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Views.Employer.EmployerView();
        }
        public void Product_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Views.Product.ProductView();
        }
        public void Service_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Views.Service.ServiceView();
        }
        public void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Views.Order.OrderView(_user);
        }
        public void Admin_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Views.Admin.AdminView();
        }
        private void Logout(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show("Are you sure to Logout ?", "Confirm Logout", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirm == MessageBoxResult.Yes)
            {
                Window loginWindow = new Views.Login.LoginView();
                loginWindow.Show();
                this.Close();
            }
        }

    }
}
