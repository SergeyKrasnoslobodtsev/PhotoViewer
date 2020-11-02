using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PhotoViewer.App.Converters
{
    public class UriToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                var path = value as string;
                var bitmap = new BitmapImage();
                if (value != null)
                    using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                        bitmap.BeginInit();
                        bitmap.DecodePixelHeight = 400;
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = stream;
                        bitmap.EndInit();
                        
                        bitmap.Freeze();
                    }
                
                return bitmap;
            }catch {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
