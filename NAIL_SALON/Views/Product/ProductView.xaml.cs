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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NAIL_SALON.Views.Product
{
    /// <summary>
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : UserControl
    {

        public ProductView()
        {
            InitializeComponent();
            DataContext = new ProductViewModel();          
        }

        public void OpenCreateProduct(object sender, RoutedEventArgs e)
        {
            var vm = (ProductViewModel)this.DataContext;
            vm.IsCreateSuccess = false;
            var showDialog = new CreateProduct(vm)
            {
                Owner = Window.GetWindow(this)
            };
            showDialog.ShowDialog();

        }
    }
}
