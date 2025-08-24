using NAIL_SALON.Models;
using NAIL_SALON.Models.Components;
using NAIL_SALON.Models.Repositories;
using NAIL_SALON.Views.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace NAIL_SALON.ViewModels
{
    public class ServiceViewModel : INotifyPropertyChanged
    {
        private HashSet<ServiceModel> _allServices;
        private ObservableCollection<ServiceModel> _services;
        private ServiceModel _currentService;
        private int _currentPage = 1;
        private int _pageSize = 10;
        private string _showCurrentPage = "Page 1";
        private string _filterName;
        private string _filterDescription;
        private string _filterPrice;
        private System.Timers.Timer _filterTimer;
        private bool _isCreateSuccess;
        public ProductViewModel ProductViewModel { set; get; }
        public ServiceProductModel ServiceProductModel { set; get; }
        public ObservableCollection<ServiceProductModel> TempServiceProduct { get; set; }       
        
        public string FilterName
        {
            get => _filterName;
            set
            {
                if(_filterName != value)
                {
                    _filterName = value;
                    OnPropertyChanged(nameof(FilterName));
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
            get => _services ?? new ObservableCollection<ServiceModel>();
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
        public ICommand PrevPageCommand { get; }
        public ICommand CreateServiceCommand { get; }
        public ICommand ChangeActiveCommand { get; }
        public ICommand OpenEditServiceCommand { get; }
        public ICommand SaveEditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ChoseProductCommand { get; }
        public ICommand RemoveProductCommand { get; }
        public ICommand CloseCreateServiceCommand { get; }
        
        public ServiceViewModel()
        {
            _allServices = ServiceRepository.Instance.GetAll();
            if(CurrentService == null)
            {
                CurrentService = new ServiceModel();
            }
            Services = new ObservableCollection<ServiceModel>();
            NextPageCommand = new RelayCommand(_ => { CurrentPage++; LoadPage(CurrentPage); });
            PrevPageCommand = new RelayCommand(_ => {
                if (CurrentPage > 1) { CurrentPage--; LoadPage(CurrentPage); }
            });
            ChoseProductCommand = new RelayCommand(param => ChoseProduct(param as ProductModel));
            RemoveProductCommand = new RelayCommand(param => RemoveProduct(param as ServiceProductModel));
            ChangeActiveCommand = new RelayCommand(param => ChangeActive(param as ServiceModel));
            CreateServiceCommand = new RelayCommand(_ => CreateService());
            OpenEditServiceCommand = new RelayCommand(param => OpenEditService(param as ServiceModel));
            CloseCreateServiceCommand = new RelayCommand(_ => CloseCreateService());
            TempServiceProduct = new ObservableCollection<ServiceProductModel>();
            LoadPage(_currentPage);

        }

        public void LoadPage(int page)
        {
            if (page < 1) page = 1;
            var pageData = ServiceRepository.Instance.GetAllPaging(page, PageSize);
            Services.Clear();
            int number = 1 + (page - 1) * PageSize;
            foreach(var item in pageData)
            {
                item.RowNumber = number++;
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
            service.DefaultProducts = new ObservableCollection<ServiceProductModel>(
                service.ServiceProductModel
            );
            var showDialog = new Views.Service.EditService(service)
            {
                Owner = Application.Current.MainWindow
            };
            showDialog.ShowDialog();

        }
        public void CloseCreateService()
        {
            IsCreateSuccess = true;
            TempServiceProduct.Clear();
        }
        public void ChangeActive(ServiceModel service)
        {
            service.Active = service.Active == 1 ? 0 : 1;          
            ServiceRepository.Instance.ChangeActive(service);
            service.OnPropertyChanged(nameof(ServiceModel.Active));
            _allServices = ServiceRepository.Instance.GetAll();
        }






        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)=>PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));  
    }
}
