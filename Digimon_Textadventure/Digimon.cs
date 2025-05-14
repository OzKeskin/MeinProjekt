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

        // Level- und Erfahrungssystem
        public int Level { get; set; } = 1;
        public int Erfahrung { get; set; } = 0;
        public int ErfahrungFürNaechstesLevel => Level * 100;

        // Level-Up & Erfahrungsmethode
        public void VergibErfahrung(int erfahrung)
        {
            Erfahrung += erfahrung;

            while (Erfahrung >= ErfahrungFürNaechstesLevel)
            {
                Erfahrung -= ErfahrungFürNaechstesLevel;
                Level++;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n>> {Name} erreicht Level {Level}!");
                Console.ResetColor();

                // Werteverbesserung
                MaximaleLebenspunkte += 10;
                Lebenspunkte = MaximaleLebenspunkte;
                Angriff += 2;
                Verteidigung += 1;
            }
        }


        public static List<Digimon> VerfügbareStartDigimon()
        {
            return new List<Digimon>
        {
            ErstelleAgumon(),
            ErstelleGabumon(),
            ErstellePatamon()
        };
        }

        public static Digimon WaehleStartDigimon()
        {
            var digimonListe = VerfügbareStartDigimon();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nWähle dein Start-Digimon:");
            Console.ResetColor();

            for (int i = 0; i < digimonListe.Count; i++)
            {
                var d = digimonListe[i];
                Console.WriteLine($"[{i + 1}] {d.Name} (Stufe: {d.Stufe}, LP: {d.Lebenspunkte}, ATK: {d.Angriff}, DEF: {d.Verteidigung}, Spezial: {d.Spezialattacke})");
            }

            int auswahl = 0;
            while (auswahl < 1 || auswahl > digimonListe.Count)
            {
                Console.Write("\nDeine Wahl: ");
                int.TryParse(Console.ReadLine(), out auswahl);
            }

            Console.Clear();
            var gewaehltesDigimon = digimonListe[auswahl - 1];

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nDu hast {gewaehltesDigimon.Name} gewählt!\n");
            Console.ResetColor();

            gewaehltesDigimon.ZeigeProfil();
            return gewaehltesDigimon;
        }

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
            Spezialattacke = "Windstoß"
        };
        public static Digimon ErstelleBetamon() => new Digimon
        {
            Name = "Betamon",
            Lebenspunkte = 90,
            MaximaleLebenspunkte = 90,
            Angriff = 17,
            Verteidigung = 9,
            Stufe = "Rookie",
            Spezialattacke = "Blitzschlag"
        };

        public static Digimon ErstelleVeemon() => new Digimon
        {
            Name = "Veemon",
            Lebenspunkte = 105,
            MaximaleLebenspunkte = 105,
            Angriff = 21,
            Verteidigung = 9,
            Stufe = "Rookie",
            Spezialattacke = "Power-Schlag"
        };

        public static Digimon ErstelleGomamon() => new Digimon
        {
            Name = "Gomamon",
            Lebenspunkte = 100,
            MaximaleLebenspunkte = 100,
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

            // Fortschritt berechnen
            int fortschritt = (Erfahrung * 20) / ErfahrungFürNaechstesLevel;
            
            // Animierter Fortschrittsbalken
            Console.Write("Level-Fortschritt: [");

            for (int i = 0; i < 20; i++)
            {
                if (i < fortschritt)
                {
                    // Farbe je nach Fortschritt setzen
                    if (i < 6)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (i < 14)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = ConsoleColor.Green;

                    Console.Write("#");
                }
                else
                {
                    Console.ResetColor();
                    Console.Write("-");
                }

                Thread.Sleep(50); // Animations-Geschwindigkeit (kann angepasst werden)
            }

            Console.ResetColor();
            Console.WriteLine("]");


        }


        public void FuehreSpezialAttackeAus(Digimon gegner)
        {
            int schaden = 0;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{Name} setzt {Spezialattacke} ein!");
            Console.ResetColor();

            switch (Spezialattacke)
            {
                case "Feuerstoß":
                    schaden = (int)(Angriff * 1.5) - gegner.Verteidigung;
                    break;

                case "Eisblock":
                    schaden = (int)(Angriff * 1.5) - gegner.Verteidigung;
                    break;

                case "Windstoß":
                    // Windstoß: Immer 20% der aktuellen LP des Gegners als Schaden
                    schaden = (int)(gegner.Lebenspunkte * 0.2);
                    if (schaden < 1) schaden = 1; // Mindestens 1 Schaden
                    break;

                default:
                    schaden = Angriff - gegner.Verteidigung;
                    break;
            }

            if (schaden < 1) schaden = 1; // Mindestens 1 Schaden zufügen
            gegner.Lebenspunkte -= schaden;
            if (gegner.Lebenspunkte < 0) gegner.Lebenspunkte = 0;

            Console.WriteLine($"{gegner.Name} erleidet {schaden} Schaden durch {Spezialattacke}.");
            Console.WriteLine($"{gegner.Name} hat noch {gegner.Lebenspunkte} LP.\n");
        }


    }

}
