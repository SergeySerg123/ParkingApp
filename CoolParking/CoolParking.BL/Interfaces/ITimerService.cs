using System.Timers;

namespace CoolParking.BL.Interfaces
{
    public interface ITimerService
    {
        event ElapsedEventHandler Elapsed;
        double Interval { get; set; }
        bool Start();
        bool Stop();
        void Dispose();
    }
}
