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
        public Ort AktuellerOrt { get; set; }
        public List<string> Inventar { get; set; }

        public Spieler(string name)
        {
            Name = name;
            Inventar = new List<string>();
        }

        public void ZeigeProfil()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n===== SPIELER PROFIL =====");
            Console.ResetColor();

            Console.WriteLine($"Name: {Name}");
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

        public void ItemHinzufuegen(string item)
        {
            Inventar.Add(item);
            Console.WriteLine($"\n>> {item} wurde dem Inventar hinzugefügt.");
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

