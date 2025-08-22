using NAIL_SALON.Models;
using NAIL_SALON.Models.Components;
using NAIL_SALON.Models.Repositories;
using NAIL_SALON.Views.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        public ICommand NextPageCommand { get; }
        public ICommand PrevPageCommand { get; }
        public ICommand CreateServiceCommand { get; }
        public ICommand ChangeActiveCommand { get; }
        public ICommand OpenEditServiceCommand { get; }
        public ICommand SaveEditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ChoseProductCommand { get; }
        
        public ServiceViewModel()
        {
            _allServices = ServiceRepository.Instance.GetAll();
            CurrentService = new ServiceModel();
            Services = new ObservableCollection<ServiceModel>();
            NextPageCommand = new RelayCommand(_ => { CurrentPage++; LoadPage(CurrentPage); });
            PrevPageCommand = new RelayCommand(_ => {
                if (CurrentPage > 1) { CurrentPage--; LoadPage(CurrentPage); }
            });
            ChoseProductCommand = new RelayCommand(param => ChoseProduct(param as ProductModel));
            CreateServiceCommand = new RelayCommand(_ => CreateService());
            OpenEditServiceCommand = new RelayCommand(param => OpenEditService(param as ServiceModel));
            LoadPage(_currentPage);

        }

        public void LoadPage(int page)
        {
            if (page < 1) page = 1;
        }
        
        public void ChoseProduct(ProductModel product)
        {

        }
        public void CreateService()
        {

        }
        public void OpenEditService(ServiceModel service)
        {

        }







        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)=>PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));  
    }
}
