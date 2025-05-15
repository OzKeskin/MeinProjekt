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
            Console.WriteLine("=================================================================================");
            Console.WriteLine("\t\t >>>   WILLKOMMEN IM DIGIMON TEXTADVENTURE   <<<");
            Console.WriteLine("=================================================================================");
            Console.ResetColor();

            Console.WriteLine("\n\tEine alte Legende berichtet von einer verborgenen Welt...");
            Console.WriteLine("\tEiner Welt voller digitaler Kreaturen – den Digimon.");
            Console.WriteLine("\tDoch dunkle Mächte erheben sich, um das Gleichgewicht zu stören.");
            Console.WriteLine("\tNur ein wahrer Digiritter kann das Licht wiederherstellen!");

            Console.WriteLine("\nBist du bereit, dein Schicksal zu erfüllen und mit deinem Digimon zu kämpfen?");
            Console.WriteLine("\nDrücke [ENTER], um deine Reise zu beginnen...");
            Console.ReadLine();
            Console.Clear();

        }

        public static void Zwischensequenz(string ort)
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
