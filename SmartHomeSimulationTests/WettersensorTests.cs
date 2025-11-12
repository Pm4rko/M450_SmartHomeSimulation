namespace M320_SmartHome.Tests {

    [TestClass]
    public class WettersensorTests {
        [TestMethod]
        public void WettersensorMock_ReturnsConfiguredValues() {
            // Arrange
            var mock = new WettersensorMock(18.5, 5, true);
            var wohnung = new Wohnung(mock);

            // Act
            wohnung.GenerateWetterdaten(1);

            // Assert
            var data = mock.GetWetterdaten();
            Assert.AreEqual(18.5, data.Aussentemperatur);
            Assert.AreEqual(5, data.Windgeschwindigkeit);
            Assert.IsTrue(data.Regen);
        }

        [TestMethod]
        public void Wettersensor_GetWetterdaten_ProducesValuesWithinBounds() {
            // Arrange
            var sensor = new Wettersensor();

            // Act & Assert
            for (int i = 0; i < 100; i++) {
                var d = sensor.GetWetterdaten();
                Assert.IsTrue(d.Aussentemperatur >= -25 && d.Aussentemperatur <= 35);
                Assert.IsTrue(d.Windgeschwindigkeit >= 0 && d.Windgeschwindigkeit <= 35);
            }
        }
    }
}
