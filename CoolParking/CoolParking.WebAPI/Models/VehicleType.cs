namespace CoolParking.WebAPI.Interfaces
{
    public enum VehicleType
    {
        PassengerCar, Truck, Bus, Motorcycle
    }

    public static class VehicleTypeHelper
    {
        public static VehicleType GetVehicleType(int type)
        {
            VehicleType vehicleType = 0;
            switch(type)
            {
                case 0:
                    vehicleType = VehicleType.PassengerCar;
                    break;

                case 1:
                    vehicleType = VehicleType.Truck;
                    break;

                case 2:
                    vehicleType = VehicleType.Bus;
                    break;

                case 3:
                    vehicleType = VehicleType.Motorcycle;
                    break;
            }
            return vehicleType;
        }
    }
}

