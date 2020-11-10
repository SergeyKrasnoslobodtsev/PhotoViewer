﻿using GalaSoft.MvvmLight;
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




        private Items _selected;
        public Items SelectedItem {
            get { return _selected; }
            set {
                Set(ref _selected, value);
                RaisePropertyChanged(nameof(SelectedItem));

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

        public void Selected(object item)
        {
            var photo = item as Photo;
            Trace.WriteLine(SelectedItem); 
            Trace.WriteLine(photo.Path);
        }
    }

    public class PreferenceGroup : ViewModelBase, IHeightMeasurer
    {
        private double _estimatedHeight = -1;

        private double _estimatedWidth;


        public string Name { get; set; }
        public bool IsSelectedGroup { get; set; }

        public ObservableCollection<Items> Preferences { get; private set; }

        

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
