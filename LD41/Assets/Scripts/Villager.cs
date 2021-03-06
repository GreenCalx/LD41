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

        private int eat_counter;
        private const int hungry_threshold = 300;
        private World attachedWorld;
        private int unit_ressource_consumption = 2; // EATS THIS MUCH

        ~Villager()
        {
            used_poi.unsign(this);
        }

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
            eat_counter = 0;
        }

        // ---------- WOOD ------------
        public bool seekWood()
        {
            bool foundAJob = false;

            List<POI.Tree> trees = attachedWorld.getTrees();
            if ((trees == null) || (trees.Count == 0))
                return foundAJob;

            // Select a Tree
            foreach (POI.Tree tree in trees)
            {
                if ( tree.use(this) )
                {
                    // Teleport to PoI
                    teleportToPOI(tree);
                    used_poi = tree;
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


            List<Rocks> rocks = attachedWorld.getRocks();
            if ((rocks == null) || (rocks.Count == 0))
                return foundAJob;

            // Select a Tree
            foreach (Rocks rock in rocks)
            {
                if (rock.use(this))
                {
                    // Teleport to PoI
                    teleportToPOI(rock);
                    used_poi = rock;
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

            List<Iron> irons = attachedWorld.getIron();
            if ((irons == null) || (irons.Count == 0))
                return foundAJob;

            // Select a Tree
            foreach (Iron iron in irons)
            {
                if (iron.use(this))
                {
                    // Teleport to PoI
                    teleportToPOI(iron);
                    used_poi = iron;

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

            List<CropField> cfs = attachedWorld.getCropField();
            if ((cfs == null) || (cfs.Count == 0))
                return foundAJob;

            // Select a Tree
            foreach (CropField cf in cfs)
            {
                if (cf.use(this))
                {
                    // Teleport to PoI
                    teleportToPOI(cf);
                    used_poi = cf;

                    foundAJob = true;
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
            attachedWorld = world_GO.GetComponent<World>();
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

                // ======= OCCUPY ==============
                // > GIVE ORDERS LOGIC HERE

                // DUMMY STRATEGY
                int randomStrategy = UnityEngine.Random.Range(0, 4);
                if (randomStrategy == 0) seekWood();
                if (randomStrategy == 1) seekStone();
                if (randomStrategy == 2) seekFood();
                if (randomStrategy == 3) seekIron();

                // ==============================
            }

            // EATS
            eat_counter++;
            if (eat_counter > hungry_threshold)
            { eat_counter = 0; attachedWorld.events.AddLast(EventBank.generateFoodEvent(-unit_ressource_consumption)); }
        }

    }
}
