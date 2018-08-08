using CoreLib;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace TachTypingTutor_v1._06._18
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : TWindow
    {
        public ResultWindow()
        {
            InitializeComponent();
            Loaded += ResultWindow_Loaded;
            timer.Interval = TimeSpan.FromMilliseconds(350);
        }
        DispatcherTimer timer = new DispatcherTimer();
        int rate = 0;
        int counter = 0;
        private void ResultWindow_Loaded(object sender, RoutedEventArgs e)
        {
            rate = int.Parse(tblRate.Text);
            timer.Tick += Timer_TickAsync;
            timer.Start();
        }

        private async void Timer_TickAsync(object sender, EventArgs e)
        {
            if (rate < 1)
            {
                timer.Stop();
                return;
            }

            switch (counter)
            {
                case 1:
                    star1.Visibility = Visibility.Visible;
                    break;
                case 2:
                    star2.Visibility = Visibility.Visible;
                    break;
                case 3:
                    star3.Visibility = Visibility.Visible;
                    break;
                case 4:
                    star4.Visibility = Visibility.Visible;
                    break;
                case 5:
                    star5.Visibility = Visibility.Visible;
                    break;
            }
            if (counter >= rate)
            {
                timer.Stop();
                return;
            }
            await Task.Delay(150);
            TypingSounds.PlayCoinSound();
            counter++;
        }
    }
}
