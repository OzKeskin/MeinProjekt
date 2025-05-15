using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digimon_Textadventure;


namespace Digimon_Textadventure
{
    public class Spieler
    {
        public string Name { get; set; }
        public Avatar Avatar { get; set; }
        public Ort AktuellerOrt { get; set; }
        public List<string> Inventar { get; set; } = new List<string>();
        public Digimon DigimonPartner { get; set; }

        public Spieler(string name, Avatar avatar)
        {
            Name = name;
            Avatar = avatar;
            if (!string.IsNullOrEmpty(avatar.StartItem))
            {
                Inventar.Add(avatar.StartItem); // Start-Item vom Avatar ins Inventar legen
            }
        }

        public void ZeigeProfil()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n===== SPIELER PROFIL =====");
            Console.ResetColor();

            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Avatar: {Avatar?.Name ?? "Kein Avatar gewählt"}");
            Console.WriteLine($"Aktueller Ort: {AktuellerOrt?.Name ?? "Unbekannt"}");

            Console.WriteLine("\nInventar:");
            if (Inventar.Count == 0)
            {
                Console.WriteLine(" - (leer)");
            }
            else
            {
                foreach (var item in Inventar)
                {
                    Console.WriteLine($" - {item}");
                }
            }

            Console.WriteLine("==========================\n");
        }
        public void ZeigeInventar()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== INVENTAR ===");
            Console.ResetColor();

            if (Inventar.Count == 0)
            {
                Console.WriteLine(" - (Leer)");
            }
            else
            {
                foreach (var item in Inventar)
                {
                    Console.WriteLine($" - {item}");
                }
            }

            Console.WriteLine("\nDrücke [ENTER], um fortzufahren...");
            Console.ReadLine();
        }

        public void ItemHinzufuegen(string item)
        {
            if (!string.IsNullOrWhiteSpace(item))
            {
                Inventar.Add(item);
                Console.WriteLine($"\n>> {item} wurde dem Inventar hinzugefügt.");
            }
        }

        public void ItemEntfernen(string item)
        {
            if (Inventar.Contains(item))
            {
                Inventar.Remove(item);
                Console.WriteLine($"\n>> {item} wurde aus dem Inventar entfernt.");
            }
            else
            {
                Console.WriteLine($"\n>> {item} befindet sich nicht im Inventar.");
            }
        }
    }



}

