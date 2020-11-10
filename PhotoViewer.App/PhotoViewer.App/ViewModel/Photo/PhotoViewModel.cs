using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PhotoViewer.App.Controls.SmoothPanel;
using PhotoViewer.App.Model;
using PhotoViewer.App.Services;
using PhotoViewer.App.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;

namespace PhotoViewer.App.ViewModel
{
    public class PhotoViewModel : ViewModelBase
    {
        private ObservableCollection<PreferenceGroup> preferenceGroups;
        public ObservableCollection<PreferenceGroup> PreferenceGroups {
            get { return preferenceGroups; }
            set {
                Set(ref preferenceGroups, value);
                RaisePropertyChanged(nameof(PreferenceGroups));

            }
        }

        private PreferenceGroup _selected;
        public PreferenceGroup Selected {
            get { return _selected; }
            set {
                Set(ref _selected, value);
                RaisePropertyChanged(nameof(Selected));

            }
        }

        protected readonly IDataService _dataService;

        public PhotoViewModel(IDataService dataService)
        {
            _dataService = dataService;
            PreferenceGroups = new ObservableCollection<PreferenceGroup>();
            foreach (var items in _dataService.GetPhoto().GroupBy(p => p.CreationTime))
            {
                var item = new PreferenceGroup() { Name = items.Key, IsSelectedGroup = false };
                foreach (var i in items)
                    item.Preferences.Add(new Items() { file = i.Path, IsSelectedItem = false});
                PreferenceGroups.Add(item);
            }
        }

        
    }

   
    public class PreferenceGroup : ViewModelBase, IHeightMeasurer
    {
        private double _estimatedHeight = -1;

        private double _estimatedWidth;


        public string Name { get; set; }

        public bool IsSelectedGroup { get; set; }
        public ObservableCollection<Items> Preferences { get; set; }


        public PreferenceGroup()
        {
            Preferences = new ObservableCollection<Items>();
            
        }
        public double GetEstimatedHeight(double availableWidth)
        {
            if (_estimatedHeight < 0 || _estimatedWidth != availableWidth)
            {
                _estimatedWidth = availableWidth;
                _estimatedHeight = ImageMeasurer.GetEstimatedHeight(Preferences.Select(p => p.file), availableWidth) + 20;
            }
            return _estimatedHeight;
        }
    }

    public class Items
    {
        public string file { get; set; }
        public bool IsSelectedItem { get; set; }

    }

}
