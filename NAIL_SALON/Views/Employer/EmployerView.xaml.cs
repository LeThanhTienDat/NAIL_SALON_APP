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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NAIL_SALON.Views.Employer
{
    /// <summary>
    /// Interaction logic for EmployerView.xaml
    /// </summary>
    public partial class EmployerView : UserControl
    {
        public EmployerView()
        {
            InitializeComponent();
            DataContext = new EmployerViewModel();
        }
        public void OpenCreateEmployer(object sender, RoutedEventArgs e)
        {
            var vm = (EmployerViewModel)this.DataContext;
            vm.IsCreateSuccess = false;
            var showDialog = new CreateEmployer(vm)
            {
                Owner = Window.GetWindow(this)
            };
            showDialog.ShowDialog();
        }

        //private void dgEmployers_LoadingRow(object sender, DataGridRowEventArgs e)
        //{
        //    var item = e.Row.Item as EmployerModel;
        //    if (item != null)
        //    {
        //        item.RowNumber = e.Row.GetIndex() + 1;
        //    }
        //}


    }
}
