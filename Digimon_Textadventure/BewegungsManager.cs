using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon_Textadventure
{
    public static class BewegungsManager
    {
        private static Random random = new Random();

        public static void BewegeSpieler(Spieler spieler)
        {
            string eingabe = "";

            while (eingabe != "exit")
            {
                Console.Clear();
                spieler.AktuellerOrt.Betreten(spieler);


                Console.WriteLine("\nWohin möchtest du gehen? (oder 'exit' zum Verlassen)");
                Console.Write("Eingabe: ");
                eingabe = Console.ReadLine()??"".ToLower() ?? "";

                if (spieler.AktuellerOrt.Verbindungen.ContainsKey(eingabe))
                {
                    var zielOrt = spieler.AktuellerOrt.Verbindungen[eingabe];

                    // Prüfen, ob der Spieler den Endboss-Ort betreten darf
                    if (zielOrt.Name == "Berg der Unendlichkeit" &&
                        (spieler.DigimonPartner == null || spieler.DigimonPartner.Level < 5))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n>> Der Zugang zum Berg der Unendlichkeit ist erst ab Level 5 möglich!");
                        Console.ResetColor();
                        Console.WriteLine("Drücke [ENTER], um fortzufahren...");
                        Console.ReadLine();
                        continue;
                    }

                    // Zugang freigeben
                    spieler.AktuellerOrt = zielOrt;
                    LoeseZufallsEreignisAus(spieler);
                }
                else if (eingabe != "exit")
                {
                    Console.WriteLine("Ungültige Richtung. Drücke [ENTER]...");
                    Console.ReadLine();
                }
            }

            Console.WriteLine("\nDu verlässt die Digiwelt. Drücke [ENTER]...");
            Console.ReadLine();
        }

        private static void LoeseZufallsEreignisAus(Spieler spieler)
        {
            int ereignis = random.Next(1, 101);

            if (ereignis <= 30)
            {
                // Kampf 30%
                //Console.WriteLine("\nEin wildes Digimon erscheint!");
                Gegner gegner = Gegner.ErstelleZufaelligenGegner();

                Kampf kampf = new Kampf(spieler.DigimonPartner, gegner.Digimon);
                kampf.StarteKampf();
            }
            else if (ereignis <= 60)
            {
                // Item-Fund 30%
                string item = "Heiltrank";
                Console.WriteLine($"\nDu hast ein {item} gefunden!");
                spieler.ItemHinzufuegen(item);
                Console.WriteLine("Drücke [ENTER], um weiterzugehen...");
                Console.ReadLine();
            }
            else
            {
                // Nichts passiert 40%
                Console.WriteLine("\nDer Ort ist ruhig. Es passiert nichts Besonderes.");
                Console.WriteLine("Drücke [ENTER], um weiterzugehen...");
                Console.ReadLine();
            }
        }
    }





}

