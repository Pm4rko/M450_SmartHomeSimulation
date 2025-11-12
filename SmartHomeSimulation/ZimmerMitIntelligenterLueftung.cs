namespace M320_SmartHome {
    public class ZimmerMitIntelligenterLueftung : ZimmerMitAktor {
        public ZimmerMitIntelligenterLueftung(Zimmer zimmer, ILogger logger) : base(zimmer) {
            IntelligenteLueftung = new IntelligenteLueftung(logger);
        }

        public IntelligenteLueftung IntelligenteLueftung { get; }

        public override void VerarbeiteWetterdaten(Wetterdaten wetterdaten) {
            IntelligenteLueftung.VerarbeiteWetterdaten(wetterdaten, this.Zimmer.Temperaturvorgabe, this.Zimmer.PersonenImZimmer);
            base.VerarbeiteWetterdaten(wetterdaten);
        }
    }
}
