using System;
using System.Threading;

namespace Digimon_Textadventure
{
    public class Kampf
    {
        private Digimon spielerDigimon;
        private Digimon gegnerDigimon;
        private int runde;

        private bool heilungVerwendet = false;

        private int spezialCooldown = 0; // Runden bis Spezial wieder verfügbar
        private int gegnerSpezialCooldown = 0; // Cooldown-Variable

        public Kampf(Digimon spieler, Digimon gegner)
        {
            this.spielerDigimon = spieler;
            this.gegnerDigimon = gegner;
            this.runde = 1;
        }

        public void StarteKampf()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\nEin wildes {gegnerDigimon.Name} erscheint!\n");
            Console.ResetColor();
            Console.WriteLine("Drücke [ENTER], um den Kampf zu beginnen...");
            Console.ReadLine();

            while (spielerDigimon.Lebenspunkte > 0 && gegnerDigimon.Lebenspunkte > 0)
            {
                ZeigeStatus();

                SpielerAktion();
                if (gegnerDigimon.Lebenspunkte <= 0) break;

                GegnerAktion();
                runde++;
                Thread.Sleep(1500);
            }

            ZeigeKampfErgebnis();
        }

        private void ZeigeStatus()
        {
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== Status ===");
            Console.ResetColor();
            Console.WriteLine($"[Runde: {runde}]");

            // Format: Name (10 Zeichen, linksbündig), LP (10 Zeichen, rechtsbündig), ATK (5 Zeichen, rechtsbündig), DEF (5 Zeichen, rechtsbündig)
            Console.WriteLine(string.Format("{0,-10} | LP: {1,3}/{2,-3} | ATK: {3,2} | DEF: {4,2}",
                spielerDigimon.Name,
                spielerDigimon.Lebenspunkte,
                spielerDigimon.MaximaleLebenspunkte,
                spielerDigimon.Angriff,
                spielerDigimon.Verteidigung));

            Console.WriteLine(string.Format("{0,-10} | LP: {1,3}/{2,-3} | ATK: {3,2} | DEF: {4,2}",
                gegnerDigimon.Name,
                gegnerDigimon.Lebenspunkte,
                gegnerDigimon.MaximaleLebenspunkte,
                gegnerDigimon.Angriff,
                gegnerDigimon.Verteidigung));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("======================");
            Console.ResetColor();
        }
        private void SpielerAktion()
        {
            
            Console.WriteLine("Wähle eine Aktion:");
            Console.WriteLine("[1] Normaler Angriff");

            if (spezialCooldown == 0)
                Console.WriteLine("[2] Spezialfähigkeit einsetzen (bereit)");
            else
                Console.WriteLine($"[2] Spezialfähigkeit einsetzen (verfügbar in {spezialCooldown} Runden)");

            Console.WriteLine("[3] Heilen (+30 LP, einmal pro Kampf)");
            Console.WriteLine("[4] Verteidigen (+5 Verteidigung für diese Runde)");

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
                    spezialCooldown = 3; // Cool-Down setzt sich nach Einsatz
                    break;
                case "3":
                    Heilen();
                    break;
                case "4":
                    Verteidigen();
                    break;
                default:
                    Console.WriteLine("\nUngültige Eingabe oder Spezialfähigkeit noch im Cooldown.");
                    Console.WriteLine("Drücke [ENTER], um erneut zu wählen...");
                    Console.ReadLine();
                    SpielerAktion();
                    break;
            }
            if (spezialCooldown > 0)
                spezialCooldown--;
        }

        private void SpezialAngriffAnimation()
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
                Console.WriteLine("\nDu hast die Heilung bereits in diesem Kampf verwendet.");
            }
            else
            {
                spielerDigimon.Lebenspunkte += 30;
                if (spielerDigimon.Lebenspunkte > spielerDigimon.MaximaleLebenspunkte)
                    spielerDigimon.Lebenspunkte = spielerDigimon.MaximaleLebenspunkte;

                heilungVerwendet = true;
                Console.WriteLine($"\n{spielerDigimon.Name} hat sich um 30 LP geheilt!");
            }
        }

        private void Verteidigen()
        {
            Console.WriteLine($"\n{spielerDigimon.Name} verteidigt sich und erhöht die Verteidigung um 5 für diese Runde!");
            spielerDigimon.Verteidigung += 5;
        }

        private void GegnerAktion()
        {
            int aktion = new Random().Next(1, 4); // 1: Angriff, 2: Verteidigen, 3: Spezialangriff (wenn möglich)
            int schaden;

            switch (aktion)
            {
                case 1: // Angriff
                    bool kritisch = new Random().Next(1, 101) <= 10;
                    schaden = gegnerDigimon.Angriff - spielerDigimon.Verteidigung;
                    if (kritisch) schaden *= 2;
                    if (schaden < 0) schaden = 0;

                    spielerDigimon.Lebenspunkte -= schaden;
                    Console.WriteLine($"{gegnerDigimon.Name} greift an und verursacht {schaden} Schaden{(kritisch ? " (Kritisch!)" : "")}.");
                    break;

                case 2: // Verteidigen
                    gegnerDigimon.Verteidigung += 0;
                    Console.WriteLine($"{gegnerDigimon.Name} verteidigt sich und erhöht seine Verteidigung um 5!");
                    break;

                case 3: // Spezialangriff mit Cooldown
                    if (gegnerSpezialCooldown == 0)
                    {
                        gegnerDigimon.FuehreSpezialAttackeAus(spielerDigimon);
                        gegnerSpezialCooldown = 3; // Cooldown auf 3 Runden setzen
                    }
                    else
                    {
                        Console.WriteLine($"{gegnerDigimon.Name} kann die Spezialfähigkeit noch nicht einsetzen! ({gegnerSpezialCooldown} Runden Cooldown)");
                        // Stattdessen Standardangriff
                        schaden = gegnerDigimon.Angriff - spielerDigimon.Verteidigung;
                        if (schaden < 0) schaden = 0;

                        spielerDigimon.Lebenspunkte -= schaden;
                        Console.WriteLine($"{gegnerDigimon.Name} greift stattdessen normal an und verursacht {schaden} Schaden.");
                    }
                    break;
            }

            // Cooldown am Ende der Aktion reduzieren
            if (gegnerSpezialCooldown > 0)
                gegnerSpezialCooldown--;
        }
        private void FuehreAngriffAus(Digimon angreifer, Digimon verteidiger)
        {
            bool kritisch = new Random().Next(1, 101) <= 20; // 20% Chance auf kritischen Treffer
            int schaden = BerechneNormalenSchaden(angreifer.Angriff, verteidiger.Verteidigung, kritisch);

            verteidiger.Lebenspunkte -= schaden;
            verteidiger.Lebenspunkte = Math.Max(verteidiger.Lebenspunkte, 0);

            Console.WriteLine($"{angreifer.Name} greift an und verursacht {schaden} Schaden{(kritisch ? " (Kritisch!)" : "")}.");
            Console.WriteLine($"{verteidiger.Name} hat noch {verteidiger.Lebenspunkte} LP.\n");
        }

        private int BerechneNormalenSchaden(int angriff, int verteidigung, bool kritisch)
        {
            int schaden = angriff - verteidigung;
            if (kritisch) schaden *= 2;
            return schaden < 0 ? 0 : schaden;
        }

        private void ZeigeKampfErgebnis()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== KAMPF ERGEBNIS ===");
            Console.ResetColor();

            if (spielerDigimon.Lebenspunkte > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\r>> {spielerDigimon.Name} HAT GEWONNEN! <<   ");
                    Thread.Sleep(300);
                    Console.Write("\r                             ");
                    Thread.Sleep(300);
                }

                int erfahrungspunkte = 20 + new Random().Next(10);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n>> {spielerDigimon.Name} erhält {erfahrungspunkte} Erfahrungspunkte!");
                Console.ResetColor();

                spielerDigimon.VergibErfahrung(erfahrungspunkte);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($">> {spielerDigimon.Name} wurde besiegt...");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n>> Drücke eine Taste, um fortzufahren...");
            Console.ResetColor();
            Console.ReadKey();
        }


    }

}
