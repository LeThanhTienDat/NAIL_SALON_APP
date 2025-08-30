using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAIL_SALON.Models
{
    public class SessionModel : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _phone;
        private string _email;
        private string _password;
        private string _salt;
        private bool _isAdmin;
        private int _active;

        public int ID
        {
            get => _id;
            set
            {
                if(_id != value)
                {
                    _id  = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                if(_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public string Phone
        {
            get => _phone;
            set
            {
                if(_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged(nameof(Phone));
                }
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                if(_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                if(_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                if(_isAdmin != value)
                {
                    _isAdmin = value;
                    OnPropertyChanged(nameof(IsAdmin));
                }
            }
        }
        public int Active
        {
            get => _active;
            set
            {
                if(_active != value)
                {
                    _active = value;
                    OnPropertyChanged(nameof(Active));
                }
            }
        }
        public string Salt
        {
            get => _salt;
            set
            {
                if(_salt != value)
                {
                    _salt = value;
                    OnPropertyChanged(nameof(Salt));
                }
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)=>PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));  
    }
}
