using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon_Textadventure
{
    public class Digiwelt
    {
        private Ort aktuellerOrt;
        private Spieler spieler;

        public void Starte()
        {
            // Digiwelt mit festen Orten aufbauen
            aktuellerOrt = Ort.ErstelleWelt();

            // Spieler-Objekt erstellen (ggf. Namen später dynamisch setzen)
            spieler = new Spieler("Oguz", Avatar.WaehleAvatar())
            {
                AktuellerOrt = aktuellerOrt
            };

            BewegungsManager.BewegeSpieler(spieler);
        }

        
    }

}

