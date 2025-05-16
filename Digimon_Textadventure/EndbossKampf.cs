using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon_Textadventure
{
    public class EndbossKampf
    {
        private Spieler spieler;
        private Digimon spielerDigimon;
        private Digimon devimon;
        private int runde;
        private int todeskralleCooldown = 0;
        private int boeseFluegelCooldown = 0;

        public EndbossKampf(Spieler spieler, Digimon devimon)
        {
            this.spieler = spieler;
            this.spielerDigimon = spieler.DigimonPartner;
            this.devimon = devimon;
            this.runde = 1;
        }

        public void StarteKampf()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nDer Endboss Devimon erscheint! Der finale Kampf beginnt!\n");
            Console.ResetColor();

            Console.WriteLine("Drücke [ENTER], um den Kampf zu beginnen...");
            Console.ReadLine();
            Console.Clear();

            while (spielerDigimon.Lebenspunkte > 0 && devimon.Lebenspunkte > 0)
            {
                ZeigeStatus();
                SpielerAktion();

                if (devimon.Lebenspunkte <= 0) break;

                DevimonAktion();

                Console.WriteLine("\nDrücke [ENTER], um die nächste Runde zu starten...");
                Console.ReadLine();
                Console.Clear();

                runde++;
            }

            ZeigeKampfErgebnis();
        }

        private void ZeigeStatus()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n===== Endboss Kampf - Runde " + runde + " =====");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{spielerDigimon.Name,-10} | LP: {spielerDigimon.Lebenspunkte,3}/{spielerDigimon.MaximaleLebenspunkte,-3} | ATK: {spielerDigimon.Angriff,2} | DEF: {spielerDigimon.Verteidigung,2}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Devimon     | LP: {devimon.Lebenspunkte,3}/{devimon.MaximaleLebenspunkte,-3} | ATK: {devimon.Angriff,2} | DEF: {devimon.Verteidigung,2}");
            Console.ResetColor();

            Console.WriteLine("=============================================");
        }

        private void SpielerAktion()
        {
            Console.WriteLine("\nWähle deine Aktion:");
            Console.WriteLine("[1] Angriff");
            Console.WriteLine("[2] Spezialfähigkeit einsetzen");
            Console.WriteLine("[3] Heilen (+30 LP, 1x pro Kampf)");
            Console.WriteLine("[4] Verteidigen (+5 DEF für diese Runde)");

            Console.Write("\nDeine Wahl: ");
            string eingabe = Console.ReadLine() ?? "";

            switch (eingabe)
            {
                case "1":
                    FuehreAngriffAus(spielerDigimon, devimon);
                    break;
                case "2":
                    spielerDigimon.FuehreSpezialAttackeAus(devimon);
                    break;
                case "3":
                    spielerDigimon.Lebenspunkte = Math.Min(spielerDigimon.Lebenspunkte + 30, spielerDigimon.MaximaleLebenspunkte);
                    Console.WriteLine($"\n{spielerDigimon.Name} heilt sich um 30 LP!");
                    break;
                case "4":
                    spielerDigimon.Verteidigung += 5;
                    Console.WriteLine($"{spielerDigimon.Name} verteidigt sich!");
                    break;
                default:
                    Console.WriteLine("\nUngültige Eingabe.");
                    SpielerAktion(); // Nochmal Eingabe verlangen
                    break;
            }
        }

        private void DevimonAktion()
        {
            if (todeskralleCooldown == 0)
            {
                Todeskralle();
                todeskralleCooldown = 5;
            }
            else if (boeseFluegelCooldown == 0)
            {
                BoeseFluegel();
                boeseFluegelCooldown = 3;
            }
            else
            {
                FuehreAngriffAus(devimon, spielerDigimon);
            }

            if (todeskralleCooldown > 0) todeskralleCooldown--;
            if (boeseFluegelCooldown > 0) boeseFluegelCooldown--;
        }

        private void Todeskralle()
        {
            int schaden = devimon.Angriff * 3;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\nDevimon setzt die mächtige Todeskralle ein und verursacht {schaden} Schaden!");
            Console.ResetColor();

            spielerDigimon.Lebenspunkte -= schaden;
        }

        private void BoeseFluegel()
        {
            int schaden = devimon.Angriff * 2;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nDevimon entfesselt die Böse Flügel-Attacke und verursacht {schaden} Schaden!");
            Console.ResetColor();

            spielerDigimon.Lebenspunkte -= schaden;
        }

        private void FuehreAngriffAus(Digimon angreifer, Digimon verteidiger)
        {
            int schaden = Math.Max(1, angreifer.Angriff - verteidiger.Verteidigung);
            Console.WriteLine($"\n{angreifer.Name} greift an und verursacht {schaden} Schaden.");
            verteidiger.Lebenspunkte -= schaden;
        }

        private void ZeigeKampfErgebnis()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== KAMPF ERGEBNIS ===");
            Console.ResetColor();

            if (spielerDigimon.Lebenspunkte > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n>> {spielerDigimon.Name} hat den finalen Kampf gewonnen! <<");
                Console.ResetColor();

                // Devimon letzter Dialog
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nDevimon: \"Unglaublich... wie konntest du mich besiegen...?!\"");
                Thread.Sleep(2000);
                Console.WriteLine("Devimon: \"Doch dies... ist nicht das Ende... Die Dunkelheit wird zurückkehren...\"");
                Thread.Sleep(2500);
                Console.ResetColor();

                Console.WriteLine("\nDrücke [ENTER], um die Abschlussgeschichte zu hören...");
                Console.ReadLine();

                // Abschlussgeschichte + Abspann direkt aufrufen
                StoryManager.ErzaehleAbschlussGeschichte(spieler);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($">> {spielerDigimon.Name} wurde vom finsteren Devimon besiegt...");
                Console.ResetColor();

                Console.WriteLine("\nDrücke [ENTER], um zur Digiwelt zurückzukehren...");
                Console.ReadLine();
                BewegungsManager.BewegeSpieler(spieler);
            }
        }

    }


}
