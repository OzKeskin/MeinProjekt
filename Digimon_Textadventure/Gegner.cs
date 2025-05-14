using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon_Textadventure
{
    public class Gegner
    {
        public Digimon Digimon { get; private set; }
        public string Schwierigkeit { get; private set; } // Optional: "Leicht", "Normal"

        private static Random random = new Random();

        public Gegner(Digimon digimon, string schwierigkeit = "Normal")
        {
            Digimon = digimon;
            Schwierigkeit = schwierigkeit;
            PasseAttributeAn();
        }

        private void PasseAttributeAn()
        {
            switch (Schwierigkeit.ToLower()??"")
            {
                case "leicht":
                    Digimon.Lebenspunkte -= 10;
                    Digimon.Angriff -= 2;
                    break;
                
                    // "Normal" → keine Anpassung nötig
            }
        }

        public static Gegner ErstelleZufaelligenGegner()
        {
            var gegnerDigimon = ErstelleZufallsDigimon();
            string[] schwierigkeitsgrade = { "Leicht", "Normal"};
            string schwierigkeit = schwierigkeitsgrade[random.Next(schwierigkeitsgrade.Length)];

            return new Gegner(gegnerDigimon, schwierigkeit);
        }

        private static Digimon ErstelleZufallsDigimon()
        {
            var gegnerListe = new List<Func<Digimon>>
        {
            Digimon.ErstelleBetamon,
            Digimon.ErstelleVeemon,
            Digimon.ErstelleGomamon
        };

            int index = random.Next(gegnerListe.Count);
            return gegnerListe[index]();
        }

        public void ZeigeProfil()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n=== GEGNER PROFIL ===");
            Console.WriteLine($"Name: {Digimon.Name}");
            Console.WriteLine($"Schwierigkeit: {Schwierigkeit}");
            Console.WriteLine($"Stufe: {Digimon.Stufe}");
            Console.WriteLine($"LP: {Digimon.Lebenspunkte}");
            Console.WriteLine($"ATK: {Digimon.Angriff}");
            Console.WriteLine($"DEF: {Digimon.Verteidigung}");
            Console.WriteLine($"Spezial: {Digimon.Spezialattacke}");
            Console.WriteLine("=======================");
            Console.ResetColor();
        }
    }



}
