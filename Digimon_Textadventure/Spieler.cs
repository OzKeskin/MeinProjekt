using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digimon_Textadventure
{
    class Spieler
    {
        public string Name { get; set; }

        public Digimon MeinDigimon {get;set;}

        public Spieler(string name)
        {
            Name = name;
        }

    }
}
