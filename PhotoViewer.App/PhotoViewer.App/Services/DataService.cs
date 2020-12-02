using Data.Model;
using PhotoViewer.App.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoViewer.App.Services
{
    public class DataService : IDataService
    {
        private string[] searchPatterns = { "*.mp4", "*.avi", "*.jpg", "*.jpeg" };

        public IEnumerable<Photo> All()
        {
            var list = new List<Photo>();
            var items = searchPatterns.
                SelectMany(searchPattern =>
                FastDirectoryEnumerator.
                EnumerateFiles(@"C:\Users\Админ\Documents\Файлики", searchPattern, SearchOption.AllDirectories));
            foreach (var item in items)
                list.Add(new Photo { Path = item.Path, CreationTime = item.CreationTime.ToString("d") });
            return list;
        }

        public Task<IOrderedEnumerable<IGrouping<string, FileData>>> GetGroups() =>
            Task.Run(() =>
            searchPatterns.SelectMany(searchPattern =>
            FastDirectoryEnumerator.EnumerateFiles(@"C:\Users\Админ\Documents\Файлики", searchPattern, SearchOption.AllDirectories)).
            GroupBy(p => p.CreationTime.ToString("d")).
            OrderByDescending(p => Convert.ToDateTime(p.Key)));

    }
}
