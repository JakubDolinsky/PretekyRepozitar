using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Preteky
{
    public class CasovacView:INotifyPropertyChanged 
    {
        DispatcherTimer timer = new DispatcherTimer();
       public Stopwatch stopwatch = new Stopwatch();
        public string currentTime { get; set; }
        public TimeSpan casZobrazeny { get; set; }

        public TimeSpan vymedzenyCas { get; set; }
        public CasovacView(SpravcaPretekov spravcaPretekov)
        {
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            vymedzenyCas = TimeSpan.Parse(spravcaPretekov.zoznamPretekov[0].casovyLimit);
            currentTime = $"{vymedzenyCas.Hours }:{vymedzenyCas.Minutes}:{vymedzenyCas.Seconds},{vymedzenyCas.Milliseconds / 10}";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string vlastnost)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs (vlastnost));
            }
        }
        public event EventHandler VynulovanyCasovac;

        public void OnVynulovanyCasovac(EventArgs e)
        {
            TimeSpan nula = new TimeSpan(0, 0, 0, 0, 0);
            //if (currentTime =="0:0:0,0" )
            if( casZobrazeny <= nula )
            {
                VynulovanyCasovac(this, e );
            }
        }
        public void Timer_Tick(object sender, EventArgs e)
        {
            
            if (stopwatch.IsRunning && stopwatch.Elapsed <= vymedzenyCas)
            {
                TimeSpan a = new TimeSpan(0, 0, 0, 0, 30);
                TimeSpan cas = vymedzenyCas - stopwatch.Elapsed - a;
                currentTime = $"{cas.Hours }:{cas.Minutes }:{cas.Seconds},{cas.Milliseconds / 10}";
                casZobrazeny = cas;
                OnPropertyChanged("currentTime");
                OnVynulovanyCasovac(EventArgs.Empty);               
            }
            else
            {
                
                Zastav();

            }
        }
        public void Zastav()
        {
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();

            }
            
        }
        public void Spust()
        {
            stopwatch.Start();
            timer.Start();
        }

    }
}
