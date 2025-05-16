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
        public static void ErzaehleLebensAmulettGeschichte(Spieler spieler)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Ein alter Mann mit langem weißen Bart tritt langsam aus dem Schatten...");
            Thread.Sleep(1500);
            Console.WriteLine("\nMeister Genkai: \"Ah, junger Digiritter... du hast große Stärke bewiesen.\"");
            Thread.Sleep(2000);
            Console.WriteLine("Meister Genkai: \"Dieses Amulett wurde einst von den legendären Digirittern getragen.\"");
            Thread.Sleep(2000);
            Console.WriteLine("Meister Genkai: \"Es stärkt die Lebensenergie deines Digimons... für immer!\"");
            Thread.Sleep(2000);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n>> Du hast das 'Amulett der Vitalität' erhalten! Die Lebenspunkte deines Digimons steigen dauerhaft um 30%!");
            Console.ResetColor();

            spieler.DigimonPartner.MaximaleLebenspunkte = (int)(spieler.DigimonPartner.MaximaleLebenspunkte * 1.3);
            spieler.DigimonPartner.Lebenspunkte = spieler.DigimonPartner.MaximaleLebenspunkte;

            spieler.ItemHinzufuegen("Amulett der Vitalität");

            Console.WriteLine("\nDrücke [ENTER], um weiterzugehen...");
            Console.ReadLine();
            Console.Clear();
        }
        public static void ErzaehleKraftAmulettGeschichte(Spieler spieler)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Meister Genkai taucht erneut aus dem Nebel auf, sein Blick voller Ernst...");
            Thread.Sleep(1500);
            Console.WriteLine("\nMeister Genkai: \"Du hast den Weg der Stärke gemeistert, aber wahre Kraft liegt im Herzen.\"");
            Thread.Sleep(2000);
            Console.WriteLine("Meister Genkai: \"Nimm dieses Amulett. Es wird die Schlagkraft deines Digimon für alle Zeiten erhöhen.\"");
            Thread.Sleep(2000);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n>> Du hast das 'Amulett der Stärke' erhalten! Der Angriff deines Digimons steigt dauerhaft um 20%!");
            Console.ResetColor();

            spieler.DigimonPartner.Angriff = (int)(spieler.DigimonPartner.Angriff * 1.2);
            spieler.ItemHinzufuegen("Amulett der Stärke");

            Console.WriteLine("\nDrücke [ENTER], um weiterzugehen...");
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
