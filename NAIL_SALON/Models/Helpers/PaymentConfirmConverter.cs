using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;

namespace NAIL_SALON.Models.Helpers
{
    internal class PaymentConfirmConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is int intValue)
            {
                return intValue == 1 ? "Yes" : "No";
            }
            return "No";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string stringValue)
            {
                return stringValue.Equals("Yes", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
            }
            return 0;
        }
    }
}
