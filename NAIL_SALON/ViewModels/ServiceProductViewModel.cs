using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIL_SALON.Models;

namespace NAIL_SALON.ViewModels
{
    internal class ServiceProductViewModel : INotifyPropertyChanged
    {
        private int _currentPage = 1;
        public int PageSize = 10;
        private string _showCurrentPage = "Page 1";
        private ServiceProductModel _currentServiceProduct;
        private HashSet<ServiceProductModel> _allServiceProduct;
        private ObservableCollection<ServiceProductModel> _serviceProducts;
        private ObservableCollection<ServiceProductModel> _tempServiceProduct;
        private System.Timers.Timer _filterTime;

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
        public ServiceProductModel CurrentServiceProduct
        {
            get => _currentServiceProduct;
            set
            {
                if(_currentServiceProduct != value)
                {
                    _currentServiceProduct = value;
                    OnPropertyChanged(nameof(CurrentServiceProduct));
                }
            }
        }
        public HashSet<ServiceProductModel> AllServiceProduct
        {
            get => _allServiceProduct;
            set
            {
                if(_allServiceProduct != value)
                {
                    _allServiceProduct = value;
                    OnPropertyChanged(nameof(AllServiceProduct));
                }
            }
        }
        public ObservableCollection<ServiceProductModel> ServiceProducts
        {
            get => _serviceProducts;
            set
            {
                if(_serviceProducts != value)
                {
                    _serviceProducts = value;
                    OnPropertyChanged(nameof(ServiceProducts));
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

        public ServiceProductViewModel()
        {

        }













        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)=>PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
