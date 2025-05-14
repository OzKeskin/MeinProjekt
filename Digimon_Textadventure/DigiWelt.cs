using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon_Textadventure
{
    public class Digiwelt
    {
        private Spieler spieler;

        // Konstruktor erwartet den Spieler-Objekt
        public Digiwelt(Spieler spieler)
        {
            this.spieler = spieler;
        }

        public void Starte()
        {
            // Welt initialisieren und aktuellen Ort setzen
            Ort startOrt = Ort.ErstelleWelt();
            spieler.AktuellerOrt = startOrt;

            // Starte die Bewegung/Erkundung
            BewegungsManager.BewegeSpieler(spieler);
        }
    }


}

