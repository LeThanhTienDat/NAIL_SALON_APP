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

namespace NAIL_SALON.Views.Service
{
    /// <summary>
    /// Interaction logic for CreateService.xaml
    /// </summary>
    public partial class CreateService : Window
    {
        private ServiceViewModel _vm;
        public CreateService(ServiceViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            this.DataContext = _vm;
            this.Loaded += Vm_CreateService_Loaded;
            this.Closing += CreateService_Closing;
        }

        private void Vm_CreateService_Loaded(object sender, RoutedEventArgs e)
        {
            _vm.PropertyChanged += Vm_PropertyChanged;
        }

        private void CreateService_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _vm.TempServiceProduct.Clear();
        }

        public void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ServiceViewModel.IsCreateSuccess))
            {
                if (_vm.IsCreateSuccess == true)
                {
                    this.Close();
                }
            }
        }
    }
}
