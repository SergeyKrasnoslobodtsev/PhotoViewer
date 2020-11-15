using PhotoViewer.App.Utils;
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
    public class ItemCollectionToBooleanConverter : IValueConverter
    {
        private IEnumerable<Items> items;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            items = value as IEnumerable<Items>;
            if(items != null)
                return items.Select(p => p.IsSelectedItem).Contains(true);
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {

                foreach(var item in items) {
                if ((bool)value)
                    item.IsSelectedItem = true;
                else
                    item.IsSelectedItem = false;
            }
            return items;
        }
    }
}
