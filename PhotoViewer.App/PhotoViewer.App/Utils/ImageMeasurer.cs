using System.Collections.Generic;

namespace PhotoViewer.App.Utils
{
    internal class ImageMeasurer
    {
        private static double _lineHeight = -1;

        private static double _charWidth = -1;

        public static double GetEstimatedHeight(IEnumerable<string> source, double width) {
            double height = 0;
            double widthItems = 0;
            foreach (var img in source) {
                CheckInitialization(img);
                var v = widthItems + _charWidth;
                widthItems = v + 20;
            }
            height = widthItems / width;
            return height * 400;
        }

        private static void CheckInitialization(string img) {
            if (_lineHeight >= 0) {
                return;
            }

            var control = new System.Windows.Controls.Image();
            control.Source = Helpers.ImageHelpers.CreateBitmap(img);

            control.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));

            _lineHeight = 400; //control.DesiredSize.Height;
            _charWidth = 400;// control.DesiredSize.Width;
        }
    }
}
