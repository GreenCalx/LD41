using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;

namespace Assets.Strategies
{
    public abstract class  Strategy
    {
        abstract public Event getOutputEvent();
    }
}
