using System;
using System.Threading;

namespace Digimon_Textadventure
{
    public class Kampf
    {
        private Digimon spielerDigimon;
        private Digimon gegnerDigimon;
        private int runde;

        public Kampf(Digimon spieler, Digimon gegner)
        {
            this.spielerDigimon = spieler;
            this.gegnerDigimon = gegner;
            this.runde = 1;
        }

        public void StarteKampf()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\nEin wildes {gegnerDigimon.Name} erscheint!\n");
            Console.ResetColor();

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
            Console.WriteLine($"\n--- Runde {runde} ---");
            Console.WriteLine($"{spielerDigimon.Name} - {spielerDigimon.Lebenspunkte}/{spielerDigimon.MaximaleLebenspunkte}");
            Console.WriteLine($"{gegnerDigimon.Name} - {gegnerDigimon.Lebenspunkte}/{gegnerDigimon.MaximaleLebenspunkte}");
        }

        private void SpielerAktion()
        {
            Console.WriteLine("Wähle eine Aktion:");
            Console.WriteLine("[1] Angriff");
            if (!spielerDigimon.SpezialVerwendet)
                Console.WriteLine("[2] Spezialfähigkeit einsetzen");

            Console.Write("Deine Wahl: ");
            string eingabe = Console.ReadLine() ?? "";

            if (eingabe == "2" && !spielerDigimon.SpezialVerwendet)
            {
                spielerDigimon.FuehreSpezialAttackeAus(gegnerDigimon);
                spielerDigimon.SpezialVerwendet = true;
            }
            else if (eingabe == "1")
            {
                FuehreAngriffAus(spielerDigimon, gegnerDigimon);
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe oder Spezialfähigkeit wurde bereits verwendet.");
                SpielerAktion(); // Wiederhole Eingabe
            }
        }

        private void GegnerAktion()
        {
            int aktion = new Random().Next(1, 3);
            int schaden;

            if (aktion == 1)
            {
                bool kritisch = new Random().Next(1, 101) <= 10;
                schaden = SchadensBerechnung.BerechneNormalenSchaden(gegnerDigimon.Angriff, spielerDigimon.Verteidigung, kritisch);
                spielerDigimon.Lebenspunkte -= schaden;
                Console.WriteLine($"{gegnerDigimon.Name} greift an und verursacht {schaden} Schaden{(kritisch ? " (Kritisch!)" : "")}.");
            }
            else
            {
                schaden = SchadensBerechnung.BerechneSpezialschaden(gegnerDigimon.Spezialattacke, gegnerDigimon.Angriff, spielerDigimon.Verteidigung);
                spielerDigimon.Lebenspunkte -= schaden;
                Console.WriteLine($"{gegnerDigimon.Name} setzt {gegnerDigimon.Spezialattacke} ein und verursacht {schaden} Schaden!");
            }
        }
        private void FuehreAngriffAus(Digimon angreifer, Digimon verteidiger)
        {
            int schaden = angreifer.Angriff - verteidiger.Verteidigung;
            if (schaden < 1) schaden = 1;

            verteidiger.Lebenspunkte -= schaden;
            if (verteidiger.Lebenspunkte < 0) verteidiger.Lebenspunkte = 0;

            Console.WriteLine($"{angreifer.Name} greift an und verursacht {schaden} Schaden!");
            Console.WriteLine($"{verteidiger.Name} hat noch {verteidiger.Lebenspunkte} LP.\n");
        }

        private void ZeigeKampfErgebnis()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n--- Kampfergebnis ---");
            Console.ResetColor();

            if (spielerDigimon.Lebenspunkte > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{spielerDigimon.Name} hat gewonnen!");
                Console.ResetColor();

                int erfahrungspunkte = 20 + new Random().Next(10);
                LevelSystem.VergibErfahrung(spielerDigimon, erfahrungspunkte);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{spielerDigimon.Name} wurde besiegt...");
                Console.ResetColor();
            }

            Console.WriteLine("\nDrücke eine Taste, um fortzufahren...");
            Console.ReadKey();
        }
    }
}
