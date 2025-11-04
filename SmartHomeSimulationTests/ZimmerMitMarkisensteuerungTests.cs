using Microsoft.VisualStudio.TestTools.UnitTesting;
using M320_SmartHome;

namespace M320_SmartHome.Tests
{
    [TestClass]
    public class ZimmerMitMarkisensteuerungTests
    {
        [TestMethod]
        public void VerarbeiteWetterdaten_TemperatureBelowSetpoint_OpensAwning()
        {
            // Arrange
            int minute =5;
            var wettersensor = new WettersensorMock(10,2, false);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Wintergarten",20);
            wohnung.SetPersonenImZimmer("Wintergarten", false);

            // Act
            wohnung.GenerateWetterdaten(minute);

            // Assert
            var z = wohnung.GetZimmer<ZimmerMitMarkisensteuerung>("Wintergarten");
            Assert.IsNotNull(z);
            Assert.IsTrue(z.MarkiseOffen);
        }

        [TestMethod]
        public void VerarbeiteWetterdaten_RainingWhileClosed_OpensAwning()
        {
            // Arrange: ensure closed first
            int minute =5;
            var hotSensor = new WettersensorMock(30,2, false);
            var hotWohnung = new Wohnung(hotSensor);
            hotWohnung.SetTemperaturvorgabe("Wintergarten",20);
            hotWohnung.SetPersonenImZimmer("Wintergarten", false);
            hotWohnung.GenerateWetterdaten(minute);
            var z = hotWohnung.GetZimmer<ZimmerMitMarkisensteuerung>("Wintergarten");
            Assert.IsFalse(z.MarkiseOffen);

            // Act: raining while hot on a new Wohnung instance
            var rainSensor = new WettersensorMock(30,2, true);
            var rainWohnung = new Wohnung(rainSensor);
            rainWohnung.SetTemperaturvorgabe("Wintergarten",20);
            rainWohnung.SetPersonenImZimmer("Wintergarten", false);
            rainWohnung.GenerateWetterdaten(minute);

            // Assert
            var z2 = rainWohnung.GetZimmer<ZimmerMitMarkisensteuerung>("Wintergarten");
            Assert.IsNotNull(z2);
            Assert.IsTrue(z2.MarkiseOffen);
        }
    }
}
