using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using NAIL_SALON.Models;
using NAIL_SALON.Models.Components;
using NAIL_SALON.Models.Helpers;
using NAIL_SALON.Models.Repositories;

namespace NAIL_SALON.ViewModels
{
    public class AdminViewModel : INotifyPropertyChanged
    {
        private AdminModel _currentAdmin;
        private bool _isCreateSuccess;
        public AdminModel CurrentAdmin
        {
            get => _currentAdmin;
            set
            {
                if (_currentAdmin != value)
                {
                    _currentAdmin = value;
                    OnPropertyChanged(nameof(CurrentAdmin));    
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
        public ICommand CreateAdminCommand { get; }
       

        public AdminViewModel()
        {
            CurrentAdmin = new AdminModel();
            CreateAdminCommand = new RelayCommand(_ => CreateAdmin());
            
        }
        public void CreateAdmin()
        {
            try
            {
                var item = CurrentAdmin;
                if(string.IsNullOrEmpty(item.Name) ||
                   string.IsNullOrEmpty(item.Phone) ||
                   string.IsNullOrEmpty(item.Password) ||
                   string.IsNullOrEmpty(item.ConfirmPassword))
                {
                    MessageBox.Show("Please input all Information!");
                }
                else if(item.ConfirmPassword.Trim() != item.Password.Trim())
                {
                    MessageBox.Show("Password Confirmation doesn't match, please try again!");
                }
                else
                {
                    item.Salt = PasswordHelper.GetSalt();
                    item.Password = PasswordHelper.HashPassword(item.Password, item.Salt);
                    AdminRepository.Instance.Create(item);
                    if (item.ID > 0)
                    {
                        MessageBox.Show("Create Success!");
                        IsCreateSuccess = true;
                    }
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
