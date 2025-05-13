using Digimon_Textadventure;

public class Kampf
{
    private Digimon spieler;
    private Gegner gegner;
    private bool heilungVerwendet = false;

    public Kampf(Digimon spieler, Digimon gegnerDigimon)
    {
        this.spieler = spieler;
        this.gegner = new Gegner(gegnerDigimon);
    }

    public void StarteKampf()
    {
        Console.Clear();
        ZeigeBegruessung();

        int runde = 1;
        while (spieler.Lebenspunkte > 0 && gegner.Digimon.Lebenspunkte > 0)
        {
            ZeigeRundenStart(runde);
            ZeigeLPStatus();
            SpielerAktion();

            if (gegner.Digimon.Lebenspunkte <= 0)
            {
                BeendeKampfMitSieg();
                break;
            }

            Thread.Sleep(1000);
            GegnerGreiftAn();

            if (spieler.Lebenspunkte <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{spieler.Name} wurde besiegt! Game Over.");
                Console.ResetColor();
                break;
            }

            Console.WriteLine("\nDrücke [ENTER] für die nächste Runde...");
            Console.ReadLine();
            Console.Clear();
            runde++;
        }

        ZeigeKampfErgebnis();
        VergibErfahrung(spieler);
        NachKampfMenue();
    }

    private void ZeigeBegruessung()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nEin feindliches Digimon erscheint: {gegner.Digimon.Name}!\n");
        Console.ResetColor();

