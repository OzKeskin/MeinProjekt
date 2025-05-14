using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon_Textadventure
{
    public class Digiwelt
    {
        public void Starte()
        {
            Ort startOrt = Ort.ErstelleWelt();
            Spieler spieler = new Spieler("Oguz");
            spieler.AktuellerOrt = startOrt;

            spieler.AktuellerOrt.Betreten();

            BewegungsManager bewegung = new BewegungsManager();
            bewegung.BewegeSpieler(spieler);
        }
    }
}

