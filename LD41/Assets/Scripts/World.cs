using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using POI;
using Assets.Strategies;
// alias
using CoreEvent = Assets.Scripts.Event;

public class World : MonoBehaviour {
    
    // Tuning
    public const int MAX_POIS = 50;
    private const int STARTER_MAX_VILLAGER = 6;
    private const int STARTER_MAX_BUILDINGS = 10;
    private const int STARTER_MAX_TREES = 20;
    private const int STARTER_MAX_ROCKS = 20;
    private const int STARTER_MAX_IRON = 20;
    private const int STARTER_MAX_CROPS = 20;
    private const int STARTER_WOOD_UNITS = 50;
    private const int STARTER_STONE_UNITS = 20;
    private const int STARTER_IRON_UNITS = 0;
    private const int STARTER_FOOD_UNITS = 25;
    private const int STARTER_GOLD_UNITS = 8;
    private const int STARTER_FOODSTORAGE_SIZE = 20;

    private const int HUNGER_DIV_RATIO = 25; // 1/4th



    // Village stats
    public enum STATS { HAPPINESS, HUNGER, MILITARY, FERTILITY, POPULATION, MAX_POPULATION, FOOD_STORAGE }
    public int happiness { get; set; }
    public const int MAX_HAPPINESS = 1000;
    public int hunger { get; set; }
    public const int MAX_HUNGER = 1000;
    public int military { get; set; }
    public const int MAX_MILITARY = 1000;
    public int fertility { get; set; }
    public const int MAX_FERTILITY = 1000;
    public int foodStorage { get; set; }
    public const int MAX_STORAGE = 1000;

    // Ressources
    public Dictionary<Ressource.TYPE, int> ressource_table;
    
    // Events
    public LinkedList<CoreEvent> events;

    // Strategies
    public List<Strategy> strategies;

    // Attributes
    private List<PointOfInterest> __unclassed_pois;
    private List<Entities> __unclassed_entities;

    public int max_buildings { get; set; }
    private List<Building> __building_pois;

    public int max_trees { get; set; }
    private List<POI.Tree> __trees_pois;

    public int max_rocks { get; set; }
    private List<Rocks> __rocks_pois;

    public int max_iron { get; set; }
    private List<Iron> __iron_pois;

    public int max_CropField { get; set; }
    private List<CropField> __cropfield_pois;


    public int max_villagers { get; set; }
    public int population { get; set; }
    private List<Villager> __villager_entities;
    public GameObject villagerGO;


