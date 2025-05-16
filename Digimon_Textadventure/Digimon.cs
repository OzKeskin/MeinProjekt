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
        public int BasisVerteidigung { get; set; }
        public string Stufe { get; set; }
        public string Spezialattacke { get; set; }
        public bool SpezialVerwendet { get; set; } = false;
        public bool WurdeWeiterentwickelt { get; set; } = false;


        // Level- und Erfahrungssystem
        public int Level { get; set; } = 1;
        
        public int Erfahrung { get; set; } = 0;
        
        public int ErfahrungFürNaechstesLevel 
        {
            get
            {
                return Level switch
                {
                    1 => 100,
                    2 => 125,
                    3 => 150,
                    4 => 175,
                    _ => 0                                          // max level erreicht
                };
            }
        
        
        
        }
        
        // Level-Up & Erfahrungsmethode
        public void VergibErfahrung(int erfahrung,Spieler spieler)
        {
            Erfahrung += erfahrung;
            ZeigeLevelFortschritt(animiert: true);                              // fortschrittsbalken direkt nach XP-Gewinn
            LevelUp(spieler);                                                   // hier wird automatisch geprüft ob ein Level-Up erfolgt
        }
        
        public void LevelUp(Spieler spieler)
        {
            while (Erfahrung >= ErfahrungFürNaechstesLevel)
            {
                Erfahrung -= ErfahrungFürNaechstesLevel;
                Level++;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\n>> {Name} erreicht Level {Level}!");
                Console.ResetColor();

                // Werte verbessern
                MaximaleLebenspunkte += 40;
                Lebenspunkte = MaximaleLebenspunkte;
                Angriff += 25;
                Verteidigung += 20;

                Console.WriteLine($"- Neue Lebenspunkte: {MaximaleLebenspunkte}");
                Console.WriteLine($"- Neuer Angriff: {Angriff}");
                Console.WriteLine($"- Neue Verteidigung: {Verteidigung}");

                // Animierter Fortschrittsbalken nach Level-Up
                ZeigeLevelFortschritt(animiert: true);

                // Prüfen auf Weiterentwicklung ab Level 3
                if (Level >= 3 && !WurdeWeiterentwickelt && spieler.Inventar.Contains("Digivice"))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n>> {Name} kann sich weiterentwickeln! Möchtest du die Entwicklung durchführen? (j/n)");
                    Console.ResetColor();

                    string eingabe = Console.ReadLine() ?? "".ToLower() ?? "";
                    if (eingabe == "j")
                    {
                        FühreWeiterentwicklungDurch(spieler);
                    }
                }
            }
        }
        
        private void FühreWeiterentwicklungDurch(Spieler spieler)
        {
            switch (Name)
            {
                case "Agumon":
                    Name = "Greymon";
                    Spezialattacke = "Mega-Feuerstoß";
                    break;
                case "Gabumon":
                    Name = "Garurumon";
                    Spezialattacke = "Mega-Eisblock";
                    break;
                case "Patamon":
                    Name = "Angemon";
                    Spezialattacke = "Heiliges Licht";
                    break;
                default:
                    Console.WriteLine("Keine bekannte Weiterentwicklung.");
                    return;
            }

            Stufe = "Champion";
            MaximaleLebenspunkte *= 3;
            Lebenspunkte = MaximaleLebenspunkte;
            Angriff *= 3;
            Verteidigung *= 3;
            BasisVerteidigung = Verteidigung;
            WurdeWeiterentwickelt = true;

            // Amulett entfernen
            spieler.ItemEntfernen("Digivice");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n>> {Name} hat sich erfolgreich weiterentwickelt!");
            Console.ResetColor();
        }
        
        public void ZeigeProfil()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("===== DIGIMON PROFIL =====");
            Console.ResetColor();
            Console.WriteLine($"Name:\t\t{Name}");
            Console.WriteLine($"Stufe:\t\t{Stufe}");
            Console.WriteLine($"Lebenspunkte:\t{Lebenspunkte}/{MaximaleLebenspunkte}");
            Console.WriteLine($"Angriff:\t{Angriff}");
            Console.WriteLine($"Verteidigung:\t{Verteidigung}");
            Console.WriteLine($"Level:\t\t{Level}");
            Console.WriteLine($"Erfahrung:\t{Erfahrung}/{ErfahrungFürNaechstesLevel}");

            ZeigeLevelFortschritt(animiert: false);

            if (!string.IsNullOrEmpty(Spezialattacke))
                Console.WriteLine($"Spezialfähigkeit\n-> {Spezialattacke} <-");

            if (SpezialVerwendet)
                Console.WriteLine("Spezialfähigkeit wurde in diesem Kampf eingesetzt!");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==========================");
            Console.ResetColor();

        }

        public void ZeigeLevelFortschritt(bool animiert)
        {
            int fortschritt = (ErfahrungFürNaechstesLevel == 0) ? 20 : (Erfahrung * 20) / ErfahrungFürNaechstesLevel;

            Console.Write("Level-Fortschritt\n[");

            for (int i = 0; i < 20; i++)
            {
                if (i < fortschritt)
                {
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

                if (animiert) Thread.Sleep(50);
            }

            Console.ResetColor();

            if (ErfahrungFürNaechstesLevel == 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("] MAX LEVEL erreicht!");
            }
            else
            {
                Console.WriteLine($"] {fortschritt * 5}%");
            }

            Console.ResetColor();
        }

        public static List<Digimon> VerfügbareStartDigimon()
        {
            return 
        [
            ErstelleAgumon(),
            ErstelleGabumon(),
            ErstellePatamon()
        ];
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

            int auswahl;
            while (true)
            {
                Console.Write("\nDeine Wahl: ");
                string eingabe = Console.ReadLine() ?? "";

                if (int.TryParse(eingabe, out auswahl) && auswahl >= 1 && auswahl <= digimonListe.Count)
                {
                    break; // Gültige Eingabe, Schleife beenden
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ungültige Eingabe. Bitte gib eine gültige Zahl ein.");
                Console.ResetColor();
            }

            Console.Clear();
            var gewaehltesDigimon = digimonListe[auswahl - 1];

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nDu hast {gewaehltesDigimon.Name} gewählt!\n");
            Console.ResetColor();

            gewaehltesDigimon.ZeigeProfil();
            return gewaehltesDigimon;
        }
        
        public static Digimon ErstelleAgumon() => new ()
        {
            Name = "Agumon",
            Lebenspunkte = 100,
            MaximaleLebenspunkte = 100,
            Angriff = 20,
            Verteidigung = 10,
            BasisVerteidigung = 10, // Neu hinzugefügt
            Stufe = "Rookie",
            Spezialattacke = "Feuerstoß"
        };
        
        public static Digimon ErstelleGabumon() => new ()
        {
            Name = "Gabumon",
            Lebenspunkte = 95,
            MaximaleLebenspunkte = 95,
            Angriff = 18,
            Verteidigung = 12,
            BasisVerteidigung = 12, // Neu hinzugefügt
            Stufe = "Rookie",
            Spezialattacke = "Eisblock"
        };
        
        public static Digimon ErstellePatamon() => new ()
        {
            Name = "Patamon",
            Lebenspunkte = 90,
            MaximaleLebenspunkte = 90,
            Angriff = 16,
            Verteidigung = 11,
            BasisVerteidigung = 11, // Neu hinzugefügt
            Stufe = "Rookie",
            Spezialattacke = "Windstoß"
        };
        
        public static Digimon ErstelleBetamon() => new ()
        {
            Name = "Betamon",
            Lebenspunkte = 90,
            MaximaleLebenspunkte = 90,
            Angriff = 17,
            Verteidigung = 9,
            BasisVerteidigung = 9, // Neu hinzugefügt
            Stufe = "Rookie",
            Spezialattacke = "Blitzschlag"
        };
        
        public static Digimon ErstelleVeemon() => new ()
        {
            Name = "Veemon",
            Lebenspunkte = 105,
            MaximaleLebenspunkte = 105,
            Angriff = 21,
            Verteidigung = 9,
            BasisVerteidigung = 9, // Neu hinzugefügt

            Stufe = "Rookie",
            Spezialattacke = "Power-Schlag"
        };
        
        public static Digimon ErstelleGomamon() => new ()
        {
            Name = "Gomamon",
            Lebenspunkte = 100,
            MaximaleLebenspunkte = 100,
            Angriff = 17,
            Verteidigung = 13,
            BasisVerteidigung = 13, // Neu hinzugefügt
            Stufe = "Rookie",
            Spezialattacke = "Wasserblase"
        };

        // Boss-Digimon
        public static Digimon ErstelleDevimon() => new ()
        {
            Name = "Devimon",
            Lebenspunkte = 500,
            MaximaleLebenspunkte = 500,
            Angriff = 50,
            Verteidigung = 30,
            BasisVerteidigung = 30,
            Stufe = "Champion",
            Spezialattacke = "Todeskralle, Böse Flügel",                            // Beide Attacken als Hinweis
            WurdeWeiterentwickelt = true                                            // Damit keine weitere Entwicklung erfolgt
        };
        
        public void FuehreSpezialAttackeAus(Digimon gegner)
        {
            int schaden = 0;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{Name} setzt {Spezialattacke} ein!");
            Console.ResetColor();

            switch (Spezialattacke)
            {
                case "Feuerstoß":
                case "Eisblock":
                case "Windstoß":
                    schaden = (int)(gegner.Lebenspunkte * 0.5);
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
