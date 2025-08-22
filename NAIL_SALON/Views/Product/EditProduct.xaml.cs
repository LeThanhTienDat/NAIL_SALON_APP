using NAIL_SALON.Models;
using NAIL_SALON.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NAIL_SALON.Views.Product
{
    /// <summary>
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window
    {
        private ProductModel _product;
        public EditProduct(ProductModel product)
        {
            InitializeComponent();
            _product = product;
            DataContext = new ProductViewModel
            {
                CurrentProduct = product,
            };
            this.Loaded += Vm_EditProductLoaded;
        }

        public void Vm_EditProductLoaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is ProductViewModel vm)
            {
                vm.PropertyChanged += Vm_PropertyChanged;
            }
        }

        public void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ProductViewModel.IsCreateSuccess))
            {
                if(sender is ProductViewModel vm && vm.IsCreateSuccess == true)
                {
                    this.Close();
                }
            }
        }
        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
