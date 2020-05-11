// TODO: implement class Vehicle.
//       Properties: Id (string), VehicleType (VehicleType), Balance (decimal).
//       The format of the identifier is explained in the description of the home task.
//       Id and VehicleType should not be able for changing.
//       The Balance should be able to change only in the CoolParking.BL project.
//       The type of constructor is shown in the tests and the constructor should have a validation, which also is clear from the tests.
//       Static method GenerateRandomRegistrationPlateNumber should return a randomly generated unique identifier.

using System;
using System.Text.RegularExpressions;

namespace CoolParking.BL.Models
{
    public class Vehicle
    {
        public string Id { get; private set; }
        public VehicleType VehicleType { get; private set; }
        public decimal Balance { get; private set; }

        public Vehicle(string id, VehicleType type, decimal b)
        {
            bool isValid = ValidateVechicle(id, b);
            if(!isValid)
            {
                throw new ArgumentException();
            }
            Id = id;
            VehicleType = type;
            Balance = b;
        }

        private bool ValidateVechicle(string vehicleId, decimal d)
        {
            Regex regex = new Regex(@"\w{2}-\d{4}-\w{2}", RegexOptions.IgnoreCase);
            return regex.IsMatch(vehicleId) && d >= 0;
        }

        public void TopUpVehicle(decimal sum)
        {
            Balance += sum;
        }

        public void Withdraw(decimal sum)
        {
            Balance -= sum;
        }
    }
}
