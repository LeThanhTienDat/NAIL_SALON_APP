using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NAIL_SALON.Models;

namespace NAIL_SALON.ViewModels
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        private ProductViewModel _productViewModel;
        private CustomerViewModel _customerViewModel;
        private ServiceViewModel _serviceViewModel;
        
        public ProductViewModel ProductViewModel
        {
            get => _productViewModel;
            set
            {
                if(_productViewModel != value)
                {
                    _productViewModel = value;
                    OnPropertyChanged(nameof(ProductViewModel));
                }
            }
        }
        public ServiceViewModel ServiceViewModel
        {
            get => _serviceViewModel;
            set
            {
                if(_serviceViewModel != value)
                {
                    _serviceViewModel = value;
                    OnPropertyChanged(nameof(ServiceViewModel));
                }
            }
        }
        public CustomerViewModel CustomerViewModel
        {
            get => _customerViewModel;
            set
            {
                if(_customerViewModel != value)
                {
                    _customerViewModel = value;
                    OnPropertyChanged(nameof(CustomerViewModel));
                }
            }
        }

        public ICommand CreateCustomerCommand { get;}


        public OrderViewModel()
        {

        }







        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));    
    }
}
