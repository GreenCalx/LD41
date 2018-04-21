using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Buildings;

public class World : MonoBehaviour {
    
    // Tuning
    public const int MAX_POIS = 50;
    private const int STARTER_MAX_VILLAGER = 6;
    private const int STARTER_MAX_BUILDINGS = 10;
    private const int STARTER_MAX_TREES = 10;

    //Attributes
    private List<PointOfInterest> __unclassed_pois;
    private List<Entities> __unclassed_entities;

    public int max_buildings { get; set; }
    private List<Building> __building_pois;

    public int max_trees { get; set; }
    private List<Buildings.Tree> __trees_pois;

    public int max_villagers { get; set; }
    private List<Villager> __villager_entities;


    public World()
    {
        max_buildings = STARTER_MAX_BUILDINGS;
        max_villagers = STARTER_MAX_VILLAGER;
        max_trees = STARTER_MAX_TREES;
    }

    // ------------------------- PRIVATE SPACE -------------------------------

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
            if (poi.GetType() == typeof(Building))
            {
                __building_pois.Add((Building)poi);
                __unclassed_pois.Remove(poi);
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
    private void cleanEntities()
    {

    }



    // ------------------------- PUBLIC SPACE -------------------------------

    // Mutators
    public List<Villager> getVillagers()
        { return __villager_entities; }
    // Mutators
    public List<Buildings.Tree> getTrees()
    { return __trees_pois; }

    // Methods
    public void createWorld()
    {
        __unclassed_pois = new List<PointOfInterest>(MAX_POIS);
        __unclassed_entities = new List<Entities>();

        // Init Default Villagers
        max_villagers = STARTER_MAX_VILLAGER;
       __villager_entities = new List<Villager>(max_villagers);
        for (int i = 0; i < __villager_entities.Capacity; ++i)
            __villager_entities.Add( new Villager() );

        // Init Default Buildings
        max_buildings = STARTER_MAX_BUILDINGS;
        __building_pois = new List<Building>(max_buildings);
        for (int i = 0; i < __building_pois.Capacity; ++i)
            __building_pois.Add( new Building() );

        // Init Default Trees
        max_trees = STARTER_MAX_TREES;
        __trees_pois = new List<Buildings.Tree>(max_trees);
        for (int i = 0; i < __building_pois.Capacity; ++i)
            __trees_pois.Add( new Buildings.Tree() );

    }


    // ------------------------- UNITY SPACE -------------------------------

    // Use this for initialization
    void Start () {
		
	}  
	
	// Update is called once per frame
	void Update () {
		
	}
}
