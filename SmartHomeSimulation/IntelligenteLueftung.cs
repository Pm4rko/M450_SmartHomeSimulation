namespace M320_SmartHome {
    public class IntelligenteLueftung {
        public bool LueftungAktiv { get; private set; }

        private readonly ILogger logger;

        public IntelligenteLueftung(ILogger logger) {
            this.logger = logger;
        }

        public void VerarbeiteWetterdaten(Wetterdaten wetterdaten, double zimmertemperatur, bool personenImZimmer) {
            if (personenImZimmer && !wetterdaten.Regen && zimmertemperatur > wetterdaten.Aussentemperatur) {
                if (!LueftungAktiv) {
                    logger.Log($"Lüftung wird aktiviert.");
                    LueftungAktiv = true;
                }
            }
            else {
                if (LueftungAktiv) {
                    logger.Log($"Lüftung wird deaktiviert.");
                    LueftungAktiv = false;
                }
            }
        }
    }
}
