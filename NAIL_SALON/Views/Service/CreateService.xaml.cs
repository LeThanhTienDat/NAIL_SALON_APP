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

namespace NAIL_SALON.Views.Service
{
    /// <summary>
    /// Interaction logic for CreateService.xaml
    /// </summary>
    public partial class CreateService : Window
    {
        private ServiceViewModel _vm;
        public CreateService(ServiceViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            this.DataContext = _vm;
        }


        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
}
