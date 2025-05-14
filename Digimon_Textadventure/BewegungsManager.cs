using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon_Textadventure
{
    public class BewegungsManager
    {
        public void BewegeSpieler(Spieler spieler)
        {
            while (true)
            {
                Console.WriteLine("\nWohin möchtest du gehen? (norden, osten, süden, westen oder 'ende')");
                Console.Write("Deine Eingabe: ");
                string eingabe = Console.ReadLine()?.ToLower() ?? "";

                if (eingabe == "ende") break;

                if (spieler.AktuellerOrt.Verbindungen.ContainsKey(eingabe))
                {
                    spieler.AktuellerOrt = spieler.AktuellerOrt.Verbindungen[eingabe];
                    Console.Clear();
                    spieler.AktuellerOrt.Betreten();
                }
                else
                {
                    Console.WriteLine("Diese Richtung gibt es nicht. Bitte erneut versuchen.");
                }
            }

            Console.WriteLine("\nDu verlässt die Digiwelt. Drücke [ENTER]...");
            Console.ReadLine();
        }
    }
}

