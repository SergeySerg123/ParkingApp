// TODO: implement class TimerService from the ITimerService interface.
//       Service have to be just wrapper on System Timers.

using CoolParking.BL.Interfaces;
using System.Timers;

namespace CoolParking.BL.Services
{
    public class TimerService : ITimerService
    {
        public double Interval { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public event ElapsedEventHandler Elapsed;

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}