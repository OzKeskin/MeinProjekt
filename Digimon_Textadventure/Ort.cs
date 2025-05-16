using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Digimon_Textadventure;



    namespace Digimon_Textadventure
    {
    public class Ort(string name, string beschreibung, int benoetigtesLevel = 1)
    {
        public string Name { get; } = name;
        public string Beschreibung { get; } = beschreibung;
        public int BenoetigtesLevel { get; } = benoetigtesLevel;
        public Dictionary<string, Ort> Verbindungen { get; } = new();

        // Ort betreten Methode (Anzeige mit Level-Sperre)
        public void Betreten(Spieler spieler)
        {
            BewegungsManager.PrüfeDevimonBegegnung(spieler);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n=== Du befindest dich am {Name} ===");
            Console.ResetColor();
            Console.WriteLine($"{Beschreibung}");

            // Pfad nur ab Level 5 anzeigen
            if (spieler.DigimonPartner?.Level == 5)
            {
                ZeigeRichtungspfad();
            }

            Console.WriteLine("\nVon hier aus kannst du in folgende Richtungen gehen:");
            foreach (var richtung in Verbindungen.Keys)
            {
                var zielOrt = Verbindungen[richtung];
                bool zugangErlaubt = spieler.DigimonPartner != null && spieler.DigimonPartner.Level >= zielOrt.BenoetigtesLevel;

                if (zielOrt.Name == "Berg der Unendlichkeit" && spieler.DigimonPartner.Level < 5)
                {
                    zugangErlaubt = false;
                }

                if (zugangErlaubt)
                    Console.WriteLine($"- {richtung} nach {zielOrt.Name}");
                else
                    Console.WriteLine($"- {richtung} nach ??? (Zugang erst ab Level 5 freigeschaltet!)");
            }
        }

        // Der Richtungspfad zum Endboss-Ort
        private void ZeigeRichtungspfad()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n>> Richtungspfad zum Berg der Unendlichkeit:");
            Console.WriteLine("Heimatwald → Panoramaberg → Fabrikstadt → Berg der Unendlichkeit");
            Console.ResetColor();
        }

        public static Ort ErstelleWelt()
        {
            // Orte erstellen
            Ort heimatwald = new ("Heimatwald", "Der Ausgangspunkt deiner Reise durch die Digiwelt.");
            Ort panoramaberg = new ("Panoramaberg", "Von hier aus hast du einen großartigen Blick auf die Digiwelt.");
            Ort strand = new ("Strand von File Island", "Ein ruhiger Strand mit dem Rauschen der digitalen Wellen.");
            Ort spielzeugstadt = new ("Spielzeugstadt", "Eine bunte Stadt voller Rätsel und versteckter Schätze.");
            Ort tropendschungel = new ("Tropendschungel", "Ein dichter Dschungel voller geheimnisvoller Digimon.");
            Ort tempelDesDigivices = new ("Tempel des Digivices", "Ein uralter Tempel, der große Geheimnisse birgt.");
            Ort fabrikstadt = new ("Fabrikstadt", "Hier werden mächtige Digimon erschaffen.");
            Ort zahnradsteppe = new ("Zahnradsteppe", "Ein ödes, windgepeitschtes Land mit alten Maschinen.");
            Ort schattenvilla = new ("Schattenvilla", "Eine düstere Ruine, in der Gefahren lauern.");
            Ort stadtDesEwigenAnfangs = new ("Stadt des ewigen Anfangs", "Hier beginnt jedes digitale Leben von Neuem.");

            Ort bergDerUnendlichkeit = new ("Berg der Unendlichkeit", "Hier erwartet dich der finale Kampf gegen Devimon!",5);

            // Verbindungen setzen (logisch mit Rückwegen)
            heimatwald.Verbindungen["norden"] = panoramaberg;
            heimatwald.Verbindungen["osten"] = spielzeugstadt;
            heimatwald.Verbindungen["westen"] = strand;
            heimatwald.Verbindungen["süden"] = tropendschungel;

            panoramaberg.Verbindungen["süden"] = heimatwald;
            panoramaberg.Verbindungen["osten"] = schattenvilla;
            panoramaberg.Verbindungen["norden"] = fabrikstadt;

            strand.Verbindungen["osten"] = heimatwald;
            spielzeugstadt.Verbindungen["westen"] = heimatwald;

            tropendschungel.Verbindungen["norden"] = heimatwald;
            tropendschungel.Verbindungen["osten"] = tempelDesDigivices;

            tempelDesDigivices.Verbindungen["westen"] = tropendschungel;
            tempelDesDigivices.Verbindungen["norden"] = fabrikstadt;

            fabrikstadt.Verbindungen["süden"] = tempelDesDigivices;
            fabrikstadt.Verbindungen["norden"] = bergDerUnendlichkeit;
            fabrikstadt.Verbindungen["westen"] = zahnradsteppe;

            zahnradsteppe.Verbindungen["osten"] = fabrikstadt;

            schattenvilla.Verbindungen["westen"] = panoramaberg;
            schattenvilla.Verbindungen["osten"] = stadtDesEwigenAnfangs;

            stadtDesEwigenAnfangs.Verbindungen["westen"] = schattenvilla;

            bergDerUnendlichkeit.Verbindungen["süden"] = fabrikstadt;

            // Start-Ort festlegen
            return heimatwald;
        }
        
        public static Ort FindeOrtNachName(string name)
        {
            var startOrt = ErstelleWelt(); // Weltstruktur aufbauen
            return SucheOrtRekursiv(startOrt, name);
        }
        
        private static Ort SucheOrtRekursiv(Ort ort, string name)
        {
            if (ort.Name == name) return ort;

            foreach (var verbindung in ort.Verbindungen.Values)
            {
                var gefundenerOrt = SucheOrtRekursiv(verbindung, name);
                if (gefundenerOrt != null) return gefundenerOrt;
            }

            return null;
        }
    }
}



