using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using CoreEvent = Assets.Scripts.Event;

namespace POI
{
    class Storage : Building
    {
        //////////////////////////////////////////////
        // STATS
        public int storageSize = 20; // storage increase
        public static Dictionary<Ressource.TYPE, int> cost = new Dictionary<Ressource.TYPE, int>()
        {
            {Ressource.TYPE.WOOD, 15 },
            {Ressource.TYPE.STONE, 10 }
        };
        //////////////////////////////////////////////
        override public List<CoreEvent> generateEvents()
        {
            if (staticEvents.Count == 0)
                return null;
            List<CoreEvent> dumpEvents = new List<CoreEvent>();
            dumpEvents.AddRange(staticEvents);
            if (isStaticBonus)
                staticEvents.Clear();
            return dumpEvents;
        }

        public Storage()
        {
            staticEvents = new List<CoreEvent>(1);
        }

        // Use this for initialization
        void Start()
        {
            HP = 30;
            isStaticBonus = true;
            staticEvents = new List<CoreEvent>(1);
            staticEvents.Add(EventBank.generateFoodStorageEvent(storageSize));
        }

        void Destroy()
        {
            staticEvents.Add(EventBank.generateFoodStorageEvent((-1) * storageSize));
        }

        // Update is called once per frame
        void Update()
        {
            if (HP == 0)
                Destroy();
        }

    }
}
