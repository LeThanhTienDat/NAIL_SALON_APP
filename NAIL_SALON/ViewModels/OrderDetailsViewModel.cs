using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIL_SALON.Models;
using NAIL_SALON.Models.Repositories;

namespace NAIL_SALON.ViewModels
{
    internal class OrderDetailsViewModel : INotifyPropertyChanged
    {
        private int _rowNumber;
        public int _currentPage =1;
        public int PageSize = 5;
        public string ShowCurrentPage = "Page 1";
        private int _id;
        private int _serviceId;
        private int _orderId;
        private ObservableCollection<OrderDetailsModel> _orderDetails;
        private ObservableCollection<OrderDetailsViewModel> _tempOrderDetails;
        

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
        public int OrderId
        {
            get => _orderId;
            set
            {
                if(_orderId != value)
                {
                    _orderId = value;
                    OnPropertyChanged(nameof(OrderId));
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
        public ObservableCollection<OrderDetailsModel> OrderDetails
        {
            get => _orderDetails;
            set
            {
                if(_orderDetails != value)
                {
                    _orderDetails = value;
                    OnPropertyChanged(nameof(OrderDetails));
                }
            }
        }
        public OrderDetailsViewModel()
        {
            
        }
        




        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
