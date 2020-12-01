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
                var file = new FileInfo(path);
                if (file.Extension == ".jpg" || file.Extension == ".jpeg") {
                    var bitmap = new BitmapImage();
                    if (value != null) {
                        bitmap = Helpers.ImageHelpers.CreateBitmap(path);

                        // Crop image (cut the side which is too long)
                        //var expectedHeightAtCurrentWidth = width * 4.0 / 3.0;
                        //var expectedWidthAtCurrentHeight = height * 3.0 / 4.0;
                        //var newWidth = Math.Min(expectedWidthAtCurrentHeight, width);
                        //var newHeight = Math.Min(expectedHeightAtCurrentWidth, height);
                        //var croppedImage = CropImage(bitmap, (int)newWidth, (int)newHeight);

                    }
                    return bitmap;
                }
                return null;
            }
            catch {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
