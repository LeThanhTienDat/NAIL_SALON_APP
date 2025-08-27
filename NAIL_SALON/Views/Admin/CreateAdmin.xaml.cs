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

namespace NAIL_SALON.Views.Admin
{
    /// <summary>
    /// Interaction logic for CreateAdmin.xaml
    /// </summary>
    public partial class CreateAdmin : Window
    {
        private AdminViewModel _vm;
        public CreateAdmin(AdminViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            this.DataContext = _vm;
            this.Loaded += CreateAdmin_Loaded;
        }
        private void CreateAdmin_Loaded(object sender, RoutedEventArgs e)
        {
            _vm.PropertyChanged += Vm_PropertyChanged;
        }


        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AdminViewModel.IsCreateSuccess))
            {
                if (_vm.IsCreateSuccess == true)
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
