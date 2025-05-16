using System;
using System.Collections.Generic;

namespace Digimon_Textadventure
{
    public static class BewegungsManager
    {
        private static  Random random = new ();

        public static void BewegeSpieler(Spieler spieler)
        {
            if (spieler == null)
            {
                Console.WriteLine("Fehler: Spieler-Objekt ist null.");
                return;
            }

            string eingabe = "";

            while (eingabe != "exit")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Aktueller Ort: {spieler.AktuellerOrt?.Name ?? "Unbekannt"}");
                Console.ResetColor();
                Console.WriteLine(spieler.AktuellerOrt?.Beschreibung ?? "Unbekannt");

                PrüfeAmulettGeschichten(spieler);

                if (spieler.AktuellerOrt != null)
                {
                    Console.WriteLine("\nVon hier aus kannst du in folgende Richtungen gehen:");
                    foreach (var richtung in spieler.AktuellerOrt.Verbindungen.Keys)
                    {
                        var zielOrt = spieler.AktuellerOrt.Verbindungen[richtung];
                        bool zugangErlaubt = spieler.DigimonPartner != null && spieler.DigimonPartner.Level >= zielOrt.BenoetigtesLevel;

                        if (zielOrt.Name == "Berg der Unendlichkeit" && spieler.DigimonPartner.Level < 5)
                            zugangErlaubt = false;

                        if (zugangErlaubt)
                            Console.WriteLine($"- {richtung} nach {zielOrt.Name}");
                        else
                            Console.WriteLine($"- {richtung} nach ??? (Zugang erst ab Level 5 freigeschaltet!)");
                    }
                }

                Console.WriteLine("\n[Tippe eine Richtung ein, 'm' für Menü oder 'exit' zum Verlassen]");
                Console.Write("Eingabe: ");
                eingabe = Console.ReadLine()?.ToLower() ?? "";

                if (eingabe == "m")
                {
                    ZeigeSpielerMenue(spieler);
                    continue;
                }

                if (spieler.AktuellerOrt != null && spieler.AktuellerOrt.Verbindungen.ContainsKey(eingabe))
                {
                    spieler.AktuellerOrt = spieler.AktuellerOrt.Verbindungen[eingabe];
                    spieler.AktuellerOrt.Betreten(spieler);
                    LoeseZufallsEreignisAus(spieler);
                }
                else if (eingabe != "exit")
                {
                    Console.WriteLine("Ungültige Richtung. Drücke [ENTER]...");
                    Console.ReadLine();
                }

                // Endboss-Check direkt nach Bewegung
                if (spieler.AktuellerOrt?.Name == "Berg der Unendlichkeit" && spieler.DigimonPartner?.Level >= 5)
                {
                    Digimon devimon = Digimon.ErstelleDevimon();
                    EndbossKampf endbossKampf = new EndbossKampf(spieler, devimon);
                    endbossKampf.StarteKampf();
                    return;
                }
            }

            Console.WriteLine("\nDu verlässt die Digiwelt. Drücke [ENTER]...");
            Console.ReadLine();
        }

        private static void PrüfeAmulettGeschichten(Spieler spieler)
        {
            var digimon = spieler.DigimonPartner;
            if (digimon == null) return;

            if (digimon.Level == 2 && !spieler.Inventar.Contains("Amulett der Vitalität"))
            {
                StoryManager.ErzaehleLebensAmulettGeschichte(spieler);
            }
            else if (digimon.Level == 4 && !spieler.Inventar.Contains("Amulett der Stärke"))
            {
                StoryManager.ErzaehleKraftAmulettGeschichte(spieler);
            }
        }
        
        public static void PrüfeDevimonBegegnung(Spieler spieler)
        {
            if (spieler == null || spieler.DigimonPartner == null) return;

            // Devimon erscheint nur ab Level 3 und nur einmal
            if (spieler.DigimonPartner.Level >= 3 && !spieler.HatDevimonGesehen)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nEine dunkle Präsenz erfüllt plötzlich die Luft...");
                Thread.Sleep(1000);
                Console.WriteLine("Eine finstere Gestalt tritt aus dem Schatten hervor...");
                Thread.Sleep(1500);
                Console.WriteLine("\n>> DEVIMON erscheint kurz vor dir! <<");
                Thread.Sleep(1500);

                // Devimon spricht
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nDevimon: \"Ah, ein kleiner Digiritter wagt es also, meine Welt zu betreten...\"");
                Thread.Sleep(2000);
                Console.WriteLine("Devimon: \"Genieße deine letzten friedlichen Momente, wir sehen uns bald...\"");
                Thread.Sleep(2000);
                Console.WriteLine("\n* Mit einem düsteren Lachen verschwindet Devimon wieder im Schatten... *");
                Console.ResetColor();

                // Markiere, dass Devimon bereits gesehen wurde, um ein erneutes Erscheinen zu verhindern
                spieler.HatDevimonGesehen = true;

                Console.WriteLine("\nDrücke [ENTER], um dich zu sammeln...");
                Console.ReadLine();
                Console.Clear();
            }
        }
        
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
                        spieler.ZeigeInventar();
                        Pause();
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
                        Pause();
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

            if (ereignis <= 60)
            {
                Console.WriteLine("\nEin wildes Digimon erscheint!");
                Gegner gegner = Gegner.ErstelleZufaelligenGegner();

                if (spieler.DigimonPartner != null)
                {
                    Kampf kampf = new (spieler, gegner.Digimon, spieler.DigimonPartner);
                    kampf.StarteKampf();
                }
                else
                {
                    Console.WriteLine("\nDu hast aktuell kein Digimon zum Kämpfen!");
                    Pause();
                }
            }
            else
            {
                Console.WriteLine("\nEs passiert nichts Besonderes.");
                Pause();
            }
        }
        

    }
}
