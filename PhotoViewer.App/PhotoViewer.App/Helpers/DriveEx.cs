using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoViewer.App.Helpers
{
    public static class ConverterValueSizeToString
    {
        static readonly string[] suffixes = { "Байт", "Кб", "Мб", "Гб", "Тб", "Пб" };
        public static string FormatSize(long bytes) {
            int counter = 0;
            decimal number = (decimal)bytes;
            while (Math.Round(number / 1024) >= 1) {
                number = number / 1024;
                counter++;
            }
            return string.Format("{0:n1} {1}", number, suffixes[counter]);
        }
    }

    public class Drive
    {
        public string TotalSize { get; private set; }
        public string AvailableFreeSpace { get; private set; }
        public double MaxValue { get; private set; }
        public double CurrentValue { get; private set; }
        public Drive(string folder) {
            var drive = new DriveInfo(Path.GetPathRoot(new FileInfo(folder).FullName));
            MaxValue = drive.TotalSize;
            CurrentValue = drive.TotalSize - drive.AvailableFreeSpace;
            TotalSize = ConverterValueSizeToString.FormatSize(drive.TotalSize);
            AvailableFreeSpace = ConverterValueSizeToString.FormatSize(drive.TotalSize - drive.AvailableFreeSpace);
        }
    }
}
