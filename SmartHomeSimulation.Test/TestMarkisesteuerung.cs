using M320_SmartHome;

namespace SmartHomeSimulation.Test
{
    [TestClass]
    public class TestMarkisesteuerung
    {
        [TestMethod]
        public void markiseTest1()
        {
            int zeit = 30;
            var wettersensor = new WettersensorMock(50, 50, false);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Wintergarten", 20);
            wohnung.SetPersonenImZimmer("Wintergarten", false);

            wohnung.GenerateWetterdaten(zeit);

            var wintergarten = wohnung.GetZimmer<ZimmerMitMarkisensteuerung>("Wintergarten");

            Assert.AreEqual(wintergarten.MarkiseOffen, false);
        }

        [TestMethod]
        public void markiseTest2()
        {
            int zeit = 30;
            var wettersensor = new WettersensorMock(-50, 50, false);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Wintergarten", 20);
            wohnung.SetPersonenImZimmer("Wintergarten", false);

            wohnung.GenerateWetterdaten(zeit);

            var wintergarten = wohnung.GetZimmer<ZimmerMitMarkisensteuerung>("Wintergarten");

            Assert.AreEqual(wintergarten.MarkiseOffen, false);
        }

        [TestMethod]
        public void markiseTest3()
        {
            int zeit = 30;
            var wettersensor = new WettersensorMock(50, -50, false);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Wintergarten", 20);
            wohnung.SetPersonenImZimmer("Wintergarten", false);

            wohnung.GenerateWetterdaten(zeit);

            var wintergarten = wohnung.GetZimmer<ZimmerMitMarkisensteuerung>("Wintergarten");

            Assert.AreEqual(wintergarten.MarkiseOffen, false);
        }

        [TestMethod]
        public void markiseTest4()
        {
            int zeit = 30;
            var wettersensor = new WettersensorMock(-50, -50, false);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Wintergarten", 20);
            wohnung.SetPersonenImZimmer("Wintergarten", false);

            wohnung.GenerateWetterdaten(zeit);

            var wintergarten = wohnung.GetZimmer<ZimmerMitMarkisensteuerung>("Wintergarten");

            Assert.AreEqual(wintergarten.MarkiseOffen, true);
        }
    }
}