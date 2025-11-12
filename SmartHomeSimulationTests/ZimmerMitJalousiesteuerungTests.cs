namespace M320_SmartHome.Tests {

    [TestClass]
    public class ZimmerMitJalousiesteuerungTests {
        [TestMethod]
        public void VerarbeiteWetterdaten_TemperatureAboveSetpoint_NoPeople_ClosesJalousie() {
            // Arrange
            int minute = 5;
            var wettersensor = new WettersensorMock(30, 2, false);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Schlafen", 20);
            wohnung.SetPersonenImZimmer("Schlafen", false);

            // Act
            wohnung.GenerateWetterdaten(minute);

            // Assert
            var z = wohnung.GetZimmer<ZimmerMitJalousiesteuerung>("Schlafen");
            Assert.IsNotNull(z);
            Assert.IsTrue(z.JalousieHeruntergefahren);
        }

        [TestMethod]
        public void VerarbeiteWetterdaten_TemperatureAboveSetpoint_WithPeople_DoesNotCloseJalousie() {
            // Arrange
            int minute = 5;
            var wettersensor = new WettersensorMock(30, 2, false);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Schlafen", 20);
            wohnung.SetPersonenImZimmer("Schlafen", true);

            // Act
            wohnung.GenerateWetterdaten(minute);

            // Assert
            var z = wohnung.GetZimmer<ZimmerMitJalousiesteuerung>("Schlafen");
            Assert.IsNotNull(z);
            Assert.IsFalse(z.JalousieHeruntergefahren);
        }

        [TestMethod]
        public void VerarbeiteWetterdaten_TemperatureBelowSetpoint_OpensJalousie() {
            // Arrange
            int minute = 5;
            var warmSensor = new WettersensorMock(30, 2, false);
            var warmWohnung = new Wohnung(warmSensor);
            warmWohnung.SetTemperaturvorgabe("Schlafen", 20);
            warmWohnung.SetPersonenImZimmer("Schlafen", false);
            warmWohnung.GenerateWetterdaten(minute);
            var z = warmWohnung.GetZimmer<ZimmerMitJalousiesteuerung>("Schlafen");
            Assert.IsTrue(z?.JalousieHeruntergefahren);

            // Act
            var coldSensor = new WettersensorMock(10, 1, false);
            var coldWohnung = new Wohnung(coldSensor);
            coldWohnung.SetTemperaturvorgabe("Schlafen", 20);
            coldWohnung.SetPersonenImZimmer("Schlafen", false);
            coldWohnung.GenerateWetterdaten(minute);

            // Assert
            var z2 = coldWohnung.GetZimmer<ZimmerMitJalousiesteuerung>("Schlafen");
            Assert.IsNotNull(z2);
            Assert.IsFalse(z2.JalousieHeruntergefahren);
        }
    }
}
