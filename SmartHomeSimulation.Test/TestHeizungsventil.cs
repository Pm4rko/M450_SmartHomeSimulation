using M320_SmartHome;

namespace SmartHomeSimulation.Test
{
    [TestClass]
    public class TestHeizungsventil
    {
        [TestMethod]
        public void heizTest1()
        {
            int zeit = 30;
            var wettersensor = new WettersensorMock(50, 18.5, true);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Wohnzimmer", 20);
            wohnung.SetPersonenImZimmer("Wohnzimmer", false);

            wohnung.GenerateWetterdaten(zeit);

            var wohnzimmer = wohnung.GetZimmer<ZimmerMitHeizungsventil>("Wohnzimmer");

            Assert.AreEqual(wohnzimmer.HeizungsventilOffen, true);
        }

        [TestMethod]
        public void heizTest2()
        {
            int zeit = 30;
            var wettersensor = new WettersensorMock(-50, 18.5, true);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Wohnzimmer", 20);
            wohnung.SetPersonenImZimmer("Wohnzimmer", false);

            wohnung.GenerateWetterdaten(zeit);

            var wohnzimmer = wohnung.GetZimmer<ZimmerMitHeizungsventil>("Wohnzimmer");

            Assert.AreEqual(wohnzimmer.HeizungsventilOffen, true);
        }

        [TestMethod]
        public void heizTest3()
        {
            int zeit = 30;
            var wettersensor = new WettersensorMock(-50, 18.5, true);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Wohnzimmer", 20);
            wohnung.SetPersonenImZimmer("Wohnzimmer", true);

            wohnung.GenerateWetterdaten(zeit);

            var wohnzimmer = wohnung.GetZimmer<ZimmerMitHeizungsventil>("Wohnzimmer");

            Assert.AreEqual(wohnzimmer.HeizungsventilOffen, true);
        }

        [TestMethod]
        public void heizTest4()
        {
            int zeit = 30;
            var wettersensor = new WettersensorMock(50, 18.5, true);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Wohnzimmer", 20);
            wohnung.SetPersonenImZimmer("Wohnzimmer", true);

            wohnung.GenerateWetterdaten(zeit);

            var wohnzimmer = wohnung.GetZimmer<ZimmerMitHeizungsventil>("Wohnzimmer");

            Assert.AreEqual(wohnzimmer.HeizungsventilOffen, false);
        }
    }
}