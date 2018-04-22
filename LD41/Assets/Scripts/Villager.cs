using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using POI;

namespace Assets.Scripts
{
    public class Villager : InternalEntities
    {
        public STATE currentState { get; set; }

        public void teleportToPOI(PointOfInterest POI)
        {
            if (!!POI)
                transform.position = POI.transform.position;
        }

        // ---------- WOOD ------------
        public void getWood()
        {

        }

        public void seekWood()
        {
            GameObject world_GO = GameObject.Find("World");
            World world = world_GO.GetComponent<World>();

            List<POI.Tree>    trees = world.getTrees();
            if ((trees == null) || (trees.Count == 0))
                return ;

            // Select a Tree
            POI.Tree tree = trees[0];

            // Teleport to PoI
            teleportToPOI(tree);

            // Perform Action
            tree.chop();
        }

        // ----------------------------

        void Update()
        {
            seekWood();
        }

    }
}
