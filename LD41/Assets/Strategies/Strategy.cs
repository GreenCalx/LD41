using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
// alias
using CoreEvent = Assets.Scripts.Event;

namespace Assets.Strategies
{

    public abstract class  Strategy
    {
        protected const bool TEST_MODE = true;

        abstract public List<CoreEvent> getOutputEvents();

    }
}
