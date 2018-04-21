using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts;

namespace Buildings
{
    public class Building : PointOfInterest
    {
        public enum TYPES { HOUSE, UNDEFINED };

        private Building.TYPES __type { get; set; }

        public Building() { __type = TYPES.UNDEFINED; }
    }
}
