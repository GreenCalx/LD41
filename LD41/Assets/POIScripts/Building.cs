using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts;
using CoreEvent = Assets.Scripts.Event;

namespace POI
{
    public class Building : PointOfInterest
    {
        public int HP;

        // Determines if the building bonus is only applied once
        protected bool isStaticBonus = false;
        protected List<CoreEvent> staticEvents;

        public enum TYPES { HOUSE, FORT, STORAGE, MARKET, UNDEFINED };

        private TYPES __type { get; set; }

        public Building() { __type = TYPES.UNDEFINED; }

        override public List<CoreEvent> generateEvents() { return null; }
    }
}
