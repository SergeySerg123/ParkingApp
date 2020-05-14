using System;

namespace CoolParking.WebAPI.Helpers
{
    public class ExceptionMessageGenerator
    {
        public static string GenereteAgrumentExceptionMessage() => new ArgumentException().Message;
        public static string GenereteAgrumentNullExceptionMessage() => new ArgumentNullException().Message;
    }
}
