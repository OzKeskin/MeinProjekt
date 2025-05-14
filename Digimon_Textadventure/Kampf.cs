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
            Console.WriteLine("\nWähle deine Aktion:");
            Console.WriteLine("1. Normaler Angriff");
            Console.WriteLine("2. Spezialfähigkeit");

            string eingabe = Console.ReadLine();
            int schaden;

            switch (eingabe)
            {
                case "1":
                    bool kritisch = new Random().Next(1, 101) <= 10;
                    schaden = SchadensBerechnung.BerechneNormalenSchaden(spielerDigimon.Angriff, gegnerDigimon.Verteidigung, kritisch);
                    gegnerDigimon.Lebenspunkte -= schaden;
                    Console.WriteLine($"{spielerDigimon.Name} greift an und verursacht {schaden} Schaden{(kritisch ? " (Kritisch!)" : "")}.");
                    break;

                case "2":
                    schaden = SchadensBerechnung.BerechneSpezialschaden(spielerDigimon.Spezialattacke, spielerDigimon.Angriff, gegnerDigimon.Verteidigung);
                    gegnerDigimon.Lebenspunkte -= schaden;
                    Console.WriteLine($"{spielerDigimon.Name} setzt {spielerDigimon.Spezialattacke} ein und verursacht {schaden} Schaden!");
                    break;

                default:
                    Console.WriteLine("Ungültige Eingabe! Du verlierst deine Runde.");
                    break;
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
