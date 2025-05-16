using System;
using System.Threading;

namespace Digimon_Textadventure
{
    public class Kampf
    {
        private Spieler spieler;
        public Digimon spielerDigimon;
        public Digimon gegnerDigimon;
        private int runde;
        private int spezialCooldown = 0;
        private int gegnerSpezialCooldown = 0;
        private bool heilungVerwendet = false;
        private bool kampfVerlassen = false;

        public Kampf(Spieler spieler, Digimon gegnerDigimon, Digimon spielerDigimon, bool istEndboss = false)
        {
            this.spieler = spieler;
            this.spielerDigimon = spielerDigimon;
            this.gegnerDigimon = gegnerDigimon;
            this.runde = 1;
        }


        public void StarteKampf()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\nEin wildes {gegnerDigimon.Name} erscheint!\n");
            Console.ResetColor();

            Console.WriteLine("Drücke [ENTER], um den Kampf zu beginnen...");
            Console.ReadLine();
            Console.Clear();

            while (spielerDigimon.Lebenspunkte > 0 && gegnerDigimon.Lebenspunkte > 0 && !kampfVerlassen) // Flag prüfen
            {
                ZeigeStatus();
                SpielerAktion();

                if (kampfVerlassen) break; // Kampf abbrechen, wenn Flag gesetzt

                if (gegnerDigimon.Lebenspunkte <= 0) break;

                GegnerAktion();

                Console.WriteLine("\nDrücke [ENTER], um die nächste Runde zu starten...");
                Console.ReadLine();
                Console.Clear();

                runde++;
            }

            if (!kampfVerlassen)
            {
                ZeigeKampfErgebnis();
            }
            else
            {
                Console.WriteLine("\nDu hast erfolgreich den Kampf verlassen und kehrst in die Digiwelt zurück...");
                Console.ReadLine();
                BewegungsManager.BewegeSpieler(spieler);
            }

            // Zurück in die Digiwelt
            BewegungsManager.BewegeSpieler(spieler);
        }

        private void ZeigeStatus()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n================= Status =================");
            Console.ResetColor();
            Console.WriteLine($"[Runde: {runde}]");

            // Spieler-Digimon in Blau
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{spielerDigimon.Name,-10} | LP: {spielerDigimon.Lebenspunkte,3}/{spielerDigimon.MaximaleLebenspunkte,-3} | ATK: {spielerDigimon.Angriff,2} | DEF: {spielerDigimon.Verteidigung,2}");
            Console.ResetColor();

