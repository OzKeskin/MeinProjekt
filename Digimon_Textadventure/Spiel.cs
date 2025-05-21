using System;

namespace Digimon_Textadventure
{
    public class Spiel
    {
        private Spieler spieler;

        public void Starte()
        {
            Console.Title = "DIGIMON WORLD";
            Ladebildschirm.ZeigeLadebildschirm();
            StoryManager.ErzaehleEinleitung();
            ZeigeHauptmenue();

        }

        private void ZeigeHauptmenue()
        {
            string[] menuePunkte =
            {
               "Neues Abenteuer starten",
               "Spielstand laden",
               "Digiwelt erkunden",
               "Beenden"
            };

            int ausgewaehlt = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                // Menü zentrieren
                int startY = (Console.WindowHeight / 2) - (menuePunkte.Length / 2);
                for (int i = 0; i < menuePunkte.Length; i++)
                {
                    string zeile = (i == ausgewaehlt)
                        ? $"=> {menuePunkte[i]} <="
                        : $"   {menuePunkte[i]}";

                    int x = (Console.WindowWidth - zeile.Length) / 2;
                    int y = startY + i;

                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = (i == ausgewaehlt) ? ConsoleColor.DarkBlue : ConsoleColor.Gray;
                    Console.WriteLine(zeile);
                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        ausgewaehlt = (ausgewaehlt == 0) ? menuePunkte.Length - 1 : ausgewaehlt - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        ausgewaehlt = (ausgewaehlt + 1) % menuePunkte.Length;
                        break;
                    case ConsoleKey.Enter:
                        FuehreAktionAus(ausgewaehlt);
                        return;
                }

            } while (true);
        }

        private void FuehreAktionAus(int index)
        {
            switch (index)
            {
                case 0:
                    NeuesAbenteuer();
                    break;
                case 1:
                    Spieler geladenerSpieler = SpeicherManager.Laden();
                    if (geladenerSpieler != null)
                    {
                        Ladebildschirm.ZeigeLadebildschirm(1); // Ladebildschirm anzeigen
                        geladenerSpieler.ZeigeProfil();
                        BewegungsManager.BewegeSpieler(geladenerSpieler);
                    }
                    break;
                case 2:
                    StarteDigiwelt();
                    break;
                case 3:
                    Console.WriteLine("Danke fürs Spielen! Bis bald...");
                    Environment.Exit(0);
                    break;
            }
        }

        private void NeuesAbenteuer()
        {
            Console.Clear();
            StoryManager.ErzaehleEinleitung();

            Avatar avatar = Avatar.WaehleAvatar();
            spieler = new Spieler("Oguz", avatar);

            Digimon startDigimon = Digimon.WaehleStartDigimon();
            spieler.DigimonPartner = startDigimon;

            Console.WriteLine("\nDein Abenteuer beginnt jetzt...");
            Console.WriteLine("Drücke [ENTER], um in die Digiwelt zu reisen.");
            Console.ReadLine();

            StoryManager.ErzeahleAbenteuerStart();
            StarteDigiwelt();

        }

        private void StarteDigiwelt()
        {
            Ladebildschirm.ZeigeLadebildschirm(1); // Ladebildschirm anzeigen
            Digiwelt welt = new Digiwelt(spieler);
            welt.Starte();
        }

    }
}

