using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PhotoViewer.App.Controls
{
    /// <summary>
    /// Логика взаимодействия для Thumbnail.xaml
    /// </summary>
    public partial class Thumbnail : UserControl
    {
        public Thumbnail() {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            Media.Play();
            Media.Pause();
        }

        private void Timer_Tick(object sender, EventArgs e) {
            if (Media.Source != null) {
                if (Media.NaturalDuration.HasTimeSpan)
                    time.Text = (Media.NaturalDuration.TimeSpan - Media.Position).ToString(@"mm\:ss");
            } else
                time.Text = "No file selected...";
        }

        private void Media_MouseEnter(object sender, MouseEventArgs e) {
            Media.Play();
            play.Style = (Style)FindResource("PauseCircleButton");
        }

        private void Media_MouseLeave(object sender, MouseEventArgs e) {
            Media.Pause();
            play.Style = (Style)FindResource("PlayCircleButton");
        }
        private void play_Click(object sender, RoutedEventArgs e) {
            if (Media.CanPause) {
                Media.Pause();
                play.Style = (Style)FindResource("PlayCircleButton");
            }
            Media.Play();
            play.Style = (Style)FindResource("PauseCircleButton");
        }
    }
}
