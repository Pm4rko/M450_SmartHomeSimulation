using Microsoft.VisualStudio.TestTools.UnitTesting;
using M320_SmartHome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M320_SmartHome.Tests
{
    [TestClass]
    public class ZimmerMitHeizungsventilTests
    {
        [TestMethod]
        public void VerarbeiteWetterdaten_TemperatureBelowSetpoint_OpensHeaterValve()
        {
            // Arrange
            int minute =30;
            var wettersensor = new WettersensorMock(15,5, false); // cold
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Wohnzimmer",20);
            wohnung.SetPersonenImZimmer("Wohnzimmer", false);

            // Act
            wohnung.GenerateWetterdaten(minute);

            // Assert
            var wohnzimmer = wohnung.GetZimmer<ZimmerMitHeizungsventil>("Wohnzimmer");
            Assert.IsNotNull(wohnzimmer);
            Assert.IsTrue(wohnzimmer.HeizungsventilOffen);
        }

        [TestMethod]
        public void VerarbeiteWetterdaten_TemperatureAboveOrEqualSetpoint_ClosesHeaterValve()
        {
            // Arrange
            int minute =10;
            var wettersensor = new WettersensorMock(25,3, false); // warm
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Wohnzimmer",20);
            wohnung.SetPersonenImZimmer("Wohnzimmer", false);

            // Act
            wohnung.GenerateWetterdaten(minute);

            // Assert
            var wohnzimmer = wohnung.GetZimmer<ZimmerMitHeizungsventil>("Wohnzimmer");
            Assert.IsNotNull(wohnzimmer);
            Assert.IsFalse(wohnzimmer.HeizungsventilOffen);
        }
    }
}