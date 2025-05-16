using Digimon_Textadventure;
using System;
using System.IO;
using System.Text.Json;
namespace Digimon_Textadventure
{
    
    public static class SpeicherManager
    {
        public static string SpeicherPfad { get; set; } = @"C:\Users\KeskinOguz\Desktop\MeinProjekt\Digimon_Textadventure";
        public static string SpeicherDatei { get; set; } = "spielstand.json";

        public static void Speichern(Spieler spieler)
        {
            if (!Directory.Exists(SpeicherPfad))
                Directory.CreateDirectory(SpeicherPfad);

            string pfad = Path.Combine(SpeicherPfad, SpeicherDatei);

            var optionen = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };

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

            var optionen = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };

            string json = File.ReadAllText(pfad);
            var spieler = JsonSerializer.Deserialize<Spieler>(json, optionen);

            if (spieler == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n>> Fehler beim Laden des Spielstands. Datei beschädigt?");
                Console.ResetColor();
                return null;
            }

            // Stelle sicher, dass der Avatar korrekt referenziert wird
            if (!string.IsNullOrEmpty(spieler.Avatar?.Name))
            {
                var avatar = Avatar.VerfügbareAvatare()
                                   .FirstOrDefault(a => a.Name == spieler.Avatar.Name);
                if (avatar != null)
                {
                    spieler.Avatar = avatar;
                }
            }

            // Ort anhand des Namens setzen
            if (spieler.AktuellerOrt != null)
            {
                spieler.AktuellerOrt = Ort.FindeOrtNachName(spieler.AktuellerOrt.Name);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n>> Spielstand erfolgreich geladen!");
            Console.ResetColor();

            // Starte direkt in der Digiwelt
            BewegungsManager.BewegeSpieler(spieler);

            return spieler;
        }
    }

}