using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using UnityEngine;
// alias
using CoreEvent = Assets.Scripts.Event;

namespace Assets.Strategies
{

    public abstract class  Strategy : MonoBehaviour
    {
        protected const bool TEST_MODE = false;

        abstract public List<CoreEvent> getOutputEvents();

    }
}
