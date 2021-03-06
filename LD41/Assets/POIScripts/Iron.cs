﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using Assets.POIScripts;

using CoreEvent = Assets.Scripts.Event;
namespace POI
{
    public class Iron : PointOfInterest
    {
        public const string POI_NAME = "Iron";
        public Ressource.TYPE ressourceType = Ressource.TYPE.IRON;
        public int ressource_units_pool = 2000;
        public int ressource_per_hit = 1;

        public Iron()
        {
            max_users = 2;
            users = new List<InternalEntities>(max_users);

            frequency = 15;
            _gathererTicker = new GathererTicker(frequency);

        }

        override public List<CoreEvent> generateEvents()
        {
            List<CoreEvent> events = new List<CoreEvent>();
            foreach (InternalEntities ie in users)
            {
                if (readyToConsume)
                {
                    events.Add(EventBank.generateIronEvent(ressource_per_hit));
                    ressource_units_pool -= ressource_per_hit;
                    readyToConsume = false;
                }
            }
            return events;
        }
        void Awake()
        {
            gameObject.name = POI_NAME;
        }

        void Update()
        {
            gather();

            if (ressource_units_pool < 0)
            {
                // Unsubscribe users
                foreach (InternalEntities ie in users)
                    ie.currentState = Entities.STATE.FREE;

                Destroy(gameObject);
            }

        }

    }
}
