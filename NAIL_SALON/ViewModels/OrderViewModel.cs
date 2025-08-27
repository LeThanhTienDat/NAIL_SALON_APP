using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NAIL_SALON.Models;
using NAIL_SALON.Models.Components;
using NAIL_SALON.Models.Repositories;

namespace NAIL_SALON.ViewModels
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        private ProductViewModel _productViewModel;
        private CustomerViewModel _customerViewModel;
        private ServiceViewModel _serviceViewModel;
        private bool _isCreateSuccess;
        private OrderModel _currentOrder;
        private CustomerModel _currentCustomer;
        private ObservableCollection<OrderModel> _orders;
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
        public CustomerModel CurrentCustomer
        {
            get => _currentCustomer;
            set
            {
                if(_currentCustomer != value)
                {
                    _currentCustomer = value;
                    OnPropertyChanged(nameof(CurrentCustomer));
                }
            }
        }
        public ObservableCollection<OrderModel> Orders
        {
            get => _orders;
            set
            {
                if(_orders != value)
                {
                    _orders = value;
                    OnPropertyChanged(nameof(Orders));
                }
            }
        }
        public OrderModel CurrentOrder
        {
            get => _currentOrder;
            set
            {
                if(_currentOrder != value)
                {
                    _currentOrder = value;
                    OnPropertyChanged(nameof(CurrentOrder));
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
        public bool IsCreateSuccess
        {
            get => _isCreateSuccess;
            set
            {
                if(_isCreateSuccess != value)
                {
                    _isCreateSuccess = value;
                    OnPropertyChanged(nameof(IsCreateSuccess));
                }
            }
        }

        public ICommand CreateCustomerCommand { get;}
        public ICommand ShowProductBelongCommand { get;}
        public ICommand ChoseServiceCommand { get; }
        public ICommand ChoseCustomerCommand { get; }
        public ICommand CheckProductBelongCommand { get; }

        public OrderViewModel()
        {
            CurrentOrder = new OrderModel();
            CurrentCustomer = new CustomerModel();
            CurrentOrder.OrderDetailsModel = new ObservableCollection<OrderDetailsModel>();
            CreateCustomerCommand = new RelayCommand(_ => CreateCustomer());
            ShowProductBelongCommand = new RelayCommand(param => ShowProductBelong(param as ServiceModel));
            ChoseServiceCommand = new RelayCommand(param => ChoseService(param as ServiceModel));
            ChoseCustomerCommand = new RelayCommand(param => ChoseCustomer(param as CustomerModel));
            CheckProductBelongCommand = new RelayCommand(param => CheckProductBelong(param as OrderDetailsModel));
        }             

        public void CreateCustomer()
        {
            IsCreateSuccess = false;
            var customerDatacontext = CustomerViewModel;
            var showDialog = new Views.Customer.CreateCustomer(customerDatacontext)
            {
                Owner = Application.Current.MainWindow,
            };
            showDialog.ShowDialog();

        }
        public void ShowProductBelong(ServiceModel service)
        {
            var chosenService = ServiceRepository.Instance.FindById(service.ID);
            ServiceViewModel.CurrentService = chosenService;
            ServiceViewModel.CurrentService.DefaultProducts = new ObservableCollection<ServiceProductModel>(chosenService.ServiceProductModel);           

        }
        public void ChoseService(ServiceModel service)
        {
            var newOrderDetail = new OrderDetailsModel();
            newOrderDetail.CurrentServiceBelong = service;
            newOrderDetail.ServiceId = service.ID;
            CurrentOrder.OrderDetailsModel.Add(newOrderDetail);
            for(int i = 0; i<CurrentOrder.OrderDetailsModel.Count; i++)
            {
                CurrentOrder.OrderDetailsModel[i].RowNumber = i + 1;
            }
        }
        public void ChoseCustomer(CustomerModel customer)
        {
            CurrentCustomer = customer;


        }
        public void CheckProductBelong(OrderDetailsModel orderDetails)
        {
            var chosenService = ServiceRepository.Instance.FindById(orderDetails.ServiceId);
            ServiceViewModel.CurrentService = chosenService;
            ServiceViewModel.CurrentService.DefaultProducts = new ObservableCollection<ServiceProductModel>(chosenService.ServiceProductModel);
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));    
    }
}
