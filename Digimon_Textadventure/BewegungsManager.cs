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
                Console.WriteLine($"Aktueller Ort: {spieler.AktuellerOrt.Name}");
                Console.ResetColor();
                Console.WriteLine(spieler.AktuellerOrt.Beschreibung);

                Console.WriteLine("\nMögliche Richtungen:");
                foreach (var richtung in spieler.AktuellerOrt.Verbindungen.Keys)
                {
                    Console.WriteLine($"- {richtung}");
                }

                Console.WriteLine("\nWohin möchtest du gehen? (oder 'exit' zum Verlassen)");
                Console.Write("Eingabe: ");
                eingabe = Console.ReadLine()?.ToLower() ?? "";

                if (spieler.AktuellerOrt.Verbindungen.ContainsKey(eingabe))
                {
                    spieler.AktuellerOrt = spieler.AktuellerOrt.Verbindungen[eingabe];

                    // Zufallsereignis auslösen
                    LoeseZufallsEreignisAus(spieler);
                }
                else if (eingabe != "exit")
                {
                    Console.WriteLine("Ungültige Richtung. Drücke [ENTER]...");
                    Console.ReadLine();
                }
            }

            Console.WriteLine("\nDu verlässt die Digiwelt. Drücke [ENTER]...");
            Console.ReadLine();
        }

        private static void LoeseZufallsEreignisAus(Spieler spieler)
        {
            int ereignis = random.Next(1, 101); // 1 bis 100

            if (ereignis <= 30)
            {
                // Kampf (30% Wahrscheinlichkeit)
                Console.WriteLine("\nEin wildes Digimon erscheint!");
                Gegner gegner = Gegner.ErstelleZufaelligenGegner();
                Kampf kampf = new Kampf(spieler.DigimonPartner, gegner.Digimon);
                kampf.StarteKampf();
            }
            else if (ereignis <= 60)
            {
                // Item-Fund (30% Wahrscheinlichkeit)
                string gefundenesItem = "Heiltrank";
                Console.WriteLine($"\nDu hast ein {gefundenesItem} gefunden!");
                spieler.ItemHinzufuegen(gefundenesItem);
                Console.WriteLine("Drücke [ENTER], um weiterzugehen...");
                Console.ReadLine();
            }
            else
            {
                // Nichts passiert (40% Wahrscheinlichkeit)
                Console.WriteLine("\nDer Ort ist ruhig. Es passiert nichts Besonderes.");
                Console.WriteLine("Drücke [ENTER], um weiterzugehen...");
                Console.ReadLine();
            }
        }
    }


}

