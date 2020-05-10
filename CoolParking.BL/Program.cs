using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.BL.Services;
using System;

namespace CoolParking.BL
{
    public class Program
    {
        private static ILogService _logService;
        private static IParkingService _parkingService;
        private static ITimerService _withdrawTimer;
        private static ITimerService _logTimer;
        

        public static void Main(string[] args)
        {
            bool showMenu = true;
            int num;
            var sb = ServiceBuilder
                .CreateInstance();
            sb.Build();
            var menu = Menu.CreateInstance();
            Console.WriteLine("Добро пожаловать в паркинг!");
            
            while(showMenu)
            {
                menu.ShowMenu();
                (showMenu, num) = menu.Select(Console.ReadLine());
            }
        }

        class ServiceBuilder
        {
            private ServiceBuilder() { }

            public static ServiceBuilder CreateInstance() => new ServiceBuilder();

            public void Build()
            {
                _logService = new LogService(Settings._logFilePath);
                _withdrawTimer = new TimerService();
                _logTimer = new TimerService();
                _parkingService = new ParkingService(_withdrawTimer, _logTimer, _logService);
            }
        }      
    }
}