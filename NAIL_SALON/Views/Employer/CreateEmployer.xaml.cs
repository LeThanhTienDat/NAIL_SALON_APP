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

namespace NAIL_SALON.Views.Employer
{
    /// <summary>
    /// Interaction logic for CreateEmployer.xaml
    /// </summary>
    public partial class CreateEmployer : Window
    {
        public CreateEmployer()
        {
            InitializeComponent();
        }
        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
