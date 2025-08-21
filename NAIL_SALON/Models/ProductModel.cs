using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NAIL_SALON.Models
{
    public class ProductModel: INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _description;
        private decimal _price;
        private int? _categoryId;
        private int _stock;
        private int _active;
        private string _image;
        private BitmapImage _currentImage;
        private string _categoryName;
        private int _rowNumber; 
        public int ID
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropetyChanged(nameof(ID));
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if( _name != value)
                {
                    _name = value;
                    OnPropetyChanged(nameof(Name));
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
                    OnPropetyChanged(nameof(Description));
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
                    OnPropetyChanged(nameof(Price));
                }
            }
        }

        public int? CategoryId
        {
            get => _categoryId;
            set
            {
                if(_categoryId != value)
                {
                    _categoryId = value;
                    OnPropetyChanged(nameof(CategoryId));
                }
            }
        }
        public int Stock
        {
            get => _stock;
            set
            {
                if( _stock != value)
                {
                    _stock = value;
                    OnPropetyChanged(nameof(Stock));
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
                    OnPropetyChanged(nameof(Active));
                }
            }
        }

        public string Image
        {
            get => _image;
            set
            {
                if(_image != value)
                {
                    _image = value;
                    OnPropetyChanged(nameof(Image));
                }
            }
        }

        public BitmapImage CurrentImage
        {
            get => _currentImage;
            set
            {
                if(_currentImage != value)
                {
                    _currentImage = value;
                    OnPropetyChanged(nameof(CurrentImage));
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
                    OnPropetyChanged(nameof(RowNumber));
                }
            }
            
        }

        public string CategoryName
        {
            get => _categoryName;
            set
            {
                if(_categoryName != value)
                {
                    _categoryName = value;
                    OnPropetyChanged(nameof(CategoryName));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropetyChanged(string  propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
