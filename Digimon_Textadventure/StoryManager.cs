using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon_Textadventure
{
    public static class StoryManager
    {
        public static void ErzaehleEinleitung()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===========================================");
            Console.WriteLine(">>> WILLKOMMEN IM DIGIMON TEXTADVENTURE <<<");
            Console.WriteLine("===========================================");
            Console.ResetColor();

            Console.WriteLine("\nEine alte Legende erzählt von einer verborgenen Welt...");
            Console.WriteLine("Einer Welt, in der digitale Kreaturen - die Digimon - leben.");
            Console.WriteLine("Doch dunkle Mächte ziehen auf, und nur ein wahrer Digiritter kann das Gleichgewicht bewahren.");
            Console.WriteLine("Bist du bereit, dein Schicksal anzunehmen und Seite an Seite mit deinem Digimon zu kämpfen?");

            Console.WriteLine("\nDrücke [ENTER], um deine Reise zu beginnen...");
            Console.ReadLine();
            Console.Clear();
        }

        public static void ErzaehleZwischensequenz(string ort)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nEine neue Geschichte entfaltet sich am Ort: {ort}...");
            Console.ResetColor();
            Console.WriteLine("Aber was erwartet dich dort wirklich? Gefahr oder ein neues Abenteuer?");
            Console.WriteLine("\nDrücke [ENTER], um weiterzuziehen...");
            Console.ReadLine();
        }

        public static void ErzaehleAbschluss()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDu hast das Abenteuer erfolgreich gemeistert!");
            Console.WriteLine("Doch dies ist erst der Anfang deiner wahren Reise...");
            Console.WriteLine("\nDanke fürs Spielen! Bis bald in der Digiwelt!");
            Console.ResetColor();

            Console.WriteLine("\nDrücke [ENTER], um das Spiel zu beenden...");
            Console.ReadLine();
        }
    }


}
