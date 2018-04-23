﻿using System;
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



        public void teleportToPOI(PointOfInterest POI)
        {
            if (!!POI)
            {
                int user_index = POI.user_index(this);
                float x_offset = (float)(user_index * 0.5);
                float y_offset = (float)(user_index * 0.2);
                Vector3 offset = new Vector3(0 + x_offset, 0 + y_offset, -0.1f);
                transform.position = POI.transform.position + offset;
            }
        }

        public Villager()
        {
            currentState = STATE.FREE;
        }

        // ---------- WOOD ------------
        public bool seekWood()
        {
            bool foundAJob = false;
            GameObject world_GO = GameObject.Find("World");
            World world = world_GO.GetComponent<World>();

            List<POI.Tree> trees = world.getTrees();
            if ((trees == null) || (trees.Count == 0))
                return foundAJob;

            // Select a Tree
            foreach (POI.Tree tree in trees)
            {
                if ( tree.use(this) )
                {
                    // Teleport to PoI
                    teleportToPOI(tree);

                    currentState = STATE.BUSY;
                    foundAJob = true;
                    break;
                }
            }
            return foundAJob;
        }

        
        public bool seekStone()
        {
            bool foundAJob = false;

            GameObject world_GO = GameObject.Find("World");
            World world = world_GO.GetComponent<World>();

            List<Rocks> rocks = world.getRocks();
            if ((rocks == null) || (rocks.Count == 0))
                return foundAJob;

            // Select a Tree
            foreach (Rocks rock in rocks)
            {
                if (rock.use(this))
                {
                    // Teleport to PoI
                    teleportToPOI(rock);
                    foundAJob = true;
                    currentState = STATE.BUSY;
                    break;
                }
            }
            return foundAJob;

        }

        public bool seekIron()
        {
            bool foundAJob = false;

            GameObject world_GO = GameObject.Find("World");
            World world = world_GO.GetComponent<World>();

            List<Iron> irons = world.getIron();
            if ((irons == null) || (irons.Count == 0))
                return foundAJob;

            // Select a Tree
            foreach (Iron iron in irons)
            {
                if (iron.use(this))
                {
                    // Teleport to PoI
                    teleportToPOI(iron);

                    foundAJob = true;
                    currentState = STATE.BUSY;
                    break;
                }
            }
            return foundAJob;
        }

        public bool seekFood()
        {
            bool foundAJob = false;

            GameObject world_GO = GameObject.Find("World");
            World world = world_GO.GetComponent<World>();

            List<CropField> cfs = world.getCropField();
            if ((cfs == null) || (cfs.Count == 0))
                return foundAJob;

            // Select a Tree
            foreach (CropField cf in cfs)
            {
                if (cf.use(this))
                {
                    // Teleport to PoI
                    teleportToPOI(cf);

                    currentState = STATE.BUSY;
                    break;
                }
            }
            return foundAJob;
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
                if (!seekWood())
                    if (!seekStone())
                        if (!seekIron())
                            seekWood();
            }
        }

    }
}
