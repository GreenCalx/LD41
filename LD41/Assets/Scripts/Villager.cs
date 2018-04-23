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
        protected VillagerSpawn _spawner;

        public STATE currentState { get; set; }

        public void teleportToPOI(PointOfInterest POI)
        {
            if (!!POI)
                transform.position = POI.transform.position;
        }

        public Villager()
        {
            currentState = STATE.FREE;
        }

        // ---------- WOOD ------------
        public void getWood()
        {

        }

        public void seekWood()
        {
            GameObject world_GO = GameObject.Find("World");
            World world = world_GO.GetComponent<World>();

            List<POI.Tree> trees = world.getTrees();
            if ((trees == null) || (trees.Count == 0))
                return;

            // Select a Tree
            foreach (POI.Tree tree in trees)
            {
                if ( tree.use(this) )
                {
                    // Teleport to PoI
                    teleportToPOI(tree);

                    currentState = STATE.BUSY;
                    break;
                }
            }
        }

        // ----------------------------
        void Start()
        {
            GameObject world_GO = GameObject.Find("World");
            _spawner = world_GO.GetComponentInChildren<VillagerSpawn>();
            if (!!_spawner)
                teleportToPOI(_spawner);
        }

        void Update()
        {
            if (currentState == STATE.FREE)
            {
                if( transform.position != _spawner.transform.position)
                    if (!!_spawner)
                        teleportToPOI(_spawner);
                // OCCUPY
                seekWood();
            }
        }

    }
}
