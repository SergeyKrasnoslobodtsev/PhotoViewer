using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PhotoViewer.App.Converters
{
    public class UriToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                var path = value as string;
                var bitmap = new BitmapImage();
                if (value != null) {
                   bitmap = CreateBitmap(path);
                    
                    // Crop image (cut the side which is too long)
                    //var expectedHeightAtCurrentWidth = width * 4.0 / 3.0;
                    //var expectedWidthAtCurrentHeight = height * 3.0 / 4.0;
                    //var newWidth = Math.Min(expectedWidthAtCurrentHeight, width);
                    //var newHeight = Math.Min(expectedHeightAtCurrentWidth, height);
                    //var croppedImage = CropImage(bitmap, (int)newWidth, (int)newHeight);

                }
                return bitmap;               
                
            }catch {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }

        private static BitmapImage CreateBitmap(string path) {
            var bi = new BitmapImage();
            
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                bi.BeginInit();
                bi.DecodePixelHeight = 400;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.StreamSource = stream;
                bi.EndInit();
                bi.Freeze();
            }
            return bi;
        }


        private BitmapImage ScaleImage(BitmapImage original, double scale) {
            var scaledBitmapSource = new TransformedBitmap();
            scaledBitmapSource.BeginInit();
            scaledBitmapSource.Source = original;
            scaledBitmapSource.Transform = new ScaleTransform(scale, scale);
            scaledBitmapSource.EndInit();
            return BitmapSourceToBitmap(scaledBitmapSource);
        }



        private BitmapImage CropImage(BitmapImage original, int width, int height) {
            var deltaWidth = original.PixelWidth - width;
            var deltaHeight = original.PixelHeight - height;
            var marginX = deltaWidth / 2;
            var marginY = deltaHeight / 2;
            var rectangle = new Int32Rect(marginX, marginY, width, height);
            var croppedBitmap = new CroppedBitmap(original, rectangle);
            return BitmapSourceToBitmap(croppedBitmap);
        }



        private BitmapImage BitmapSourceToBitmap(BitmapSource source) {
            var encoder = new PngBitmapEncoder();
            var memoryStream = new MemoryStream();
            var image = new BitmapImage();

            encoder.Frames.Add(BitmapFrame.Create(source));
            encoder.Save(memoryStream);



            image.BeginInit();
            image.StreamSource = new MemoryStream(memoryStream.ToArray());
            image.EndInit();
            memoryStream.Close();
            return image;
        }
    }
}
