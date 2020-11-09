using Microsoft.WindowsAPICodePack.Shell;
using PhotoViewer.App.Helpers;
using PhotoViewer.App.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PhotoViewer.App.Services
{
    public class DataService : IDataService
    {
        public Task<IEnumerable<FileData>> GetFileInfo() {
            return Task.Run(()=> FastDirectoryEnumerator.EnumerateFiles(@"C:\Users\Админ\Documents\Файлики", "*.jpg", SearchOption.AllDirectories));
        }
        public IEnumerable<Photo> GetPhoto()
        {
            var photo = new List<Photo>();
            foreach (var item in GetFileInfo().Result.OrderByDescending(p => p.CreationTime)) {
                var file = ShellFile.FromFilePath(item.Path);
                photo.Add(new Photo()
                {
                    Path = item.Path,
                    CreationTime = Convert.ToDateTime(file.Properties.System.ItemDate.Value).ToString("d")

                });
                
                
                Trace.WriteLine(Convert.ToDateTime(file.Properties.System.ItemDate.Value).ToString("d"));           
            }

            return photo;
        }
    }
}
