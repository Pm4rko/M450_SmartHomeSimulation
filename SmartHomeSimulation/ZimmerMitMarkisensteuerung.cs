namespace M320_SmartHome {
    public class ZimmerMitMarkisensteuerung : ZimmerMitAktor {
        public ZimmerMitMarkisensteuerung(Zimmer zimmer) : base(zimmer) {
        }

        public bool MarkiseOffen { get; private set; }

        public override void VerarbeiteWetterdaten(Wetterdaten wetterdaten) {
            if(wetterdaten.Aussentemperatur > this.Zimmer.Temperaturvorgabe) {
                // Markise schliessen
                if(this.MarkiseOffen) {
                    if (wetterdaten.Regen) {
                        Console.WriteLine($"{this.Name}: Markise kann nicht geschlossen werden weils regnet.");
                    } else {
                        Console.WriteLine($"{this.Name}: Markise wird geschlossen.");
                        MarkiseOffen = false;
                    }
                } else if(wetterdaten.Regen) {
                    Console.WriteLine($"{this.Name}: Markise wird geöffnet weils regnet.");
                    MarkiseOffen = true;
                }
            } else {
                // Markise öffnen
                if (!this.MarkiseOffen) {
                    Console.WriteLine($"{this.Name}: Markise wird geöffnet.");
                    MarkiseOffen = true;
                }
            }

            base.VerarbeiteWetterdaten(wetterdaten);
        }
    }
}
