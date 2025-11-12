namespace M320_SmartHome {
    public class Wohnung {
        public List<Zimmer> zimmerList { get; set; }
        private IWettersensor wettersensor;
        private readonly ILogger logger;

        public Wohnung(IWettersensor wettersensor, ILogger? logger = null) {
            this.wettersensor = wettersensor;
            this.logger = logger ?? new ConsoleFileLogger();

            zimmerList = new List<Zimmer>();
            this.zimmerList.Add(new ZimmerMitIntelligenterLueftung(new ZimmerMitHeizungsventil(new BadWC()), this.logger));
            this.zimmerList.Add(new ZimmerMitJalousiesteuerung(new ZimmerMitHeizungsventil(new Kueche())));
            this.zimmerList.Add(new ZimmerMitIntelligenterLueftung(new ZimmerMitJalousiesteuerung(new ZimmerMitHeizungsventil(new Schlafzimmer())), this.logger));
            this.zimmerList.Add(new ZimmerMitJalousiesteuerung(new ZimmerMitMarkisensteuerung(new Wintergarten())));
            this.zimmerList.Add(new ZimmerMitIntelligenterLueftung(new ZimmerMitJalousiesteuerung(new ZimmerMitHeizungsventil(new Wohnzimmer())), this.logger));
        }

        public void SetTemperaturvorgabe(string zimmername, double temperaturvorgabe) {
            var zimmer = this.zimmerList.FirstOrDefault(x => x.Name == zimmername);
            if (zimmer != null) {
                zimmer.Temperaturvorgabe = temperaturvorgabe;
            }
        }

        public void SetPersonenImZimmer(string zimmername, bool personenImZimmer) {
            var zimmer = this.zimmerList.FirstOrDefault(x => x.Name == zimmername);
            if (zimmer != null) {
                zimmer.PersonenImZimmer = personenImZimmer;
            }
        }

        public void GenerateWetterdaten(int minute = 1) {
            var wetterdaten = this.wettersensor.GetWetterdaten();

            Console.WriteLine($"\n*** Minute {minute}, Verarbeite Wetterdaten:\n    Aussentemperatur: {wetterdaten.Aussentemperatur}°C\n    Regen: {(wetterdaten.Regen ? "ja" : "nein")}\n    Windgeschwindigkeit: {wetterdaten.Windgeschwindigkeit}km/h");
            foreach (var zimmer in this.zimmerList) {
                zimmer.VerarbeiteWetterdaten(wetterdaten);
            }
        }

        public T? GetZimmer<T>(string zimmername) where T : Zimmer {
            var zimmer = this.zimmerList.FirstOrDefault(x => x.Name == zimmername);
            if (zimmer is ZimmerMitAktor) {
                return ((ZimmerMitAktor)zimmer).GetZimmerMitAktor<T>();
            }
            return zimmer as T;
        }
    }
}
