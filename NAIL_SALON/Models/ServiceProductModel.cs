using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAIL_SALON.Models
{
    public class ServiceProductModel: INotifyPropertyChanged
    {
        private int _rowNumber;
        private int _productId;
        private int _serviceId;
        private int _quantity;
        private ProductModel _currentProductBelong;
        public int ProductId
        {
            get => _productId;
            set
            {
                if(_productId != value)
                {
                    _productId = value;
                    OnPropertyChanged(nameof(ProductId));
                }
            }
        }
        public int ServiceId
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

        public int Quantity
        {
            get => _quantity;
            set
            {
                if(_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
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
        public ProductModel CurrentProductBelong
        {
            get => _currentProductBelong;
            set
            {
                if(_currentProductBelong != value)
                {
                    _currentProductBelong = value;
                    OnPropertyChanged(nameof(CurrentProductBelong));
                }
            }
        }

        public ServiceProductModel()
        {
           
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
