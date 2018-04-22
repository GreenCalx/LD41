using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
// alias
using CoreEvent = Assets.Scripts.Event;

namespace Assets.Strategies
{
    public class GrowthStrategy : Strategy
    {
        public Fretboard fretBoard { get; set; }

        public GrowthStrategy()
        { fretBoard = new Fretboard(); }

        public override List<CoreEvent> getOutputEvents()
        {
            List<Token> tokens = new List<Token>();

            if (!TEST_MODE) { 

                if (null == fretBoard)
                    return null;

                // Get Token From Fretboard
                Token token = fretBoard.GetToken();
                while (token != null)
                { token = fretBoard.GetToken(); tokens.Add(token); }

            } else {
                ///// TEST /////
                Token t1 = new Token("farm", Token.Sequence_State.Background, 1, 0);
                tokens.Add(t1);
                ////////////////
            }

            // Match Token with Event using EventTokenDictionary
            List<CoreEvent> events = EventTokenDictionary.digest(tokens);

            // Return the Event
            return events;
        }


    }
}
