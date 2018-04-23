using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using CoreEvent = Assets.Scripts.Event;

namespace POI
{
    public class CropField : PointOfInterest
    {
        public const string POI_NAME = "Food";
        public Ressource.TYPE ressourceType = Ressource.TYPE.FOOD;
        public int ressource_units_pool = 10000;
        public int ressource_per_hit = 2;

        public CropField()
        {
            MAX_USERS = 2;
            users = new List<InternalEntities>(MAX_USERS);
        }

        override public List<CoreEvent> generateEvents()
        {
            List<CoreEvent> events = new List<CoreEvent>();
            foreach (InternalEntities ie in users)
            {
                events.Add(EventBank.generateFoodEvent(ressource_per_hit));
                ressource_units_pool -= ressource_per_hit;
            }
            return events;
        }
        void Awake()
        {
            gameObject.name = POI_NAME;
        }

        void Update()
        {

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
