// TODO: implement the ParkingService class from the IParkingService interface.
//       For try to add a vehicle on full parking InvalidOperationException should be thrown.
//       For try to remove vehicle with a negative balance (debt) InvalidOperationException should be thrown.
//       Other validation rules and constructor format went from tests.
//       Other implementation details are up to you, they just have to match the interface requirements
//       and tests, for example, in ParkingServiceTests you can find the necessary constructor format and validation rules.
using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using System;
using System.Collections.ObjectModel;

namespace CoolParking.BL.Services
{
    public class ParkingService : IParkingService
    {
        private readonly Parking Parking;
        private readonly ILogService _logService;
        private readonly ITransactionService _transactionService;
        private readonly ITimerService _withdrawTimer;
        private readonly ITimerService _logTimer;
        bool disposed = false;

        public ParkingService(ITimerService withdrawTimer, ITimerService logTimer, ILogService logService)
        {
            Parking = Parking.GetInstance();
            _logService = logService;
            _transactionService = TransactionService.GetInstance();
            _withdrawTimer = withdrawTimer;
            _logTimer = logTimer;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            Parking.AddVehicle(vehicle);
        }

        public void RemoveVehicle(string vehicleId)
        {
            Parking.RemoveVehicle(vehicleId);
        }

        public void TopUpVehicle(string vehicleId, decimal sum)
        {
            Parking.TopUpVehicle(vehicleId, sum);
        }

       
        public ReadOnlyCollection<Vehicle> GetVehicles() => Parking.GetVehicles;

        public decimal GetBalance() => Parking.Balance;

        public int GetCapacity() => Parking.Capacity;

        public int GetFreePlaces() => Parking.GetFreePlaces();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                Parking.ClearVehicles();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }

        public TransactionInfo[] GetLastParkingTransactions()
        {
            return _transactionService.GetLastParkingTransactions();
        }

        public string ReadFromLog()
        {
            string s = _logService.Read();
            return s;
        }
    }
}