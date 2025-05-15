using System;
using System.Threading;

namespace Digimon_Textadventure
{
    public class Kampf
    {
        private Spieler spieler;
        private Digimon spielerDigimon;
        private Digimon gegnerDigimon;
        private int runde = 1;

        private bool heilungVerwendet = false;
        private int spezialCooldown = 0;
        private int gegnerSpezialCooldown = 0;

        public Kampf(Spieler spieler, Digimon gegner)
        {
            this.spieler = spieler;
            this.spielerDigimon = spieler.DigimonPartner;
            this.gegnerDigimon = gegner;
        }

        public void StarteKampf()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\nEin wildes {gegnerDigimon.Name} erscheint!");
            Console.ResetColor();

            Console.WriteLine("Drücke [ENTER], um den Kampf zu beginnen...");
            Console.ReadLine();
            Console.Clear();

            while (spielerDigimon.Lebenspunkte > 0 && gegnerDigimon.Lebenspunkte > 0)
            {
                ZeigeStatus();
                SpielerAktion();

                if (gegnerDigimon.Lebenspunkte <= 0) break;

                GegnerAktion();

                // Verteidigungsbonus zurücksetzen
                spielerDigimon.Verteidigung = Math.Max(spielerDigimon.Verteidigung - 5, 0);
                gegnerDigimon.Verteidigung = Math.Max(gegnerDigimon.Verteidigung - 5, 0);

                Console.WriteLine("\nDrücke [ENTER], um die nächste Runde zu starten...");
                Console.ReadLine();
                Console.Clear();

                runde++;
            }
            spielerDigimon.Verteidigung = spielerDigimon.BasisVerteidigung;
            gegnerDigimon.Verteidigung = gegnerDigimon.BasisVerteidigung;

            ZeigeKampfErgebnis();
        }

