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

namespace NAIL_SALON.Views.Admin
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {
        public AdminView()
        {
            InitializeComponent();
            DataContext = new AdminViewModel();
        }
        public void OpenCreateAdmin(object sender, RoutedEventArgs e)
        {
            var vm = (AdminViewModel)this.DataContext;
            vm.IsCreateSuccess = false;
            var showDialog = new CreateAdmin(vm)
            {
                Owner = Window.GetWindow(this)
            };
            showDialog.ShowDialog();
        }
    }
}
