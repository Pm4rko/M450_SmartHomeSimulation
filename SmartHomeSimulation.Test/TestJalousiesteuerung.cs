using M320_SmartHome;

namespace SmartHomeSimulation.Test
{
    [TestClass]
    public class TestJalousiesteuerung
    {
        [TestMethod]
        public void jalTest1()
        {
            int zeit = 30;
            var wettersensor = new WettersensorMock(50, 30, true);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Küche", 20);
            wohnung.SetPersonenImZimmer("Küche", false);

            wohnung.GenerateWetterdaten(zeit);

            var kueche = wohnung.GetZimmer<ZimmerMitJalousiesteuerung>("Küche");

            Assert.AreEqual(kueche.JalousieHeruntergefahren, true);
        }

        [TestMethod]
        public void jalTest2()
        {
            int zeit = 30;
            var wettersensor = new WettersensorMock(50, 30, true);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Küche", 20);
            wohnung.SetPersonenImZimmer("Küche", true);

            wohnung.GenerateWetterdaten(zeit);

            var kueche = wohnung.GetZimmer<ZimmerMitJalousiesteuerung>("Küche");

            Assert.AreEqual(kueche.JalousieHeruntergefahren, true);
        }

        [TestMethod]
        public void jalTest3()
        {
            int zeit = 30;
            var wettersensor = new WettersensorMock(-50, 30, true);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Küche", 20);
            wohnung.SetPersonenImZimmer("Küche", false);

            wohnung.GenerateWetterdaten(zeit);

            var kueche = wohnung.GetZimmer<ZimmerMitJalousiesteuerung>("Küche");

            Assert.AreEqual(kueche.JalousieHeruntergefahren, false);
        }

        [TestMethod]
        public void jalTest4()
        {
            int zeit = 30;
            var wettersensor = new WettersensorMock(-50, 30, true);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Küche", 20);
            wohnung.SetPersonenImZimmer("Küche", true);

            wohnung.GenerateWetterdaten(zeit);

            var kueche = wohnung.GetZimmer<ZimmerMitJalousiesteuerung>("Küche");

            Assert.AreEqual(kueche.JalousieHeruntergefahren, false);
        }
    }
}