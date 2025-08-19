using NAIL_SALON.Models;
using NAIL_SALON.Models.Components.Employer;
using NAIL_SALON.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for CreateEmployer.xaml
    /// </summary>
    public partial class CreateEmployer : Window
    {
        private EmployerViewModel _vm;
        public CreateEmployer(EmployerViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            this.DataContext = _vm;
            this.Loaded += CreateEmployer_Loaded;            
        }

        private void CreateEmployer_Loaded(object sender, RoutedEventArgs e)
        {
            _vm.PropertyChanged += Vm_PropertyChanged;
        }


        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(EmployerViewModel.IsCreateSuccess))
            {
                if(_vm.IsCreateSuccess == true)
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
