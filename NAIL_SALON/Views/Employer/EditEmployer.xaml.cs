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

namespace NAIL_SALON.Views.Employer
{
    /// <summary>
    /// Interaction logic for EditEmployer.xaml
    /// </summary>
    public partial class EditEmployer : Window
    {
        private EmployerModel _employer;
        public EditEmployer(EmployerModel employer)
        {
            InitializeComponent();
            _employer = employer;
            DataContext = new EmployerViewModel
            {
                CurrentEmployer = employer,
            };
            this.Loaded += EditEmployer_Loaded;
        }
        private void EditEmployer_Loaded(object sender, RoutedEventArgs e)
        {
            if(this.DataContext is EmployerViewModel vm)
            {
                vm.PropertyChanged += Vm_PropertyChanged;
            }
        }
        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EmployerViewModel.IsCreateSuccess))
            {
                if (sender is EmployerViewModel vm  && vm.IsCreateSuccess == true)
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
