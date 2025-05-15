using Digimon_Textadventure;
using System;
using System.IO;
using System.Text.Json;
namespace Digimon_Textadventure { 
public static class SpeicherManager
{
    private const string SpeicherPfad = "Saves";
    private const string SpeicherDatei = "spielstand.json";

    public static void Speichern(Spieler spieler)
    {
        if (!Directory.Exists(SpeicherPfad))
            Directory.CreateDirectory(SpeicherPfad);

        string pfad = Path.Combine(SpeicherPfad, SpeicherDatei);
        var optionen = new JsonSerializerOptions { WriteIndented = true };

        string json = JsonSerializer.Serialize(spieler, optionen);
        File.WriteAllText(pfad, json);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n>> Spiel erfolgreich gespeichert!");
        Console.ResetColor();
    }

    public static Spieler? Laden()
    {
        string pfad = Path.Combine(SpeicherPfad, SpeicherDatei);

        if (!File.Exists(pfad))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n>> Kein gespeicherter Spielstand gefunden.");
            Console.ResetColor();
            return null;
        }

        string json = File.ReadAllText(pfad);
        var spieler = JsonSerializer.Deserialize<Spieler>(json);

        if (spieler == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n>> Fehler beim Laden des Spielstands. Datei beschädigt?");
            Console.ResetColor();
        }

        return spieler;

    }
}
}