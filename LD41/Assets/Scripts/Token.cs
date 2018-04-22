using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Token
    {
        // Name of the sub-strategy 
        public string sequenceName;

        // = SEQUENCE LENGTH, if sequence is OK
        public int sequenceHits;

        // IF 0, Sequence is OK
        public int sequenceMiss;

        // Hits + Miss
        public int sequenceLength;

        public Token(string iSequenceName, int iSequenceHits, int iSequenceMiss)
        {
            sequenceName = iSequenceName;
            sequenceHits = iSequenceHits;
            sequenceMiss = iSequenceMiss;
            sequenceLength = sequenceHits + sequenceMiss;
        }

    }
}
