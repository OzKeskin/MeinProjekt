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

            string[] zeilen =
            {
                "Eine alte Legende berichtet von einer verborgenen Welt...",
                "Einer Welt voller digitaler Kreaturen – den Digimon.",
                "Doch dunkle Mächte erheben sich, um das Gleichgewicht zu stören.",
                "Nur ein wahrer Digiritter kann das Licht wiederherstellen!",
                "Bist du bereit, dein Schicksal zu erfüllen und mit deinem Digimon zu kämpfen?"
            };

            TextAnimator.ZeigeAnimierteGeschichte(zeilen);
        }

        public static void ErzeahleAbenteuerStart()
        {
            Console.Clear();
            string[] zeilen =
            {
                "Die Digiwelt formt sich langsam vor deinen Augen...",
                "Pixel für Pixel... Licht für Licht...",
                "Eine neue Welt erwartet dich...",
                "",
                "Bereite dich vor – dein Abenteuer beginnt JETZT!"
            };

            TextAnimator.ZeigeAnimierteGeschichte(zeilen);

        }

        public static void ErzaehleLebensAmulettGeschichte(Spieler spieler)
        {
            string[] zeilen =
            {
                "Ein alter Mann tritt aus dem Schatten eines Baumes hervor...",
                "\"Du bist also der Auserwählte...\" murmelt er leise.",
                "Er reicht dir ein leuchtendes Amulett mit warmem Glanz.",
                "\"Dieses Amulett wird die Lebensenergie deines Digimon stärken.\"",
                "\"Bewahre es gut auf – es ist selten und mächtig.\""
            };

            TextAnimator.ZeigeAnimierteGeschichte(zeilen);

            spieler.ItemHinzufuegen("Amulett der Vitalität");
            if (spieler.DigimonPartner != null)
            {
                spieler.DigimonPartner.MaximaleLebenspunkte = (int)(spieler.DigimonPartner.MaximaleLebenspunkte * 1.3);
                spieler.DigimonPartner.Lebenspunkte = spieler.DigimonPartner.MaximaleLebenspunkte;
            }
        }

        public static void ErzaehleKraftAmulettGeschichte(Spieler spieler)
        {
            string[] zeilen =
            {
                "Der alte Mann erscheint erneut, dieses Mal am Rande eines Wasserfalls.",
                "\"Du hast dich bewährt. Deine Reise ist von Mut erfüllt.\"",
                "Er überreicht dir ein weiteres Amulett – mit pulsierendem roten Licht.",
                "\"Dieses stärkt die Angriffskraft deines Digimon – dauerhaft.\"",
                "\"Nutze es weise, Krieger des Lichts.\""
            };

            TextAnimator.ZeigeAnimierteGeschichte(zeilen);

            spieler.ItemHinzufuegen("Amulett der Stärke");
            if (spieler.DigimonPartner != null)
            {
                spieler.DigimonPartner.Angriff = (int)(spieler.DigimonPartner.Angriff * 1.2);
            }

        }


        public static void ErzaehleAbschlussGeschichte(Spieler spieler)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ABSCHLUSS-GESCHICHTE – DER SIEG DER LICHTKRIEGER");
            Console.ResetColor();

            string[] zeilen =
            {
                    "",
                        "ABSCHLUSS-GESCHICHTE – DER SIEG DER LICHTKRIEGER",
                        "",
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

            TextAnimator.ZeigeAnimierteGeschichte(zeilen);
            ZeigeAbspann(spieler.Name);

        }

        public static void ZeigeAbspann(string spielerName)
        {
            Console.Clear();
            string[] zeilen =
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
                "Dein Abenteuer endet hier – aber deine Legende wird ewig weiterleben.",
                ""
            };

            TextAnimator.ZeigeAnimierteGeschichte(zeilen);


        }



    }
}
