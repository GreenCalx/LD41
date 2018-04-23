using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using CoreEvent = Assets.Scripts.Event;

namespace POI
{

public class House : Building {

        //////////////////////////////////////////////
        // STATS
        private int houseSize = 8; // Pop increase
        public static Dictionary<Ressource.TYPE, int> cost = new Dictionary<Ressource.TYPE, int>()
        {
            {Ressource.TYPE.WOOD, 0 },
            {Ressource.TYPE.STONE, 0 }
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

        public House()
        {
            staticEvents = new List<CoreEvent>(1);
        }

        // Use this for initialization
        void Start ()
        {
                HP = 10;
                isStaticBonus = true;
                staticEvents = new List<CoreEvent>(1);
                staticEvents.Add( EventBank.generateMaxPopulationEvent(houseSize) );
        }

	    void Destroy()
        {
            staticEvents.Add(EventBank.generateMaxPopulationEvent((-1) * houseSize));
        }

	    // Update is called once per frame
	    void Update ()
        {
		    if (HP==0)
                Destroy();
	    }
    }

}
