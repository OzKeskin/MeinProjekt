using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digimon_Textadventure;



    namespace Digimon_Textadventure
    {
    public class Ort
    {
        public string Name { get; set; } 
        public string Beschreibung { get; set; } 
        public Dictionary<string, Ort> Verbindungen { get; set; } = [];
        // In der Klasse Ort
        public int BenoetigtesLevel { get; set; } = 1; // Standardmäßig frei zugänglich

        // Konstruktor
        public Ort(string name, string beschreibung, int benoetigtesLevel = 1)
        {
            Name = name;
            Beschreibung = beschreibung;
            BenoetigtesLevel = benoetigtesLevel;
        }



        // Muss noch verwiesen werden
        // Ort betreten Methode (Anzeige mit Level-Sperre)
        public void Betreten(Spieler spieler)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n=== Du befindest dich am {Name} ===");
            Console.ResetColor();
            Console.WriteLine($"{Beschreibung}");

            Console.WriteLine("\nVon hier aus kannst du in folgende Richtungen gehen:");

            foreach (var richtung in Verbindungen.Keys)
            {
                var zielOrt = Verbindungen[richtung];
                bool zugangErlaubt = spieler.DigimonPartner != null && spieler.DigimonPartner.Level >= zielOrt.BenoetigtesLevel;

                if (zugangErlaubt)
                {
                    Console.WriteLine($"- {richtung} nach {zielOrt.Name} (ab Level {zielOrt.BenoetigtesLevel})");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"- {richtung} nach ??? (ab Level {zielOrt.BenoetigtesLevel} freigeschaltet!)");
                    Console.ResetColor();
                }
            }
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

    }



}



