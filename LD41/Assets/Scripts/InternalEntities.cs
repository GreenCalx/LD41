using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.POIScripts;
using POI;

namespace Assets.Scripts
{
    public class InternalEntities : Entities
    {
        public STATE currentState { get; set; }

        protected PointOfInterest used_poi;

    }
}
