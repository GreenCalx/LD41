using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using CoreEvent = Assets.Scripts.Event;

namespace POI
{
    public abstract class PointOfInterest : MonoBehaviour
    {
        public abstract List<CoreEvent> generateEvents();
    }
}
