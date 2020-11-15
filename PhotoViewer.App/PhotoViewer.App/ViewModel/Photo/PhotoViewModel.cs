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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace PhotoViewer.App.ViewModel
{
    public class PhotoViewModel : ViewModelBase
    {
        public ObservableCollection<PreferenceGroup> PreferenceGroups { get; set; }

        public ObservableCollection<SearchText> searchTexts =>
            new ObservableCollection<SearchText> {
                new SearchText { text = "День рождения" },
                new SearchText { text = "Документ" },
                new SearchText { text = "Селфи" },
                new SearchText { text = "Скриншоты" }
            };

        public ObservableCollection<SearchLoacation> searchLoacations =>
            new ObservableCollection<SearchLoacation> {
                new SearchLoacation { text = "Москва" },
                new SearchLoacation { text = "Казань" },
                new SearchLoacation { text = "усово-тупик" },
                new SearchLoacation { text = "Санкт-Петербург" }
            };

        protected readonly IDataService _dataService;

        public PhotoViewModel(IDataService dataService) {
            _dataService = dataService;
            PreferenceGroups = new ObservableCollection<PreferenceGroup>();
            AsyncLoad();
        }
        protected async void AsyncLoad() {
            foreach (var items in await _dataService.GetGroups()) {
                var item = new PreferenceGroup() { Name = items.Key, IsSelectedGroup = false };
                foreach (var i in items)
                    item.Preferences.Add(new Items() { file = i.Path, IsSelectedItem = false });
                PreferenceGroups.Add(item);
            }
        }
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
