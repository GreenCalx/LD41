using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts;
// alias
using CoreEvent = Assets.Scripts.Event;

namespace Assets.Strategies
{
    public class GrowthStrategy : Strategy
    {
        public Fretboard fretBoard { get; set; }

        public void Start()
        {
            GameObject fretBoard_ = GameObject.Find("Fretboard_Growth_Strategy");
            fretBoard = fretBoard_.GetComponent<Fretboard>();
        }

        public override List<CoreEvent> getOutputEvents()
        {
            List<Token> tokens = new List<Token>();

            if (!TEST_MODE) { 

                if (null == fretBoard)
                    return null;

                // Get Token From Fretboard
                Token token = null;
                do
                {
                    token = fretBoard.GetToken();
                    if (token != null)
                    {
                        tokens.Add(token);
                    }
                }
                while (token != null);

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
