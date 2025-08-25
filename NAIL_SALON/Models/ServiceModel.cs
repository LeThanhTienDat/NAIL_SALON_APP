using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NAIL_SALON.Models
{
    public class ServiceModel: INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _description;
        private decimal _price;
        private int _active;  
        private int _discount;
        private int _rowNumber;
        private int _productAmount;

        private HashSet<ServiceProductModel> _serviceProductModel;
        private ObservableCollection<ServiceProductModel> _defaultProducts { get; set; }
        
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
        public ObservableCollection<ServiceProductModel> DefaultProducts
        {
            get => _defaultProducts;
            set
            {

                if(_defaultProducts != value)
                {
                    if (_defaultProducts != null)
                        _defaultProducts.CollectionChanged -= DefaultProducts_CollectionChanged;

                    _defaultProducts = value;

                    if (_defaultProducts != null)
                        _defaultProducts.CollectionChanged += DefaultProducts_CollectionChanged;
                   
                    OnPropertyChanged(nameof(DefaultProducts));
                    ProductAmount = _defaultProducts?.Count ?? 0;
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
        public string Description
        {
            get => _description;
            set
            {
                if(_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if(_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
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
        public int Discount
        {
            get => _discount;
            set
            {
                if(_discount != value)
                {
                    _discount = value;
                    OnPropertyChanged(nameof(Discount));
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
        public int ProductAmount
        {
            get => _productAmount;
            set
            {
                if(_productAmount != value)
                {
                    _productAmount = value;
                    OnPropertyChanged(nameof(ProductAmount));
                }
            }
        }
        public HashSet<ServiceProductModel> ServiceProductModel
        {
            get => _serviceProductModel;
            set
            {
                if(_serviceProductModel != value)
                {
                    _serviceProductModel = value;
                    OnPropertyChanged(nameof(ServiceProductModel));
                    ProductAmount = value.Count();
                }
            }
        }

        private void DefaultProducts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ProductAmount = _defaultProducts.Count;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string  propertyName)=>PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
    }
}
