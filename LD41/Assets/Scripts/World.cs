using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class World : MonoBehaviour {
    
    // Tuning
    public const int MAX_POIS = 50;
    private const int STARTER_MAX_VILLAGER = 6;

    //Attributes
    private List<PointOfInterest> __unclassed_pois;
    private List<Entities> __unclassed_entities;

    public int max_buildings { get; set; }
    private List<Building> __building_pois;

    public int max_villagers { get; set; }
    private List<Villager> __villager_entities;


    World()
    {
        max_buildings = 10;
        max_villagers = STARTER_MAX_VILLAGER;
    }

    private void classEntities()
    {
        // Class POIS
        foreach (PointOfInterest poi in __unclassed_pois)
        {
            if (poi.GetType() == typeof(Building))
            {
                __building_pois.Add( (Building)poi );
                __unclassed_pois.Remove( poi );
            }
        }

        // Class Entities
        foreach( Entities entity in __unclassed_entities)
        {   
            string e_type = entity.GetType().ToString();
            if ( e_type == typeof(Villager).ToString() )
            {
                __villager_entities.Add( (Villager)entity );
                __unclassed_entities.Remove( entity );
            }
        }
    }

    // TODO : If an error is detected in the list, call me to clear up the lists.
    public void cleanEntities()
    {

    }

    // -------------------------------------------------------------------------

    // Use this for initialization
    void Start () {
		
	}  
	
	// Update is called once per frame
	void Update () {
		
	}
}
