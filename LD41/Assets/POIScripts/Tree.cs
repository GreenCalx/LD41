using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using CoreEvent = Assets.Scripts.Event;

namespace POI
{
    public class Tree : PointOfInterest
    {
        public Ressource.TYPE ressourceType = Ressource.TYPE.WOOD;
        public int ressource_units_pool = 2000;
        
        override public List<CoreEvent> generateEvents() { return null; }

        public int chop()
        {
            ressource_units_pool =- 5;
            if (ressource_units_pool < 0)
                Destroy(this);
            return 5;
        }
    }
}
