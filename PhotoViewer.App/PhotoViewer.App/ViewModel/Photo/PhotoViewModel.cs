using Data.Model;
using GalaSoft.MvvmLight;
using PhotoViewer.App.Helpers;
using PhotoViewer.App.Services;
using PhotoViewer.App.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace PhotoViewer.App.ViewModel
{
    public class PhotoViewModel : ViewModelBase
    {
        private bool allSelected = false;

        public bool AllSelected
        {
            get => this.allSelected;
            set
            {
                this.Set(() => AllSelected, ref allSelected, value);
            }
        }
        public ObservableCollection<GroupsThumbnaiViewModel> PreferenceGroups { get; private set; } = new ObservableCollection<GroupsThumbnaiViewModel>();
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

            AsyncLoad();
        }
        protected async void AsyncLoad() {

            foreach (var items in await _dataService.GetGroups())
            {
                var item = new GroupsThumbnaiViewModel() { Name = items.Key, IsSelectedGroup = false };
                foreach (var i in items)
                    item.Preferences.Add(new Items() {
                        file = i.Path, 
                        IsSelectedItem = false, 
                        IsVideoFile = new FileInfo(i.Path).Extension == ".mp4" || new FileInfo(i.Path).Extension == ".avi" });
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
