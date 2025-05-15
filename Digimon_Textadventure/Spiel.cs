using Digimon_Textadventure;


namespace Digimon_Textadventure
{
    public class Spiel
    {
        private Spieler spieler;

        public void Starte()
        {
            Console.Title = "DIGIMON TEXTADVENTURE";
            Hauptmenue();
        }

        private void Hauptmenue()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== DIGIMON TEXTADVENTURE ===");
                Console.ResetColor();

                Console.WriteLine("\nWähle einen Spielmodus:");
                Console.WriteLine("[1] Neues Abenteuer starten");
                Console.WriteLine("[2] Spielstand laden");
                Console.WriteLine("[3] Digiwelt erkunden");
                Console.WriteLine("[4] Beenden");
                Console.Write("\nDeine Wahl: ");

                string eingabe = Console.ReadLine() ?? "";
                switch (eingabe)
                {
                    case "1":
                        NeuesAbenteuer();
                        break;
                    case "2":
                        Spieler geladenerSpieler = SpeicherManager.Laden();
                        if (geladenerSpieler != null)
                            BewegungsManager.BewegeSpieler(geladenerSpieler);
                        break;
                    case "3":
                        StarteDigiwelt();
                        break;
                    case "4":
                        Console.WriteLine("Danke fürs Spielen! Bis bald...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Ungültige Eingabe.");
                        break;
                }
            }
        }

        private void NeuesAbenteuer()
        {
            Console.Clear();
            StoryManager.ErzaehleEinleitung();

            Avatar avatar = Avatar.WaehleAvatar();
            spieler = new Spieler("Oguz", avatar);

            Digimon startDigimon = Digimon.WaehleStartDigimon();
            spieler.DigimonPartner = startDigimon;

            Console.WriteLine("\nDein Abenteuer beginnt jetzt...");
            Console.WriteLine("Drücke [ENTER], um in die Digiwelt zu reisen.");
            Console.ReadLine();

            StarteDigiwelt();
        }

        private void StarteDigiwelt()
        {
            Digiwelt welt = new Digiwelt(spieler);
            welt.Starte();
        }
    }


}
