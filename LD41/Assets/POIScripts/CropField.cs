using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using CoreEvent = Assets.Scripts.Event;
using Assets.POIScripts;

namespace POI
{
    public class CropField : PointOfInterest
    {
        public const string POI_NAME = "Food";
        public Ressource.TYPE ressourceType = Ressource.TYPE.FOOD;
        public int ressource_units_pool = 100000;
        public int ressource_per_hit = 6;

        public CropField()
        {
            max_users = 3;
            users = new List<InternalEntities>(max_users);

            frequency = 30;
            _gathererTicker = new GathererTicker(frequency);
        }

        override public List<CoreEvent> generateEvents()
        {
            List<CoreEvent> events = new List<CoreEvent>();
            foreach (InternalEntities ie in users)
            {
                if (readyToConsume)
                {
                    events.Add(EventBank.generateFoodEvent(ressource_per_hit));
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
