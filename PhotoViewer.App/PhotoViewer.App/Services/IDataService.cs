using PhotoViewer.App.Helpers;
using PhotoViewer.App.Model;
using PhotoViewer.App.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoViewer.App.Services
{
    public interface IDataService
    {
        Task<IOrderedEnumerable<IGrouping<string, FileData>>> GetGroups();
    }
}
