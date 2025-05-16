using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon_Textadventure
{
    public static class StoryManager
    {
        public static void ErzaehleEinleitung()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=================================================================================");
            Console.WriteLine("\t\t >>>   WILLKOMMEN IM DIGIMON TEXTADVENTURE   <<<");
            Console.WriteLine("=================================================================================");
            Console.ResetColor();

            Console.WriteLine("\n\tEine alte Legende berichtet von einer verborgenen Welt...");
            Console.WriteLine("\tEiner Welt voller digitaler Kreaturen – den Digimon.");
            Console.WriteLine("\tDoch dunkle Mächte erheben sich, um das Gleichgewicht zu stören.");
            Console.WriteLine("\tNur ein wahrer Digiritter kann das Licht wiederherstellen!");

            Console.WriteLine("\nBist du bereit, dein Schicksal zu erfüllen und mit deinem Digimon zu kämpfen?");
            Console.WriteLine("\nDrücke [ENTER], um deine Reise zu beginnen...");
            Console.ReadLine();
            Console.Clear();

        }

        public static void ErzaehleLebensAmulettGeschichte(Spieler spieler)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Ein alter Mann mit langem weißen Bart tritt langsam aus dem Schatten...");
            Thread.Sleep(1500);
            Console.WriteLine("\nMeister Genkai: \"Ah, junger Digiritter... du hast große Stärke bewiesen.\"");
            Thread.Sleep(2000);
            Console.WriteLine("Meister Genkai: \"Dieses Amulett wurde einst von den legendären Digirittern getragen.\"");
            Thread.Sleep(2000);
            Console.WriteLine("Meister Genkai: \"Es stärkt die Lebensenergie deines Digimons... für immer!\"");
            Thread.Sleep(2000);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n>> Du hast das 'Amulett der Vitalität' erhalten! Die Lebenspunkte deines Digimons steigen dauerhaft um 30%!");
            Console.ResetColor();

            spieler.DigimonPartner.MaximaleLebenspunkte = (int)(spieler.DigimonPartner.MaximaleLebenspunkte * 1.3);
            spieler.DigimonPartner.Lebenspunkte = spieler.DigimonPartner.MaximaleLebenspunkte;

            spieler.ItemHinzufuegen("Amulett der Vitalität");

            Console.WriteLine("\nDrücke [ENTER], um weiterzugehen...");
            Console.ReadLine();
            Console.Clear();
        }

        public static void ErzaehleKraftAmulettGeschichte(Spieler spieler)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Meister Genkai taucht erneut aus dem Nebel auf, sein Blick voller Ernst...");
            Thread.Sleep(1500);
            Console.WriteLine("\nMeister Genkai: \"Du hast den Weg der Stärke gemeistert, aber wahre Kraft liegt im Herzen.\"");
            Thread.Sleep(2000);
            Console.WriteLine("Meister Genkai: \"Nimm dieses Amulett. Es wird die Schlagkraft deines Digimon für alle Zeiten erhöhen.\"");
            Thread.Sleep(2000);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n>> Du hast das 'Amulett der Stärke' erhalten! Der Angriff deines Digimons steigt dauerhaft um 20%!");
            Console.ResetColor();

            spieler.DigimonPartner.Angriff = (int)(spieler.DigimonPartner.Angriff * 1.2);
            spieler.ItemHinzufuegen("Amulett der Stärke");

            Console.WriteLine("\nDrücke [ENTER], um weiterzugehen...");
            Console.ReadLine();
            Console.Clear();
        }


        public static void ErzaehleAbschlussGeschichte(Spieler spieler)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ABSCHLUSS-GESCHICHTE – DER SIEG DER LICHTKRIEGER");
            Console.ResetColor();

            string[] abspann =
            {
                    "",
                        "Nach dem langen und harten Kampf gegen Devimon kehrt wieder Frieden in die Digiwelt ein...",
                        "Die dunklen Wolken verziehen sich, die Sonne scheint heller als je zuvor.",
                        "Die Digimon feiern den Sieg der Lichtkrieger mit einem großen Fest.",
                        "",
                        "Meister Gekai tritt zu dir und spricht:",
                        "\"Du hast Mut, Weisheit und ein großes Herz bewiesen, mutiger Digiritter.\"",
                        "\"Dank dir ist das Gleichgewicht der Digiwelt wiederhergestellt.\"",
                        "",
                        "Dein Abenteuer wird in den Chroniken der Digiwelt für alle Zeiten weiterleben...",
                        "",
                        "Drücke [ENTER], um den Abspann zu sehen..."
            };

            foreach (string zeile in abspann)
            {
                foreach (char buchstabe in zeile)
                {
                    Console.Write(buchstabe);
                    Thread.Sleep(30); // Geschwindigkeit der Animation
                }
                Console.WriteLine();
                Thread.Sleep(300); // Kurze Pause nach jeder Zeile
            }
            Console.ReadLine();

            // Abspann Animation
            ZeigeAbspann(spieler.Name);

        }

        public static void ZeigeAbspann(string spielerName)
        {
            Console.Clear();
            string[] abspannTexte =
            {
                        "=== DIGIMON TEXTADVENTURE - ABSPANN ===",
                        "",
                        $"Danke, mutiger Digiritter {spielerName}, dass du dich auf diese Reise begeben hast.",
                        "Gemeinsam mit deinem treuen Digimon-Partner hast du alle Prüfungen gemeistert,",
                        "Freundschaften geschlossen und das Gleichgewicht der Digiwelt wiederhergestellt.",
                        "",
                        "Doch vergiss nie…",
                        "\"Das größte Abenteuer beginnt dort, wo der Mut stärker ist als die Angst.\"",
                        "",
                        "Dein Abenteuer endet hier – aber deine Legende wird ewig weiterleben. ",
                        "",
                        "Drücke [ENTER], um das Spiel zu beenden..."
            };

            foreach (string zeile in abspannTexte)
            {
                Console.WriteLine(zeile);
                Thread.Sleep(500);                                                                                      // Pause zwischen den Zeilen für dramatische Wirkung
            }

            for (int i = 0; i < 3; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\r>> DANKE FÜR'S SPIELEN! <<   ");
                Thread.Sleep(400);
                Console.Write("\r                           ");
                Thread.Sleep(400);
                Console.ResetColor();
            }

            Console.ReadLine();
            Environment.Exit(0);                                                                                        // Spiel sauber beenden
        }

    }


}
