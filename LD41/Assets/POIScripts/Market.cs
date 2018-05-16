using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using CoreEvent = Assets.Scripts.Event;
using UnityEngine;
using Assets.POIScripts;

namespace POI
{
    class Market : Building
    {
        //////////////////////////////////////////////
        // STATS
        public int goldPerMerchant = 4;
        public int merchants = 0;
        public const int MAX_MERCHANT = 5;

        public static Dictionary<Ressource.TYPE, int> cost = new Dictionary<Ressource.TYPE, int>()
        {
            {Ressource.TYPE.WOOD, 70 },
            {Ressource.TYPE.STONE, 30 }
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

        public Market()
        {
            staticEvents = new List<CoreEvent>();

        }

        // Use this for initialization
        void Start()
        {
            HP = 25;
            isStaticBonus = false;
            staticEvents = new List<CoreEvent>();
            merchants = 0;
        }

        void Destroy()
        {
        }

        // Update is called once per frame
        void Update()
        {
            gather();

            if (HP == 0)
                Destroy();

            // Poll for incoming merchant
            World world = GameObject.FindObjectOfType<World>();
            int road_security = (world.military / 20); // 1..5 like merchants
            while ( (merchants < road_security) && (merchants< MAX_MERCHANT) )
                merchants++;

            // Poll for losing merchant
            while ((merchants > road_security) && (merchants > 0))
                merchants--;

            // Resolve market
            if (merchants > 0)
                staticEvents.Add(EventBank.generateGoldEvent( merchants * goldPerMerchant) );

        }
    }
}
