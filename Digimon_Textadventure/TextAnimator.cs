using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon_Textadventure
{
    class TextAnimator
    {
        public static void ZeigeAnimierteGeschichte(string[] zeilen)
        {
            Console.Clear();
            int startY = (Console.WindowHeight / 2) - (zeilen.Length / 2);

            for (int i = 0; i < zeilen.Length; i++)
            {
                string zeile = zeilen[i];
                int x = (Console.WindowWidth - zeile.Length) / 2;
                int y = startY + i;

                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = ConsoleColor.Green;

                foreach (char buchstabe in zeile)
                {
                    Console.Write(buchstabe);
                    Thread.Sleep(30);
                }

                Console.ResetColor();
                Thread.Sleep(300);
            }

            string weiter = "Drücke [ENTER], um fortzufahren...";
            int weiterX = (Console.WindowWidth - weiter.Length) / 2;
            int weiterY = Console.WindowHeight - 2;

            while (!Console.KeyAvailable)
            {
                Console.SetCursorPosition(weiterX, weiterY);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(weiter);
                Console.ResetColor();
                Thread.Sleep(700);

                Console.SetCursorPosition(weiterX, weiterY);
                Console.Write(new string(' ', weiter.Length));
                Thread.Sleep(500);
            }

            Console.ReadKey(true);
            Console.Clear();
        }

    }
}
