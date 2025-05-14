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

                Console.WriteLine("\n[1] Neues Abenteuer starten");
                Console.WriteLine("[2] Digiwelt erkunden");
                Console.WriteLine("[0] Spiel beenden");
                Console.Write("\nDeine Wahl: ");

                string eingabe = Console.ReadLine() ?? "";

                switch (eingabe)
                {
                    case "1":
                        NeuesAbenteuer();
                        break;
                    case "2":
                        StarteDigiwelt();
                        break;
                    case "0":
                        Console.WriteLine("Danke fürs Spielen! Bis bald.");
                        return;
                    default:
                        Console.WriteLine("Ungültige Eingabe. Drücke [ENTER]...");
                        Console.ReadLine();
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
