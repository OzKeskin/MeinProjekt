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

            public Ort(string name, string beschreibung)
            {
                Name = name;
                Beschreibung = beschreibung;
                Verbindungen = new Dictionary<string, Ort>();
            }

            public void Betreten()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n=== Du befindest dich am {Name} ===");
                Console.ResetColor();
                Console.WriteLine($"{Beschreibung}");

                Console.WriteLine("\nVon hier aus kannst du in folgende Richtungen gehen:");
                foreach (var richtung in Verbindungen.Keys)
                {
                    Console.WriteLine($"- {richtung} nach {Verbindungen[richtung].Name}");
                }
            }

            // Die Digiwelt aufbauen
            public static Ort ErstelleWelt()
            {
                // Orte erstellen
                Ort bergDerUnendlichkeit = new Ort("Berg der Unendlichkeit", "Hier befindet sich der Tempel von Devimon. Zentrum der Insel.");
                Ort fabrikstadt = new Ort("Fabrikstadt", "Das Industriegebiet der Insel. Produktion und Demontage von Gütern.");
                Ort heimatwald = new Ort("Heimatwald", "Der Ausgangspunkt der Reise der Digiritter.");
                Ort panoramaberg = new Ort("Panoramaberg", "Am Fuße des Berges liegt ein Digimon-Dorf.");
                Ort schattenvilla = new Ort("Schattenvilla", "Eine Ruine, deren Hologramm eine Falle von Devimon ist.");
                Ort spielzeugstadt = new Ort("Spielzeugstadt", "Eine bunte mittelalterliche Stadt voller Spielzeug.");
                Ort stadtDesEwigenAnfangs = new Ort("Stadt des ewigen Anfangs", "Hier beginnt alles digitale Leben neu.");
                Ort strand = new Ort("Strand von File Island", "Ein stiller Strand mit alten Telefonzellen.");
                Ort tempelDesDigivices = new Ort("Tempel des Digivices", "Ein mystischer Tempel im Tropendschungel.");
                Ort tropendschungel = new Ort("Tropendschungel", "Ein dichter Dschungel mit dem Tempel des Digivices.");
                Ort zahnradsteppe = new Ort("Zahnradsteppe", "Ein ödes Land voller Strommasten und einem Schiffswrack.");

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

                // Start-Ort
                return heimatwald;
            }
        }
    }



