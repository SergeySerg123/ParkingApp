// TODO: implement class Vehicle.
//       Properties: Id (string), VehicleType (VehicleType), Balance (decimal).
//       The format of the identifier is explained in the description of the home task.
//       Id and VehicleType should not be able for changing.
//       The Balance should be able to change only in the CoolParking.BL project.
//       The type of constructor is shown in the tests and the constructor should have a validation, which also is clear from the tests.
//       Static method GenerateRandomRegistrationPlateNumber should return a randomly generated unique identifier.

using System;
using System.Text;
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
            Id = id;
            VehicleType = type;
            Balance = b;
        }

        public void TopUpVehicle(decimal sum)
        {
            Balance += sum;
        }

        public void Withdraw(decimal sum)
        {
            Balance -= sum;
        }

        public static string GenerateRandomRegistrationPlateNumber()
        {
            Random random = new Random();
            int length = 4;
            StringBuilder str_build = new StringBuilder();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            int nums = random.Next(1111, 9999);
            string s = str_build.ToString();
            string fisrtPart = s.Substring(0, 2);
            string secondPart = s.Substring(2, 2);
            return $"{fisrtPart.ToUpper()}-{nums}-{secondPart.ToUpper()}";
        }
    }
}
