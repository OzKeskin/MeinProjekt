using System;

namespace Digimon_Textadventure
{
    public class Digimon
    {
        public string Name { get; set; }
        public int Lebenspunkte { get; set; }
        public int MaximaleLebenspunkte { get; set; }

        public int Angriff { get; set; }
        public int Verteidigung { get; set; }
        public string Stufe { get; set; }

        public string Spezialattacke { get; set; }
        public bool SpezialVerwendet { get; set; } = false;

        public int Level { get; set; } = 1;
        public int Erfahrung { get; set; } = 0;
        public int ErfahrungFürNaechstesLevel => Level * 100;

        public static Digimon ErstelleAgumon() => new Digimon
        {
            Name = "Agumon",
            Lebenspunkte = 100,
            MaximaleLebenspunkte = 100,
            Angriff = 20,
            Verteidigung = 10,
            Stufe = "Rookie",
            Spezialattacke = "Feuerstoß"
        };

        public static Digimon ErstelleGabumon() => new Digimon
        {
            Name = "Gabumon",
            Lebenspunkte = 95,
            MaximaleLebenspunkte = 95,
            Angriff = 18,
            Verteidigung = 12,
            Stufe = "Rookie",
            Spezialattacke = "Eisblock"
        };

        public static Digimon ErstellePatamon() => new Digimon
        {
            Name = "Patamon",
            Lebenspunkte = 90,
            MaximaleLebenspunkte = 90,
            Angriff = 16,
            Verteidigung = 11,
            Stufe = "Rookie",
            Spezialattacke = "Heilwelle"
        };

        public static Digimon ErstelleBetamon() => new Digimon
        {
            Name = "Betamon",
            Lebenspunkte = 90,
            Angriff = 17,
            Verteidigung = 9,
            Stufe = "Rookie",
            Spezialattacke = "Blitzschlag"
        };

        public static Digimon ErstelleVeemon() => new Digimon
        {
            Name = "Veemon",
            Lebenspunkte = 105,
            Angriff = 21,
            Verteidigung = 9,
            Stufe = "Rookie",
            Spezialattacke = "Power-Schlag"
        };

        public static Digimon ErstelleGomamon() => new Digimon
        {
            Name = "Gomamon",
            Lebenspunkte = 100,
            Angriff = 17,
            Verteidigung = 13,
            Stufe = "Rookie",
            Spezialattacke = "Wasserblase"
        };

        public void ZeigeProfil()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== DIGIMON PROFIL ===");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Stufe: {Stufe}");
            Console.WriteLine($"Lebenspunkte: {Lebenspunkte}/{MaximaleLebenspunkte}");
            Console.WriteLine($"Angriff: {Angriff}");
            Console.WriteLine($"Verteidigung: {Verteidigung}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Erfahrung: {Erfahrung}/{ErfahrungFürNaechstesLevel}");

            int fortschritt = (Erfahrung * 20) / ErfahrungFürNaechstesLevel;
            string balken = new string('#', fortschritt).PadRight(20, '-');
            Console.WriteLine($"Level-Fortschritt: [{balken}]");

            if (!string.IsNullOrEmpty(Spezialattacke))
                Console.WriteLine($"Spezialfähigkeit: {Spezialattacke}");

            if (SpezialVerwendet)
                Console.WriteLine("Spezialfähigkeit wurde in diesem Kampf eingesetzt!");

            Console.WriteLine("=======================");
            Console.ResetColor();
        }

    }
}
