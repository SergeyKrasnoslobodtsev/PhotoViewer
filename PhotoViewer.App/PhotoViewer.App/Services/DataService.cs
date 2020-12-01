using PhotoViewer.App.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoViewer.App.Services
{
    public class DataService : IDataService
    {
        private string[] searchPatterns = { "*.mp4", "*.avi", "*.jpg", "*.jpeg" };
        public Task<IOrderedEnumerable<IGrouping<string, FileData>>> GetGroups() =>
            Task.Run(() =>
            searchPatterns.SelectMany(searchPattern =>
            FastDirectoryEnumerator.EnumerateFiles(@"D:\SyncFolderImage\Disk 1\с телефона 16.09.17", searchPattern, SearchOption.AllDirectories)).
            GroupBy(p => p.CreationTime.ToString("d")).
            OrderByDescending(p => Convert.ToDateTime(p.Key)));

    }
}
