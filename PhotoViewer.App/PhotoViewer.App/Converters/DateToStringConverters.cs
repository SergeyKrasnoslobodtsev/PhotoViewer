using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace PhotoViewer.App.Converters
{
    public class DateToStringConverters : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str_date = value as string;
            var date = System.Convert.ToDateTime(str_date);
            DateTime startOfWeek = DateTime.Today.AddDays(
         (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek -
         (int)DateTime.Today.DayOfWeek);

            string result = string.Join("," + Environment.NewLine, Enumerable
              .Range(0, 7)
              .Select(i => startOfWeek
                 .AddDays(i)
                 .ToString("d")).Where(p =>p == str_date));
            if(!string.IsNullOrEmpty(result))
                return $" {date.ToString("ddd", new CultureInfo("ru-RU"))}, {date.ToString("MMM", new CultureInfo("ru-RU"))}. {date.Year} г.";
            return $" {date.ToString("dd", new CultureInfo("ru-RU"))}, {date.ToString("MMM", new CultureInfo("ru-RU"))}. {date.Year} г.";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
