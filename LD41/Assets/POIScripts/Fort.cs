using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using CoreEvent = Assets.Scripts.Event;

namespace POI
{
    class Fort : Building
    {
        //////////////////////////////////////////////
        // STATS
        private int militaryValue = 10; // military increase
        public static Dictionary<Ressource.TYPE, int> cost = new Dictionary<Ressource.TYPE, int>()
        {
            {Ressource.TYPE.WOOD, 200 },
            {Ressource.TYPE.STONE, 600 },
            {Ressource.TYPE.IRON, 400 }
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

        public Fort()
        {
            staticEvents = new List<CoreEvent>(1);
        }

        void Start()
        {
            HP = 50;
            isStaticBonus = true;
            staticEvents = new List<CoreEvent>(1);
            staticEvents.Add(EventBank.generateMilitaryEvent(militaryValue));
        }

        void Destroy()
        {
            staticEvents.Add(EventBank.generateMilitaryEvent((-1) * militaryValue));
        }

        // Update is called once per frame
        void Update()
        {
            if (HP == 0)
                Destroy();
        }


    }
}