    public World()
    {
        max_buildings = STARTER_MAX_BUILDINGS;
        max_villagers = STARTER_MAX_VILLAGER;
        max_trees = STARTER_MAX_TREES;
        foodStorage = STARTER_FOODSTORAGE_SIZE;
        ressource_table = new Dictionary<Ressource.TYPE, int>();
        strategies = new List<Strategy>();
        events = new LinkedList<CoreEvent>();
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

    // Generates events based on current world stats
    private List<CoreEvent> generateEventsFromWorldStats()
    {
        List<CoreEvent> generatedEvents = new List<CoreEvent>();

        // Hunger on Happiness
        int hungerRatio = (hunger / HUNGER_DIV_RATIO);
        if (hungerRatio > 0)
            generatedEvents.Add(EventBank.generateHappinessEvent( (-1)*hungerRatio) );
        else
            generatedEvents.Add(EventBank.generateHappinessEvent(1));


        // food_units_missing
        int food_units_missing = population -ressource_table[Ressource.TYPE.FOOD] ;
        if (food_units_missing < 0)
            generatedEvents.Add(EventBank.generateHungerEvent(food_units_missing) );
        else
            generatedEvents.Add(EventBank.generateHungerEvent(food_units_missing));



        return generatedEvents;
    }

    private void updatePOIs()
    {
        // Trees
        POI.Tree[] trees = GameObject.FindObjectsOfType<POI.Tree>();
        foreach (POI.Tree t in trees)
            if (!__trees_pois.Contains(t)) __trees_pois.Add(t);
            else if ( t.ressource_units_pool <= 0 ) Destroy(t.gameObject);
       
        // Rocks
        Rocks[] rocks = GameObject.FindObjectsOfType<Rocks>();
        foreach (Rocks r in rocks)
            if (!__rocks_pois.Contains(r)) __rocks_pois.Add(r);
            else if (r.ressource_units_pool <= 0) Destroy(r.gameObject);

        // Iron
        Iron[] irons = GameObject.FindObjectsOfType<Iron>();
        foreach (Iron i in irons)
            if (!__iron_pois.Contains(i)) __iron_pois.Add(i);
            else if (i.ressource_units_pool <= 0) Destroy(i.gameObject);

        // Fields
        CropField[] fields = GameObject.FindObjectsOfType<CropField>();
        foreach (CropField cf in fields)
            if (!__cropfield_pois.Contains(cf)) __cropfield_pois.Add(cf);
            else if (cf.ressource_units_pool <= 0) Destroy(cf.gameObject);

        
        // Buildings
        Building[] buildings = GameObject.FindObjectsOfType<Building>();
        foreach (Building b in buildings)
            if (!__building_pois.Contains(b)) __building_pois.Add(b);
            else if (b.HP <= 0) Destroy(b.gameObject);

    }
    
    private GameObject spawnVillager()
    {
        GameObject newVillager = Instantiate(villagerGO);
        Villager newVillagerBehaviour = newVillager.GetComponent<Villager>();
        __villager_entities.Add(newVillagerBehaviour);

        //Randomize color
        SpriteRenderer sr = newVillager.GetComponent<SpriteRenderer>();
        /*
        sr.color =   Random.ColorHSV();
        */
        return newVillager;
    }


    // ------------------------- PUBLIC SPACE -------------------------------

    public double getScore()
    {
        int final_score = 0;

        final_score += population * happiness;
        final_score -= hunger;
        final_score += military;
        final_score += max_villagers;
        final_score += fertility;
        foreach (Ressource.TYPE key in ressource_table.Keys)
            final_score += ressource_table[key];

        return final_score;
    }

        // Mutators
    public List<Villager> getVillagers()
        { return __villager_entities; }
    // Mutators
    public List<POI.Tree> getTrees()
    { return __trees_pois; }

    public List<Rocks> getRocks()
    { return __rocks_pois; }

    public List<Iron> getIron()
    { return __iron_pois; }

    public List<CropField> getCropField()
    { return __cropfield_pois; }

    public void addBuilding(Building iBuilding)
    { if (!!iBuilding) __building_pois.Add(iBuilding); }

    // Methods
    public void createWorld()
    {
        __unclassed_pois = new List<PointOfInterest>(MAX_POIS);
        __unclassed_entities = new List<Entities>();

        // Init Default Villagers
        max_villagers = STARTER_MAX_VILLAGER;
       __villager_entities = new List<Villager>(max_villagers);
        if (!!villagerGO)
        {
            for (int i = 0; i < __villager_entities.Capacity; ++i)
            {
                GameObject newVillager = spawnVillager();
            }//! for villager
        }// !villagerGO

            // Init Default Buildings
            max_buildings = STARTER_MAX_BUILDINGS;
        __building_pois = new List<Building>(max_buildings);


        // Init Default Trees
        max_trees = STARTER_MAX_TREES;
        __trees_pois = new List<POI.Tree>();
        POI.Tree[] trees = GameObject.FindObjectsOfType<POI.Tree>();
        foreach (POI.Tree t in trees)
            __trees_pois.Add( t );

        // Init Default Rocks
        max_rocks = STARTER_MAX_ROCKS;
        __rocks_pois = new List<Rocks>();
        Rocks[] rocks = GameObject.FindObjectsOfType<Rocks>();
        foreach (Rocks r in rocks)
            __rocks_pois.Add(r);

        // Init Default Irons
        max_trees = STARTER_MAX_IRON;
        __iron_pois = new List<Iron>();
        Iron[] irons = GameObject.FindObjectsOfType<Iron>();
        foreach (Iron i in irons)
            __iron_pois.Add(i);

        // Init Default Irons
        max_CropField = STARTER_MAX_CROPS;
        __cropfield_pois = new List<CropField>();
        CropField[] cfs = GameObject.FindObjectsOfType<CropField>();
        foreach (CropField cf in cfs)
            __cropfield_pois.Add(cf);

        
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

        // Add strategy blocks
        strategies.Add( gameObject.AddComponent<GrowthStrategy>() );
        strategies.Add(gameObject.AddComponent<MilitaryStrategy>()   );
        strategies.Add(gameObject.AddComponent<DiplomaticStrategy>());

        // Create base world
        createWorld();

        // Poll the map for POIs
        updatePOIs();

        // Class different entities
        classEntities();
    }  
	
	// Update is called once per frame
	void Update () {

        //////////////////////////////////////////////
        // Village Stats update
        population = __villager_entities.Count;


        //////////////////////////////////////////////
        // Poll For Events

        // Strategies
        foreach ( Strategy strategy in strategies)
        {
            List<CoreEvent> localEvents = strategy.getOutputEvents();
            if ((null != localEvents) && (localEvents.Count > 0))
                foreach (CoreEvent e in localEvents)
                    events.AddLast(e);
        }

        // WorldStats effect on World
        List<CoreEvent> worldFromStatsEvents = generateEventsFromWorldStats();
        foreach (CoreEvent e in worldFromStatsEvents)
            events.AddLast(e);

        //////////////////////// POINT OF INTEREST ////////////////////////
        // May be Factorizable
        // Generate events from Buildings
        foreach ( Building building in __building_pois)
        {
            List<CoreEvent> buildingEvents = building.generateEvents();

            if (null == buildingEvents) continue;
            foreach (CoreEvent e in buildingEvents)
                events.AddLast(e);
        }

        // Generate events from Trees
        foreach (POI.Tree tree in __trees_pois)
        {
            List<CoreEvent> treeEvents = tree.generateEvents();

            if (null == treeEvents) continue;
            foreach (CoreEvent e in treeEvents)
                events.AddLast(e);
        }
        // Generate events from Rocks
        foreach (Rocks rock in __rocks_pois)
        {
            List<CoreEvent> rockEvents = rock.generateEvents();
            if (null == rockEvents) continue;
            foreach (CoreEvent e in rockEvents)
                events.AddLast(e);
        }
        // Generate events from Iron
        foreach (Iron iron in __iron_pois)
        {
            List<CoreEvent> ironEvents = iron.generateEvents();
            if (null == ironEvents) continue;
            foreach (CoreEvent e in ironEvents)
                events.AddLast(e);
        }
        // Generate events from Cropfields
        foreach (CropField cf in __cropfield_pois)
        {
            List<CoreEvent> cfEvents = cf.generateEvents();
            if (null == cfEvents) continue;
            foreach (CoreEvent e in cfEvents)
                events.AddLast(e);
        }
        
        //////////////////////////////////////////////
        // Apply Events in Queue
        List<WorldEffector> worldEffectors = new List<WorldEffector>();
        foreach (CoreEvent e in events)
        {
            WorldEffector we = e.worldEffector;
            if (null!=we)
                we.consumeAll(this);
           
        }
        events.Clear();

        //////////////////////////////////////////////
        // Villages stats post-update
        happiness = (happiness > MAX_HAPPINESS) ? MAX_HAPPINESS : happiness;
        happiness = (happiness < 0) ? 0 : happiness;

        hunger = (hunger > MAX_HUNGER) ? MAX_HUNGER : hunger;
        hunger = (hunger < 0) ? 0 : hunger;

        military = (military > MAX_MILITARY) ? MAX_MILITARY : military;
        military = (military < 0) ? 0 : military;

        fertility = (fertility > MAX_FERTILITY) ? MAX_FERTILITY : fertility;
        fertility = (fertility < 0) ? 0 : fertility;

        foodStorage = (foodStorage > MAX_STORAGE) ? MAX_STORAGE : foodStorage;
        foodStorage = (foodStorage < 0) ? 0 : foodStorage;

        //////////////////////////////////////////////
        // Village Ressource updates
        List<Ressource.TYPE> keys = new List<Ressource.TYPE>(ressource_table.Keys);
        foreach (Ressource.TYPE res in keys)
        {
            if (ressource_table[res] < 0)
                ressource_table[res] = 0;
            
            if (res == Ressource.TYPE.FOOD)
                if (ressource_table[res] > foodStorage)
                    ressource_table[res] = foodStorage;
                    
        }

        if (population < max_villagers)
            spawnVillager();

        // Console Dump
        dumpWorldValues();
    }


    void dumpWorldValues()
    {
        foreach (Ressource.TYPE res in ressource_table.Keys)
            System.Console.Write(" RESSOURCE : " + res + ressource_table[res]);

        System.Console.Write(" fertility : " + fertility);
        System.Console.Write(" military : " + military);
        System.Console.Write(" hunger : " + hunger);
        System.Console.Write(" happiness : " + happiness);

    }
}
