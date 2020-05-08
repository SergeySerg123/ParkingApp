// TODO: implement class TimerService from the ITimerService interface.
//       Service have to be just wrapper on System Timers.

using CoolParking.BL.Interfaces;
using System;
using System.Timers;

namespace CoolParking.BL.Services
{
    public class TimerService : ITimerService
    {
        private readonly Timer timer;
        public double Interval { get; set; }
        public TimerService(int interval)
        {
            Interval = interval;
            timer = new Timer
            {
                Interval = Interval,
                AutoReset = true
            };
            Elapsed += OnTimedEvent;
        }

        
        public event ElapsedEventHandler Elapsed;

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {

        }

        public void FireElapsedEvent()
        {
            Elapsed?.Invoke(this, null);
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Start()
        {
            timer.Enabled = true;
        }

        public void Stop()
        {
            timer.Enabled = false;
        }
    }
}