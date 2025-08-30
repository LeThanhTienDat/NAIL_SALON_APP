using NAIL_SALON.Models;
using NAIL_SALON.Models.Components;
using NAIL_SALON.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NAIL_SALON.ViewModels
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        //paging and toggle create dialog
        private int _currentPage = 1;
        private int _pageSize = 10;
        private string _showCurrentPage = "Page 1";
        private bool _isCreateSuccess;
        private bool _isBuyProductSuccess;

        //init variable in create dialog
        private ObservableCollection<OrderModel> _orders;
        private ProductViewModel _productViewModel;
        private CustomerViewModel _customerViewModel;
        private ServiceViewModel _serviceViewModel;
        private OrderModel _currentOrder;
        

        //Show in main screen
        private OrderModel _currentShowedOrder;
        private ObservableCollection<OrderDetailsModel> _currentShowedServices;
        private ServiceModel _currentShowedService;

        //BuyProduct handler
        private ObservableCollection<ServiceProductModel> _tempProducts;
        private ServiceModel _temService;
        private OrderDetailsModel _tempOrderDetail;
        private bool _checkAccompanyProduct;
       

        //create Order variable
        private SessionModel _currentAdmin;
        private CustomerModel _currentCustomer;

        //INotifyChanged functions
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
        public bool IsBuyProductSuccess
        {
            get => _isBuyProductSuccess;
            set
            {
                if(_isBuyProductSuccess != value)
                {
                    _isBuyProductSuccess = value;
                    OnPropertyChanged(nameof(IsBuyProductSuccess));
                }
            }
        }       
        public OrderDetailsModel TempOrderDetail
        {
            get => _tempOrderDetail;
            set
            {
                if(_tempOrderDetail != value)
                {
                    _tempOrderDetail = value;
                    OnPropertyChanged(nameof(TempOrderDetail));
                }
            }
        }
        public ServiceModel TemService
        {
            get => _temService;
            set
            {
                if(_temService != value)
                {
                    _temService = value;
                    OnPropertyChanged(nameof(TemService));
                }
            }
        }
        public ObservableCollection<ServiceProductModel> TempProducts
        {
            get => _tempProducts;
            set
            {
                if(_tempProducts != value)
                {
                    _tempProducts = value;
                    OnPropertyChanged(nameof(TempProducts));
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
        public OrderModel CurrentShowedOrder
        {
            get => _currentShowedOrder;
            set
            {
                if(_currentShowedOrder != value)
                {
                    _currentShowedOrder = value;
                    OnPropertyChanged(nameof(CurrentShowedOrder));
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
        public string ShowCurrentPage
        {
            get => _showCurrentPage;
            set
            {
                if(_showCurrentPage != value)
                {
                    _showCurrentPage = value;
                    OnPropertyChanged(nameof(ShowCurrentPage));
                }
            }
        }
        public ObservableCollection<OrderDetailsModel> CurrentShowedServices
        {
            get => _currentShowedServices;
            set
            {
                if(_currentShowedServices != value)
                {
                    _currentShowedServices = value;
                    OnPropertyChanged(nameof(CurrentShowedServices));
                }
            }
        }
        public ServiceModel CurrentShowedService
        {
            get => _currentShowedService;
            set
            {
                if(_currentShowedService != value)
                {
                    _currentShowedService = value;
                    OnPropertyChanged(nameof(CurrentShowedService));
                }
            }
        }

        public ICommand CreateCustomerCommand { get;}
        public ICommand ShowProductBelongCommand { get;}
        public ICommand ChoseServiceCommand { get; }
        public ICommand EditChoseServiceCommand { get; }
        public ICommand SaveEditOrderCommand { get; }
        public ICommand ChoseCustomerCommand { get; }
        public ICommand CheckProductBelongCommand { get; }
        public ICommand CreateOrderCommand { get; }
        public ICommand CloseCreateOrderCommand { get; } 
        public ICommand BuyThisProductCommand { get; }
        public ICommand EditBuyThisProductCommand { get; }
        public ICommand CloseBuyProductCommand { get; }
        public ICommand CancelOrderCommand { get; }
        public ICommand ShowOrderInforCommand { get; }
        public ICommand RemoveProductCommand { get; }
        public ICommand EditRemoveAccompanyProductCommand { get; }
        public ICommand EditRemoveProductCommand { get; }
        public ICommand ShowOrderProductsCommand { get; }
        public ICommand OpenEditOrderCommand { get; }
        public ICommand OpenBuyProductCommand { get; }
        public ICommand ConfirmBuyProductCommand { get; }
        public ICommand EditAccompanyProductsCommand { get; }
        public ICommand ConfirmBuyAccompanyProductCommand { get; }
        public ICommand CloseBuyAccompanyProductCommand { get; }
        public OrderViewModel(SessionModel admin)
        {
            _currentAdmin = admin;
            CurrentOrder = new OrderModel();
            CurrentCustomer = new CustomerModel();
            Orders = new ObservableCollection<OrderModel>();
            CurrentOrder.OrderDetailsModel = new ObservableCollection<OrderDetailsModel>();
            CreateCustomerCommand = new RelayCommand(_ => CreateCustomer());
            ShowProductBelongCommand = new RelayCommand(param => ShowProductBelong(param as ServiceModel));
            ChoseServiceCommand = new RelayCommand(param => ChoseService(param as ServiceModel));
            SaveEditOrderCommand = new RelayCommand(_ => SaveEditOrder());
            EditChoseServiceCommand = new RelayCommand(param => EditChoseService(param as ServiceModel));
            ChoseCustomerCommand = new RelayCommand(param => ChoseCustomer(param as CustomerModel));
            CheckProductBelongCommand = new RelayCommand(param => CheckProductBelong(param as OrderDetailsModel));
            CloseCreateOrderCommand = new RelayCommand(_ => CloseCreateOrder());
            BuyThisProductCommand = new RelayCommand(param => BuyThisProduct(param as ProductModel));
            CloseBuyProductCommand = new RelayCommand(_ => CloseBuyProduct());
            CreateOrderCommand = new RelayCommand(_ => CreateOrder());
            CancelOrderCommand = new RelayCommand(param => CancelOrder(param as OrderModel));
            ShowOrderInforCommand = new RelayCommand(param => ShowOrderInfor(param as OrderModel));
            RemoveProductCommand = new RelayCommand(param => RemoveProduct(param as OrderDetailsModel));
            EditRemoveProductCommand = new RelayCommand(param => EditRemoveProduct(param as OrderDetailsModel));
            ShowOrderProductsCommand = new RelayCommand(param => ShowOrderProducts(param as OrderDetailsModel));
            OpenEditOrderCommand = new RelayCommand(param => OpenEditOrder(param as OrderModel));
            OpenBuyProductCommand = new RelayCommand(_ => OpenBuyProduct());
            ConfirmBuyProductCommand = new RelayCommand(_ => ConfirmBuyProduct());
            EditAccompanyProductsCommand = new RelayCommand(param => EditAccompanyProducts(param as OrderDetailsModel));
            EditBuyThisProductCommand = new RelayCommand(param => EditBuyThisProduct(param as ProductModel));
            EditRemoveAccompanyProductCommand = new RelayCommand(param => EditRemoveAccompanyProduct(param as ServiceProductModel));
            ConfirmBuyAccompanyProductCommand = new RelayCommand(_ => ConfirmBuyAccompanyProduct());
            CloseBuyAccompanyProductCommand = new RelayCommand(_ => CloseBuyAccompanyProduct());
            LoadPage(_currentPage);
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
        public void EditChoseService(ServiceModel service)
        {
            var newOrderDetail = new OrderDetailsModel();
            newOrderDetail.CurrentServiceBelong = service;
            newOrderDetail.ServiceId = service.ID;
            newOrderDetail.OrderId=CurrentShowedOrder.ID;
            CurrentShowedOrder.OrderDetailsModel.Add(newOrderDetail);
            for (int i = 0; i < CurrentShowedOrder.OrderDetailsModel.Count; i++)
            {
                CurrentShowedOrder.OrderDetailsModel[i].RowNumber = i + 1;
            }
        }
        public void ChoseCustomer(CustomerModel customer)
        {
            var checkOrder = OrderRepository.Instance.CheckOrder(customer.ID);
            if(checkOrder != null)
            {
                MessageBox.Show("This customer is being serviced!");
            }
            else
            {
                CurrentCustomer = customer;
            }
        }
        public void CheckProductBelong(OrderDetailsModel orderDetails)
        {
            var chosenService = ServiceRepository.Instance.FindById(orderDetails.ServiceId ?? 0);
            ServiceViewModel.CurrentService = chosenService;
            ServiceViewModel.CurrentService.DefaultProducts = new ObservableCollection<ServiceProductModel>(chosenService.ServiceProductModel);
        }
        public void OpenBuyProduct()
        {

            if(CurrentOrder.OrderDetailsModel == null)
            {
                IsBuyProductSuccess = false;
                var showBuyProduct = new Views.Order.BuyProduct(this, CurrentShowedOrder);
                showBuyProduct.ShowDialog();
            }
            else if(CurrentOrder.OrderDetailsModel != null)
            {
                _checkAccompanyProduct = false;
                foreach(var item in CurrentOrder.OrderDetailsModel)
                {
                    if(item.CurrentServiceBelong.Name.Equals("accompanying product", StringComparison.OrdinalIgnoreCase))
                    {
                        _checkAccompanyProduct = true;
                    }
                }
                if(_checkAccompanyProduct == true)
                {
                    MessageBox.Show("This order has Service with Accompany products");
                }
                else
                {
                    IsBuyProductSuccess = false;
                    var showBuyProduct = new Views.Order.BuyProduct(this, CurrentShowedOrder);
                    showBuyProduct.ShowDialog();
                }
            }                                             
        }
        public void ConfirmBuyProduct()
        {
            try
            {
                TemService = new ServiceModel();
                TemService.Name = "accompanying product";
                TemService.Description = "";
                TemService.Price = 0;
                TemService.Active = 1;
                TemService.Discount = 0;
                ServiceRepository.Instance.Create(TemService);
                if(TemService.ID > 0)
                {                    
                    foreach(var item in TempProducts)
                    {
                        item.ServiceId = TemService.ID;
                        item.ProductId = item.CurrentProductBelong.ID;
                        ServiceProductRepository.Instance.Create(item);                       
                    }
                    TempOrderDetail = new OrderDetailsModel();
                    TempOrderDetail.CurrentServiceBelong = TemService;
                    TempOrderDetail.ServiceId = TemService.ID;
                    CurrentOrder.OrderDetailsModel.Add(TempOrderDetail);
                    int number = 1 + (_currentPage - 1) * _pageSize;
                    foreach (var item in CurrentOrder.OrderDetailsModel)
                    {
                        item.RowNumber = number++;
                    }
                    IsBuyProductSuccess = true;

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void EditAccompanyProducts(OrderDetailsModel orderDetail)
        {
            try
            {
                IsBuyProductSuccess = false;
                TempOrderDetail = orderDetail;
                var showChangeAccompanyProduct = new Views.Order.EditBuyProduct(this);
                showChangeAccompanyProduct.ShowDialog();               
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void EditBuyThisProduct(ProductModel product)
        {
            try
            {
                ServiceProductModel newProduct = new ServiceProductModel();
                newProduct.CurrentProductBelong = product;
                newProduct.ProductId = product.ID;
                newProduct.ServiceId = TempOrderDetail.CurrentServiceBelong.ID;
                TempOrderDetail.CurrentServiceBelong.DefaultProducts.Add(newProduct);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void CreateOrder()
        {
            try
            {
                if (CurrentCustomer.Name  == null)
                {
                    MessageBox.Show("Please chose Customer!");
                }else if(CurrentOrder.OrderDetailsModel.Count == 0)
                {
                    MessageBox.Show("Please chose Service!");
                }
                else
                {
                    var confirm = MessageBox.Show("Are you sure to create new Order ?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if(confirm == MessageBoxResult.Yes)
                    {
                        var newOrder = new OrderModel();
                        newOrder.CustomerId = CurrentCustomer.ID;
                        if(_currentAdmin.IsAdmin == false)
                        {
                            newOrder.EmployerId = _currentAdmin.ID;
                        }
                        else
                        {
                            newOrder.EmployerId = null;
                        }
                        newOrder.OrderDate = DateTime.Now;
                        newOrder.Status = "Processing";
                        OrderRepository.Instance.Create(newOrder);

                        if(newOrder.ID > 0)
                        {
                            int check = 0;
                            foreach(var item in CurrentOrder.OrderDetailsModel)
                            {
                                var newOrderDetails = new OrderDetailsModel();
                                newOrderDetails.ServiceId = item.ServiceId;
                                newOrderDetails.OrderId = newOrder.ID;
                                OrderDetailsRepository.Instance.Create(newOrderDetails);
                                if(newOrderDetails.ID == 0)
                                {
                                    check++;
                                }
                            }
                            if(check == 0)
                            {
                                MessageBox.Show("Create Order Successful!");
                                CurrentCustomer = new CustomerModel();
                                if(ServiceViewModel.CurrentService.DefaultProducts != null)
                                {
                                    ServiceViewModel.CurrentService.DefaultProducts.Clear();
                                }
                                if(CurrentOrder.OrderDetailsModel != null)
                                {
                                    CurrentOrder.OrderDetailsModel.Clear();
                                }
                                LoadPage(_currentPage);
                                IsCreateSuccess = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        
        public void LoadPage(int page)
        {
            if (page < 1) page = 1;
            var pageData = OrderRepository.Instance.GetProcessingOrder();
            Orders.Clear();
            int number = 1 + (page - 1) * _pageSize;
            foreach(var item in pageData)
            {
                item.RowNumber = number++;
                Orders.Add(item);
            }
            
        }

        public void CancelOrder(OrderModel order)
        {
            try
            {
                var checkCancelOrder = OrderRepository.Instance.CancelOrder(order);
                LoadPage(_currentPage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        } 
        public void BuyThisProduct(ProductModel product)
        {
            try
            {
                ServiceProductModel tempServiceProduct = new ServiceProductModel();
                tempServiceProduct.CurrentProductBelong = product;
                TempProducts.Add(tempServiceProduct);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void CloseCreateOrder()
        {
            IsCreateSuccess = true;
        }
        public void CloseBuyProduct()
        {
            IsBuyProductSuccess = true;
        }
        public void ShowOrderInfor(OrderModel order)
        {
            try
            {
                CurrentShowedOrder = order;
                CurrentShowedServices = order.OrderDetailsModel; 
                foreach(var item in CurrentShowedOrder.OrderDetailsModel)
                {
                    item.OrderId = order.ID;
                }
                int number = 1 + (_currentPage -1) * _pageSize;
                foreach(var item in CurrentShowedServices)
                {
                    item.RowNumber = number++;
                }
                CurrentShowedService = new ServiceModel();                
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void ShowOrderProducts(OrderDetailsModel orderDetailsModel)
        {
            try
            {
                var currentService = ServiceRepository.Instance.FindById(orderDetailsModel.ServiceId ?? 0);
                
                CurrentShowedService = currentService;
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void RemoveProduct(OrderDetailsModel orderDetails)
        {
            try
            {
                var removeCurrentProductBelong = CurrentOrder.OrderDetailsModel.Where(d => d.CurrentServiceBelong.ID == orderDetails.CurrentServiceBelong.ID).ToList();
                foreach(var item in removeCurrentProductBelong){
                    CurrentOrder.OrderDetailsModel.Remove(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void EditRemoveAccompanyProduct(ServiceProductModel serviceProduct)
        {
            try
            {
                var removeCurrentAccompanyProduct = TempOrderDetail.CurrentServiceBelong.DefaultProducts.Where(d => d.ProductId == serviceProduct.ProductId).FirstOrDefault();
                TempOrderDetail.CurrentServiceBelong.DefaultProducts.Remove(removeCurrentAccompanyProduct);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void ConfirmBuyAccompanyProduct()
        {
            try
            {
                var serviceId = TempOrderDetail.CurrentServiceBelong.ID;
                bool check = ServiceProductRepository.Instance.DeleteById(serviceId);
                if (check)
                {
                    foreach (var item in TempOrderDetail.CurrentServiceBelong.DefaultProducts)
                    {
                        ServiceProductRepository.Instance.Create(item);

                    }
                    MessageBox.Show("Update successful!");
                    IsBuyProductSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void CloseBuyAccompanyProduct()
        {
            IsBuyProductSuccess = true;
        }
        public void EditRemoveProduct(OrderDetailsModel orderDetails)
        {
            try
            {
                var removeCurrentProductBelong = CurrentShowedOrder.OrderDetailsModel.Where(d => d.CurrentServiceBelong.ID == orderDetails.CurrentServiceBelong.ID).ToList();
                foreach(var item in removeCurrentProductBelong)
                {
                    CurrentShowedOrder.OrderDetailsModel.Remove(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void OpenEditOrder(OrderModel order)
        {
            IsCreateSuccess = false;
            CurrentShowedOrder = order;

            
            for (int i = 0; i < CurrentShowedOrder.OrderDetailsModel.Count; i++)
            {
                CurrentShowedOrder.OrderDetailsModel[i].RowNumber = i + 1;
                CurrentShowedOrder.OrderDetailsModel[i].OrderId = order.ID;
            }
            var showDialog = new Views.Order.EditOrder(this,order, _currentAdmin)
            {
             
            };
            showDialog.ShowDialog();
        }
        public void SaveEditOrder()
        {
            try
            {
                //Delete all orderDetail
                var checkDelOrderDetail = OrderDetailsRepository.Instance.DeleteById(CurrentShowedOrder.ID);
                var checkCreateNew = 0;
                foreach (var item in CurrentShowedOrder.OrderDetailsModel)
                {
                    OrderDetailsRepository.Instance.Create(item);
                    if (item.ID == 0)
                    {
                        checkCreateNew++;
                    }
                }
                if (checkCreateNew == 0)
                {
                    MessageBox.Show("Update Successful!");
                    IsCreateSuccess = true;
                    LoadPage(_currentPage);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));    
    }
}
