using M320_SmartHome;

namespace M320_SmartHome.Tests
{
    public class WettersensorMock : IWettersensor
    {
        private readonly double temp;
        private readonly double wind;
        private readonly bool regen;

        public WettersensorMock(double temp, double wind, bool regen)
        {
            this.temp = temp;
            this.wind = wind;
            this.regen = regen;
        }

        public Wetterdaten GetWetterdaten()
        {
            return new Wetterdaten { Aussentemperatur = this.temp, Windgeschwindigkeit = this.wind, Regen = this.regen };
        }
    }
}
