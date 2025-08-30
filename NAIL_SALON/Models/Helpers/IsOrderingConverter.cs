using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NAIL_SALON.Models.Helpers
{
    internal class IsOrderingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool boolValue)
            {
                return boolValue == true ? "Ordering" : "Waiting";
            }
            return "Waiting";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string stringValue)
            {
                return stringValue.Equals("Ordering") ? true : false;
            }
            return false;
        }
    }
}
