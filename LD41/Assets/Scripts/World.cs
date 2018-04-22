﻿using System.Collections;
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
    private const int STARTER_WOOD_UNITS = 50;
    private const int STARTER_STONE_UNITS = 20;
    private const int STARTER_IRON_UNITS = 0;
    private const int STARTER_FOOD_UNITS = 25;
    private const int STARTER_GOLD_UNITS = 8;



    // Village stats
    public int happiness { get; set; }
    public const int MAX_HAPPINESS = 100;
    public int hunger { get; set; }
    public const int MAX_HUNGER = 100;
    public int military { get; set; }
    public const int MAX_MILITARY = 100;


    // Ressources
    Dictionary<Ressource.TYPE, int> ressource_table;


    // Attributes
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
        ressource_table = new Dictionary<Ressource.TYPE, int>();
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

        // Add starting ressources
        ressource_table.Add(Ressource.TYPE.WOOD, STARTER_WOOD_UNITS);
        ressource_table.Add(Ressource.TYPE.STONE, STARTER_STONE_UNITS);
        ressource_table.Add(Ressource.TYPE.FOOD, STARTER_FOOD_UNITS);
        ressource_table.Add(Ressource.TYPE.IRON, STARTER_IRON_UNITS);
        ressource_table.Add(Ressource.TYPE.GOLD, STARTER_GOLD_UNITS);

    }  
	
	// Update is called once per frame
	void Update () {



        // Villages stats update
        if (happiness > MAX_HAPPINESS)
            happiness = MAX_HAPPINESS;
        if (hunger > MAX_HUNGER)
            hunger = MAX_HUNGER;
        if (military > MAX_MILITARY)
            military = MAX_MILITARY;

    }
}
