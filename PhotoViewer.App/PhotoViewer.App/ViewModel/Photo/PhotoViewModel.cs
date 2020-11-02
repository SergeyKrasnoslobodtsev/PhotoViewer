using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using PhotoViewer.App.Model;
using PhotoViewer.App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;

namespace PhotoViewer.App.ViewModel
{
    public class PhotoViewModel : ViewModelBase
    {
        public ObservableCollection<PreferenceGroup> PreferenceGroups { get; private set; }

        protected readonly IDataService _dataService;
        public PhotoViewModel(IDataService dataService)
        {
            _dataService = dataService;
            PreferenceGroups = new ObservableCollection<PreferenceGroup>();
            foreach (var items in _dataService.GetPhoto().GroupBy(p => p.CreationTime))
            {
                DateTime date = Convert.ToDateTime(items.Key);
                var item = new PreferenceGroup() { Name = $" {date.ToString("ddd", new CultureInfo("ru-RU"))}, {date.ToString("MMM", new CultureInfo("ru-RU"))}. {date.Year} г.", SelectionMode = 1 };
                foreach (var i in items)
                    item.Preferences.Add(i);
                PreferenceGroups.Add(item);
            }
        }
    }

    public class PreferenceGroup
    {
        public string Name { get; set; }
        public int SelectionMode { get; set; }
        public ObservableCollection<Photo> Preferences { get; private set; }

        public PreferenceGroup()
        {
            Preferences = new ObservableCollection<Photo>();
        }
    }
}
