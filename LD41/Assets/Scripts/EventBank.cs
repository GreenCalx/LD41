using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreEvent = Assets.Scripts.Event;

namespace Assets.Scripts
{
    public class EventBank
    {
        // Basic WorldStats
        public static CoreEvent generateHappinessEvent(int value)
        { return generateWorldStatEvent(World.STATS.HAPPINESS, value); }

        public static CoreEvent generateHungerEvent(int value)
        { return generateWorldStatEvent(World.STATS.HUNGER, value); }

        public static CoreEvent generateMilitaryEvent(int value)
        { return generateWorldStatEvent(World.STATS.MILITARY, value); }

        public static CoreEvent generateFertilityEvent(int value)
        { return generateWorldStatEvent(World.STATS.FERTILITY, value); }

        public static CoreEvent generatePopulationEvent(int value)
        { return generateWorldStatEvent(World.STATS.POPULATION, value); }

        public static CoreEvent generateMaxPopulationEvent(int value)
        { return generateWorldStatEvent(World.STATS.MAX_POPULATION, value); }
        //-----------------------------------------------------------------------
        public static CoreEvent generateWoodEvent(int value)
        { return generateRessourceEvent(Ressource.TYPE.WOOD, value); }

        public static CoreEvent generateStoneEvent(int value)
        { return generateRessourceEvent(Ressource.TYPE.STONE, value); }

        public static CoreEvent generateFoodEvent(int value)
        { return generateRessourceEvent(Ressource.TYPE.FOOD, value); }

        public static CoreEvent generateGoldEvent(int value)
        { return generateRessourceEvent(Ressource.TYPE.GOLD, value); }

        public static CoreEvent generateIronEvent(int value)
        { return generateRessourceEvent(Ressource.TYPE.IRON, value); }
        //-------------------------------------------------------------------------
        
        

        // ==============================================================================
        private static CoreEvent generateWorldStatEvent(World.STATS iStat, int iValue)
        {
            CoreEvent e = new CoreEvent();
            WorldEffector we = new WorldEffector(
                new Dictionary<World.STATS, int>()
                {
                    { iStat , iValue }
                }
            );
            e.worldEffector = we;
            return e;
        }

        private static CoreEvent generateRessourceEvent(Ressource.TYPE iRes, int iValue)
        {
            CoreEvent e = new CoreEvent();
            WorldEffector we = new WorldEffector(
                new Dictionary<Ressource.TYPE, int>()
                {
                    { iRes , iValue }
                }
            );
            e.worldEffector = we;
            return e;
        }




    }
}