        private void ZeigeStatus()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=========== Kampfstatus ===========");
            Console.ResetColor();
            Console.WriteLine($"[Runde: {runde}]");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(string.Format("{0,-10} | LP: {1,3}/{2,-3} | ATK: {3,2} | DEF: {4,2}",
                spielerDigimon.Name, spielerDigimon.Lebenspunkte, spielerDigimon.MaximaleLebenspunkte,
                spielerDigimon.Angriff, spielerDigimon.Verteidigung));
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(string.Format("{0,-10} | LP: {1,3}/{2,-3} | ATK: {3,2} | DEF: {4,2}",
                gegnerDigimon.Name, gegnerDigimon.Lebenspunkte, gegnerDigimon.MaximaleLebenspunkte,
                gegnerDigimon.Angriff, gegnerDigimon.Verteidigung));
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("====================================");
            Console.ResetColor();
        }

        private void SpielerAktion()
        {
            Console.WriteLine("\nWähle eine Aktion:");
            Console.WriteLine("[1] Angriff");

            if (spezialCooldown == 0)
                Console.WriteLine("[2] Spezialfähigkeit (bereit)");
            else
                Console.WriteLine($"[2] Spezialfähigkeit (Cooldown: {spezialCooldown})");

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
                case "2" when spezialCooldown == 0:
                    SpezialAngriffAnimation();
                    spielerDigimon.FuehreSpezialAttackeAus(gegnerDigimon);
                    spezialCooldown = 3;
                    break;
                case "3":
                    Heilen();
                    break;
                case "4":
                    Verteidigen(spielerDigimon);
                    break;
                case "5":
                    Console.WriteLine("\nDu fliehst aus dem Kampf. Drücke [ENTER]...");
                    Console.ReadLine();
                    BewegungsManager.BewegeSpieler(spieler);
                    return;
                default:
                    Console.WriteLine("Ungültige Eingabe. Drücke [ENTER], um erneut zu wählen...");
                    Console.ReadLine();
                    SpielerAktion();
                    return;
            }

            if (spezialCooldown > 0)
                spezialCooldown--;
        }

        private void SpezialAngriffAnimation()
        {
            Console.Write("\nSpezialfähigkeit wird vorbereitet");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(400);
                Console.Write(".");
            }
            Console.WriteLine("\n");
        }

        private void Heilen()
        {
            if (heilungVerwendet)
            {
                Console.WriteLine("\nHeilung bereits verwendet.");
            }
            else
            {
                spielerDigimon.Lebenspunkte += 30;
                if (spielerDigimon.Lebenspunkte > spielerDigimon.MaximaleLebenspunkte)
                    spielerDigimon.Lebenspunkte = spielerDigimon.MaximaleLebenspunkte;

                heilungVerwendet = true;
                Console.WriteLine($"{spielerDigimon.Name} heilt sich um 30 LP!");
            }
        }

        private void Verteidigen(Digimon digimon)
        {
            digimon.Verteidigung += 5;
            Console.WriteLine($"{digimon.Name} verteidigt sich und erhöht die Verteidigung um 5 für diese Runde!");
        }

        private void GegnerAktion()
        {
            int aktion = new Random().Next(1, 101);

            if (aktion <= 60)
            {
                FuehreAngriffAus(gegnerDigimon, spielerDigimon);
            }
            else if (aktion <= 70)
            {
                Verteidigen(gegnerDigimon);
            }
            else
            {
                if (gegnerSpezialCooldown == 0)
                {
                    gegnerDigimon.FuehreSpezialAttackeAus(spielerDigimon);
                    gegnerSpezialCooldown = 3;
                }
                else
                {
                    FuehreAngriffAus(gegnerDigimon, spielerDigimon);
                }
            }

            if (gegnerSpezialCooldown > 0)
                gegnerSpezialCooldown--;
        }

        private void FuehreAngriffAus(Digimon angreifer, Digimon verteidiger)
        {
            bool kritisch = new Random().Next(1, 101) <= 20;
            int schaden = BerechneNormalenSchaden(angreifer.Angriff, verteidiger.Verteidigung, kritisch);

            verteidiger.Lebenspunkte -= schaden;
            verteidiger.Lebenspunkte = Math.Max(verteidiger.Lebenspunkte, 0);

            Console.ForegroundColor = angreifer == spielerDigimon ? ConsoleColor.Blue : ConsoleColor.DarkRed;
            Console.WriteLine($"{angreifer.Name} greift an und verursacht {schaden} Schaden{(kritisch ? " (Kritisch!)" : "")}.");
            Console.ResetColor();
        }

        private int BerechneNormalenSchaden(int angriff, int verteidigung, bool kritisch)
        {
            int schaden = angriff - verteidigung;
            if (kritisch) schaden *= 2;
            return Math.Max(schaden, 0);
        }

        private void ZeigeKampfErgebnis()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== KAMPF ERGEBNIS ===");
            Console.ResetColor();

            if (spielerDigimon.Lebenspunkte > 0)
            {
                
                int erfahrungspunkte = 20 + new Random().Next(10);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n>> {spielerDigimon.Name} erhält {erfahrungspunkte} Erfahrungspunkte!");
                Console.ResetColor();

                spielerDigimon.VergibErfahrung(erfahrungspunkte);

                // Digimon-Profil anzeigen
                Console.WriteLine("\nDein Digimon-Profil nach dem Kampf:");
                spielerDigimon.ZeigeProfil();

                // Avatar-Info anzeigen
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nDein Avatar:");
                Console.ResetColor();
                Console.WriteLine($"Name: {spieler.Avatar.Name}");
                Console.WriteLine($"Beschreibung: {spieler.Avatar.Beschreibung}");
                Console.WriteLine($"Start-Item: {spieler.Avatar.StartItem}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($">> {spielerDigimon.Name} wurde besiegt...");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nDrücke [ENTER], um in die Digiwelt zurückzukehren...");
            Console.ResetColor();
            Console.ReadLine();

            // Zurück in die Digiwelt
            BewegungsManager.BewegeSpieler(spieler);
        }

    }


}
