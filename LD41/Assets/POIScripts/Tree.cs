using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using CoreEvent = Assets.Scripts.Event;
using Assets.POIScripts;

namespace POI
{
    public class Tree : PointOfInterest
    {
        public const string POI_NAME = "Tree";


        public Ressource.TYPE ressourceType = Ressource.TYPE.WOOD;
        public int ressource_units_pool = 5000;
        public int wood_per_chop = 1;


        public Tree()
        {
            max_users = 2;
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
                    events.Add(EventBank.generateWoodEvent(wood_per_chop));
                    ressource_units_pool -= wood_per_chop;
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

            // Ressource pool check update
            if (ressource_units_pool < 0)
            {
                // Unsubscribe users
                foreach (InternalEntities ie in users)
                    ie.currentState = Entities.STATE.FREE;

                Destroy(gameObject);
            }
                
        }//! Update
    }
}
