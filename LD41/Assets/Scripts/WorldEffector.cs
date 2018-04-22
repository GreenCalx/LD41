using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    /*
     * Can be consumed by World entity to modify itself
     */
    public class WorldEffector
    {
        

        public Dictionary<Ressource.TYPE, int> ressources { get; set; }
        public Dictionary<World.STATS, int> worldStats { get; set; }

        // Constructors
        public WorldEffector()
        {
            ressources = buildRessourcesDictionary();
            worldStats = buildWorldDictionary();
        }
        public WorldEffector(Dictionary<Ressource.TYPE, int> iRessources) : this()
        {
            ressources = DictionaryMerger<Ressource.TYPE>.merge(ressources, iRessources);
        }
        public WorldEffector(Dictionary<World.STATS, int> iWorldStats) : this()
        {
            worldStats = DictionaryMerger<World.STATS>.merge(worldStats, iWorldStats);
        }
        public WorldEffector(Dictionary<Ressource.TYPE, int> iRessources, Dictionary<World.STATS, int> iWorldStats) : this()
        {
            ressources = DictionaryMerger<Ressource.TYPE>.merge(ressources, iRessources);
            worldStats = DictionaryMerger<World.STATS>.merge(worldStats, iWorldStats);
        }

        public Dictionary<Ressource.TYPE, int> buildRessourcesDictionary()
        {
            Dictionary<Ressource.TYPE, int> retDec = new Dictionary<Ressource.TYPE, int>()
            {
                { Ressource.TYPE.WOOD, 0 },
                { Ressource.TYPE.STONE, 0 },
                { Ressource.TYPE.FOOD, 0 },
                { Ressource.TYPE.IRON, 0 },
                { Ressource.TYPE.GOLD, 0 }
            };
            return retDec;
        }

        public Dictionary<World.STATS, int> buildWorldDictionary()
        {
            Dictionary<World.STATS, int> retDec = new Dictionary<World.STATS, int>()
            {
                { World.STATS.HAPPINESS, 0 },
                { World.STATS.HUNGER, 0 },
                { World.STATS.MILITARY, 0 },
                { World.STATS.FERTILITY, 0 },
                { World.STATS.POPULATION, 0 },
                { World.STATS.MAX_POPULATION, 0 }
            };
            return retDec;
        }

        // ------------------------------------------------------------------------
        // WorldStats + Ressource
        public void consumeAll(World iWorld)
        {
            consumeWorldStats(iWorld);
            consumeRessource(iWorld);
        }
        // Consume World Stats ( happiness, hunger, etc...)
        public void consumeWorldStats(World iWorld)
        {
            if (iWorld == null)
                return;

            iWorld.happiness += worldStats[World.STATS.HAPPINESS];

            iWorld.hunger += worldStats[World.STATS.HUNGER];

            iWorld.military += worldStats[World.STATS.MILITARY];

            iWorld.fertility += worldStats[World.STATS.FERTILITY];

            iWorld.population += worldStats[World.STATS.POPULATION];

            iWorld.max_villagers += worldStats[World.STATS.MAX_POPULATION];
        }

        // Consume Village ressources (food, wood, etc...)
        public void consumeRessource(World iWorld)
        {
            if (iWorld == null)
                return;

            iWorld.ressource_table[Ressource.TYPE.WOOD] += ressources[Ressource.TYPE.WOOD];

            iWorld.ressource_table[Ressource.TYPE.GOLD] += ressources[Ressource.TYPE.GOLD];

            iWorld.ressource_table[Ressource.TYPE.IRON] += ressources[Ressource.TYPE.IRON];

            iWorld.ressource_table[Ressource.TYPE.STONE] += ressources[Ressource.TYPE.STONE];

            iWorld.ressource_table[Ressource.TYPE.FOOD] += ressources[Ressource.TYPE.FOOD];
        }

    }
}
