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

namespace NAIL_SALON.Models.Components.Admin
{
    /// <summary>
    /// Interaction logic for AdminUC.xaml
    /// </summary>
    public partial class AdminUC : UserControl
    {
        public AdminUC()
        {
            InitializeComponent();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is AdminViewModel vm)
            {
                vm.CurrentAdmin.Password = (sender as PasswordBox).Password;
            }
        }
        private void PasswordBox_ConfirmPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is AdminViewModel vm)
            {
                vm.CurrentAdmin.ConfirmPassword = (sender as PasswordBox).Password;
            }
        }
        private void NumberOnlyTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
    }
}
