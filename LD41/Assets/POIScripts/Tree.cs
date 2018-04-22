using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;

namespace Buildings
{
    public class Tree : PointOfInterest
    {
        public Ressource.TYPE ressourceType = Ressource.TYPE.WOOD;
        public int ressource_units_pool = 2000;
        
        public int chop()
        {
            ressource_units_pool =- 5;
            if (ressource_units_pool < 0)
                Destroy(this);
            return 5;
        }
    }
}
