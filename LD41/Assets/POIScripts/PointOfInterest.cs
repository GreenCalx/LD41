using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts;
using Assets.POIScripts;

using CoreEvent = Assets.Scripts.Event;

namespace POI
{
    public abstract class PointOfInterest : MonoBehaviour
    {
        // USERS
        public List<InternalEntities> users;
        protected int max_users;
        // GATHERING
        protected int frequency; // times per minute
        protected GathererTicker _gathererTicker;
        protected bool readyToConsume = false;


        public bool use(InternalEntities iEntity)
        {
            if (users.Count < max_users)
            {
                users.Add(iEntity);
                return true;
            }
            return false;
        }
        public int user_index (InternalEntities iIE)
        { return users.IndexOf(iIE); }

        // Gatherer ticker
        protected void gather()
        {
            // Gatherer tickers
            if (null != _gathererTicker)
            {
                if ((users.Count > 0) && !_gathererTicker.isGathering())
                    _gathererTicker.startCollect();
                else if ((users.Count == 0) && _gathererTicker.isGathering())
                    _gathererTicker.stopCollect();
                else if (_gathererTicker.isGathering() && !readyToConsume)
                    readyToConsume = _gathererTicker.tick();
            }
        }//! gather

        // EVENTS
        public abstract List<CoreEvent> generateEvents();
    }
}
