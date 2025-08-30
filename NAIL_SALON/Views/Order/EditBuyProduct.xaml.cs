using NAIL_SALON.Models;
using NAIL_SALON.ViewModels;
using System;
using System.Collections.Generic;
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
using System.ComponentModel;

namespace NAIL_SALON.Views.Order
{
    /// <summary>
    /// Interaction logic for EditBuyProduct.xaml
    /// </summary>
    public partial class EditBuyProduct : Window
    {
        private OrderViewModel _vm;        
        public EditBuyProduct(OrderViewModel vm)
        {
            InitializeComponent();
            _vm = vm;   
            this.DataContext = _vm;
            this.Loaded += EditBuyProduct_Loaded;
        }
        private void EditBuyProduct_Loaded(object sender, RoutedEventArgs e)
        {
            _vm.PropertyChanged += Vm_PropertyChanged;
        }


        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OrderViewModel.IsBuyProductSuccess))
            {
                if (_vm.IsBuyProductSuccess == true)
                {
                    this.Close();
                }
            }

        }
    }
}
