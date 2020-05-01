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


namespace Cronometro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer dispatcherTimer;
        private int timerTick;
        private bool isTimerRunning;
        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer = new DispatcherTimer(DispatcherPriority.Render);
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;

            timerTick = 1;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            timerLbl.Content = TransformTimer(timerTick++);
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!isTimerRunning) {
                dispatcherTimer.Start();
                isTimerRunning = true;
            }
        }

        private void PauseBtn_Click(object sender, RoutedEventArgs e)
        {
            if(isTimerRunning)
            {
                dispatcherTimer.Stop();
                pauseBtn.Content = "Resume";
                isTimerRunning = !isTimerRunning;
            }
            else if (timerTick>1)
            {
                dispatcherTimer.Start();
                pauseBtn.Content = "Pause";
                isTimerRunning = !isTimerRunning;
            }
           
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            timerLbl.Content = "00::00::00";
            timerTick = 1;
            isTimerRunning = false;
            pauseBtn.Content = "Pause";
        }

        /// <summary>
        /// This method transforms the integer time into the actual string
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private static string TransformTimer(int time)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            return timeSpan.ToString(@"hh\:\:mm\:\:ss");
        }
    }
}
