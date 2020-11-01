using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using PhotoViewer.App.Model;
using PhotoViewer.App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;

namespace PhotoViewer.App.ViewModel
{
    public class PhotoViewModel : ViewModelBase
    {
        private int start = 0;
        private int itemCount = 300;
        private int totalItems = 0;
        /// <summary>
        /// Gets the index of the first item in the products list.
        /// </summary>
        public int Start { get { return start + 1; } }

        /// <summary>
        /// Gets the index of the last item in the products list.
        /// </summary>
        public int End { get { return start + itemCount < totalItems ? start + itemCount : totalItems; } }

        /// <summary>
        /// The number of total items in the data store.
        /// </summary>
        public int TotalItems { get { return totalItems; } }

        protected readonly IDataService _dataService;
        public PhotoViewModel(IDataService dataService) {
            _dataService = dataService;
            DispatcherHelper.Initialize();
            RefreshData();

            fetchMoreDataCommand = new RelayCommand(
                () => {
                    if (busy)
                        return;
                    
                    Busy = true;
                    ThreadPool.QueueUserWorkItem(
                    delegate {
                        /* This is just to demonstrate a slow operation. */
                        Thread.Sleep(3000);
                        /* We invoke back to the UI thread. 
                            * Ordinarily this would be done 
                            * by the Calcium infrastructure automatically. */
                        DispatcherHelper.UIDispatcher.Invoke(new Action(() => {
                            AddMoreItems();
                            Busy = false;
                        }), DispatcherPriority.ContextIdle, null);
                    });

                });
            previewDataCommand = new RelayCommand(
                () => {
                    if (busy) 
                        return;
                    
                    Busy = true;
                    ThreadPool.QueueUserWorkItem(
                    delegate {
                        /* This is just to demonstrate a slow operation. */
                        Thread.Sleep(3000);
                        /* We invoke back to the UI thread. 
                            * Ordinarily this would be done 
                            * by the Calcium infrastructure automatically. */
                        DispatcherHelper.UIDispatcher.Invoke(new Action(() => {
                            PreviewItems();
                            Busy = false;
                        }), DispatcherPriority.ContextIdle, null);
                    });

                });
        }

        void PreviewItems() {

            if (start - itemCount >= 0) {
                if (start > 50)
                    items.Clear();
                start -= itemCount;
                RefreshData();
            }
            Trace.WriteLine($"Start: {start} items: {itemCount} collection: {items.Count}");
        }
        void AddMoreItems() {

            if (start + itemCount < totalItems) {
                if (start > 50)
                    items.Clear();
                start += itemCount;
                RefreshData();
            }
            Trace.WriteLine($"Start: {start} items: {itemCount} collection: {items.Count}");
        }
       async void RefreshData() {
            var temp = await _dataService.GetAll();
            totalItems = temp.Count();
            foreach (var item in _dataService.GetPages(start, itemCount))
                items.Add(new Photo() { Path = item.Path, Name = item.Name, CreationTime = item.CreationTime });
        }
        readonly ICommand fetchMoreDataCommand;

        public ICommand FetchMoreDataCommand {
            get {
                return fetchMoreDataCommand;
            }
        }

        readonly ICommand previewDataCommand;

        public ICommand PreviewDataCommand {
            get {
                return previewDataCommand;
            }
        }
        readonly ObservableCollection<Photo> items = new ObservableCollection<Photo>();

        public ObservableCollection<Photo> Items => items;

        bool busy;

        public bool Busy {
            get {
                return busy;
            }
            set {
                if (busy == value) {
                    return;
                }
                busy = value;
                RaisePropertyChanged(() => Busy);
            }
        }
    }
}
