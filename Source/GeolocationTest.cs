using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace Source
{
    [TestClass]
    public class GeolocationTest
    {
        [TestMethod]
        public void SUbStringTest()
        {
            var latitude = "50.525008008485216";
            var longitude  = "30.459802096027136";

            var anotherY  = "50.52493549099314";
            var anotherX = "30.462896111788933";

            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            provider.NumberGroupSeparator = ",";

            var x = ConvertToDouble(longitude);
            var y = ConvertToDouble(latitude);

            var ax = ConvertToDouble(anotherX);
            var ay = ConvertToDouble(anotherY);

            var xRes = x - ax;
            var yRes = y - ay;
            Assert.AreNotEqual(y, 0);
            Assert.AreNotEqual(x, 0);
        }

        [TestMethod]
        public void TestMathRound()
        {
            var latitude = 50.525008008485216;
            var longitude = 30.459802096027136;

            var anotherY = 50.52536993141412;
            var anotherX = 30.45577329645037;

            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            provider.NumberGroupSeparator = ",";

            var x = Math.Round(longitude, 5);
            var y = Math.Round(latitude, 5);

            var ax = Math.Round(anotherX, 5);
            var ay = Math.Round(anotherY, 5);

            var xRes = Math.Round(x - ax, 4);
            var yRes = Math.Round(y - ay, 4);
            var xres = Math.Round(xRes < 0 ? -xRes : xRes, 4);
            var yres = Math.Round(yRes < 0 ? -yRes : yRes, 4);
            Assert.IsTrue(xres <= 0.0065);
            Assert.IsTrue(yres <= 0.0065);
        }


        private double ConvertToDouble(string value)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            provider.NumberGroupSeparator = ",";

            var @short = value.Substring(0, 6);
            return double.TryParse(@short, NumberStyles.Number, provider, out var res) ? res : 0;
        }
    }
}
