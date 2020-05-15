using System;
using System.Text;
using System.Text.RegularExpressions;

namespace CoolParking.WebAPI.Interfaces
{
    public class Vehicle
    {
        public string Id { get; private set; }
        public VehicleType VehicleType { get; private set; }
        public decimal Balance { get; private set; }

        private Vehicle(string id, VehicleType type, decimal b)
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

        public static Vehicle CreateInstance(string id, VehicleType type, decimal b)
         => new Vehicle(id, type, b);

        private bool ValidateVechicle(string vehicleId, decimal d)
        {
            Regex regex = new Regex(@"\w{2}-\d{4}-\w{2}", RegexOptions.IgnoreCase);
            return regex.IsMatch(vehicleId) && d >= 0;
        }

        public Vehicle TopUpVehicle(decimal sum)
        {
            Balance += sum;
            return this;
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
