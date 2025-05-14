using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon_Textadventure
{
    public class Avatar
    {
        public string Name { get; set; }
        public string Beschreibung { get; set; }
        public string StartItem { get; set; } = "Geheimnisvolles Amulett"; // Standard-Item

        private Avatar(string name, string beschreibung)
        {
            Name = name;
            Beschreibung = beschreibung;
        }

        // Liste der vorgefertigten Avatare
        public static List<Avatar> VerfügbareAvatare()
        {
            return new List<Avatar>
        {
            new Avatar("Takuya", "Ein mutiger junger Abenteurer mit einem brennenden Willen."),
            new Avatar("Koji", "Ein ruhiger, aber starker Kämpfer mit klarem Verstand."),
            new Avatar("Zoe", "Eine herzliche Kämpferin mit einem unerschütterlichen Glauben an das Gute.")
        };
        }

        public static Avatar WaehleAvatar()
        {
            var avatare = VerfügbareAvatare();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nWähle deinen Avatar:");
            Console.ResetColor();

            for (int i = 0; i < avatare.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {avatare[i].Name} - {avatare[i].Beschreibung}");
            }

            int auswahl = 0;
            while (auswahl < 1 || auswahl > avatare.Count)
            {
                Console.Write("\nDeine Wahl: ");
                int.TryParse(Console.ReadLine(), out auswahl);
            }

            Console.Clear();
            Avatar gewaehlterAvatar = avatare[auswahl - 1];

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Du hast den Avatar {gewaehlterAvatar.Name} gewählt!");
            Console.WriteLine($"Start-Item: {gewaehlterAvatar.StartItem}");
            Console.ResetColor();

            return gewaehlterAvatar;
        }
    }


}
