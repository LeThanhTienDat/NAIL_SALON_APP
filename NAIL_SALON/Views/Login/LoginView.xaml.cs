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

namespace NAIL_SALON.Views.Login
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private LoginViewModel _vm;
        public LoginView()
        {
            InitializeComponent();
             _vm = new LoginViewModel();
            DataContext = _vm;
            this.Loaded += Login_Loaded;
        }
        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            _vm.PropertyChanged += Vm_PropertyChanged;
        }


        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginViewModel.IsLoginSuccess))
            {
                if (_vm.IsLoginSuccess == true)
                {
                    this.Close();
                }
            }

        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is LoginViewModel vm)
            {
                vm.EnteredPassword = (sender as PasswordBox).Password;
            }
        }
    }
}
