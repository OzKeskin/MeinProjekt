using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon_Textadventure
{
    public class Digimon
    {
        public string Name { get; set; }

        public int Lebenspunkte { get; set; }

        public int Angriff { get; set; }

        public int Verteidigung { get; set; }

        public string Stufe { get; set; }                           // zb. Rookie, Champion

        public Digimon(string name, int lebenspunkte, int angriff, int verteidigung, string stufe)
        {
            Name = name;
            Lebenspunkte = lebenspunkte;
            Angriff = angriff;
            Verteidigung = verteidigung;
            Stufe = stufe;
        }

        public void Anzeigen() 
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Stufe: Stufe");
            Console.WriteLine($"LP: {Lebenspunkte} | ATK: {Angriff} | DEF: {Verteidigung}");
            Console.WriteLine("----------------------------------");
        }

    }
}
