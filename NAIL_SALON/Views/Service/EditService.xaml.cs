using NAIL_SALON.Models;
using NAIL_SALON.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for EditService.xaml
    /// </summary>
    public partial class EditService : Window
    {
        private ServiceModel _service;
        public event EventHandler ServiceSaved;
        public EditService(ServiceModel service)
        {
            InitializeComponent();
            
            _service = service;
            DataContext = new ServiceViewModel
            {
                CurrentService = service,
                ProductViewModel = new ProductViewModel()
            };        
            var vm =DataContext as ServiceViewModel;
            if(vm != null)
            {
                var toRemove = vm.ProductViewModel.Products.Where(x => service.DefaultProducts.Any(y=>y.ProductId == x.ID)).ToList();
                foreach(var item in toRemove)
                {
                    vm.ProductViewModel.Products.Remove(item);
                }
                vm.InitServiceProduct = new ObservableCollection<ServiceProductModel>(service.DefaultProducts);
            }
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
            if (e.PropertyName == nameof(ServiceViewModel.IsCreateSuccess))
            {
                if (sender is ServiceViewModel vm && vm.IsCreateSuccess == true)
                {
                    ServiceSaved?.Invoke(this, EventArgs.Empty);
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
