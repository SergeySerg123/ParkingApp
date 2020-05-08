// TODO: implement class Parking.
//       Implementation details are up to you, they just have to meet the requirements 
//       of the home task and be consistent with other classes and tests.



using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CoolParking.BL.Models
{
    public class Parking 
    {
        private static Parking instance = null;

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

        public void TopUpVehicle(string vehicleId, decimal sum)
        {
            Vehicle vehicle = GetVehicle(vehicleId);
            if (vehicle != null)
            {
                vehicle.TopUpVehicle(sum);
            }
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

