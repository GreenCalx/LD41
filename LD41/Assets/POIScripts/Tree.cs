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
        public const string POI_NAME = "Tree";
        public Ressource.TYPE ressourceType = Ressource.TYPE.WOOD;
        public int ressource_units_pool = 2000
            ;
        public int wood_per_chop = 2;
        
        public Tree()
        {
            MAX_USERS = 2;
            users = new List<InternalEntities>(MAX_USERS);
        }

        override public List<CoreEvent> generateEvents()
        {
            List<CoreEvent> events = new List<CoreEvent>();
            foreach (InternalEntities ie in users)
            {
                events.Add(EventBank.generateWoodEvent(wood_per_chop));
                ressource_units_pool -= wood_per_chop;
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
