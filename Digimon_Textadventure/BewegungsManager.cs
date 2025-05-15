using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon_Textadventure
{
    public static class BewegungsManager
    {
        private static Random random = new Random();

        public static void BewegeSpieler(Spieler spieler)
        {
            string eingabe = "";

            while (eingabe != "exit")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nAktueller Ort: {spieler.AktuellerOrt.Name}");
                Console.ResetColor();
                Console.WriteLine(spieler.AktuellerOrt.Beschreibung);

                Console.WriteLine("\nMögliche Richtungen:");
                foreach (var richtung in spieler.AktuellerOrt.Verbindungen.Keys)
                {
                    Ort zielOrt = spieler.AktuellerOrt.Verbindungen[richtung];
                    bool zugangErlaubt = !(zielOrt.Name == "Berg der Unendlichkeit" && spieler.DigimonPartner.Level < 5);

                    if (zugangErlaubt)
                        Console.WriteLine($"- {richtung} nach {zielOrt.Name}");
                    else
                        Console.WriteLine($"- {richtung} nach ??? (ab Level 5)");
                }

                Console.WriteLine("\nWohin möchtest du gehen? (oder 'exit' zum Verlassen)");
                Console.Write("Eingabe: ");
                eingabe = Console.ReadLine()?.ToLower() ?? "";

                if (spieler.AktuellerOrt.Verbindungen.ContainsKey(eingabe))
                {
                    Ort zielOrt = spieler.AktuellerOrt.Verbindungen[eingabe];

                    // Zugang prüfen
                    if (zielOrt.Name == "Berg der Unendlichkeit" && spieler.DigimonPartner.Level < 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n>> Du benötigst mindestens Level 5, um den Endboss zu betreten!");
                        Console.ResetColor();
                        Console.WriteLine("Drücke [ENTER], um fortzufahren...");
                        Console.ReadLine();
                        continue;
                    }

                    spieler.AktuellerOrt = zielOrt;
                    LoeseZufallsEreignisAus(spieler);
                }
                else if (eingabe != "exit")
                {
                    Console.WriteLine("\nUngültige Richtung. Drücke [ENTER], um es erneut zu versuchen...");
                    Console.ReadLine();
                }
            }

            Console.WriteLine("\nDu verlässt die Digiwelt. Drücke [ENTER], um das Spiel zu beenden...");
            Console.ReadLine();
        }

        private static void LoeseZufallsEreignisAus(Spieler spieler)
        {
            int ereignis = random.Next(1, 101);

            if (ereignis <= 30)
            {
                // Kampf
                Console.WriteLine("\nEin wildes Digimon erscheint!");
                Gegner gegner = Gegner.ErstelleZufaelligenGegner();
                Kampf kampf = new Kampf(spieler, gegner.Digimon);
                kampf.StarteKampf();
            }
            else if (ereignis <= 60)
            {
                // Item-Fund
                string gefundenesItem = "Heiltrank";
                Console.WriteLine($"\nDu hast ein {gefundenesItem} gefunden!");
                spieler.ItemHinzufuegen(gefundenesItem);
                Console.WriteLine("Drücke [ENTER], um weiterzugehen...");
                Console.ReadLine();
            }
            else
            {
                // Nichts passiert
                Console.WriteLine("\nEs passiert nichts Besonderes.");
                Console.WriteLine("Drücke [ENTER], um weiterzugehen...");
                Console.ReadLine();
            }
        }
    }

}

