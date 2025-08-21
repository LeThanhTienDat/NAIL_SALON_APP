using NAIL_SALON.Models;
using NAIL_SALON.Models.Components;
using NAIL_SALON.Models.Helpers;
using NAIL_SALON.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace NAIL_SALON.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        public string ImagePath = "";
        public string ProductImage = "";
        public int _currentPage = 1;
        public int PageSize =5;
        public string _showCurrentPage = "Page 1";
        private ProductModel _currentProduct;
        private HashSet<ProductModel> _allProducts;
        private HashSet<CategoryModel> _categories;
        private ObservableCollection<ProductModel> _products;
        private System.Timers.Timer _filterTimer;
        private bool _isCreateSuccess;


        public ICommand UploadImgCommand { get; }
        public ICommand CreateProductCommand { get; }

        public HashSet<CategoryModel> Categories
        {
            get => _categories ?? (_categories = new HashSet<CategoryModel>());
            set
            {
                if(_categories != value)
                {
                    _categories = value;
                    OnPropertyChanged(nameof(Categories));
                }
            }
        }
        public ProductModel CurrentProduct
        {
            get => _currentProduct;
            set
            {
                if (_currentProduct != value)
                {
                    _currentProduct = value;
                    OnPropertyChanged(nameof(CurrentProduct));
                }
            }
        }
        public bool IsCreateSuccess
        {
            get => _isCreateSuccess;
            set
            {
                if(_isCreateSuccess != value)
                {
                    _isCreateSuccess = value;
                    OnPropertyChanged(nameof(IsCreateSuccess));
                }
            }
        }
        public ObservableCollection<ProductModel> Products { get => _products; set { if (_products != value) { _products = value; OnPropertyChanged(nameof(Products)); } } }
        
        
        public ProductViewModel()
        {
            UploadImgCommand = new RelayCommand(_ => UploadImg());
            CreateProductCommand = new RelayCommand(_ => CreateProduct());
            Categories = CategoryRepository.Instance.GetAll();
            Products = new ObservableCollection<ProductModel>();
            if (CurrentProduct == null)
            {
                CurrentProduct = new ProductModel();
            }
            LoadPage(_currentPage);
        }

        public void LoadPage(int page)
        {
            if (page < 1) page = 1;
            var pageData = ProductRepository.Instance.GetAllPaging(page, PageSize);
            Products.Clear();
            int number = 1 + (page - 1) * PageSize;
            foreach (var item in pageData)
            {
                item.RowNumber = number ++;
                Products.Add(item);
            }
        }



        



        public void UploadImg()
        {
            try
            {
                var dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";

                if (dialog.ShowDialog() == true)
                {
                    
                    CurrentProduct.CurrentImage = new BitmapImage(new Uri(dialog.FileName));
                    var extension = System.IO.Path.GetExtension(dialog.FileName);
                    var name = Guid.NewGuid().ToString();
                    ImagePath = dialog.FileName;
                    ProductImage = name + "-product" + extension;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void CreateProduct()
        {
            StoreImageHelper.StoreImage(ProductImage, ImagePath);
            ProductModel newItem = new ProductModel();
            newItem.Name = CurrentProduct.Name;
            newItem.Description = CurrentProduct.Description;
            newItem.Price = CurrentProduct.Price;
            newItem.CategoryId = CurrentProduct.CategoryId;
            newItem.Stock = CurrentProduct.Stock;
            newItem.Active = CurrentProduct.Active;
            newItem.Image = ProductImage;
            ProductRepository.Instance.Create(newItem);
            if(newItem.ID > 0)
            {
                IsCreateSuccess = true;
                CurrentProduct = new ProductModel();
                LoadPage(_currentPage);
                _allProducts = ProductRepository.Instance.GetAll();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));        
    }
}
