using GalaSoft.MvvmLight;
using System;
using System.Windows.Threading;

namespace CodingDojo3.ViewModel
{
 
    public class MainViewModel : ViewModelBase
    {
        private string CurrentDate
        {
            get { return CurrentDate; }
            set { CurrentDate = value; RaisePropertyChanged(); }
        }

        private string CurrentTime
        {
            get { return CurrentTime; }
            set { CurrentTime = value; RaisePropertyChanged(); }
        }
        public MainViewModel()
        {
            //Zeit und Datum
            //Timer erstellen
            DispatcherTimer timer = new DispatcherTimer();
            //Timer Interval festlegen
            timer.Interval = new TimeSpan(0, 0, 40);
            //Timer mit derezitigen Datum und Uhrzeit belegen
            timer.Tick += UpdateTime;

            if (!IsInDesignMode)
            {
                //starte timer für Datum und Uhrzeit
                timer.Start();
            }
        }

        //derzeitige Zeit und Datum analysieren und zuweisen
        private void UpdateTime(object sender, EventArgs e)
        {
            CurrentDate = DateTime.Now.ToLocalTime().ToShortDateString();
            CurrentTime = DateTime.Now.ToLocalTime().ToShortTimeString();
        }
    }
}