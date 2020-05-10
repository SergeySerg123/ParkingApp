using CoolParking.BL.Models;
using System;
using System.Text;

namespace CoolParking.BL
{
    public sealed class DataGenerator
    {
        private readonly Random random = new Random();

        private DataGenerator() { }

        //Factory method
        public static DataGenerator CreateInstance() => new DataGenerator();

        public Vehicle GenerateVehicle()
         => new Vehicle(
             GenerateRandomId(), GenerateRandomVechicleType(), GenerateRandomVechicleBalance());

        private string GenerateRandomId()
        {
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
    
        private VehicleType GenerateRandomVechicleType()
        {
            Array values = Enum.GetValues(typeof(VehicleType));
            return (VehicleType)values.GetValue(random.Next(values.Length));
        }

        private int GenerateRandomVechicleBalance()
        {
            return random.Next(100, 999);
        }
    }
}
