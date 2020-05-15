using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.BL.Services;
using System;
using System.Collections.Generic;

namespace CoolParking.BL
{
    public class Program
    {
        private static ILogService _logService;
        private static IParkingService _parkingService;
        private static ITimerService _withdrawTimer;
        private static ITimerService _logTimer;
        private static bool showMenu = true;
        private static int selectedNum = 0;
        

        public static void Main(string[] args)
        {
            var sb = ServiceBuilder
                .CreateInstance();
            sb.Build();
            _withdrawTimer.Interval = 5000;
            //_logTimer.Interval = 60000;
            var dg = DataGenerator.CreateInstance();
            var ivm = InputValidatorMessages.CreateInstance();
            var menu = Menu.CreateInstance(ivm);
            Console.WriteLine("Добро пожаловать в паркинг!");
            Show(dg, menu, ivm);           
        }

        static void Show(DataGenerator dg, Menu menu, InputValidatorMessages ivm)
        {
            while (showMenu)
            {
                menu.ShowMenu();
                Tuple<bool, int> tuple = menu.Select(Console.ReadLine());
                showMenu = tuple.Item1;
                if (!showMenu)
                {
                    selectedNum = tuple.Item2;
                }

            }

            var vechicles = _parkingService.GetVehicles();
            
            switch (selectedNum)
            {
                case 1:
                    _withdrawTimer.Start();
                    Console.WriteLine("=====================");
                    Console.WriteLine("Parking started!");
                    Console.WriteLine("=====================");
                    break;

                case 2:
                    _withdrawTimer.Stop();
                    Console.WriteLine("=====================");
                    Console.WriteLine("Parking stoped!");
                    Console.WriteLine("=====================");
                    break;

                case 3:
                    Vehicle v = dg.GenerateVehicle();
                    bool succeed = _parkingService.AddVehicle(v);
                    if (succeed)
                        Console.WriteLine($"Added Vechicle '{v.VehicleType}' with Balance {v.Balance}");
                    break;

                case 4:
                    Console.WriteLine($"Parking Balance is {_parkingService.GetBalance()}");
                    break;

                case 5:
                    Console.WriteLine($"Parking has {_parkingService.GetFreePlaces()} free place(s) from {_parkingService.GetCapacity()}");
                    break;

                case 6:
                    Console.WriteLine($"Cars in the parking:");
                    foreach (var vechicle in vechicles)
                    {
                        Console.WriteLine($"- id: {vechicle.id}, type: {vechicle.vehicleType}, balance: {vechicle.balance}");
                    }
                    break;

                case 7:
                    bool openedSubMenu = true;
                    if(vechicles.Count > 0 && openedSubMenu)
                    {
                        while(openedSubMenu)
                        {
                            IDictionary<int, VehicleSchema> dictionary = new Dictionary<int, VehicleSchema>();
                            Console.WriteLine($"Select vechicle for top up balance from list:");
                            int i = 0;
                            foreach (var vechicle in vechicles)
                            {
                                ++i;
                                Console.WriteLine($"{i} - id: {vechicle.id}, type: {vechicle.vehicleType}, balance: {vechicle.balance}");
                                dictionary.Add(i, vechicle);
                            }

                            int num = 0;
                            try
                            {
                                num = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Top Up balance on: ");
                                int sum = Convert.ToInt32(Console.ReadLine());
                                bool res = _parkingService.TopUpVehicle(dictionary[num].id, sum);
                                if (res)
                                    Console.WriteLine($"Balance vehicle with id {dictionary[num].id} top uped on {sum}. Thank you!");

                                openedSubMenu = false;
                            }
                            catch (Exception e)
                            {
                                ivm.IsNotNumber();                             
                            }
                        }
                        
                    } else
                    {
                        Console.WriteLine($"Add vechicle to parking before! Press 3!");
                    }

                    
                    break;

                case 8:
                    var transactions = _parkingService.GetLastParkingTransactions();
                    if(transactions.Length != 0)
                    {
                        foreach (var t in transactions)
                        {
                            Console.WriteLine($"VehicleID:{t.VechicleId}, Time: {t.DateTime.ToString()}, Sum: {t.Sum}");
                        }
                    } else
                    {
                        Console.WriteLine($"No transactions");
                    }
                    
                    break;

            }

            showMenu = true;
            Show(dg, menu, ivm);
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