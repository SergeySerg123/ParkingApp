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
            int num = 0;
            var sb = ServiceBuilder
                .CreateInstance();
            sb.Build();
            var dg = DataGenerator.CreateInstance();
            var menu = Menu.CreateInstance();
            Console.WriteLine("Добро пожаловать в паркинг!");
            
            while(showMenu)
            {
                menu.ShowMenu();
                Tuple<bool, int> tuple = menu.Select(Console.ReadLine());
                showMenu = tuple.Item1;
                if(!showMenu)
                {
                    num = tuple.Item2;
                }
                
            }

            switch(num)
            {
                case 3:
                    Vehicle v = dg.GenerateVehicle();
                    _parkingService.AddVehicle(v);
                    Console.WriteLine($"Added Vechicle '{v.VehicleType}' with Balance {v.Balance}");
                    break;
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