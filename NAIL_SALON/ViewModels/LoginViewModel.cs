using NAIL_SALON.Models.Components;
using NAIL_SALON.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NAIL_SALON.Models;
using NAIL_SALON.Models.Helpers;

namespace NAIL_SALON.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        private string _enteredPhone;
        private string _enteredPassword;
        private AdminModel _currentAdmin;
        private EmployerModel _currentEmployer;
        private SessionModel _currentUser;
        private bool _isLoginSuccess;

        public SessionModel CurrentUser
        {
            get => _currentUser;
            set
            {
                if(_currentUser != value )
                {
                    _currentUser = value;
                    OnPropertyChanged(nameof(CurrentUser));
                }
            }
        }
        public string EnteredPhone
        {
            get => _enteredPhone;
            set
            {
                if(_enteredPhone != value)
                {
                    _enteredPhone = value;
                    OnPropertyChanged(nameof(EnteredPhone));
                }
            }
        }
        public string EnteredPassword
        {
            get => _enteredPassword;
            set
            {
                if(_enteredPassword != value)
                {
                    _enteredPassword = value;
                    OnPropertyChanged(nameof(EnteredPassword));
                }
            }
        }
        public bool IsLoginSuccess
        {
            get => _isLoginSuccess;
            set
            {
                if(_isLoginSuccess != value)
                {
                    _isLoginSuccess = value;
                    OnPropertyChanged(nameof(IsLoginSuccess));
                }
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(_ => Login());
            CurrentUser = new SessionModel();
        }

        public void Login()
        {
            if(string.IsNullOrEmpty(EnteredPassword) || string.IsNullOrEmpty(EnteredPhone))
            {
                MessageBox.Show("Phone or password must not be empty!");
            }
            else
            {
                var infoAdmin = AdminRepository.Instance.FindByPhone(EnteredPhone);
                var infoEmployer = EmployerRepository.Instance.FindByPhone(EnteredPhone);

                if(infoAdmin != null && PasswordHelper.VerifyPasswrod(EnteredPassword, infoAdmin.Password, infoAdmin.Salt))
                {
                    CurrentUser.ID = infoAdmin.ID;
                    CurrentUser.Password = infoAdmin.Password;
                    CurrentUser.Salt = infoAdmin.Salt;
                    CurrentUser.Name = infoAdmin.Name;
                    CurrentUser.Phone = infoAdmin.Phone;
                    CurrentUser.Active = infoAdmin.Active;
                    CurrentUser.IsAdmin = true;
                    MainWindow mainWindow = new MainWindow(CurrentUser);
                    mainWindow.Show();
                    IsLoginSuccess = true;
                }
                else if( infoEmployer!= null && PasswordHelper.VerifyPasswrod(EnteredPassword, infoEmployer.Password, infoEmployer.Salt))
                {
                    CurrentUser.ID= infoEmployer.ID;
                    CurrentUser.Password = infoEmployer.Password;
                    CurrentUser.Name = infoEmployer.Name;
                    CurrentUser.Phone = infoEmployer.Phone;
                    CurrentUser.Email = infoEmployer.Email;
                    CurrentUser.Active = infoEmployer.Active;
                    CurrentUser.Salt= infoEmployer.Salt;
                    CurrentUser.IsAdmin = false;
                    MainWindow mainWindow = new MainWindow(CurrentUser);
                    mainWindow.Show();
                    IsLoginSuccess = true;
                }
                else
                {
                    MessageBox.Show("Phone or Password inCorrect, please try again!");
                }           
            }
        }
        


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));    
    }
}
