// TODO: implement the ParkingService class from the IParkingService interface.
//       For try to add a vehicle on full parking InvalidOperationException should be thrown.
//       For try to remove vehicle with a negative balance (debt) InvalidOperationException should be thrown.
//       Other validation rules and constructor format went from tests.
//       Other implementation details are up to you, they just have to match the interface requirements
//       and tests, for example, in ParkingServiceTests you can find the necessary constructor format and validation rules.
using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using System.Collections.ObjectModel;

namespace CoolParking.BL.Services
{
    public class ParkingService : IParkingService
    {
        private readonly Parking Parking;

        public ParkingService()
        {
            Parking = Parking.GetInstance();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            Parking.AddVehicle(vehicle);
        }

        public void RemoveVehicle(string vehicleId)
        {
            Parking.RemoveVehicle(vehicleId);
        }

        public decimal GetBalance() => Parking.Balance;


        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        

        public int GetCapacity()
        {
            throw new System.NotImplementedException();
        }

        public int GetFreePlaces()
        {
            throw new System.NotImplementedException();
        }

        public TransactionInfo[] GetLastParkingTransactions()
        {
            throw new System.NotImplementedException();
        }

        public ReadOnlyCollection<Vehicle> GetVehicles()
        {
            throw new System.NotImplementedException();
        }

        public string ReadFromLog()
        {
            throw new System.NotImplementedException();
        }

        

        public void TopUpVehicle(string vehicleId, decimal sum)
        {
            throw new System.NotImplementedException();
        }
    }
}