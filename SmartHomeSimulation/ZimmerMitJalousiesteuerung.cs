namespace M320_SmartHome {
    public class ZimmerMitJalousiesteuerung : ZimmerMitAktor {
        public ZimmerMitJalousiesteuerung(Zimmer zimmer) : base(zimmer) {
        }

        public bool JalousieHeruntergefahren { get; private set; }

        public override void VerarbeiteWetterdaten(Wetterdaten wetterdaten) {
            if(wetterdaten.Aussentemperatur > this.Zimmer.Temperaturvorgabe) {
                // Jalousie schliessen
                if(!this.JalousieHeruntergefahren) {
                    if (this.Zimmer.PersonenImZimmer) {
                        Console.WriteLine($"{this.Name}: Jalousie kann nicht geschlossen werden weil Personen im Zimmer sind.");
                    } else {
                        Console.WriteLine($"{this.Name}: Jalousie wird geschlossen.");
                        JalousieHeruntergefahren = true;
                    }
                }
            } else {
                // Jalousie öffnen
                if (this.JalousieHeruntergefahren) {
                    Console.WriteLine($"{this.Name}: Jalousie wird geöffnet.");
                    JalousieHeruntergefahren = false;
                }
            }

            base.VerarbeiteWetterdaten(wetterdaten);
        }
    }
}
