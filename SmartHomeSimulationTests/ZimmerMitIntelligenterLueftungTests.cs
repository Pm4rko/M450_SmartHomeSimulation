namespace M320_SmartHome.Tests {

    [TestClass]
    public class ZimmerMitIntelligenterLueftungTests {
        [TestMethod]
        public void VerarbeiteWetterdaten_TempHigherThanOutside_WithPerson_NoRain_ActivatesLueftung() {
            // Arrange
            int minute = 1;
            var wettersensor = new WettersensorMock(15, 2, false);
            var wohnung = new Wohnung(wettersensor);
            wohnung.SetTemperaturvorgabe("Schlafen", 20);
            wohnung.SetPersonenImZimmer("Schlafen", true);

            // Act
            wohnung.GenerateWetterdaten(minute);

            // Assert
            var z = wohnung.GetZimmer<ZimmerMitIntelligenterLueftung>("Schlafen");
            Assert.IsNotNull(z);
            Assert.IsTrue(z.IntelligenteLueftung.LueftungAktiv);
        }

        [TestMethod]
        public void VerarbeiteWetterdaten_TempLowerThanOutside_DoesNotActivateLueftung() {
            // Arrange
            int minute = 1;
            var wettersensor = new WettersensorMock(25, 2, false);
            var wohnung = new Wohnung(wettersensor);
            wohnung.SetTemperaturvorgabe("Schlafen", 20);
            wohnung.SetPersonenImZimmer("Schlafen", true);

            // Act
            wohnung.GenerateWetterdaten(minute);

            // Assert
            var z = wohnung.GetZimmer<ZimmerMitIntelligenterLueftung>("Schlafen");
            Assert.IsNotNull(z);
            Assert.IsFalse(z.IntelligenteLueftung.LueftungAktiv);
        }

        [TestMethod]
        public void VerarbeiteWetterdaten_WithRain_DoesNotActivateLueftung() {
            // Arrange
            int minute = 1;
            var wettersensor = new WettersensorMock(15, 2, true);
            var wohnung = new Wohnung(wettersensor);
            wohnung.SetTemperaturvorgabe("Schlafen", 20);
            wohnung.SetPersonenImZimmer("Schlafen", true);

            // Act
            wohnung.GenerateWetterdaten(minute);

            // Assert
            var z = wohnung.GetZimmer<ZimmerMitIntelligenterLueftung>("Schlafen");
            Assert.IsNotNull(z);
            Assert.IsFalse(z.IntelligenteLueftung.LueftungAktiv);
        }

        [TestMethod]
        public void VerarbeiteWetterdaten_NoPerson_DoesNotActivateLueftung() {
            // Arrange
            int minute = 1;
            var wettersensor = new WettersensorMock(15, 2, false);
            var wohnung = new Wohnung(wettersensor);
            wohnung.SetTemperaturvorgabe("Schlafen", 20);
            wohnung.SetPersonenImZimmer("Schlafen", false);

            // Act
            wohnung.GenerateWetterdaten(minute);

            // Assert
            var z = wohnung.GetZimmer<ZimmerMitIntelligenterLueftung>("Schlafen");
            Assert.IsNotNull(z);
            Assert.IsFalse(z.IntelligenteLueftung.LueftungAktiv);
        }
    }
}
