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

                Console.WriteLine("\n[Tippe eine Richtung ein, 'm' für Menü oder 'exit' zum Verlassen]");
                Console.Write("Eingabe: ");
                eingabe = Console.ReadLine()?.ToLower() ?? "";

                if (eingabe == "m")
                {
                    ZeigeSpielerMenue(spieler);
                    continue;
                }

                if (spieler.AktuellerOrt.Verbindungen.ContainsKey(eingabe))
                {
                    spieler.AktuellerOrt = spieler.AktuellerOrt.Verbindungen[eingabe];
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
        private static void ZeigeSpielerMenue(Spieler spieler)
        {
            string eingabe = "";

            while (eingabe != "5")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n=== SPIELER MENÜ ===");
                Console.ResetColor();
                Console.WriteLine("[1] Inventar anzeigen");
                Console.WriteLine("[2] Profil anzeigen");
                Console.WriteLine("[3] Spiel speichern");
                Console.WriteLine("[4] Spiel laden");
                Console.WriteLine("[5] Zurück zur Digiwelt");

                Console.Write("\nDeine Wahl: ");
                eingabe = Console.ReadLine() ?? "";

                switch (eingabe)
                {
                    case "1":
                        ZeigeInventar(spieler);
                        break;
                    case "2":
                        spieler.ZeigeProfil();
                        Pause();
                        break;
                    case "3":
                        // Platzhalter für Speicherfunktion
                        Console.WriteLine(">> Spiel speichern... (Funktion noch nicht implementiert)");
                        Pause();
                        break;
                    case "4":
                        // Platzhalter für Ladefunktion
                        Console.WriteLine(">> Spiel laden... (Funktion noch nicht implementiert)");
                        Pause();
                        break;
                    case "5":
                        Console.WriteLine(">> Zurück zur Digiwelt...");
                        break;
                    default:
                        Console.WriteLine("Ungültige Eingabe.");
                        Pause();
                        break;
                }
            }
        }

        private static void ZeigeInventar(Spieler spieler)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== INVENTAR ===");
            Console.ResetColor();

            if (spieler.Inventar.Count == 0)
            {
                Console.WriteLine("Dein Inventar ist leer.");
            }
            else
            {
                foreach (var item in spieler.Inventar)
                {
                    Console.WriteLine($"- {item}");
                }
            }
            Pause();
        }

        private static void Pause()
        {
            Console.WriteLine("\nDrücke [ENTER], um fortzufahren...");
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

