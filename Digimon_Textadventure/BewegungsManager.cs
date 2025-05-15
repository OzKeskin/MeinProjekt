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

        //  Korrigiertes Spieler-Menü
        public static void ZeigeSpielerMenue(Spieler spieler)
        {
            bool imMenue = true;

            while (imMenue)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n==== SPIELER-MENÜ ====");
                Console.ResetColor();
                Console.WriteLine("[1] Inventar anzeigen");
                Console.WriteLine("[2] Digimon-Profil anzeigen");
                Console.WriteLine("[3] Spielstand speichern");
                Console.WriteLine("[4] Zurück zur Digiwelt");
                Console.Write("\nDeine Wahl: ");

                string eingabe = Console.ReadLine() ?? "";

                switch (eingabe)
                {
                    case "1":
                        spieler.ZeigeInventar(); //  Diese Methode muss in Spieler vorhanden sein
                        break;
                    case "2":
                        if (spieler.DigimonPartner != null)
                        {
                            spieler.DigimonPartner.ZeigeProfil();
                        }
                        else
                        {
                            Console.WriteLine("\nDu hast aktuell keinen Digimon-Partner.");
                        }
                        Pause();
                        break;
                    case "3":
                        SpeicherManager.Speichern(spieler);
                        break;
                    case "4":
                        imMenue = false;
                        break;
                    default:
                        Console.WriteLine("Ungültige Eingabe.");
                        Pause();
                        break;
                }
            }
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
                Console.WriteLine("\nEin wildes Digimon erscheint!");
                Gegner gegner = Gegner.ErstelleZufaelligenGegner();
                Kampf kampf = new Kampf(spieler, gegner.Digimon, spieler.DigimonPartner);
                kampf.StarteKampf();
            }
            else if (ereignis <= 60)
            {
                string gefundenesItem = "Heiltrank";
                Console.WriteLine($"\nDu hast ein {gefundenesItem} gefunden!");
                spieler.ItemHinzufuegen(gefundenesItem);
                Pause();
            }
            else
            {
                Console.WriteLine("\nEs passiert nichts Besonderes.");
                Pause();
            }
        }
    }


}

