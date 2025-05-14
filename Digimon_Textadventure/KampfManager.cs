using System;

namespace Digimon_Textadventure
{
    public class KampfManager
    {
        public void Starte(Digimon spieler, Gegner gegner)
        {
            Kampf kampf = new Kampf(spieler, gegner.Digimon);
            kampf.StarteKampf();
        }
    }
}
