using PhotoViewer.App.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PhotoViewer.App.Converters
{
    public class ItemCollectionToBooleanConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable<Items> items = values[0] as IEnumerable<Items>;

            if (items.Any())
            {

                int selectedCountries = items.Where(c => c.IsSelectedItem).Count();

                //if (selectedCountries.Equals(countriesOnTheCurrentContinent.Count()))
                //    return true;

                if (selectedCountries > 0)
                    return null;
            }

            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
