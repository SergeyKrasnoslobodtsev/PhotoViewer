using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PhotoViewer.App.Helpers
{
    internal static class ImageHelpers
    {
        public static BitmapImage CreateBitmap(string path)
        {
            var bi = new BitmapImage();

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                bi.BeginInit();
                bi.DecodePixelHeight = 300;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
                bi.StreamSource = stream;
                bi.EndInit();
                bi.Freeze();
            }
            return bi;
        }


        public static BitmapImage ScaleImage(BitmapImage original, double scale)
        {
            var scaledBitmapSource = new TransformedBitmap();
            scaledBitmapSource.BeginInit();
            scaledBitmapSource.Source = original;
            scaledBitmapSource.Transform = new ScaleTransform(scale, scale);
            scaledBitmapSource.EndInit();
            return BitmapSourceToBitmap(scaledBitmapSource);
        }



        public static BitmapImage CropImage(BitmapImage original, int width, int height)
        {
            var deltaWidth = original.PixelWidth - width;
            var deltaHeight = original.PixelHeight - height;
            var marginX = deltaWidth / 2;
            var marginY = deltaHeight / 2;
            var rectangle = new Int32Rect(marginX, marginY, width, height);
            var croppedBitmap = new CroppedBitmap(original, rectangle);
            return BitmapSourceToBitmap(croppedBitmap);
        }



        public static BitmapImage BitmapSourceToBitmap(BitmapSource source)
        {
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
