using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts;

using CoreEvent = Assets.Scripts.Event;

namespace POI
{
    public class VillagerSpawn : PointOfInterest
    {
        override public List<CoreEvent> generateEvents() { return null; }
    }
}
