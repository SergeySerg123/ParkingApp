﻿// TODO: implement class Settings.
//       Implementation details are up to you, they just have to meet the requirements of the home task.
using System.IO;
using System.Reflection;

namespace CoolParking.BL.Models
{
    public static class Settings
    {
        public static readonly decimal InitialSum = 0;
        public static readonly int Capacity = 10;
        public static readonly int EveryTimePay = 5;
        public static readonly int EveryTimeWriteToLog = 60;
        public static readonly double PenaltyRatio = 2.5;

        public static readonly string _logFilePath = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Transactions.log";

        public static decimal GetPrice(VehicleType type)
        {
            switch (type)
            {
                case VehicleType.PassengerCar:
                    return 2;

                case VehicleType.Bus:
                    return 3.5M;

                case VehicleType.Motorcycle:
                    return 1;

                case VehicleType.Truck:
                    return 5;

                default:
                    return 0;
            }
        }

        public const string BASE_URL= "http://localhost:51183/";
        public const string BASE_URL_PARKING_API = "http://localhost:51183/api/parking/";
        public const string BASE_URL_VEHICLES_API = "http://localhost:51183/api/vehicles/";
        public const string BASE_URL_TRANSACTIONS_API = "http://localhost:51183/api/transactions/";
    }
}