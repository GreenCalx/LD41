using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using CoreEvent = Assets.Scripts.Event;

namespace POI
{

public class House : Building {

        private int houseSize = 8; // Pop increase
        public override List<CoreEvent> generateEvents()
        {
            List<CoreEvent> dumpEvents = new List<CoreEvent>();
            dumpEvents.AddRange(staticEvents);
            if (isStaticBonus)
                staticEvents.Clear();
            return dumpEvents;
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
