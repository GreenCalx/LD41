using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
namespace POI
{
    class Barrack : Building
    {
        //////////////////////////////////////////////
        // STATS
        private int militaryValue = 6; // storage increase
        public static Dictionary<Ressource.TYPE, int> cost = new Dictionary<Ressource.TYPE, int>()
        {
            {Ressource.TYPE.WOOD, 50 },
            {Ressource.TYPE.STONE, 30 },
            {Ressource.TYPE.IRON, 20 }
        };
        //////////////////////////////////////////////
    }
}
