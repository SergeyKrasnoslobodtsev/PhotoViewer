using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PhotoViewer.App.Services;
using PhotoViewer.App.Services.Navigation;
using System;

namespace PhotoViewer.App.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public string TotalSizeValueDisk { get; set; }
        public string AvailableSizeDisk { get; set; }
        public double CurrentValue { get; set; }
        public double MaxValue { get; set; }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            Helpers.Drive drive = new Helpers.Drive(@"E:\");
            TotalSizeValueDisk = drive.TotalSize;
            AvailableSizeDisk = drive.AvailableFreeSpace;
            CurrentValue = drive.CurrentValue;
            MaxValue = drive.MaxValue;
            //if (IsInDesignMode)
            //{
            //    // Code runs in Blend --> create design time data.
            //}
            //else
            //{
            //    // Code runs "for real"
            //}
        }

        
    }
}