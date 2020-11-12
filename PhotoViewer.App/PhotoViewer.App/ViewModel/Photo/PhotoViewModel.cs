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
        public ObservableCollection<PreferenceGroup> PreferenceGroups { get; private set; }

        public ObservableCollection<SearchText> searchTexts { get; private set; }

        public ObservableCollection<SearchLoacation> searchLoacations { get; private set; }

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

            searchTexts = new ObservableCollection<SearchText> { 
                new SearchText { text = "День рождения" }, 
                new SearchText { text = "Документ" }, 
                new SearchText { text = "Селфи" }, 
                new SearchText { text = "Скриншоты" }
            };
            searchLoacations = new ObservableCollection<SearchLoacation> {
                new SearchLoacation { text = "Москва" },
                new SearchLoacation { text = "Казань" },
                new SearchLoacation { text = "усово-тупик" },
                new SearchLoacation { text = "Санкт-Петербург" }
            };
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


    public class SearchText
    {
        public string text { get; set; }
    }

    public class SearchFace
    {
        public string image { get; set; }
    }
    public class SearchLoacation
    {
        public string text { get; set; }
    }
}
