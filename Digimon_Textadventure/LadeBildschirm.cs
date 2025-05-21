using System;
using System.Threading;

public static class Ladebildschirm
{
    public static void ZeigeLadebildschirm(int wiederholungen = 1)
    {
        Console.Clear();
        int y = Console.WindowHeight / 2 - 2;

        // Bunter Titel: "DIGIMON ADVENTURE"
        string titel = "DIGIMON ADVENTURE";
        int xTitel = (Console.WindowWidth - titel.Length) / 2;
        ConsoleColor[] farben = new[]
        {
            ConsoleColor.DarkYellow, // Tai
            ConsoleColor.Blue,       // Matt
            ConsoleColor.Red,        // Sora
            ConsoleColor.Magenta,    // Mimi
            ConsoleColor.Cyan,       // Joe
            ConsoleColor.Yellow,     // T.K.
            ConsoleColor.DarkMagenta // Kari
        };

        for (int i = 0; i < titel.Length; i++)
        {
            Console.SetCursorPosition(xTitel + i, y - 2);
            Console.ForegroundColor = farben[i % farben.Length];
            Console.Write(titel[i]);
            Thread.Sleep(150);
        }
        Console.ResetColor();

        // Schrittweises Anzeigen von "LADEN"
        string text = "LADEN";
        int xText = (Console.WindowWidth - text.Length) / 2;
        int yText = y;

        for (int i = 0; i < text.Length; i++)
        {
            Console.SetCursorPosition(xText + i, yText);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text[i]);
            Console.ResetColor();
            Thread.Sleep(300);
        }

        // Ladebalken anzeigen (in Weiß)
        int balkenBreite = 20;
        int xBalken = (Console.WindowWidth - (balkenBreite + 2)) / 2;
        int yBalken = y + 2;

        Console.SetCursorPosition(xBalken, yBalken);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("[");
        Console.SetCursorPosition(xBalken + balkenBreite + 1, yBalken);
        Console.Write("]");
        Console.ResetColor();

        for (int i = 0; i < balkenBreite; i++)
        {
            Console.SetCursorPosition(xBalken + 1 + i, yBalken);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("#");
            Console.ResetColor();
            Thread.Sleep(100);
        }

        // Kurze Pause nach Ladebalken
        Thread.Sleep(800);

        // Alles gleichzeitig löschen
        Console.SetCursorPosition(xText, yText);
        Console.Write(new string(' ', text.Length));

        Console.SetCursorPosition(xBalken, yBalken);
        Console.Write(new string(' ', balkenBreite + 2));

        // Abschlussanzeige an Position von "LADEN"
        // Abschlussanzeige
        string weiter = "Drücke [ENTER]";
        int weiterX = (Console.WindowWidth - weiter.Length) / 2;
        int weiterY = yText;

        bool enterGedrückt = false;
        while (!enterGedrückt)
        {
            Console.SetCursorPosition(weiterX, weiterY);
            Console.ForegroundColor = ConsoleColor.Cyan; 
            Console.Write(weiter);
            Console.ResetColor();
            Thread.Sleep(700);

            Console.SetCursorPosition(weiterX, weiterY);
            Console.Write(new string(' ', weiter.Length));
            Thread.Sleep(500);

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    enterGedrückt = true;
            }
        }

    }
}