        Console.WriteLine("Drücke [ENTER], um den Kampf zu beginnen...");
        Console.ReadLine();
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("========== KAMPF BEGINNT ==========\n");
        Console.ResetColor();
    }

    private void ZeigeRundenStart(int runde)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine($"----- Runde {runde} -----");
        Console.ResetColor();
    }

    private void ZeigeLPStatus()
    {
        Console.WriteLine($"{spieler.Name} (LP: {spieler.Lebenspunkte}) vs {gegner.Digimon.Name} (LP: {gegner.Digimon.Lebenspunkte})\n");
    }

    private void SpielerAktion()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Wähle deine Aktion:");
        Console.ResetColor();

        Console.WriteLine("[1] Angreifen");
        Console.WriteLine("[2] Heilen" + (heilungVerwendet ? " (bereits verwendet)" : ""));
        Console.WriteLine("[3] Verteidigen");
        Console.WriteLine("[4] Spezialfähigkeit" + (spieler.SpezialVerwendet ? " (bereits verwendet)" : $" ({spieler.Spezialattacke})"));

        int wahl = 0;
        while (wahl < 1 || wahl > 4)
        {
            Console.Write("Deine Wahl: ");
            int.TryParse(Console.ReadLine(), out wahl);
        }

        switch (wahl)
        {
            case 1: SpielerGreiftAn(); break;
            case 2: Heilen(); break;
            case 3: Verteidigen(); break;
            case 4: Spezialangriff(); break;
        }
    }

    private void SpielerGreiftAn()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{spieler.Name} greift {gegner.Digimon.Name} an!");
        Console.ResetColor();

        bool kritisch = new Random().Next(1, 101) <= 20;
        int schaden = spieler.Angriff - gegner.Digimon.Verteidigung;

        if (kritisch)
        {
            schaden *= 2;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Kritischer Treffer!");
            Console.ResetColor();
        }

        if (schaden < 0) schaden = 0;
        gegner.Digimon.Lebenspunkte -= schaden;
        Console.WriteLine($"{gegner.Digimon.Name} erleidet {schaden} Schaden!");
    }

    private void GegnerGreiftAn()
    {
        bool nutzeSpezial = !gegner.Digimon.SpezialVerwendet && new Random().Next(1, 101) <= 30;

        if (nutzeSpezial)
        {
            GegnerSpezialangriff();
            gegner.Digimon.SpezialVerwendet = true;
            return;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n{gegner.Digimon.Name} greift {spieler.Name} an!");
        Console.ResetColor();

        bool kritisch = new Random().Next(1, 101) <= 20;
        int schaden = gegner.Digimon.Angriff - spieler.Verteidigung;

        if (kritisch)
        {
            schaden *= 2;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Kritischer Treffer!");
            Console.ResetColor();
        }

        if (schaden < 0) schaden = 0;
        spieler.Lebenspunkte -= schaden;
        Console.WriteLine($"{spieler.Name} erleidet {schaden} Schaden!");
    }

    private void Heilen()
    {
        if (heilungVerwendet)
        {
            Console.WriteLine("Du hast deine Heilung bereits verwendet.");
            return;
        }

        int heilwert = 30;
        spieler.Lebenspunkte += heilwert;
        heilungVerwendet = true;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{spieler.Name} heilt sich um {heilwert} LP!");
        Console.ResetColor();
    }

    private void Verteidigen()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{spieler.Name} erhöht seine Verteidigung!");
        Console.ResetColor();
        spieler.Verteidigung += 5;
    }

    private void Spezialangriff()
    {
        if (spieler.SpezialVerwendet)
        {
            Console.WriteLine("Du hast deine Spezialfähigkeit bereits verwendet.");
            return;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"{spieler.Name} setzt {spieler.Spezialattacke} ein!");
        Console.ResetColor();

        switch (spieler.Spezialattacke)
        {
            case "Feuerstoß":
                int schaden = (spieler.Angriff * 2) - gegner.Digimon.Verteidigung;
                if (schaden < 0) schaden = 0;
                gegner.Digimon.Lebenspunkte -= schaden;
                Console.WriteLine($"{gegner.Digimon.Name} erleidet {schaden} Schaden!");
                break;

            case "Heilwelle":
                int heilung = 40;
                spieler.Lebenspunkte += heilung;
                Console.WriteLine($"{spieler.Name} heilt sich um {heilung} LP!");
                break;

            case "Eisblock":
                spieler.Verteidigung += 10;
                Console.WriteLine($"{spieler.Name} erhöht seine Verteidigung um 10!");
                break;
        }

        spieler.SpezialVerwendet = true;
    }

    private void GegnerSpezialangriff()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n{gegner.Digimon.Name} setzt {gegner.Digimon.Spezialattacke} ein!");
        Console.ResetColor();

        int schaden = gegner.Digimon.Spezialattacke switch
        {
            "Blitzschlag" => gegner.Digimon.Angriff * 2 - spieler.Verteidigung,
            "Power-Schlag" => gegner.Digimon.Angriff + 5 - spieler.Verteidigung,
            "Wasserblase" => (gegner.Digimon.Angriff * 3 / 2) - spieler.Verteidigung,
            _ => 0
        };

        if (schaden < 0) schaden = 0;
        spieler.Lebenspunkte -= schaden;
        Console.WriteLine($"{spieler.Name} erleidet {schaden} Schaden durch {gegner.Digimon.Spezialattacke}!");
    }

    private void HeileDigimon(Digimon digimon)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n{digimon.Name} wird vollständig geheilt!");
        Console.ResetColor();

        digimon.Lebenspunkte = digimon.MaximaleLebenspunkte;
        Console.WriteLine($"{digimon.Name} hat nun wieder {digimon.Lebenspunkte} Lebenspunkte.");
    }

    private void BeendeKampfMitSieg()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nDu hast {gegner.Digimon.Name} besiegt!");
        Console.ResetColor();

        HeileDigimon(spieler);
        LevelUp(spieler);
    }

    private void LevelUp(Digimon digimon)
    {
        int erfahrung = 100;
        digimon.Erfahrung += erfahrung;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n{digimon.Name} erhält {erfahrung} Erfahrungspunkte!");
        Console.ResetColor();

        while (digimon.Erfahrung >= digimon.ErfahrungFürNaechstesLevel)
        {
            digimon.Erfahrung -= digimon.ErfahrungFürNaechstesLevel;
            digimon.Level++;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n{digimon.Name} steigt auf Level {digimon.Level} auf!");
            Console.ResetColor();

            digimon.MaximaleLebenspunkte += 10;
            digimon.Lebenspunkte = digimon.MaximaleLebenspunkte;
            digimon.Angriff += 2;
            digimon.Verteidigung += 1;

            Console.WriteLine($" => Lebenspunkte: {digimon.Lebenspunkte}");
            Console.WriteLine($" => Angriff: {digimon.Angriff}");
            Console.WriteLine($" => Verteidigung: {digimon.Verteidigung}");
        }

        Console.WriteLine($"Aktuelle Erfahrung: {digimon.Erfahrung}/{digimon.ErfahrungFürNaechstesLevel}");
    }

    private void VergibErfahrung(Digimon digimon)
    {
        int basisXP = gegner.Digimon.Angriff + gegner.Digimon.Verteidigung + (gegner.Digimon.Level * 50);
        int bonusXP = digimon.SpezialVerwendet ? 20 : 0;
        int gesamtXP = basisXP + bonusXP;

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"\n{digimon.Name} erhält {gesamtXP} Erfahrungspunkte!");
        Console.ResetColor();

        digimon.Erfahrung += gesamtXP;

        while (digimon.Erfahrung >= digimon.ErfahrungFürNaechstesLevel)
        {
            digimon.Erfahrung -= digimon.ErfahrungFürNaechstesLevel;
            digimon.Level++;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{digimon.Name} steigt auf Level {digimon.Level}!");
            Console.ResetColor();
        }
    }

    private void ZeigeKampfErgebnis()
    {
        Console.WriteLine("\n=== Kampf beendet ===");
        if (spieler.Lebenspunkte <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Dein Digimon wurde besiegt. Du hast verloren.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Du hast gewonnen!");
        }
        Console.ResetColor();
    }

    private void NachKampfMenue()
    {
        Console.WriteLine("\nWas möchtest du als Nächstes tun?");
        Console.WriteLine("[1] Neues Spiel starten");
        Console.WriteLine("[2] Spiel beenden");

        string eingabe = "";
        while (eingabe != "1" && eingabe != "2")
        {
            Console.Write("Deine Auswahl: ");
            eingabe = Console.ReadLine() ?? "";
        }

        if (eingabe == "1")
        {
            Console.Clear();
            Spiel neuesSpiel = new Spiel();
            neuesSpiel.Starte();
        }
        else
        {
            Console.WriteLine("Danke fürs Spielen! Bis bald.");
            Environment.Exit(0);
        }
    }
}
