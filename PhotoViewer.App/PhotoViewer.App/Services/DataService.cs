using PhotoViewer.App.Helpers;
using PhotoViewer.App.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoViewer.App.Services
{
    public class DataService : IDataService
    {
        public Task<IEnumerable<FileData>> GetFileInfo() {
            return Task.Run(()=> FastDirectoryEnumerator.EnumerateFiles(@"E:\", "*.jpg", SearchOption.AllDirectories));
        }
        public IEnumerable<Photo> GetPhoto()
        {
            var photo = new List<Photo>();
            foreach (var item in GetFileInfo().Result.OrderByDescending(p => p.CreationTime))
                photo.Add(new Photo() { 
                    Path = item.Path, 
                    CreationTime = item.CreationTime.ToString("dd.MM.yyyy"), 
                    Name = item.Name });
            return photo;
        }
    }
}
