using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using CoreEvent = Assets.Scripts.Event;

namespace POI
{
    public class BuildableSpot : PointOfInterest {

        public Building buildingHolder { get; set; }

        public BuildableSpot()
        {
            buildingHolder = null;
        }

        public override List<CoreEvent> generateEvents()
        {
            throw new NotImplementedException();
        }


        public bool isAvailable()
        { return (buildingHolder == null); } 


        public void construct( Building iBuilding )
        {
            buildingHolder = iBuilding;
        }

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

    }
}
