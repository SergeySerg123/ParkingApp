using System;
using System.Text.RegularExpressions;

namespace CoolParking.WebAPI.Helpers
{
    public static class VehicleValidator
    {
        public static bool IsValidVehicleId(string vehicleId)
        {
            try
            {
                Regex regex = new Regex(@"\w{2}-\d{4}-\w{2}", RegexOptions.IgnoreCase);
                return regex.IsMatch(vehicleId);
            } catch (Exception e)
            {
                return false;
            }
            
        }

        public static bool IsExistingVehicleType(int type) => (type >= 0 && type <= 3);

        public static bool IsValidTopUpSum(decimal sum) => sum > 0;
    }
}
