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
        public Dictionary<string, Ort> Verbindungen { get; set; }
        public int BenoetigtesLevel { get; set; } // Neu: Level-Voraussetzung für diesen Ort

        public Ort(string name, string beschreibung, int benoetigtesLevel = 1)
        {
            Name = name;
            Beschreibung = beschreibung;
            BenoetigtesLevel = benoetigtesLevel;
            Verbindungen = new Dictionary<string, Ort>();
        }
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
                bool zugangErlaubt = true;

                if (zielOrt.Name == "Berg der Unendlichkeit" && spieler.DigimonPartner.Level < 5)
                {
                    zugangErlaubt = false;
                }

                if (zugangErlaubt)
                {
                    Console.WriteLine($"- {richtung} nach {zielOrt.Name}");
                }
                else
                {
                    Console.WriteLine($"- {richtung} nach ??? (Zugang erst ab Level 5 freigeschaltet!)");
                }
            }
        }


        public static Ort ErstelleWelt()
        {
            // Orte mit Level-Beschränkungen
            Ort heimatwald = new Ort("Heimatwald", "Der Ausgangspunkt der Reise.", 1);
            Ort panoramaberg = new Ort("Panoramaberg", "Ein Digimon-Dorf am Fuße des Berges.", 1);
            Ort spielzeugstadt = new Ort("Spielzeugstadt", "Eine bunte Stadt voller Spielzeug.", 1);
            Ort strand = new Ort("Strand von File Island", "Ein stiller Strand mit Telefonzellen.", 1);

            Ort tropendschungel = new Ort("Tropendschungel", "Ein dichter Dschungel.", 3);
            Ort tempelDesDigivices = new Ort("Tempel des Digivices", "Ein mystischer Tempel.", 3);
            Ort fabrikstadt = new Ort("Fabrikstadt", "Industriegebiet der Insel.", 3);

            Ort zahnradsteppe = new Ort("Zahnradsteppe", "Ödes Land voller Strommasten.", 4);
            Ort schattenvilla = new Ort("Schattenvilla", "Eine mysteriöse Ruine.", 4);
            Ort stadtDesEwigenAnfangs = new Ort("Stadt des ewigen Anfangs", "Hier beginnt alles Leben neu.", 4);

            Ort bergDerUnendlichkeit = new Ort("Berg der Unendlichkeit", "Tempel des Endgegners Devimon.", 5);

            // Verbindungen setzen
            heimatwald.Verbindungen["norden"] = panoramaberg;
            heimatwald.Verbindungen["osten"] = spielzeugstadt;
            heimatwald.Verbindungen["westen"] = strand;
            heimatwald.Verbindungen["süden"] = tropendschungel;

            panoramaberg.Verbindungen["süden"] = heimatwald;

            spielzeugstadt.Verbindungen["westen"] = heimatwald;

            strand.Verbindungen["osten"] = heimatwald;

            tropendschungel.Verbindungen["norden"] = heimatwald;
            tropendschungel.Verbindungen["osten"] = tempelDesDigivices;

            tempelDesDigivices.Verbindungen["westen"] = tropendschungel;

            fabrikstadt.Verbindungen["süden"] = zahnradsteppe;
            fabrikstadt.Verbindungen["norden"] = bergDerUnendlichkeit;

            zahnradsteppe.Verbindungen["norden"] = fabrikstadt;

            bergDerUnendlichkeit.Verbindungen["süden"] = fabrikstadt;

            schattenvilla.Verbindungen["osten"] = stadtDesEwigenAnfangs;
            stadtDesEwigenAnfangs.Verbindungen["westen"] = schattenvilla;

            return heimatwald;
        }
    }



}



