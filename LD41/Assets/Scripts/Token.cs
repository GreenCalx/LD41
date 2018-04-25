using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Token
    {
        public enum Sequence_State
        {
            Background,
            Fail,
            Success
        };

        public Sequence_State sequenceState;
        // Name of the sub-strategy 
        public string sequenceName
            ;

        // = SEQUENCE LENGTH, if sequence is OK
        public int sequenceHits;

        // IF 0, Sequence is OK
        public int sequenceMiss;

        // Hits + Miss
        public int sequenceLength;

        public Token(string iSequenceName, Sequence_State iSequenceState, int iSequenceHits, int iSequenceMiss)
        {
            sequenceName = iSequenceName;
            sequenceState = iSequenceState;
            sequenceHits = iSequenceHits;
            sequenceMiss = iSequenceMiss;
            sequenceLength = sequenceHits + sequenceMiss;
        }

    }
}
