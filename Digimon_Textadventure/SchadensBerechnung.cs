namespace Digimon_Textadventure
{
    public static class SchadensBerechnung
    {
        public static int BerechneNormalenSchaden(int angriff, int verteidigung, bool kritisch)
        {
            int schaden = angriff - verteidigung;
            if (kritisch) schaden *= 2;
            return schaden < 0 ? 0 : schaden;
        }

        public static int BerechneSpezialschaden(string spezial, int angriff, int verteidigung)
        {
            return spezial switch
            {
                "Blitzschlag" => (angriff * 2) - verteidigung,
                "Power-Schlag" => (angriff + 5) - verteidigung,
                "Wasserblase" => ((angriff * 3) / 2) - verteidigung,
                _ => 0
            } switch
            {
                < 0 => 0,
                int wert => wert
            };
        }
    }
}

