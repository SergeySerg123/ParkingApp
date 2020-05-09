// TODO: implement class Settings.
//       Implementation details are up to you, they just have to meet the requirements of the home task.
namespace CoolParking.BL.Models
{
    public static class Settings
    {
        public static decimal InitialSum = 0;
        public static int Capacity = 10;
        public static int EveryTimePay = 5;
        public static int EveryTimeWriteToLog = 60;
        public static double PenaltyRatio = 2.5;

        public static decimal GetPrice(VehicleType type)
        {
            switch (type)
            {
                case VehicleType.PassengerCar:
                    return 2;

                case VehicleType.Bus:
                    return 3.5M;

                case VehicleType.Motorcycle:
                    return 1;

                case VehicleType.Truck:
                    return 5;

                default:
                    return 0;
            }
        }
    }
}