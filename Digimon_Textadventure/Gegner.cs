using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon_Textadventure
{
    public class Gegner
    {
        public Digimon Digimon { get; set; }

        public Gegner(Digimon digimon)
        {
            Digimon = digimon;
        }

        public void ZeigeProfil()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nGegner: {Digimon.Name}");
            Console.ResetColor();
            Console.WriteLine($"Stufe: {Digimon.Stufe}");
            Console.WriteLine($"LP: {Digimon.Lebenspunkte}");
            Console.WriteLine($"ATK: {Digimon.Angriff}");
            Console.WriteLine($"DEF: {Digimon.Verteidigung}");
            Console.WriteLine("----------------------------");
        }

        public static Gegner ErstelleZufaelligenGegner()
        {
            Random random = new Random();
            int zufallszahl = random.Next(1, 4); // 1 bis 3
            Digimon gegnerDigimon;
            switch (zufallszahl)
            {
                case 1:
                    gegnerDigimon = Digimon.ErstelleBetamon();
                    break;
                case 2:
                    gegnerDigimon = Digimon.ErstelleVeemon();
                    break;
                case 3:
                    gegnerDigimon = Digimon.ErstelleGomamon();
                    break;
                default:
                    gegnerDigimon = Digimon.ErstelleBetamon(); // Fallback
                    break;
            }
            return new Gegner(gegnerDigimon);
        }



    }
    //public class Gegner
    //{
    //    public Digimon Digimon { get; set; }

    //    public Gegner(Digimon digimon)
    //    {
    //        Digimon = digimon;
    //    }

    //    public static Gegner ErstelleZufaelligenGegner()
    //    {
    //        Random rnd = new Random();
    //        List<Digimon> gegnerListe = new List<Digimon>
    //    {
    //        Digimon.ErstelleBetamon(),
    //        Digimon.ErstelleVeemon(),
    //        Digimon.ErstelleGomamon()
    //    };

    //        int index = rnd.Next(gegnerListe.Count);
    //        return new Gegner(gegnerListe[index]);
    //    }

    //    public void ZeigeProfil()
    //    {
    //        Console.ForegroundColor = ConsoleColor.Red;
    //        Console.WriteLine($"\nEin feindliches Digimon ist erschienen: {Digimon.Name}");
    //        Console.ResetColor();
    //        Console.WriteLine($"Stufe: {Digimon.Stufe}");
    //        Console.WriteLine($"LP: {Digimon.Lebenspunkte}");
    //        Console.WriteLine($"ATK: {Digimon.Angriff}");
    //        Console.WriteLine($"DEF: {Digimon.Verteidigung}");
    //        Console.WriteLine("----------------------------");
    //    }
    //}

}
