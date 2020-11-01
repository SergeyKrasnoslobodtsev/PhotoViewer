using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PhotoViewer.App.Views
{
    /// <summary>
    /// Логика взаимодействия для PhotoPage.xaml
    /// </summary>
    public partial class PhotoPage : Page
    {
        public PhotoPage() {
            InitializeComponent();
        }
    }

    public class ScrollViewerMonitor
    {
        public static DependencyProperty NextCommandProperty
            = DependencyProperty.RegisterAttached(
                "NextCommand", typeof(ICommand),
                typeof(ScrollViewerMonitor),
                new PropertyMetadata(OnNextCommandChanged));

        public static DependencyProperty PreviewCommandProperty
            = DependencyProperty.RegisterAttached(
                "PreviewCommand", typeof(ICommand),
                typeof(ScrollViewerMonitor),
                new PropertyMetadata(OnPreviewCommandChanged));

        public static ICommand GetNextCommand(DependencyObject obj) {
            return (ICommand)obj.GetValue(NextCommandProperty);
        }

        public static void SetNextCommand(DependencyObject obj, ICommand value) {
            obj.SetValue(NextCommandProperty, value);
        }
        public static ICommand GetPreviewCommand(DependencyObject obj) {
            return (ICommand)obj.GetValue(PreviewCommandProperty);
        }

        public static void SetPreviewCommand(DependencyObject obj, ICommand value) {
            obj.SetValue(PreviewCommandProperty, value);
        }
        public static void OnNextCommandChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e) {
            FrameworkElement element = (FrameworkElement)d;
            if (element != null) {
                element.Loaded -= element_Loaded;
                element.Loaded += element_Loaded;
            }
        }
        public static void OnPreviewCommandChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e) {
            FrameworkElement element = (FrameworkElement)d;
            if (element != null) {
                element.Loaded -= element_Loaded;
                element.Loaded += element_Loaded;
            }
        }

        static void element_Loaded(object sender, RoutedEventArgs e) {
            FrameworkElement element = (FrameworkElement)sender;
            element.Loaded -= element_Loaded;
            ScrollViewer scrollViewer = FindChildOfType<ScrollViewer>(element);
            if (scrollViewer == null) {
                throw new InvalidOperationException("ScrollViewer not found.");
            }

            var dpd = DependencyPropertyDescriptor.FromProperty(ScrollViewer.VerticalOffsetProperty, typeof(ScrollViewer));
            dpd.AddValueChanged(scrollViewer, delegate (object o, EventArgs args) {
                bool atBottom = scrollViewer.VerticalOffset
                               >= scrollViewer.ScrollableHeight /2;
                bool atTop = scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight / 2;

                if (atBottom) {
                    var atEnd = GetNextCommand(element);
                    if (atEnd != null) {
                        atEnd.Execute(null);
                    }
                }
                if (atTop) {
                    var atEnd = GetPreviewCommand(element);
                    if (atEnd != null) {
                        atEnd.Execute(null);
                    }
                }
            });
        }

        static T FindChildOfType<T>(DependencyObject root) where T : class {
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(root);

            while (queue.Count > 0) {
                DependencyObject current = queue.Dequeue();
                for (int i = VisualTreeHelper.GetChildrenCount(current) - 1; 0 <= i; i--) {
                    var child = VisualTreeHelper.GetChild(current, i);
                    var typedChild = child as T;
                    if (typedChild != null) {
                        return typedChild;
                    }
                    queue.Enqueue(child);
                }
            }
            return null;
        }
    }
}
