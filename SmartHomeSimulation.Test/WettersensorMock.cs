using M320_SmartHome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulation.Test
{
    public class WettersensorMock : IWettersensor
    {
        public double Aussentemperatur { get; set; }
        public double Windgeschwindigkeit { get; set; }
        public bool Regen { get; set; }

        public WettersensorMock(double aussentemperatur, double windgeschwindigkeit, bool regen)
        {
            Aussentemperatur = aussentemperatur;
            Windgeschwindigkeit = windgeschwindigkeit;
            Regen = regen;
        }

        public Wetterdaten GetWetterdaten()
        {
            return new Wetterdaten()
            {
                Aussentemperatur = this.Aussentemperatur,
                Windgeschwindigkeit = this.Windgeschwindigkeit,
                Regen = this.Regen
            };
        }
    }
}
