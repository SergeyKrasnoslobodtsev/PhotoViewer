using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace PhotoViewer.App.Converters
{
    public class ValueToUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                var path = value as string;
                var file = new FileInfo(path);
                if (file.Extension == ".mp4" || file.Extension == ".avi") {

                    return new Uri(file.FullName).AbsoluteUri;
                }
                return null;
            }
            catch {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class ValueToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                var path = value as string;
                var file = new FileInfo(path);
                if (file.Extension == ".mp4" || file.Extension == ".avi") {

                    return true;
                }
                return false;
            }
            catch {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