            // Gegner-Digimon in Dunkelrot
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"{gegnerDigimon.Name,-10} | LP: {gegnerDigimon.Lebenspunkte,3}/{gegnerDigimon.MaximaleLebenspunkte,-3} | ATK: {gegnerDigimon.Angriff,2} | DEF: {gegnerDigimon.Verteidigung,2}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==========================================");
            Console.ResetColor();
        }

        private void SpielerAktion()
        {
            Console.WriteLine("Wähle eine Aktion:");
            Console.WriteLine("[1] Angriff");
            Console.WriteLine(spezialCooldown == 0 ? "[2] Spezialfähigkeit (bereit)" : $"[2] Spezialfähigkeit (Cooldown: {spezialCooldown})");
            Console.WriteLine("[3] Heilen (+30 LP, 1x pro Kampf)");
            Console.WriteLine("[4] Verteidigen (+5 DEF für 1 Runde)");
            Console.WriteLine("[5] Kampf verlassen");

            Console.Write("\nDeine Wahl: ");
            string eingabe = Console.ReadLine() ?? "";

            switch (eingabe)
            {
                case "1":
                    FuehreAngriffAus(spielerDigimon, gegnerDigimon);
                    break;
                case "2":
                    if (spezialCooldown == 0)
                    {
                        SpezialAngriffAnimation();
                        spielerDigimon.FuehreSpezialAttackeAus(gegnerDigimon);
                        spezialCooldown = 3;
                    }
                    else
                    {
                        Console.WriteLine("\nSpezialfähigkeit noch im Cooldown!");
                    }
                    break;
                case "3":
                    Heilen();
                    break;
                case "4":
                    Verteidigen();
                    break;
                case "5":
                    kampfVerlassen = true;  // Setze das Flag, um den Kampf zu verlassen
                    return;
                default:
                    Console.WriteLine("\nUngültige Eingabe.");
                    break;
            }

            if (spezialCooldown > 0)
                spezialCooldown--;
        }

        private static void SpezialAngriffAnimation()
        {
            Console.Write("\nSpezialfähigkeit wird vorbereitet");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }
            Console.WriteLine("\n");
        }

        private void Heilen()
        {
            if (heilungVerwendet)
            {
                Console.WriteLine("\nDu hast die Heilung bereits verwendet.");
            }
            else
            {
                spielerDigimon.Lebenspunkte = Math.Min(spielerDigimon.Lebenspunkte + 30, spielerDigimon.MaximaleLebenspunkte);
                heilungVerwendet = true;
                Console.WriteLine($"\n{spielerDigimon.Name} wurde um 30 LP geheilt!");
            }
        }

        private void Verteidigen()
        {
            Console.WriteLine($"\n{spielerDigimon.Name} verteidigt sich! Verteidigung +5 für diese Runde.");
            spielerDigimon.Verteidigung += 5;
        }

        public void GegnerAktion()
        {
            Random rnd = new();
            int aktion = rnd.Next(1, 101);
            int schaden;

            if (aktion <= 60) // 60% Angriff
            {
                bool kritisch = rnd.Next(1, 101) <= 10;
                schaden = BerechneNormalenSchaden(gegnerDigimon.Angriff, spielerDigimon.Verteidigung, kritisch);

                Console.ForegroundColor = kritisch ? ConsoleColor.Red : ConsoleColor.DarkRed;
                Console.WriteLine($"{gegnerDigimon.Name} greift an und verursacht {schaden} Schaden{(kritisch ? " (Kritisch!)" : "")}.");
                Console.ResetColor();

                spielerDigimon.Lebenspunkte -= schaden;
            }
            else if (aktion <= 70) // 10% Verteidigen
            {
                Console.WriteLine($"{gegnerDigimon.Name} verteidigt sich!");
                gegnerDigimon.Verteidigung += 5;
            }
            else // 30% Spezialangriff
            {
                if (gegnerSpezialCooldown == 0)
                {
                    gegnerDigimon.FuehreSpezialAttackeAus(spielerDigimon);
                    gegnerSpezialCooldown = 3;
                }
                else
                {
                    Console.WriteLine($"{gegnerDigimon.Name} kann die Spezialfähigkeit noch nicht einsetzen! (Cooldown: {gegnerSpezialCooldown})");
                    FuehreAngriffAus(gegnerDigimon, spielerDigimon);
                }
            }
            gegnerDigimon.Verteidigung = gegnerDigimon.BasisVerteidigung;
            if (gegnerSpezialCooldown > 0)
                gegnerSpezialCooldown--;

        }

        private void FuehreAngriffAus(Digimon angreifer, Digimon verteidiger)
        {
            Random rnd = new();
            bool kritisch = rnd.Next(1, 101) <= 20;
            int schaden = BerechneNormalenSchaden(angreifer.Angriff, verteidiger.Verteidigung, kritisch);

            Console.ForegroundColor = angreifer == spielerDigimon ? ConsoleColor.Cyan : ConsoleColor.DarkRed;
            Console.WriteLine($"{angreifer.Name} greift an und verursacht {schaden} Schaden{(kritisch ? " (Kritisch!)" : "")}.");
            Console.ResetColor();

            verteidiger.Lebenspunkte -= schaden;
            verteidiger.Lebenspunkte = Math.Max(verteidiger.Lebenspunkte, 0);
        }

        private static int BerechneNormalenSchaden(int angriff, int verteidigung, bool kritisch)
        {
            int schaden = angriff - verteidigung;
            if (kritisch) schaden *= 2;
            return schaden < 1 ? 1 : schaden;
        }

        private void ZeigeKampfErgebnis()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== KAMPF ERGEBNIS ===");
            Console.ResetColor();

            if (spielerDigimon.Lebenspunkte > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n>> {spielerDigimon.Name} hat den Kampf gewonnen! <<");
                Console.ResetColor();

                int erfahrungspunkte = 100;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n>> {spielerDigimon.Name} erhält {erfahrungspunkte} Erfahrungspunkte!");
                Console.ResetColor();
                // erfahrung für sieg 100 Punkte
                spielerDigimon.VergibErfahrung(erfahrungspunkte, spieler);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($">> {spielerDigimon.Name} wurde besiegt...");
                Console.ResetColor();

                int erfahrungspunkte = 50; // Niederlage gibt 50 XP
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n>> {spielerDigimon.Name} erhält {erfahrungspunkte} Erfahrungspunkte trotz Niederlage.");
                Console.ResetColor();
                // erfahrung für niederlage 50 Punkte
                spielerDigimon.VergibErfahrung(erfahrungspunkte, spieler);
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nKampf beendet! Drücke [ENTER], um in die Digiwelt zurückzukehren...");
            Console.ResetColor();
            Console.ReadLine();

            spieler.ZeigeProfil();
            // Digimon-Profil anzeigen, um den Fortschritt direkt zu sehen
            spielerDigimon.ZeigeProfil();

            // Zurück in die Digiwelt
            BewegungsManager.BewegeSpieler(spieler);
        }

        


    }



}
