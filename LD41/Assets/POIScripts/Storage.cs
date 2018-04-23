﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
namespace POI
{
    class Storage : Building
    {
        //////////////////////////////////////////////
        // STATS
        public int storageSize = 20; // storage increase
        public static Dictionary<Ressource.TYPE, int> cost = new Dictionary<Ressource.TYPE, int>()
        {
            {Ressource.TYPE.WOOD, 15 },
            {Ressource.TYPE.STONE, 10 }
        };
        //////////////////////////////////////////////
    }
}
