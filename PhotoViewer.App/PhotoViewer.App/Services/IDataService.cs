using PhotoViewer.App.Helpers;
using PhotoViewer.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoViewer.App.Services
{
    public interface IDataService
    {
        Task<IEnumerable<FileData>> GetAll();
        IEnumerable<FileData> GetPages(int skip, int take);
    }
}
