using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoViewer.App.Services.Navigation
{
    public interface INavigationServiceEx<T> : GalaSoft.MvvmLight.Views.INavigationService
    {
        /// <summary>
        /// Optional parameter
        /// </summary>
        object Parameter { get; }

        /// <summary>
        /// Instructs navigation service to display a new page corresponding to the given key,
        /// provided as an enum.
        /// </summary>
        /// <param name="navigationPage"></param>
        void NavigateTo(T navigationPage);
    }
}
