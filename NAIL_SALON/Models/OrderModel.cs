using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAIL_SALON.Models
{
    public class OrderModel:INotifyPropertyChanged
    {
        private int _id;
        private int? _customerId;
        private int? _employerId;
        private decimal? _totalPrice;
        private int? _totalDiscount;
        private DateTime? _orderDate;
        private string _status;
        private int? _paymentConfirm;
        private int _rowNumber;
        private OrderModel _currentOrder;
        private CustomerModel _currentCustomer;
        private EmployerModel _currentEmployer;

        public HashSet<OrderDetailsModel> OrderDetails { get; set; }
        private ObservableCollection<OrderDetailsModel> _orderDetailsModel;
        //public ObservableCollection<OrderDetailsModel> OrderDetails { get; set; }
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
        public int? PaymentConfirm
        {
            get => _paymentConfirm;
            set
            {
                if(_paymentConfirm != value)
                {
                    _paymentConfirm = value;
                    OnPropertyChanged(nameof(PaymentConfirm));
                }
            }
        }
        public CustomerModel CurrentCustomer
        {
            get => _currentCustomer;
            set
            {
                _currentCustomer = value;
                OnPropertyChanged(nameof(CurrentCustomer));
            }
        }
        public EmployerModel CurrentEmployer
        {
            get => _currentEmployer;
            set
            {
                if( _currentEmployer != value)
                {
                    _currentEmployer = value;
                    OnPropertyChanged(nameof(CurrentEmployer));
                }
            }
        }
        public string Status
        {
            get => _status;
            set
            {
                if(_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }
        public int? CustomerId
        {
            get => _customerId;
            set
            {
                if(_customerId != value )
                {
                    _customerId = value;
                    OnPropertyChanged(nameof(CustomerId));
                }
            }
        }
        public int? EmployerId
        {
            get => _employerId;
            set
            {
                if(_employerId != value )
                {
                    _employerId = value;
                    OnPropertyChanged(nameof(EmployerId));
                }
            }
        }
        public decimal? TotalPrice
        {
            get => _totalPrice;
            set
            {
                if(_totalPrice != value)
                {
                    _totalPrice = value;
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }
        public int? TotalDiscount
        {
            get => _totalDiscount;
            set
            {
                if(_totalDiscount != value)
                {
                    _totalDiscount = value;
                    OnPropertyChanged(nameof(TotalDiscount));
                }
            }
        }
        public DateTime? OrderDate
        {
            get => _orderDate;
            set
            {
                if(_orderDate != value)
                {
                    _orderDate = value;
                    OnPropertyChanged(nameof(OrderDate));
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
        public ObservableCollection<OrderDetailsModel> OrderDetailsModel
        {
            get => _orderDetailsModel;
            set
            {
                if(_orderDetailsModel != value)
                {
                    _orderDetailsModel = value;
                    OnPropertyChanged(nameof(OrderDetailsModel));
                }
            }
        }
        public OrderModel CurrentOrder
        {
            get => _currentOrder;
            set
            {
                if(_currentOrder != value)
                {
                    _currentOrder = value;
                    OnPropertyChanged(nameof(CurrentOrder));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
