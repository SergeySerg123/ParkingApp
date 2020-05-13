using CoolParking.WebAPI.Interfaces;
using CoolParking.WebAPI.Models;
using System;
using System.Collections.ObjectModel;

namespace CoolParking.WebAPI.Services
{
    public class ParkingService : IParkingService
    {
        private readonly Parking Parking;
        private readonly ITransactionService _transactionService;

        public ParkingService(Parking parking,
            ITransactionService transactionService)
        {
            Parking = parking;
            _transactionService = transactionService;
        }

        public Vehicle AddVehicle(Vehicle vehicle)
        {
            return Parking.AddVehicle(vehicle);
        }

        public Vehicle RemoveVehicle(string vehicleId)
        {
            return Parking.RemoveVehicle(vehicleId);
        }

        public void TopUpVehicle(string vehicleId, decimal sum)
        {
            Parking.TopUpVehicle(vehicleId, sum);
        }

        public decimal GetBalance() => Parking.Balance;

        public int GetCapacity() => Parking.Capacity;

        public int GetFreePlaces() => Parking.GetFreePlaces();

        public Vehicle GetVehicle(string vehicleId)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<Vehicle> GetVehicles() => Parking.GetVehicles;

        public TransactionInfo[] GetLastParkingTransactions()
        {
            return _transactionService.GetLastParkingTransactions();
        }
    }
}
