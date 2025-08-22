using NAIL_SALON.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAIL_SALON.Models
{
    public class CustomerModel : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _phone;
        private string _address;
        private int? _districtId;
        private int? _cityId;
        private string _districtName;
        private string _cityName;
        private string _fullAddress;
        private DateTime? _birthDay;
        private int _rowNumber;

        public int ID
        {
            get => _id;
            set
            {
                if (_id != value) { _id = value; OnPropertyChanged(nameof(ID)); }
            }
        }
        public string Name
        {
            get => _name;
            set { if (_name != value) { _name = value; OnPropertyChanged(nameof(Name)); } }
        }
        public string Phone
        {
            get => _phone;
            set { if(_phone != value) { _phone  = value; OnPropertyChanged(nameof(Phone)); } }
        }
        public string Address
        {
            get => _address;
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged(nameof(Address));
                    UpdateFullAddress();
                }
            }
        }
        public int? DistrictId
        {
            get => _districtId;
            set 
            { 
                if (_districtId != value) 
                { 
                    _districtId = value;
                    OnPropertyChanged(nameof(DistrictId));
                    DistrictName = DistrictRepository.Instance.GetAll()
                              .FirstOrDefault(d => d.ID == _districtId)?.DistrictName;

                    UpdateFullAddress(); 
                } 
            }
        }
        public int? CityId
        {
            get => _cityId;
            set 
            { 
                if (_cityId != value) 
                {   
                    _cityId = value; 
                    OnPropertyChanged(nameof(CityId));
                    CityName = CityRepository.Instance.GetAll()
                          .FirstOrDefault(c => c.ID == _cityId)?.CityName;
                    UpdateFullAddress(); 
                } 
            }
        }
        public string DistrictName
        {
            get => _districtName;
            set { if (_districtName != value) { _districtName = value; OnPropertyChanged(nameof(DistrictName)); UpdateFullAddress(); } }
        }
        public string CityName
        {
            get => _cityName;
            set { if (_cityName != value) { _cityName = value; OnPropertyChanged(nameof(CityName)); UpdateFullAddress(); } }
        }        
        public DateTime? BirthDay
        {
            get => _birthDay;
            set { if (_birthDay != value) { _birthDay = value; OnPropertyChanged(nameof(BirthDay)); } }
        }
        public int RowNumber
        {
            get => _rowNumber;
            set { if (_rowNumber != value) { _rowNumber = value; OnPropertyChanged(nameof(RowNumber)); } }
        }
        public string FullAddress
        {
            get => _fullAddress;
            set
            {
                if(_fullAddress != value)
                {
                    _fullAddress = value;
                    OnPropertyChanged(nameof(FullAddress));
                }
            }
        }

        private void UpdateFullAddress()
        {
            var parts = new List<string>();

            if (!string.IsNullOrWhiteSpace(Address))
                parts.Add(Address);

            if (!string.IsNullOrWhiteSpace(DistrictName))
                parts.Add(DistrictName);

            if (!string.IsNullOrWhiteSpace(CityName))
                parts.Add(CityName);

            FullAddress = string.Join(", ", parts);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}