using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAIL_SALON.Models
{
    public class CityModel: INotifyPropertyChanged
    {
        private int _id;
        private string _city_name;
        public int ID
        {
            get => _id;
            set
            {
                if(_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(ID));  
                }
            }
        }
        public string CityName
        {
            get => _city_name;
            set
            {
                if (_city_name != value)
                {
                    _city_name = value;
                    OnPropertyChanged(nameof(CityName));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
