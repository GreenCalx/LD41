using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreEvent = Assets.Scripts.Event;
using Assets.Scripts;
using Assets.POIScripts;

namespace POI
{
    public class Rocks : PointOfInterest
    {
        public const string POI_NAME = "Rocks";
        public Ressource.TYPE ressourceType = Ressource.TYPE.STONE;
        public int ressource_units_pool = 10000;
        public int ressource_per_hit = 2;

        public Rocks()
        {
            max_users = 1;
            users = new List<InternalEntities>(max_users);

            frequency = 20;
            _gathererTicker = new GathererTicker(frequency);
        }

        override public List<CoreEvent> generateEvents()
        {
            List<CoreEvent> events = new List<CoreEvent>();
            foreach (InternalEntities ie in users)
            {
                if (readyToConsume)
                {
                    events.Add(EventBank.generateStoneEvent(ressource_per_hit));
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
