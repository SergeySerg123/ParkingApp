using CoolParking.WebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CoolParking.WebAPI.Models
{
    public class Parking 
    {
        //private static Parking instance = null;
        //private ILogService _logService = new LogService(Settings._logFilePath);

        public int Capacity { get; private set; }
        public decimal Balance { get; private set; }
        private readonly List<Vehicle> Vehicles = new List<Vehicle>();
        private int Busy;

        public int GetFreePlaces() => Capacity - Busy;

        public Parking() {
            Capacity = Settings.Capacity;
            Busy = 0;
        }

        //public static Parking GetInstance()
        //{
        //    if (instance == null)
        //    {
        //        instance = new Parking();
        //        instance.Capacity = Settings.Capacity;
        //        instance.Busy = 0;
        //    }
        //    return instance;
        //}

        public void AddVehicle(Vehicle vehicle)
        {
            Vehicle v = GetVehicle(vehicle.Id);
            if (v != null)
            {
                throw new ArgumentException();
            }

            if (Vehicles.Count >= 10)
            {
                throw new InvalidOperationException();
            }

            Vehicles.Add(vehicle);
            Busy += 1;
        }

        public Vehicle RemoveVehicle(string vehicleId)
        {
            Vehicle vehicle = GetVehicle(vehicleId);
            if (vehicle == null)
            {
                return vehicle;
            }

            //if (vehicle.Balance < 0)
            //{
            //    throw new InvalidOperationException();
            //}

            Vehicles.Remove(vehicle);
            return vehicle;
        }

        public void TopUpParking(decimal sum)
        {
            Balance += sum;
        }
        public decimal WithdrawFromVechicle(Vehicle v, decimal sum) 
        {
            var balance = v.Balance;
            decimal actualSum = ((balance - sum) < 0) ? sum * ApplyPenalty(balance, sum) : sum;
            v.Withdraw(actualSum);
            return actualSum;
        }

        private decimal ApplyPenalty(decimal balance, decimal sum)
        {
            decimal total = balance - sum;
            return total * (decimal)Settings.PenaltyRatio;
        }

        public void TopUpVehicle(string vehicleId, decimal sum)
        {
            Vehicle vehicle = GetVehicle(vehicleId);
            if (vehicle == null || sum < 0)
            {
                throw new ArgumentException();
            }
            vehicle.TopUpVehicle(sum);
        }

        public void WriteToLog(string mess)
        {
            //_logService.Write(mess);
        }

        public void Distruct()
        {
            //instance = null;
        }

        public ReadOnlyCollection<Vehicle> GetVehicles
            => new ReadOnlyCollection<Vehicle>(Vehicles);

        private Vehicle GetVehicle(string vehicleId)
        {
            Vehicle vehicle = Vehicles?.Find(v => v.Id == vehicleId);
            return vehicle;
        }
    }
}

