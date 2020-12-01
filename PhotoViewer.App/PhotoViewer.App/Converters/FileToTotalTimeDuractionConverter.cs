using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace PhotoViewer.App.Converters
{
    public class FileToTotalTimeDuractionConverter : IValueConverter
    {
        private static TimeSpan GetVideoDuration(string filePath) {
            using (var shell = ShellObject.FromParsingName(filePath)) {
                var prop = shell.Properties.System.Media.Duration;
                var t = (ulong)prop.ValueAsObject;
                return TimeSpan.FromTicks((long)t);
            }
        }
        string FormatTimeSpan(TimeSpan timeSpan) {
            if (timeSpan.Hours > 0)
                return string.Format("{0}:{1}:{2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            else
                return string.Format("{0}:{1}", timeSpan.Minutes, timeSpan.Seconds);
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return new FileInfo(value as string).Extension == ".mp4" || new FileInfo(value as string).Extension == ".avi" ? FormatTimeSpan(GetVideoDuration(value as string)) : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
