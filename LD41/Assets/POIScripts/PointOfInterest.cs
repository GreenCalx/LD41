using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts;

using CoreEvent = Assets.Scripts.Event;

namespace POI
{
    public abstract class PointOfInterest : MonoBehaviour
    {
        // USE
        public List<InternalEntities> users;
        protected int MAX_USERS;
        public bool use(InternalEntities iEntity)
        {
            if (users.Count < MAX_USERS)
            {
                users.Add(iEntity);
                return true;
            }
            return false;
        }
        public int user_index (InternalEntities iIE)
        { return users.IndexOf(iIE); }

        // EVENTS
        public abstract List<CoreEvent> generateEvents();
    }
}
