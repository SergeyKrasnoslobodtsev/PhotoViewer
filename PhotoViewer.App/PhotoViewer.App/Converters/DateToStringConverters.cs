using System;
using System.Globalization;
using System.Windows.Data;

namespace PhotoViewer.App.Converters
{
    public class DateToStringConverters : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str_date = value as string;
            var date = System.Convert.ToDateTime(str_date);
            return $" {date.ToString("ddd", new CultureInfo("ru-RU"))}, {date.ToString("MMM", new CultureInfo("ru-RU"))}. {date.Year} г.";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
