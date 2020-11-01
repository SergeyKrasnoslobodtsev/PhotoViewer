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
        public Task<IEnumerable<FileData>> GetAll() {
            return Task.Run(()=> FastDirectoryEnumerator.EnumerateFiles(@"E:\", "*.jpg", SearchOption.AllDirectories));
        }

        public IEnumerable<FileData> GetPages(int skip, int take) {
            return FastDirectoryEnumerator.EnumerateFiles(@"E:\", "*.jpg", SearchOption.AllDirectories).Skip(skip).Take(take);
        }
    }
}
