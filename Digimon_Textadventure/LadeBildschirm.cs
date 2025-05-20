public static class Ladebildschirm
{
    private static string titel = "DIGIMON ADVENTURE";
    private static void ZeigeLadebalken(int x, int y, int breite = 20, int delay = 80)
    {
        Console.SetCursorPosition(x, y);
        Console.Write("["); // Linke Klammer

        for (int i = 0; i < breite; i++)
        {
            // Farbverlauf
            if (i < 6)
                Console.ForegroundColor = ConsoleColor.Red;
            else if (i < 14)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Green;

            Console.Write("#");
            Thread.Sleep(delay);
        }

        Console.ResetColor();
        Console.Write("]"); // Rechte Klammer
    }


    public static void ZeigeLadebildschirm(int wiederholungen = 1)
    {
        Console.Clear();
        int y = Console.WindowHeight / 2 - 2;
        int xTitel = (Console.WindowWidth - titel.Length) / 2;

        // Titel anzeigen
        Console.SetCursorPosition(xTitel, y - 2);
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine(titel);
        Console.ResetColor();

        while (wiederholungen-- > 0)
        {
            // Animierter Lade-Text mit Punkten
            for (int i = 0; i < 2; i++)
            {
                string punktText = "LADEN" + new string('.', i + 1);
                int xPunkt = (Console.WindowWidth - punktText.Length) / 2;
                Console.SetCursorPosition(xPunkt, y);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(punktText);
                Console.ResetColor();
                Thread.Sleep(400);
            }

            // Ladebalken
            int xBalken = (Console.WindowWidth - (20 + 2)) / 2; // 20 Zeichen + 2 Klammern
            int yBalken = y + 2;

            ZeigeLadebalken(xBalken, yBalken);
        }

        // Abschlussanzeige

        string weiter = "Drücke [ENTER]";
        int xWeiter = (Console.WindowWidth - weiter.Length) / 2;
        Console.SetCursorPosition(xWeiter, y + 4);
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write(weiter);
        Console.ResetColor();

        Console.ReadLine();
        Console.Clear();
    }
}
