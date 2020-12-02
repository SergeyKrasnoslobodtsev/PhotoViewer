using Data.Model;
using PhotoViewer.App.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoViewer.App.Services
{
    public interface IDataService
    {
        Task<IOrderedEnumerable<IGrouping<string, FileData>>> GetGroups();
        IEnumerable<Photo> All();
    }
}
