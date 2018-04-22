using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// alias
using CoreEvent = Assets.Scripts.Event;

namespace Assets.Scripts
{
    static class EventTokenDictionary
    {
        public static Dictionary<string, Dictionary<Token.Sequence_State, CoreEvent>>  strategyEvents = initStrategyEvents();

        public static List<CoreEvent> digest(List<Token> iTokens)
        {
            List<CoreEvent> retEvents = new List<CoreEvent>();

            // Analyze Token
            foreach (Token token in iTokens)
            {
                string                  name    = token.sequenceName;
                Token.Sequence_State    state   = token.sequenceState;

                CoreEvent tokenEvent = strategyEvents[name][state];
                retEvents.Add(tokenEvent);

            }//!for tokens

            return retEvents;
        }


        public static Dictionary<string, Dictionary<Token.Sequence_State, CoreEvent>> initStrategyEvents()
        {
            Dictionary<string, Dictionary<Token.Sequence_State, CoreEvent>> strategyEvents =
                new Dictionary<string, Dictionary<Token.Sequence_State, CoreEvent>>()
            {
                    /////////////////////////////////////////////////////////////////////////////////////
                    // DIPLOMATICS
                    { "test1", new Dictionary<Token.Sequence_State, CoreEvent>()
                        {
                            { Token.Sequence_State.Success, EventBank.generateHappinessEvent(2) },
                            { Token.Sequence_State.Background, EventBank.generateHappinessEvent(1) },
                            { Token.Sequence_State.Fail, EventBank.generateHappinessEvent(-1) }
                        }
                    },
                    /////////////////////////////////////////////////////////////////////////////////////
                    // GROWTH
                    { "farm", new Dictionary<Token.Sequence_State, CoreEvent>()
                        {
                            { Token.Sequence_State.Success, EventBank.generateFoodEvent(2) },
                            { Token.Sequence_State.Background, EventBank.generateFoodEvent(1) },
                            { Token.Sequence_State.Fail, EventBank.generateFoodEvent(-1) }
                        }
                    },
                    /////////////////////////////////////////////////////////////////////////////////////
                    // MILITARY
                    { "defense", new Dictionary<Token.Sequence_State, CoreEvent>()
                        {
                            { Token.Sequence_State.Success, EventBank.generateMilitaryEvent(2) },
                            { Token.Sequence_State.Background, EventBank.generateMilitaryEvent(1) },
                            { Token.Sequence_State.Fail, EventBank.generateMilitaryEvent(-1) }
                        }
                    }
                    /////////////////////////////////////////////////////////////////////////////////////
            };//! strategyEvents declaration

            return strategyEvents;
        }


    }
}
