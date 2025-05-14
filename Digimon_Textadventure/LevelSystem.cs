using System;

namespace Digimon_Textadventure
{
    public static class LevelSystem
    {
        public static void VergibErfahrung(Digimon digimon, int erfahrung)
        {
            digimon.Erfahrung += erfahrung;

            while (digimon.Erfahrung >= digimon.ErfahrungFürNaechstesLevel)
            {
                digimon.Erfahrung -= digimon.ErfahrungFürNaechstesLevel;
                digimon.Level++;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{digimon.Name} steigt auf Level {digimon.Level}!");
                Console.ResetColor();

                digimon.MaximaleLebenspunkte += 10;
                digimon.Angriff += 2;
                digimon.Verteidigung += 1;
                digimon.Lebenspunkte = digimon.MaximaleLebenspunkte;

                Console.WriteLine($" => Lebenspunkte: {digimon.Lebenspunkte}");
                Console.WriteLine($" => Angriff: {digimon.Angriff}");
                Console.WriteLine($" => Verteidigung: {digimon.Verteidigung}");
            }

            Console.WriteLine($"Aktuelle Erfahrung: {digimon.Erfahrung}/{digimon.ErfahrungFürNaechstesLevel}");
        }
    }
}

