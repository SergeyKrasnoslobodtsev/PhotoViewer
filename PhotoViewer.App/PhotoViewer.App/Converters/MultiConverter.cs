using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PhotoViewer.App.Converters
{
    class MultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool val1 = (bool)values[0];
            bool val2 = (bool)values[1];
            val2 = val1;
            return val2;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            bool val = (bool)value;
            return new object[] { val, val };
        }
    }
}
