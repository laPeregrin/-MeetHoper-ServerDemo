using System;
using System.Globalization;

namespace Common.Models.DTOs
{
    public class Geoposition
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Geoposition(double longitude, double latitude)
        {
            var provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            provider.NumberGroupSeparator = ",";

            Longitude = Math.Round(longitude, 5);
            Latitude = Math.Round(latitude, 5);
        }

    }
}
