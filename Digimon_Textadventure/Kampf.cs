using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon_Textadventure
{
    public class Kampf
    {
        private Digimon spieler;
        private Gegner gegner;
        private bool heilungVerwendet = false;

        public Kampf(Digimon spieler, Digimon gegnerDigimon)
        {
            this.spieler = spieler;
            this.gegner = new Gegner(gegnerDigimon);
        }

        public void StarteKampf()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nEin feindliches Digimon erscheint: {gegner.Digimon.Name}!\n");
            Console.ResetColor();

            Console.WriteLine("Drücke [ENTER], um den Kampf zu beginnen...");
            Console.ReadLine();
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("========== KAMPF BEGINNT ==========\n");
            Console.ResetColor();

            int runde = 1;

            while (spieler.Lebenspunkte > 0 && gegner.Digimon.Lebenspunkte > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"----- Runde {runde} -----");
                Console.ResetColor();

                ZeigeLPStatus();

                SpielerAktion();

                if (gegner.Digimon.Lebenspunkte <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nDu hast {gegner.Digimon.Name} besiegt!");
                    Console.ResetColor();
                    break;
                }

                Thread.Sleep(1000);

                GegnerGreiftAn();

                if (spieler.Lebenspunkte <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{spieler.Name} wurde besiegt! Game Over.");
                    Console.ResetColor();
                    break;
                }

                Console.WriteLine("\nDrücke [ENTER] für die nächste Runde...");
                Console.ReadLine();
                Console.Clear();
                runde++;
            }

            ZeigeKampfErgebnis();
            NachKampfMenue();
        }

        private void ZeigeLPStatus()
        {
            Console.WriteLine($"{spieler.Name} (LP: {spieler.Lebenspunkte}) vs {gegner.Digimon.Name} (LP: {gegner.Digimon.Lebenspunkte})\n");
        }

        private void SpielerAktion()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Wähle deine Aktion:");
            Console.ResetColor();
            Console.WriteLine("[1] Angreifen");
            Console.WriteLine("[2] Heilen" + (heilungVerwendet ? " (bereits verwendet)" : ""));
            Console.WriteLine("[3] Verteidigen");

            int wahl = 0;
            while (wahl < 1 || wahl > 3)
            {
                Console.Write("Deine Wahl: ");
                int.TryParse(Console.ReadLine(), out wahl);
            }

            switch (wahl)
            {
                case 1:
                    SpielerGreiftAn();
                    break;
                case 2:
                    Heilen();
                    break;
                case 3:
                    Verteidigen();
                    break;
            }
        }

        private void SpielerGreiftAn()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{spieler.Name} greift {gegner.Digimon.Name} an!");
            Console.ResetColor();

            bool kritischerTreffer = new Random().Next(1, 101) <= 20; // 20% Chance
            int schaden = spieler.Angriff - gegner.Digimon.Verteidigung;

            if (schaden < 0) schaden = 0;
            if (kritischerTreffer)
            {
                schaden *= 2;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Kritischer Treffer!");
                Console.ResetColor();
            }

            gegner.Digimon.Lebenspunkte -= schaden;

            Console.WriteLine($"{gegner.Digimon.Name} erleidet {schaden} Schaden!");
        }


        private void GegnerGreiftAn()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{gegner.Digimon.Name} greift {spieler.Name} an!");
            Console.ResetColor();

            bool kritischerTreffer = new Random().Next(1, 101) <= 20;
            int schaden = gegner.Digimon.Angriff - spieler.Verteidigung;

            if (schaden < 0) schaden = 0;
            if (kritischerTreffer)
            {
                schaden *= 2;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Kritischer Treffer!");
                Console.ResetColor();
            }

            spieler.Lebenspunkte -= schaden;

            Console.WriteLine($"{spieler.Name} erleidet {schaden} Schaden!");
        }


        private void Heilen()
        {
            if (heilungVerwendet)
            {
                Console.WriteLine("Du hast deine Heilung bereits verwendet.");
                return;
            }

            int heilwert = 30;
            spieler.Lebenspunkte += heilwert;
            heilungVerwendet = true;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{spieler.Name} heilt sich um {heilwert} LP!");
            Console.ResetColor();
        }

        private void Verteidigen()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{spieler.Name} bereitet sich vor und erhöht seine Verteidigung!");
            Console.ResetColor();
            spieler.Verteidigung += 5;
        }

        private void ZeigeKampfErgebnis()
        {
            Console.WriteLine("\n=== Kampf beendet ===");
            if (spieler.Lebenspunkte <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Dein Digimon wurde besiegt. Du hast verloren.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Du hast gewonnen!");
            }
            Console.ResetColor();
        }

        private void NachKampfMenue()
        {
            Console.WriteLine("\nWas möchtest du als Nächstes tun?");
            Console.WriteLine("[1] Neues Spiel starten");
            Console.WriteLine("[2] Spiel beenden");

            string eingabe = "";
            while (eingabe != "1" && eingabe != "2")
            {
                Console.Write("Deine Auswahl: ");
                eingabe = Console.ReadLine() ?? "";
            }

            if (eingabe == "1")
            {
                Console.Clear();
                Spiel neuesSpiel = new Spiel();
                neuesSpiel.Starte();
            }
            else
            {
                Console.WriteLine("Danke fürs Spielen! Bis bald.");
                Environment.Exit(0);
            }
        }
    }



}
