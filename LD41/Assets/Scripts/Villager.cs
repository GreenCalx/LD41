using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Villager : InternalEntities
    {
        public STATE currentState { get; set; }

        // ---------- WOOD ------------
        public void getWood()
        {

        }
        public void seekWood()
        {
            World                   world = GetComponentInParent<World>();
            List<Buildings.Tree>    trees = world.getTrees();


        }

        // ----------------------------
    }
}
