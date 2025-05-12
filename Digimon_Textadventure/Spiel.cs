using System;
using System.Collections.Generic;

public class Spiel
{
    private List<Digimon> digimonliste = new List<Digimon>();

    public void Starte()
    {
        Console.Title = "DIGIMON TEXTADVENTURE";
        Begrueßung();

        ErstelleStartDigimon();

        Console.WriteLine("\nDein Abenteuer beginnt gleich...");
        Console.WriteLine("Drücke [ENTER], um fortzufahren...");
        Console.ReadLine();
        Console.Clear();

        Begrueßung();


        Console.WriteLine("Du hast folgende Digimon zur Auswahl:");
        ZeigeDigimonListe();

        Digimon spielerDigimon = WaehleStartDigimon();
    }

    private void Begrueßung()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("===========================================");
        Console.WriteLine(">>>> WILLKOMMEN IM DIGIMON TEXTADVENTURE <<<<");
        Console.WriteLine("===========================================");
        Console.ResetColor();
    }

    private void ErstelleStartDigimon()
    {
        Digimon agumon = new Digimon
        {
            Name = "Agumon",
            Lebenspunkte = 100,
            Angriff = 20,
            Verteidigung = 10,
            Stufe = "Rookie"
        };

        Digimon gabumon = new Digimon
        {
            Name = "Gabumon",
            Lebenspunkte = 95,
            Angriff = 18,
            Verteidigung = 12,
            Stufe = "Rookie"
        };

        Digimon patamon = new Digimon
        {
            Name = "Patamon",
            Lebenspunkte = 90,
            Angriff = 15,
            Verteidigung = 14,
            Stufe = "Rookie"
        };

        digimonliste.Add(agumon);
        digimonliste.Add(gabumon);
        digimonliste.Add(patamon);
    }

    private void ZeigeDigimonListe()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nDeine verfügbaren Digimon:");
        Console.WriteLine("----------------------------");
        Console.ResetColor();

        foreach (var digi in digimonliste)
        {
            Console.WriteLine($"Name: {digi.Name}");
            Console.WriteLine($"Stufe: {digi.Stufe}");
            Console.WriteLine($"LP: {digi.Lebenspunkte}");
            Console.WriteLine($"ATK: {digi.Angriff}");
            Console.WriteLine($"DEF: {digi.Verteidigung}");
            Console.WriteLine("----------------------------");
        }
    }

    private Digimon WaehleStartDigimon()
    {
        Console.WriteLine("\nWähle dein Start-Digimon:");
        int auswahl = 0;

        while (auswahl < 1 || auswahl > digimonliste.Count)
        {
            Console.Write("=====Deine Wahl=====\n[1] [2] [3] => ");
            int.TryParse(Console.ReadLine(), out auswahl);
        }

        Digimon gewaehltesDigimon = digimonliste[auswahl - 1];

        // Bildschirm leeren (außer Überschrift)
        Console.Clear();
        Begrueßung();

        Console.WriteLine("\nDu hast folgendes Digimon gewählt:");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Name: {gewaehltesDigimon.Name}");
        Console.WriteLine($"Stufe: {gewaehltesDigimon.Stufe}");
        Console.WriteLine($"Lebenspunkte: {gewaehltesDigimon.Lebenspunkte}");
        Console.WriteLine($"Angriff: {gewaehltesDigimon.Angriff}");
        Console.WriteLine($"Verteidigung: {gewaehltesDigimon.Verteidigung}");
        Console.ResetColor();

        return gewaehltesDigimon;
    }

}

public class Digimon
{
    public string Name { get; set; }
    public int Lebenspunkte { get; set; }
    public int Angriff { get; set; }
    public int Verteidigung { get; set; }
    public string Stufe { get; set; }  // z.B. Rookie, Champion
}
