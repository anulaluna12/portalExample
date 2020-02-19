using System;
using Microsoft.AspNetCore.Http;

namespace PortalExample.API.Helpers
{
    public static class Extensions
    {
        public static int CalculateAge(this DateTime dateTime)
        {
            var age = DateTime.Today.Year - dateTime.Year;
            if (dateTime.AddYears(age) > DateTime.Today)
                age--;

            return age;

        }
        public static void AddApplicationError(this HttpResponse respone, string message)
        {
            respone.Headers.Add("Application-error", message);
            respone.Headers.Add("Access-Control-Expose-Headers", "Application-error");
            respone.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}