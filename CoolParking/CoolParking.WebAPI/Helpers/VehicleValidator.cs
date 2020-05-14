using System.Text.RegularExpressions;

namespace CoolParking.WebAPI.Helpers
{
    public static class VehicleValidator
    {
        public static bool IsValidVehicleId(string vehicleId)
        {
            Regex regex = new Regex(@"\w{2}-\d{4}-\w{2}", RegexOptions.IgnoreCase);
            return regex.IsMatch(vehicleId);
        }

        public static bool IsValidTopUpSum(decimal sum) => sum > 0;
    }
}
