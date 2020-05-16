using CoolParking.WebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CoolParking.WebAPI.Models
{
    public class Parking 
    {
        //private ILogService _logService = new LogService(Settings._logFilePath);

        public int Capacity { get; private set; } = Settings.Capacity;
        public decimal Balance { get; private set; }
        private readonly List<Vehicle> Vehicles = new List<Vehicle>();
        private int Busy = 0;

        public int GetFreePlaces() => Capacity - Busy;

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

