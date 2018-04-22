using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Grid
{
    class Arc
    {
        public Node from;
        public Node to;
        public bool bilateral = true;

        public Arc(Node iFrom, Node iTo)
        {
            from = iFrom;
            to = iTo;
        }
    }
}
