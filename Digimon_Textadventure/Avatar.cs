using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon_Textadventure
{
    public class Avatar
    {
        public string Name { get; set; } = string.Empty;
        public string Beschreibung { get; set; } = string.Empty;
        public string StartItem { get; set; } = string.Empty;

        public Avatar() { }

        // Vorgefertigte, nicht änderbare Avatare
        public static List<Avatar> VerfügbareAvatare() => new()
    {
        new Avatar
        {
            Name = "Taichi",
            Beschreibung = "Ein junger, mutiger und entschlossener Abenteurer.",
            StartItem = "Digivice"
        },
        new Avatar
        {
            Name = "Takeru",
            Beschreibung = "Ein junger, freundlicher Abenteurer voller Hoffnung.",
            StartItem = "Digivice"
        },
        new Avatar
        {
            Name = "Hikari",
            Beschreibung = "Eine ruhige Kämpferin mit großem Herzen.",
            StartItem = "Digivice"
        }
    };

        public static Avatar WaehleAvatar()
        {
            var avatare = VerfügbareAvatare();

            Console.WriteLine("\nWähle deinen Avatar:");
            for (int i = 0; i < avatare.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {avatare[i].Name} - {avatare[i].Beschreibung}");
            }

            int auswahl = 0;
            while (true)
            {
                Console.Write("\nDeine Wahl: ");
                string input = Console.ReadLine() ?? "";

                if (int.TryParse(input, out auswahl) && auswahl >= 1 && auswahl <= avatare.Count)
                {
                    break;
                }

                Console.WriteLine("Ungültige Eingabe. Bitte eine Zahl zwischen 1 und 3 eingeben.");
            }

            Console.Clear();
            var gewaehlterAvatar = avatare[auswahl - 1];

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Du hast den Avatar {gewaehlterAvatar.Name} gewählt!\n");
            Console.ResetColor();

            return gewaehlterAvatar;
        }
    }
}
