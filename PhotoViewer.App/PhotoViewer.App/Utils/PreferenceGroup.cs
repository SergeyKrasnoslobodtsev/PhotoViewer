using GalaSoft.MvvmLight;
using PhotoViewer.App.Controls.SmoothPanel;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace PhotoViewer.App.Utils
{
    public class PreferenceGroup : ViewModelBase, IHeightMeasurer
    {
        private double _estimatedHeight = -1;

        private double _estimatedWidth;


        public string Name { get; set; }

        public bool IsSelectedGroup { get; set; }

        public ObservableCollection<Items> Preferences { get; set; }


        public PreferenceGroup() {
            Preferences = new ObservableCollection<Items>();
            Preferences.CollectionChanged += Preferences_CollectionChanged;
        }

        private void Preferences_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            var action = e.Action;
                Trace.WriteLine(action);
        }

        public double GetEstimatedHeight(double availableWidth) {
            if (_estimatedHeight < 0 || _estimatedWidth != availableWidth) {
                _estimatedWidth = availableWidth;
                _estimatedHeight = 400; //ImageMeasurer.GetEstimatedHeight(Preferences.Select(p => p.file), availableWidth) + 20;
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
