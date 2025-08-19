using NAIL_SALON.Models;
using NAIL_SALON.Models.Components;
using NAIL_SALON.Models.Components.Employer;
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
using System.Windows.Input;

namespace NAIL_SALON.ViewModels
{
    public class EmployerViewModel : INotifyPropertyChanged
    {
        private int _currentPage = 1;
        public string _showCurrentPage = "Page 1";
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
                if( _showCurrentPage != value)
                {
                    _showCurrentPage= value;
                    OnPropertyChanged(nameof(ShowCurrentPage)); 
                }
            }
        }
        private int _pageSize = 10;
        public ICommand NextPageCommand { get; }
        public ICommand PrevPageCommand { get; }
        public ICommand CreateEmployerCommand { get; }


        private ObservableCollection<EmployerModel> _employers;
        public ObservableCollection<EmployerModel> Employers
        {
            get => _employers;
            set
            {
                if (_employers != value)
                {
                    _employers = value;
                    OnPropertyChanged(nameof(Employers));
                }
            }
        }
        private EmployerModel _currentEmloyer;
        public EmployerModel CurrentEmployer
        {
            get => _currentEmloyer;
            set
            {
                if (_currentEmloyer != value)
                {
                    _currentEmloyer = value;
                    OnPropertyChanged(nameof(CurrentEmployer));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged; 
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        private bool _isCreateSuccess;
        public bool IsCreateSuccess
        {
            get => _isCreateSuccess;
            set
            {
                _isCreateSuccess = value;
                OnPropertyChanged(nameof(IsCreateSuccess));
            }
        }


        public HashSet<EmployerModel> items;
        public EmployerViewModel()
        {
            CurrentEmployer  = new EmployerModel();
            //items = EmployerRepository.Instance.GetAll();
            Employers = new ObservableCollection<EmployerModel>();
            //foreach(var item in items)
            //{               
            //    Employers.Add(item);
            //}
            CreateEmployerCommand = new RelayCommand(_ => CreateEmployer());
            LoadPage(_currentPage);
            NextPageCommand = new RelayCommand(_ => { CurrentPage++; LoadPage(CurrentPage); });
            PrevPageCommand = new RelayCommand(_ => {
                if (CurrentPage > 1) { CurrentPage--; LoadPage(CurrentPage); }
            });
        }
        private void LoadPage(int page)
        {
            if (page < 1) page = 1;
            var pageData = EmployerRepository.Instance.GetAllPaging(page, _pageSize);
            Employers.Clear();
            int number =1+ (page-1) * _pageSize;
            foreach(var item in pageData)
            {
                item.RowNumber = number++;
                Employers.Add(item);
            }
        }
                     
        private void CreateEmployer()
        {           
            try
            {               
                EmployerModel newEmployer = new EmployerModel();
                newEmployer.Name = CurrentEmployer.Name;
                newEmployer.Phone = CurrentEmployer.Phone;
                newEmployer.Email = CurrentEmployer.Email;
                newEmployer.Password = CurrentEmployer.Password;

                EmployerRepository.Instance.Create(newEmployer);
                if (newEmployer.ID > 0)
                {
                    //Employers.Add(newEmployer);
                    IsCreateSuccess = true;
                    CurrentEmployer = new EmployerModel();
                    LoadPage(_currentPage);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }    
    }
}
