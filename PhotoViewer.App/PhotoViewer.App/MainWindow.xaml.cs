using ModernWpf.Controls;
using PhotoViewer.App.Services;
using PhotoViewer.App.Views;
using System;
using System.Windows;

namespace PhotoViewer.App
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() {
            InitializeComponent();
        }

        private void Nav_SelectionChanged(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewSelectionChangedEventArgs args) {

                var selectedItem = (NavigationViewItem)args.SelectedItem;
                if (selectedItem != null) {
                    string selectedItemTag = (string)selectedItem.Tag;
                    switch (selectedItemTag) {
                        case "Photo":
                            content.Navigate(typeof(PhotoPage));
                            break;
                        case "Album":
                            content.Navigate(typeof(AlbumPage));
                            break;
                    case "Maps":
                        content.Navigate(typeof(MapsPage));
                        break;
                    default:
                            content.Navigate(typeof(PhotoPage));
                            break;
                    }

                    //sender.Header = "Sample Page " + selectedItemTag.Substring(selectedItemTag.Length - 1);
                    //string pageName = "SamplesCommon.SamplePages." + selectedItemTag;
                    //Type pageType = typeof(PhotoPage).Assembly.GetType(pageName);
                    //content.Navigate(typeof(PhotoPage));
                
            }
        }
    }
}
