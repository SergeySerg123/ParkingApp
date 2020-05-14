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
        // TODO 
        private readonly List<Vehicle> Vehicles = new List<Vehicle>();
        private int Busy;

        public int GetFreePlaces() => Capacity - Busy;

        public Parking() {
            Capacity = Settings.Capacity;
            Busy = 0;
        }

        public Vehicle AddVehicle(Vehicle vehicle)
        {
            Vehicles.Add(vehicle);
            Busy += 1;
            return vehicle;
        }

        public Vehicle RemoveVehicle(Vehicle vehicle)
        {
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

        public Vehicle GetVehicle(string vehicleId)
        {
            Vehicle vehicle = Vehicles?.Find(v => v.Id == vehicleId);
            return vehicle;
        }
    }
}

