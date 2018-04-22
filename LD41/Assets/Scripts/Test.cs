using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreEvent = Assets.Scripts.Event;

namespace Assets.Scripts
{
    class Test
    {

        public static CoreEvent generateTestEvent()
        {
            CoreEvent testEvent = new CoreEvent();
            WorldEffector testWE = new WorldEffector(
                new Dictionary<Ressource.TYPE, int>()
                {
                { Ressource.TYPE.IRON , 1 },
                { Ressource.TYPE.WOOD , -5 }
                },
                new Dictionary<World.STATS, int>()
                {
                 { World.STATS.HUNGER , 1 },
                { World.STATS.HAPPINESS , 2 }
                }

            );
            testEvent.worldEffector = testWE;
            return testEvent;
        }


    }//! Test
}
