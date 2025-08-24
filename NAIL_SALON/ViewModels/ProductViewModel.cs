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
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        private string _filterName;
        private string _filterDescription;
        private string _filterCategory;
        private string _filterPrice;
        private string _filterStock;

        public ICommand UploadImgCommand { get; }
        public ICommand CreateProductCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand ChangeActiveCommand { get; }
        public ICommand OpenEditCommand { get; }
        public ICommand SaveEditCommand { get; }
        public ICommand CloseEditCommand { get; }
        public ICommand DeleteCommand { get; }

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
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if(value != _currentPage)
                {
                    _currentPage = value;
                    ShowCurrentPage = "Page " + value;
                    OnPropertyChanged(nameof(CurrentPage));
                }
            }
        }
        public bool IsCreateSuccess { get => _isCreateSuccess; set { if (_isCreateSuccess != value) { _isCreateSuccess = value; OnPropertyChanged(nameof(IsCreateSuccess)); } } }       
        public string ShowCurrentPage { get => _showCurrentPage; set { if (_showCurrentPage != value) { _showCurrentPage = value; OnPropertyChanged(nameof(ShowCurrentPage)); } } }       
        public ObservableCollection<ProductModel> Products { get => _products; set { if (_products != value) { _products = value; OnPropertyChanged(nameof(Products)); } } }
        
        public string FilterName
        {
            get => _filterName;
            set
            {
                if(_filterName != value)
                {
                    _filterName = value;
                    OnPropertyChanged(nameof(FilterName));

                    if (string.IsNullOrEmpty(FilterName))
                    {
                        LoadPage(_currentPage);
                    }
                    else
                    {
                        DebounceFilter(_currentPage, 400);
                    }
                }
            }
        }
        public string FilterDescription
        {
            get => _filterDescription;
            set
            {
                if(_filterDescription != value)
                {
                    _filterDescription = value;
                    OnPropertyChanged(nameof(FilterDescription));
                    if (string.IsNullOrEmpty(FilterDescription))
                    {
                        LoadPage(_currentPage);
                    }
                    else
                    {
                        DebounceFilter(_currentPage, 400);
                    }
                }
            }
        }
        public string FilterPrice
        {
            get => _filterPrice;
            set
            {
                if(_filterPrice != value)
                {
                    _filterPrice = value;
                    OnPropertyChanged(nameof(FilterPrice));
                    if (string.IsNullOrEmpty(FilterPrice) || (!decimal.TryParse(FilterPrice, out decimal result)))
                    {
                        LoadPage(_currentPage);
                    }
                    else
                    {
                        DebounceFilter(_currentPage, 400);
                    }
                }
            }
        }
        public string FilterStock
        {
            get => _filterStock;
            set
            {
                if(_filterStock != value)
                {
                    _filterStock = value;
                    OnPropertyChanged(nameof(FilterStock));
                    if(string.IsNullOrEmpty(FilterStock) || (!decimal.TryParse(FilterStock, out decimal result)))
                    {
                        LoadPage(_currentPage);
                    }
                    else
                    {
                        DebounceFilter(_currentPage, 400);
                    }
                }
            }
        }
        public string FilterCategory
        {
            get => _filterCategory;
            set
            {
                if(_filterCategory != value)
                {
                    _filterCategory = value;
                    OnPropertyChanged(nameof(FilterCategory));
                    if (string.IsNullOrEmpty(FilterCategory))
                    {
                        LoadPage(_currentPage);
                    }
                    else
                    {
                        DebounceFilter(_currentPage, 400);
                    }
                }
            }
        }
        public ProductViewModel()
        {
            _allProducts = ProductRepository.Instance.GetAll();
            UploadImgCommand = new RelayCommand(_ => UploadImg());
            CreateProductCommand = new RelayCommand(_ => CreateProduct());
            NextPageCommand = new RelayCommand(_ => { CurrentPage++; LoadPage(CurrentPage); });
            PreviousPageCommand = new RelayCommand(_ => {
                if (CurrentPage > 1) { CurrentPage--; LoadPage(CurrentPage); }
            });
            DeleteCommand = new RelayCommand(param => DeleteProduct(param as ProductModel));
            ChangeActiveCommand = new RelayCommand(param => ChangeActive(param as ProductModel));
            OpenEditCommand = new RelayCommand(param => OpenEdit(param as ProductModel));
            SaveEditCommand = new RelayCommand(_ => SaveEdit());
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

        public void DeleteProduct(ProductModel product)
        {
            try
            {
                var confirm = MessageBox.Show("Are you sure to Delete this Item?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(confirm == MessageBoxResult.Yes)
                {
                    var checkDel = ProductRepository.Instance.Delete(product);
                    if (checkDel)
                    {
                        MessageBox.Show("Delete Successfull!");
                        LoadPage(_currentPage);
                        _allProducts = ProductRepository.Instance.GetAll();
                    }
                    else
                    {
                        MessageBox.Show("Fail to Delete, please try again!");
                    }
                }   
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void ChangeActive(ProductModel product)
        {
            try
            {
                product.Active = product.Active ==1 ? 0 : 1;
                ProductRepository.Instance.ChangeActive(product);
                product.OnPropetyChanged(nameof(ProductModel.Active));
                _allProducts = ProductRepository.Instance.GetAll();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void OpenEdit(ProductModel product)
        {
            IsCreateSuccess = false;
            CurrentProduct.OriginalImage = CurrentProduct.Image;
            var showDialog = new Views.Product.EditProduct(product)
            {
                Owner = Application.Current.MainWindow
            };
            showDialog.ShowDialog();
        }
        public void SaveEdit()
        {
            try
            {
                ProductModel updateItem = new ProductModel();
                updateItem.ID = CurrentProduct.ID;
                updateItem.Name = CurrentProduct.Name;
                updateItem.Description = CurrentProduct.Description;
                updateItem.Price = CurrentProduct.Price;
                updateItem.Stock = CurrentProduct.Stock;
                updateItem.Active = CurrentProduct.Active;
                updateItem.CategoryId = CurrentProduct.CategoryId;
                if(CurrentProduct.OriginalImage != CurrentProduct.Image)
                {
                    updateItem.Image = ProductImage;
                    StoreImageHelper.StoreImage(ProductImage, ImagePath);
                }
                else
                {
                    updateItem.Image = CurrentProduct.Image;
                }
                var checkUpdate = ProductRepository.Instance.Update(updateItem);
                if (checkUpdate)
                {
                    MessageBox.Show("Update successful!");
                    IsCreateSuccess = true;
                    LoadPage(_currentPage);
                    _allProducts = ProductRepository.Instance.GetAll();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void DebounceFilter(int page, int delayMs)
        {
            _filterTimer?.Stop();
            _filterTimer = new System.Timers.Timer(delayMs) { AutoReset = false };
            _filterTimer.Elapsed += (s, e) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                    ApplyFilter(page)
                );
            };
            _filterTimer.Start();
        }
        public void ApplyFilter(int page)
        {
            try
            {
                if (page < 1) page = 1;
                var filtering = _allProducts.ToList();

                if (!string.IsNullOrEmpty(FilterName))
                    filtering = filtering.Where(e => e.Name != null && StringHelper.RemoveDiacritics(e.Name).IndexOf(FilterName, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                if (!string.IsNullOrEmpty(FilterDescription))
                    filtering = filtering.Where(e => e.Description != null && StringHelper.RemoveDiacritics(e.Description).IndexOf(FilterDescription, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                if (!string.IsNullOrEmpty(FilterPrice))                            
                   filtering = filtering.Where(e => e.Price.ToString().Contains(FilterPrice)).ToList();               
                if (!string.IsNullOrEmpty(FilterStock))
                {
                    var parseStock = int.TryParse(FilterStock, out var stock);
                    if(parseStock) filtering = filtering.Where(e => e.Stock.ToString().Contains(FilterStock)).ToList();
                }
                if (!string.IsNullOrEmpty(FilterCategory))
                    filtering = filtering.Where(e => e.CategoryName != null && StringHelper.RemoveDiacritics(e.CategoryName).IndexOf(FilterCategory, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                
                var pageData = filtering
                        .Skip((page -1) * PageSize)
                        .Take(PageSize)
                        .ToList();
                Products.Clear();
                int number = 1 + (page -1) * PageSize;
                foreach(var item in pageData)
                {
                    item.RowNumber = number++;
                    Products.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));        
    }
}
