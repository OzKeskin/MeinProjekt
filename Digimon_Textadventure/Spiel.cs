using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon_Textadventure
{
    public class Spiel
    {
        private List<Digimon> digimonliste = new List<Digimon>();

        public void Starte()
        {
            Console.Title = "DIGIMON TEXTADVENTURE";
            Begrueßung();

            ErstelleStartDigimon();

            Console.WriteLine("\nDein Abenteuer beginnt gleich...");
            Console.WriteLine("Drücke [ENTER], um fortzufahren...");
            Console.ReadLine();
            Console.Clear();

            Begrueßung();

            Console.WriteLine("Du hast folgende Digimon zur Auswahl:");
            ZeigeDigimonListe();

            Digimon spielerDigimon = WaehleStartDigimon();

            // Erstelle zufälligen Gegner
            Gegner gegner = Gegner.ErstelleZufaelligenGegner();

            // Starte den Kampf, übergibt das Digimon des Gegners
            Kampf kampf = new Kampf(spielerDigimon, gegner.Digimon);
            kampf.StarteKampf(); // Methode heißt StarteKampf, nicht Starte
        }

        private void Begrueßung()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===========================================");
            Console.WriteLine(">>>> WILLKOMMEN IM DIGIMON TEXTADVENTURE <<<<");
            Console.WriteLine("===========================================");
            Console.ResetColor();
        }

        private void ErstelleStartDigimon()
        {
            digimonliste.Add(Digimon.ErstelleAgumon());
            digimonliste.Add(Digimon.ErstelleGabumon());
            digimonliste.Add(Digimon.ErstellePatamon());

            

        }

        private void ZeigeDigimonListe()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nDeine verfügbaren Digimon:");
            Console.WriteLine("----------------------------");
            Console.ResetColor();

            foreach (var digi in digimonliste)
            {
                Console.WriteLine($"Name: {digi.Name}");
                Console.WriteLine($"Stufe: {digi.Stufe}");
                Console.WriteLine($"LP: {digi.Lebenspunkte}");
                Console.WriteLine($"ATK: {digi.Angriff}");
                Console.WriteLine($"DEF: {digi.Verteidigung}");
                Console.WriteLine("----------------------------");
            }
        }

        private Digimon WaehleStartDigimon()
        {
            Console.WriteLine("\nWähle dein Start-Digimon:");
            int auswahl = 0;

            while (auswahl < 1 || auswahl > digimonliste.Count)
            {
                Console.Write("=====Deine Wahl=====\n[1] [2] [3] => ");
                int.TryParse(Console.ReadLine(), out auswahl);
            }


            // Bildschirm leeren (außer Überschrift)
            Console.Clear();
            Begrueßung();

            Digimon gewaehltesDigimon = digimonliste[auswahl - 1];
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nDu hast {gewaehltesDigimon.Name} gewählt!\n");
            Console.ResetColor();

            gewaehltesDigimon.ZeigeProfil();
            return gewaehltesDigimon;

            
        }

    }
}


