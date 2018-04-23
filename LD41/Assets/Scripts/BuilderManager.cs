using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using POI;

namespace Assets.Scripts
{
    public class BuilderManager : MonoBehaviour
    {
        public World attachedWorld = null;

        public GameObject BuildableMap = null;

        public GameObject HousePrefab = null;

        public GameObject StoragePrefab = null;

        public int ffs = 2;

        void Start()
        {
            if ((attachedWorld == null) || (BuildableMap == null))
                throw new NullReferenceException();
        }

        void Update()
        {

        }


        public void PollConstruct()
        {
            if (Input.GetKey(KeyCode.H))
                tryBuildBuilding(House.cost, Building.TYPES.HOUSE);
            if (Input.GetKey(KeyCode.B))
                tryBuildBuilding(Barrack.cost, Building.TYPES.BARRACK);
            if (Input.GetKey(KeyCode.S))
                tryBuildBuilding(Storage.cost, Building.TYPES.STORAGE);

        }

        private void tryBuildBuilding(Dictionary<Ressource.TYPE, int> iCost, Building.TYPES iBuildingType)
        {
            bool canBuild = true;
            foreach (Ressource.TYPE t in iCost.Keys)
                canBuild = iCost[t] <= attachedWorld.ressource_table[t];
            if (canBuild)
                autoBuild(iBuildingType);
        }

        private void autoBuild(Building.TYPES iBuildingType)
        {
            // Find Building Spot
            BuildableSpot[] BSpots = BuildableMap.GetComponentsInChildren<BuildableSpot>();
            BuildableSpot chosenSpot = null;
            foreach (BuildableSpot bs in BSpots)
                if (bs.isAvailable())
                { chosenSpot = bs; break; }


            // Build actual Building
            GameObject createdGO = null;
            switch (iBuildingType) 
            {
                case Building.TYPES.HOUSE:
                    createdGO = Instantiate( HousePrefab );
                    createdGO.transform.position = chosenSpot.transform.position;

                    break;

                case Building.TYPES.STORAGE:
                    createdGO = Instantiate( StoragePrefab );
                    createdGO.transform.position = chosenSpot.transform.position;
                    break;

                default:
                    break;
            }
            if (!!createdGO)
            {
                Building building = createdGO.GetComponent<Building>();
                attachedWorld.addBuilding(building);
                chosenSpot.buildingHolder = building;
            }
        }
    }
}
