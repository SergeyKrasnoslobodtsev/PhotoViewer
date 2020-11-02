using PhotoViewer.App.Helpers;
using PhotoViewer.App.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoViewer.App.Services
{
    public interface IDataService
    {
        Task<IEnumerable<FileData>> GetFileInfo();
        IEnumerable<Photo> GetPhoto();
    }
}
