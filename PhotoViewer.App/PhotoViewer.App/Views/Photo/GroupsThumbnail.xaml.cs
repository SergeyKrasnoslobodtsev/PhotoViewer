using PhotoViewer.App.Utils;
using PhotoViewer.App.ViewModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PhotoViewer.App.Views
{
    /// <summary>
    /// Логика взаимодействия для Thumbnail.xaml
    /// </summary>
    public partial class GroupsThumbnail : UserControl
    {
        public GroupsThumbnail()
        {
            InitializeComponent();
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (e.AddedItems.Count > 0) {
                Items user = (Items)e.AddedItems[0];
                ListViewItem lvi = (ListViewItem)list.ItemContainerGenerator.ContainerFromItem(user);
                CheckBox chkBx = FindVisualChild<CheckBox>(lvi);
                if (chkBx != null)
                    chkBx.IsChecked = true;
            } else 
              {
                Items user = (Items)e.RemovedItems[0];
                ListViewItem lvi = (ListViewItem)list.ItemContainerGenerator.ContainerFromItem(user);
                CheckBox chkBx = FindVisualChild<CheckBox>(lvi);
                if (chkBx != null)
                    chkBx.IsChecked = false;
                
            }
            if (list.SelectedItems.Count == list.Items.Count)
                tg_group.IsChecked = true;
            else
                tg_group.IsChecked = false;
        }

        public static T FindVisualChild<T>(DependencyObject depObj) where T : DependencyObject {
            if (depObj != null) {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++) {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T) {
                        return (T)child;
                    }

                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null) return childItem;
                }
            }
            return null;
        }
        private static bool individualChkBxUnCheckedFlag { get; set; }
        private void tg_group_Checked(object sender, RoutedEventArgs e) {
            individualChkBxUnCheckedFlag = false;
            foreach (Items item in list.ItemsSource) {
                item.IsSelectedItem = true;
                list.SelectedItems.Add(item);
            }
        }

        private void tg_group_Unchecked(object sender, RoutedEventArgs e) {
            if (!individualChkBxUnCheckedFlag) {
                foreach (Items item in list.ItemsSource) {
                    item.IsSelectedItem = false;
                    list.SelectedItems.Remove(item);           
                }
            }
            
        }

    }
}
