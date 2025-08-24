using NAIL_SALON.Models;
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

namespace NAIL_SALON.Views.Service
{
    /// <summary>
    /// Interaction logic for EditService.xaml
    /// </summary>
    public partial class EditService : Window
    {
        private ServiceModel _service;
        public EditService(ServiceModel service)
        {
            InitializeComponent();
            
            _service = service;
            DataContext = new ServiceViewModel
            {
                CurrentService = service,
                ProductViewModel = new ProductViewModel()
            };
            this.Loaded += Vm_EditServiceLoaded;
        }
        public void Vm_EditServiceLoaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is ServiceViewModel vm)
            {
                vm.PropertyChanged += Vm_PropertyChanged;
            }
        }

        public void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ProductViewModel.IsCreateSuccess))
            {
                if (sender is ProductViewModel vm && vm.IsCreateSuccess == true)
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
