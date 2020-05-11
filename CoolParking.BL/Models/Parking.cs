// TODO: implement class Parking.
//       Implementation details are up to you, they just have to meet the requirements 
//       of the home task and be consistent with other classes and tests.



using CoolParking.BL.Interfaces;
using CoolParking.BL.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CoolParking.BL.Models
{
    public class Parking 
    {
        private static Parking instance = null;
        private ILogService _logService = new LogService(Settings._logFilePath);

        public int Capacity { get; private set; }
        public decimal Balance { get; private set; }
        private readonly List<Vehicle> Vehicles = new List<Vehicle>();
        private int Busy;

        public int GetFreePlaces() => Capacity - Busy;

        private Parking() { }

        public static Parking GetInstance()
        {
            if (instance == null)
            {
                instance = new Parking();
                instance.Capacity = Settings.Capacity;
                instance.Busy = 0;
            }
            return instance;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            Vehicle v = GetVehicle(vehicle.Id);
            if (v != null)
            {
                throw new ArgumentException();
            }
            Vehicles.Add(vehicle);
            Busy += 1;
        }

        public void RemoveVehicle(string vehicleId)
        {
            Vehicle vehicle = GetVehicle(vehicleId);
            if (vehicle == null)
            {
                throw new ArgumentException();
            }
            Vehicles.Remove(vehicle);
        }

        public void TopUpParking(decimal sum)
        {
            Balance += sum;
        }
        public decimal WithdrawFromVechicle(Vehicle v, decimal sum) 
        {
            var balance = v.Balance;
            decimal actualSum = ((balance - sum) < 0) ? sum * (decimal)Settings.PenaltyRatio : sum;
            v.Withdraw(actualSum);
            return actualSum;
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
            _logService.Write(mess);
        }

        public void Distruct()
        {
            instance = null;
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

