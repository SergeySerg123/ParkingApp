// TODO: implement class TimerService from the ITimerService interface.
//       Service have to be just wrapper on System Timers.

using CoolParking.WebAPI.Interfaces;
using CoolParking.WebAPI.Models;
using System;
using System.Timers;

namespace CoolParking.WebAPI.Services
{
    public class TimerService : ITimerService
    {
        private readonly ITransactionsService _transactionService;
        private Timer timer;
        private readonly Parking _parking;

        public double Interval { get; set; }
        public TimerService(ITransactionsService transactionsService, Parking parking)
        {
            _transactionService = transactionsService;
            _parking = parking;
            timer = new Timer();
            Elapsed += OnTimedEvent;
            timer.Elapsed += Callback;
            timer.AutoReset = true;
        }


        public event ElapsedEventHandler Elapsed;

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            var vehicles = _parking?.GetVehicles;
            if(vehicles != null)
            {
                foreach (Vehicle v in vehicles)
                {
                    _transactionService.CreateTransaction(_parking, v);
                }
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
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
            timer.Dispose();
            Dispose();
        }

        private void Callback(Object source, ElapsedEventArgs e)
        {
            FireElapsedEvent();
        }

    }
}