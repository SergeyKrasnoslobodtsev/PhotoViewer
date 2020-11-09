using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PhotoViewer.App.Controls.SmoothPanel;
using PhotoViewer.App.Model;
using PhotoViewer.App.Services;
using PhotoViewer.App.Utils;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Navigation;

namespace PhotoViewer.App.ViewModel
{
    public class PhotoViewModel : ViewModelBase
    {
        public ObservableCollection<PreferenceGroup> PreferenceGroups { get; private set; }
        public ObservableCollection<string> Items => new ObservableCollection<string>(_dataService.GetPhoto().Select(p => p.Path));
        private object _selected;
        public object SelectedItem { get { return _selected; } set { Set(ref _selected, value); } }

        protected readonly IDataService _dataService;

        public RelayCommand<object> SelectedCommand => new RelayCommand<object>(Selected);

        public PhotoViewModel(IDataService dataService)
        {
            _dataService = dataService;
            PreferenceGroups = new ObservableCollection<PreferenceGroup>();

            foreach (var items in _dataService.GetPhoto().GroupBy(p => p.CreationTime))
            {
                var item = new PreferenceGroup() { Name = items.Key, SelectionMode = 1 };
                foreach (var i in items)
                    item.Preferences.Add(i);
                PreferenceGroups.Add(item);
            }
        }

        public void Selected(object item)
        {
            var photo = item as Photo;
            Trace.WriteLine(SelectedItem); 
            Trace.WriteLine(photo.Path);
        }
    }

    public class PreferenceGroup : IHeightMeasurer
    {
        private double _estimatedHeight = -1;

        private double _estimatedWidth;

        public string Name { get; set; }
        public int SelectionMode { get; set; }

        public ObservableCollection<Photo> Preferences { get; private set; }

        public PreferenceGroup()
        {
            Preferences = new ObservableCollection<Photo>();
        }
        public double GetEstimatedHeight(double availableWidth)
        {
            // Do not recalc height if text and width are unchanged
            if (_estimatedHeight < 0 || _estimatedWidth != availableWidth)
            {
                _estimatedWidth = availableWidth;
                _estimatedHeight = ImageMeasurer.GetEstimatedHeight(Preferences.Select(p => p.Path), availableWidth) + 20; // Add margin
            }
            return _estimatedHeight;
        }
    }

}
