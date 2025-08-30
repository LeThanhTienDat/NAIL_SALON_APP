using NAIL_SALON.Models;
using NAIL_SALON.Models.Components;
using NAIL_SALON.Models.Helpers;
using NAIL_SALON.Models.Repositories;
using NAIL_SALON.Views.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace NAIL_SALON.ViewModels
{
    public class ServiceViewModel : INotifyPropertyChanged
    {
        public event Action RequestRefresh;
        private HashSet<ServiceModel> _allServices;
        private ObservableCollection<ServiceModel> _services;
        private ServiceModel _currentService;
        private int _currentPage = 1;
        private int _pageSize = 8;
        private string _showCurrentPage = "Page 1";
        private string _filterName;
        private string _filterDescription;
        private string _filterPrice;
        private string _filterDiscount;
        private System.Timers.Timer _filterTimer;
        private bool _isCreateSuccess;
        public ProductViewModel ProductViewModel { set; get; }
        public ServiceProductModel ServiceProductModel { set; get; }
        private ObservableCollection<ServiceProductModel> _tempServiceProduct { get; set; }
        public List<ProductModel> ProductRemoved = new List<ProductModel>();
        public List<ProductModel> ProductAdded= new List<ProductModel>();
        public ObservableCollection<ServiceProductModel> InitServiceProduct { get; set; }
        public string FilterName
        {
            get => _filterName;
            set
            {
                if(_filterName != value)
                {
                    _filterName = value;
                    OnPropertyChanged(nameof(FilterName));

                    if (string.IsNullOrEmpty(FilterName))
                    {
                        LoadPage(_currentPage);
                    }
                    else
                    {
                        DebounceFilter(_currentPage, 400);
                    }
                }
            }
        }
        public ObservableCollection<ServiceProductModel> TempServiceProduct
        {
            get => _tempServiceProduct;
            set
            {
                if(_tempServiceProduct != value)
                {
                    _tempServiceProduct = value;
                    OnPropertyChanged(nameof(TempServiceProduct));
                }
            }
        }
        public string FilterDescription
        {
            get => _filterDescription;
            set
            {
                if( _filterDescription != value)
                {
                    _filterDescription = value;
                    OnPropertyChanged(nameof(FilterDescription));
                    if (string.IsNullOrEmpty(FilterDescription))
                    {
                        LoadPage(_currentPage);
                    }
                    else
                    {
                        DebounceFilter(_currentPage, 400);
                    }
                }
            }
        }
        public string FilterDiscount
        {
            get => _filterDiscount;
            set
            {
                if(_filterDiscount != value){
                    _filterDiscount = value;
                    OnPropertyChanged(nameof(FilterDiscount));
                    if (string.IsNullOrEmpty(FilterDiscount))
                    {
                        LoadPage(_currentPage);
                    }
                    else
                    {
                        DebounceFilter(_currentPage, 400);
                    }
                }
            }
        }
        public string FilterPrice
        {
            get => _filterPrice;
            set
            {
                if(_filterPrice != value)
                {
                    _filterPrice = value;
                    OnPropertyChanged(nameof(FilterPrice));
                    if (string.IsNullOrEmpty(FilterPrice))
                    {
                        LoadPage(_currentPage);
                    }
                    else
                    {
                        DebounceFilter(_currentPage, 400);
                    }
                }
            }
        }
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if(_currentPage != value)
                {
                    _currentPage = value;
                    ShowCurrentPage = "Page " + value;
                    OnPropertyChanged(nameof(CurrentPage));
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
        public ObservableCollection<ServiceModel> Services
        {
            get => _services;
            set
            {
                if(_services != value)
                {
                    _services = value;
                    OnPropertyChanged(nameof(Services));
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
        public ServiceModel CurrentService
        {
            get => _currentService;
            set
            {
                if(_currentService != value)
                {
                    _currentService = value;
                    OnPropertyChanged(nameof(CurrentService));
                }
            }
        }
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if(_pageSize != value)
                {
                    _pageSize = value;
                    OnPropertyChanged(nameof(PageSize));
                }
            }
        }

        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand CreateServiceCommand { get; }
        public ICommand ChangeActiveCommand { get; }
        public ICommand OpenEditServiceCommand { get; }
        public ICommand SaveEditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ChoseProductCommand { get; }
        public ICommand RemoveProductCommand { get; }
        public ICommand CloseCreateServiceCommand { get; }
        public ICommand EditChoseProductCommand { get; }
        public ICommand EditRemoveProductCommand { get; }
        public ICommand SaveEditServiceCommand { get; }
        public ICommand CloseEditServiceCommand { get; }
        
        public ServiceViewModel()
        {
            _allServices = ServiceRepository.Instance.GetAll();
            if(CurrentService == null)
            {
                CurrentService = new ServiceModel();
            }
            Services = new ObservableCollection<ServiceModel>();
            NextPageCommand = new RelayCommand(_ => { CurrentPage++; LoadPage(CurrentPage); });
            PreviousPageCommand = new RelayCommand(_ => {
                if (CurrentPage > 1) { CurrentPage--; LoadPage(CurrentPage); }
            });
            ChoseProductCommand = new RelayCommand(param => ChoseProduct(param as ProductModel));
            RemoveProductCommand = new RelayCommand(param => RemoveProduct(param as ServiceProductModel));
            ChangeActiveCommand = new RelayCommand(param => ChangeActive(param as ServiceModel));
            CreateServiceCommand = new RelayCommand(_ => CreateService());
            EditChoseProductCommand = new RelayCommand(param => EditChoseProduct(param as ProductModel));
            EditRemoveProductCommand = new RelayCommand(param => EditRemoveProduct(param as ProductModel));
            OpenEditServiceCommand = new RelayCommand(param => OpenEditService(param as ServiceModel));
            CloseCreateServiceCommand = new RelayCommand(_ => CloseCreateService());
            SaveEditServiceCommand = new RelayCommand(_ => SaveEditService());
            CloseEditServiceCommand = new RelayCommand(_ => CloseEditService());
            TempServiceProduct = new ObservableCollection<ServiceProductModel>();
            LoadPage(_currentPage);

        }

        public void LoadPage(int page)
        {
            if (page < 1) page = 1;
            var pageData = ServiceRepository.Instance.GetAllPaging(page, PageSize);            
      
            Services.Clear();
            int number = 1 + (page - 1) * PageSize;
            foreach (var item in pageData)
            {
                item.RowNumber = number++;
                var getDefaulProduct = ServiceProductRepository.Instance.GetAllByServiceId(item.ID);
                item.DefaultProducts = new ObservableCollection<ServiceProductModel>(getDefaulProduct);
                Services.Add(item);              
            }

        }
        
        public void ChoseProduct(ProductModel product)
        {
            try
            {
                var removeItem = product;
                ProductViewModel.Products.Remove(removeItem);

                var chosenProduct = new ServiceProductModel();
                chosenProduct.CurrentProductBelong = product;               
                TempServiceProduct.Add(chosenProduct);
                for(int i = 0; i < TempServiceProduct.Count; i++)
                {
                    TempServiceProduct[i].RowNumber = i + 1;
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void RemoveProduct(ServiceProductModel serviceProduct)
        {
            try
            {
                TempServiceProduct.Remove(serviceProduct);
                ProductViewModel.Products.Add(serviceProduct.CurrentProductBelong);
                for(int i = 0; i < TempServiceProduct.Count; i++)
                {
                    TempServiceProduct[i].RowNumber = i + 1;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void EditChoseProduct(ProductModel product)
        {                       
            ProductModel newProduct = product;
            ServiceProductModel newServiceProduct = new ServiceProductModel();
            newServiceProduct.CurrentProductBelong = newProduct;
            CurrentService.DefaultProducts.Add(newServiceProduct);
            for (int i = 0; i < CurrentService.DefaultProducts.Count; i++)
            {
                CurrentService.DefaultProducts[i].RowNumber = i + 1;
            }
            var removeItem = product;
            ProductViewModel.Products.Remove(removeItem);           

        }
        public void EditRemoveProduct(ProductModel product)
        {            
            
            ProductViewModel.Products.Add(product);
            for (int i = 0; i < ProductViewModel.Products.Count; i++)
            {
                ProductViewModel.Products[i].RowNumber = i + 1;
            }
            foreach (var item2 in CurrentService.DefaultProducts.ToList())
            {
                if (item2.CurrentProductBelong.ID == product.ID)
                {
                    CurrentService.DefaultProducts.Remove(item2);
                }
            }
           
        }
        public void CreateService()
        {
            try
            {
                ServiceModel serviceItem = new ServiceModel();
                serviceItem.Name = CurrentService.Name;
                serviceItem.Description = CurrentService.Description;
                serviceItem.Price = CurrentService.Price;
                serviceItem.Active = CurrentService.Active;
                serviceItem.Discount = CurrentService.Discount;
                ServiceRepository.Instance.Create(serviceItem);
                if(serviceItem.ID > 0 && (TempServiceProduct != null || TempServiceProduct.Count() > 0))
                {
                    bool check = true;
                    try
                    {
                        foreach (var item in TempServiceProduct)
                        {
                            ServiceProductModel serviceProductItem = new ServiceProductModel();
                            serviceProductItem.Quantity = item.Quantity;
                            serviceProductItem.ProductId = item.CurrentProductBelong.ID;
                            serviceProductItem.ServiceId = serviceItem.ID;
                            ServiceProductRepository.Instance.Create(serviceProductItem);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        check = true;
                    }

                    if (check)
                    {
                        MessageBox.Show("Service added successful!");
                        IsCreateSuccess = true;
                        CurrentService = new ServiceModel();
                        LoadPage(_currentPage);
                        _allServices = ServiceRepository.Instance.GetAll();
                        RequestRefresh?.Invoke();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void OpenEditService(ServiceModel service)
        {
            IsCreateSuccess = false;
            ProductAdded = new List<ProductModel>();
            ProductRemoved = new List<ProductModel>();
            CurrentService.DefaultProducts = new ObservableCollection<ServiceProductModel>(
                service.ServiceProductModel
            );          
            
            for (int i = 0; i < service.DefaultProducts.Count; i++)
            {
                service.DefaultProducts[i].RowNumber = i + 1;
            }
            var showDialog = new Views.Service.EditService(service)
            {
                //Owner = Application.Current.MainWindow,               
            };
            showDialog.ShowDialog();

        }
        public void SaveEditService()
        {
            foreach (var item in CurrentService.DefaultProducts)
            {
                item.ServiceId = CurrentService.ID;
                item.ProductId = item.CurrentProductBelong.ID;
            }
            foreach (var item in InitServiceProduct)
            {
                ServiceProductRepository.Instance.Delete(item);
            }
            foreach(var item in CurrentService.DefaultProducts)
            {
                ServiceProductRepository.Instance.Create(item);
            }
            bool checkUpdateService = ServiceRepository.Instance.Update(CurrentService);
            if(checkUpdateService)
            {
                MessageBox.Show("Update successful!");
                LoadPage(_currentPage);
                _allServices = ServiceRepository.Instance.GetAll();                  
                IsCreateSuccess = true;               
            }            
        }
        public void CloseCreateService()
        {
            IsCreateSuccess = true;
            TempServiceProduct.Clear();
        }
        public void CloseEditService()
        {
            IsCreateSuccess = true;
        }
        public void ChangeActive(ServiceModel service)
        {
            service.Active = service.Active == 1 ? 0 : 1;          
            ServiceRepository.Instance.ChangeActive(service);
            service.OnPropertyChanged(nameof(ServiceModel.Active));
            _allServices = ServiceRepository.Instance.GetAll();
        }

        public void ApplyFilter(int page)
        {
            try
            {
                if (page < 1) page = 1;
                var filtering = _allServices.ToList();
                if (!string.IsNullOrEmpty(FilterName))
                    filtering = filtering.Where(e => e.Name != null && StringHelper.RemoveDiacritics(e.Name).IndexOf(FilterName, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                if (!string.IsNullOrEmpty(FilterDescription))
                    filtering = filtering.Where(e => e.Description != null && StringHelper.RemoveDiacritics(e.Description).IndexOf(FilterDescription, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                if (!string.IsNullOrEmpty(FilterPrice))
                    filtering = filtering.Where(e => e.Price.ToString().Contains(FilterPrice)).ToList();
                if (!string.IsNullOrEmpty(FilterDiscount))
                {
                    var parseDiscount = int.TryParse(FilterDiscount, out var stock);
                    if (parseDiscount) filtering = filtering.Where(e => e.Discount.ToString().Contains(FilterDiscount)).ToList();
                }
                var pageData = filtering
                        .Skip((page -1) * PageSize)
                        .Take(PageSize)
                        .ToList();
                Services.Clear();
                int number = 1 + (page - 1) * PageSize;
                foreach(var item in pageData)
                {
                    item.RowNumber = number++;
                    var getDefaulProduct = ServiceProductRepository.Instance.GetAllByServiceId(item.ID);
                    item.DefaultProducts = new ObservableCollection<ServiceProductModel>(getDefaulProduct);
                    Services.Add(item); 
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }




        private void DebounceFilter(int page, int delayMs)
        {
            _filterTimer?.Stop();
            _filterTimer = new System.Timers.Timer(delayMs) { AutoReset = false };
            _filterTimer.Elapsed += (s, e) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                    ApplyFilter(page)
                );
            };
            _filterTimer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)=>PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));  
    }
}
