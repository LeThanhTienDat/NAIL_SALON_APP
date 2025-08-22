using NAIL_SALON.Models;
using NAIL_SALON.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Diagnostics;
using NAIL_SALON.Models.Components;
using System.Security.RightsManagement;
using NAIL_SALON.Models.Helpers;
using MaterialDesignExtensions.Model;

namespace NAIL_SALON.ViewModels
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private HashSet<CustomerModel> _allCustomers;
        private ObservableCollection<CustomerModel> _customers;
        private CustomerModel _currentCustomer;
        private bool _isCreateSuccess;
        private int _currentPage = 1;
        public int PageSize = 10;
        private string _showCurrentPage = "Page 1";
        private System.Timers.Timer _filterTimer;
        private string _filterName;
        private string _filterPhone;
        private string _filterAddress;
        private string _filterDistrict;
        private string _filterCity;
        private string _filterBirthDay;
        private HashSet<DistrictModel> _allDistricts;
        private HashSet<CityModel> _allCities;
        private CityModel _selectedCity;
        private ObservableCollection<DistrictModel> _districts;

        public int CurrentPage
        {
            get => _currentPage;
            set { if (_currentPage != value) { _currentPage = value; ShowCurrentPage = "Page " + value;  OnPropertyChanged(nameof(CurrentPage)); } }
        }
        public string ShowCurrentPage
        {
            get => _showCurrentPage;
            set { if(_showCurrentPage != value) { _showCurrentPage = value;OnPropertyChanged(nameof(ShowCurrentPage)); } }
        }
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
        public string FilterPhone
        {
            get => _filterPhone;
            set
            {
                if(_filterPhone != value)
                {
                    _filterPhone = value;
                    OnPropertyChanged(nameof(FilterPhone));
                    if (string.IsNullOrEmpty(FilterPhone))
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
        public string FilterAddress
        {
            get => _filterAddress;
            set
            {
                if(_filterAddress != value)
                {
                    _filterAddress = value;
                    OnPropertyChanged(nameof(FilterAddress));
                    if(string.IsNullOrEmpty(FilterAddress))
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
        public string FilterDistrict
        {
            get => _filterDistrict;
            set
            {
                if(_filterDistrict != value)
                {
                    _filterDistrict = value;
                    OnPropertyChanged(nameof(FilterDistrict));
                    if (string.IsNullOrEmpty(FilterDistrict))
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
        public string FilterCity
        {
            get=>_filterCity;
            set
            {
                if(value != _filterCity)
                {
                    _filterCity = value;
                    OnPropertyChanged(nameof(FilterCity));
                    if (string.IsNullOrEmpty(_filterCity))
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
        public string FilterBirthDay
        {
            get => _filterBirthDay;
            set
            {
                if(_filterBirthDay != value)
                {
                    _filterBirthDay = value;
                    OnPropertyChanged(nameof(FilterBirthDay));
                    if (string.IsNullOrEmpty(_filterBirthDay))
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
        public HashSet<DistrictModel> AllDistricts
        {
            get=> _allDistricts;
            set
            {
                if(_allDistricts != value)
                {
                    _allDistricts = value;
                    OnPropertyChanged(nameof(AllDistricts));
                }
            }
        }
        public HashSet<CityModel> AllCities
        {
            get => _allCities;
            set
            {
                if(_allCities != value)
                {
                    _allCities = value;
                    OnPropertyChanged(nameof(AllCities));
                }
            }
        }
        public CityModel SelectedCity
        {
            get => _selectedCity;
            set
            {
                if(_selectedCity != value)
                {
                    _selectedCity = value;
                    OnPropertyChanged(nameof(SelectedCity));

                    if (_selectedCity != null)
                    CurrentCustomer.CityId = _selectedCity.ID;

                    LoadDistrictsForCity();
                }
            }
        }
        public ObservableCollection<DistrictModel> Districts
        {
            get => _districts;
            set
            {
                if(_districts != value)
                {
                    _districts = value;
                    OnPropertyChanged(nameof(Districts));
                }
            }
        }
        public ObservableCollection<CustomerModel> Customers
        {
            get => _customers;
            set
            {
                if(_customers != value)
                {
                    _customers = value;
                    OnPropertyChanged(nameof(Customers));
                }
            }
        }
        public CustomerModel CurrentCustomer
        {
            get => _currentCustomer;
            set
            {
                if( _currentCustomer != value)
                {
                    _currentCustomer = value;
                    OnPropertyChanged(nameof(CurrentCustomer));
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

        public ICommand NextPageCommand { get; }
        public ICommand PrevPageCommand { get; }
        public ICommand CreateCustomerCommand { get; }
        public ICommand DeleteCustomerCommand { get; }  
        public ICommand OpenEditCustomerCommand { get; }
        public ICommand SaveEditCustomerCommand { get; }     
        public CustomerViewModel()
        {
            _allCustomers = CustomerRepository.Instance.GetAll();
            CurrentCustomer = new CustomerModel();
            Customers = new ObservableCollection<CustomerModel>();
            AllCities = CityRepository.Instance.GetAll();
            AllDistricts = DistrictRepository.Instance.GetAll();
            CreateCustomerCommand = new RelayCommand(_ => CreateCustomer());
            OpenEditCustomerCommand = new RelayCommand(param => OpenEditCustomer(param as CustomerModel));
            SaveEditCustomerCommand = new RelayCommand(_ => SaveEditCustomer());
            DeleteCustomerCommand = new RelayCommand(param => DeleteCustomer(param as CustomerModel));
            LoadPage(_currentPage);
            NextPageCommand = new RelayCommand(_ => { CurrentPage++; LoadPage(CurrentPage); });
            PrevPageCommand = new RelayCommand(_ => {
                if (CurrentPage > 1) { CurrentPage--; LoadPage(CurrentPage); }
            });
        } 
        public void LoadPage(int page)
        {
            if (page < 1) page = 1;
            var pageData = CustomerRepository.Instance.GetAllPaging(page, PageSize);
            Customers.Clear();
            int number = 1 + (page - 1) * PageSize;
            foreach(var item in pageData)
            {
                item.RowNumber = number++;
                Customers.Add(item);
            }
        }
        public void ApplyFilter(int page)
        {
            try
            {
                if (page < 1) page = 1;
                var filtering = _allCustomers.ToList();

                if (!string.IsNullOrEmpty(FilterName))
                {
                    filtering = filtering.Where(e => e.Name != null && StringHelper.RemoveDiacritics(e.Name).IndexOf(FilterName, StringComparison.OrdinalIgnoreCase)>=0).ToList();
                }
                if (!string.IsNullOrEmpty(FilterPhone))
                {
                    filtering = filtering.Where(e => e.Phone!= null && e.Phone.Contains(FilterPhone)).ToList();
                }
                if (!string.IsNullOrEmpty(FilterAddress))
                {
                    filtering = filtering.Where(e=>
                        (e.Address != null && StringHelper.RemoveDiacritics(e.Address).IndexOf(FilterAddress, StringComparison.OrdinalIgnoreCase)>=0) ||
                        (e.DistrictName != null && StringHelper.RemoveDiacritics(e.DistrictName).IndexOf(FilterAddress, StringComparison.OrdinalIgnoreCase)>=0) ||
                        (e.CityName != null && StringHelper.RemoveDiacritics(e.CityName).IndexOf(FilterAddress, StringComparison.OrdinalIgnoreCase)>= 0) ).ToList();    
                }
                if (!string.IsNullOrEmpty(FilterBirthDay)){
                    var removeSlashes = FilterBirthDay.Replace("/", "");
                    filtering = filtering.Where(e =>
                            (e.BirthDay.Value.Day.ToString("D2").Contains(FilterBirthDay)) ||
                            (e.BirthDay.Value.Month.ToString("D2").Contains(FilterBirthDay)) ||
                            (e.BirthDay.Value.Year.ToString().Contains(FilterBirthDay)) ||
                            (e.BirthDay.Value.ToString("ddMMyyyy").Contains(removeSlashes))
                            ).ToList();                    
                }

                var pageData = filtering
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();
                Customers.Clear();
                int number = 1 + (page -1) * PageSize;
                foreach(var item in pageData)
                {
                    item.RowNumber = number++;
                    Customers.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void CreateCustomer()
        {
            try
            {
                CustomerModel newItem = new CustomerModel();
                newItem.Name = CurrentCustomer.Name;
                newItem.Phone = CurrentCustomer.Phone;
                newItem.Address = CurrentCustomer.Address;
                newItem.DistrictId = CurrentCustomer.DistrictId;
                newItem.CityId = CurrentCustomer.CityId;
                newItem.BirthDay = CurrentCustomer.BirthDay;
                CustomerRepository.Instance.Create(newItem);
                if(newItem.ID > 0)
                {
                    IsCreateSuccess = true;
                    _allCustomers = CustomerRepository.Instance.GetAll();
                    CurrentCustomer = new CustomerModel();
                    LoadPage(_currentPage);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void OpenEditCustomer(CustomerModel customer)
        {
            IsCreateSuccess = false;
            var showDialog = new Views.Customer.EditCustomer(customer)
            {
                Owner = Application.Current.MainWindow,
            };
            showDialog.ShowDialog();

        }
        public void SaveEditCustomer()
        {
            try
            {
                CustomerModel newItem = new CustomerModel();
                newItem.ID= CurrentCustomer.ID;
                newItem.Name = CurrentCustomer.Name;
                newItem.Phone = CurrentCustomer.Phone;
                newItem.Address = CurrentCustomer.Address;
                newItem.DistrictId= CurrentCustomer.DistrictId;
                newItem.CityId = CurrentCustomer.CityId;
                newItem.BirthDay = CurrentCustomer.BirthDay;
                var checkUpdate = CustomerRepository.Instance.Update(newItem);
                if (checkUpdate)
                {
                    MessageBox.Show("Update Successful!");
                    _allCustomers = CustomerRepository.Instance.GetAll();
                    IsCreateSuccess = true;
                    LoadPage(_currentPage);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void DeleteCustomer(CustomerModel customer)
        {
            try
            {
                var confirm = MessageBox.Show("Are you sure to Delete this Customer?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (confirm == MessageBoxResult.Yes)
                {
                    var checkDel = CustomerRepository.Instance.Delete(customer);
                    if (checkDel)
                    {
                        MessageBox.Show("Delete successful!");
                        LoadPage(_currentPage);
                        _allCustomers = CustomerRepository.Instance.GetAll();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        public void LoadDistrictsForCity()
        {
            if(SelectedCity != null)
            {               
                Districts = new ObservableCollection<DistrictModel>(AllDistricts.Where(d => d.CityId == CurrentCustomer.CityId));
            }
            else
            {
                Districts = new ObservableCollection<DistrictModel>();
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
