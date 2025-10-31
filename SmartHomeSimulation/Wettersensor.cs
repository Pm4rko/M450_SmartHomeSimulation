namespace M320_SmartHome {
    public class Wettersensor : IWettersensor
    {
        /// <summary>
        /// makes a random number
        /// </summary>
        private Random random;
        /// <summary>
        /// creates a double with the current temperature
        /// </summary>
        private double currentTemp;
        /// <summary>
        /// creates a max and min temperature
        /// </summary>
        private const int MAX_TEMP = 35;
        private const int MIN_TEMP = -25;
        public Wettersensor() {
            this.random = new Random(Guid.NewGuid().GetHashCode());
            this.currentTemp = random.Next(MIN_TEMP, MAX_TEMP);
        }

        public Wetterdaten GetWetterdaten() {
            var rand = this.random.NextDouble();

            var regen = rand > 0.5;

            var deltaTemp = rand * (regen ? -1 : 1);
            var newTemp = this.currentTemp + deltaTemp;
            if(newTemp < MIN_TEMP || newTemp > MAX_TEMP) {
                deltaTemp *= -1;
            }
            this.currentTemp += deltaTemp;
            this.currentTemp = Math.Round(this.currentTemp, 1);
            var wind = Math.Round(35d * rand, 1);
           

            return new Wetterdaten { Aussentemperatur = this.currentTemp, Windgeschwindigkeit = wind, Regen = regen };
        }
    }



}
