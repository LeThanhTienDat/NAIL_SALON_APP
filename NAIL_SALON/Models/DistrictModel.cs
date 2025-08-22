using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAIL_SALON.Models
{
    public class DistrictModel:INotifyPropertyChanged
    {
        private int _id;
        private string _district_name;
        private int? _city_id;
        public int ID
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }
        public string DistrictName
        {
            get => _district_name;
            set
            {
                if (_district_name != value)
                {
                    _district_name = value;
                    OnPropertyChanged(nameof(DistrictName));
                }
            }
        }
        public int? CityId
        {
            get => _city_id;
            set
            {
                if( _city_id != value)
                {
                    _city_id = value;
                    OnPropertyChanged(nameof(CityId));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
