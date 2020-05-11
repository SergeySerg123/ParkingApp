// TODO: implement class TimerService from the ITimerService interface.
//       Service have to be just wrapper on System Timers.

using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using System;
using System.Timers;

namespace CoolParking.BL.Services
{
    public class TimerService : ITimerService
    {
        private ITransactionService _transactionService = TransactionService.GetInstance();
        private Timer timer;

        public double Interval { get; set; }
        public TimerService()
        {
            Elapsed += OnTimedEvent;
            timer = new Timer();
            timer.Elapsed += Callback;
            
            timer.AutoReset = true;
        }

        
        public event ElapsedEventHandler Elapsed;

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            var parking = Parking.GetInstance();
            var vehicles = parking.GetVehicles;
            foreach(Vehicle v in vehicles)
            {
                _transactionService.CreateTransaction(parking, v);
            }
        }

        public void FireElapsedEvent()
        {
            Elapsed?.Invoke(this, null);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Start()
        {
            timer.Interval = Interval;
            timer.Enabled = true;
        }

        public void Stop()
        {
            timer.Enabled = false;
        }

        private void Callback(Object source, ElapsedEventArgs e)
        {
            FireElapsedEvent();
        }

    }
}