using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAIL_SALON.Models
{
    public class OrderDetailsModel:INotifyPropertyChanged
    {
        private int _id;
        private int? _serviceId;
        private int? _orderId;
        private int _rowNumber;
        private ServiceModel _currentServiceBelong;
        public int ID
        {
            get => _id;
            set
            {
                if(_id != value )
                {
                    _id = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }
        public int RowNumber
        {
            get => _rowNumber;
            set
            {
                if(_rowNumber != value)
                {
                    _rowNumber = value;
                    OnPropertyChanged(nameof(RowNumber));
                }
            }
        }
        public int? ServiceId
        {
            get => _serviceId;
            set
            {
                if(_serviceId != value)
                {
                    _serviceId = value;
                    OnPropertyChanged(nameof(ServiceId));
                }
            }
        }
        public int? OrderId
        {
            get => _orderId;
            set
            {
                if(_orderId != value )
                {
                    _orderId = value;
                    OnPropertyChanged(nameof(OrderId));
                }
            }
        }
        public ServiceModel CurrentServiceBelong
        {
            get => _currentServiceBelong;
            set
            {
                if( _currentServiceBelong != value )
                {
                    _currentServiceBelong = value;
                    OnPropertyChanged(nameof(CurrentServiceBelong));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));    


    }
}
