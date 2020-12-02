using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PhotoViewer.App.Controls.SmoothPanel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace PhotoViewer.App.ViewModel
{
    public class GroupsThumbnaiViewModel : ViewModelBase, IHeightMeasurer
    {
        private double _estimatedHeight = -1;

        private double _estimatedWidth;

        public string Name { get; set; }
        private bool _IsSelectedGroup;
        public bool IsSelectedGroup
        {
            get => _IsSelectedGroup;
            set
            {
                Set(ref _IsSelectedGroup, _IsSelectedGroup = value);
                foreach (var item in Preferences)
                        item.IsSelectedItem = IsSelectedGroup;

                RaisePropertyChanged(nameof(Preferences));
            }
        }
        

        public ObservableCollection<Items> Preferences { get; private set; } = new ObservableCollection<Items>();

        public RelayCommand<bool> SelectedItemCommand => new RelayCommand<bool>((val)=> {
            IsSelectedGroup = Preferences.All(p => p.IsSelectedItem);
        });



        public double GetEstimatedHeight(double availableWidth) {
            //if (_estimatedHeight < 0 || _estimatedWidth != availableWidth) {
            //    _estimatedWidth = availableWidth;
            //    _estimatedHeight = 400; //ImageMeasurer.GetEstimatedHeight(Preferences.Select(p => p.file), availableWidth) + 20;
            //}
            return 400;
        }
    }

    public class Items : ViewModelBase
    {
        public string file { get; set; }
        public bool IsSelectedItem { get; set; }
        public bool IsVideoFile { get; set; }
    }
}
