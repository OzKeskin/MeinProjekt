namespace Digimon_Textadventure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Spiel spiel = new Spiel();
            spiel.Starte();

            // ===================== TODO: FINALISIERUNG DES PROJEKTS =====================

            // [ ] Benutzereingaben absichern
            //     - Ungültige Zahlen abfangen
            //     - Leere Eingaben abfangen
            //     - Wiederholung der Eingabe bei Fehlern

            // [x] Kampf- und Statussystem prüfen
            //     - Spezialattacke nur alle 3 Runden nutzbar?
            //     - Heilung und Verteidigung funktionieren korrekt?
            //     - Schadensberechnung funktioniert erwartungsgemäß?
            //     - Statusanzeige ist sauber formatiert (Alignment)?

            // [x] Erfahrung & Levelsystem testen
            //     - Erfahrung wird korrekt vergeben?
            //     - Level-Up ab richtiger XP-Schwelle?
            //     - Statuswerte (ATK, DEF, LP) steigen korrekt beim Level-Up?

            // [x] Digiwelt & Bewegungen testen
            //     - Alle Wege und Rückwege korrekt gesetzt?
            //     - Zugang zum Endboss erst ab Level 5 möglich?
            //     - Zufallsereignisse (Kampf, Fund, Ruhe) werden korrekt ausgelöst?

            // [ ] Dialoge & Präsentation verbessern
            //     - Farben korrekt zurückgesetzt?
            //     - Konsolentexte lesbar und sauber formatiert?
            //     - Statusanzeigen sauber ausgerichtet?

            // [x] Optional: Schönheitskorrekturen
            //     - Runden- und Ereignisanzeigen farbig hervorheben
            //     - Fortschrittsbalken beim Level-Up einbauen
            //     - Soundeffekte mit Console.Beep() hinzufügen (z.B. bei Level-Up oder Sieg)

            // ============================================================================

        }
    }
}
