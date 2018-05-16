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

        public void unsign(InternalEntities iEntity)
        {
            users.Remove(iEntity);
        }

        public int user_index (InternalEntities iIE)
        { return users.IndexOf(iIE); }

        // Gatherer ticker
        protected void gather()
        {
            // Gatherer tickers
            if (null != _gathererTicker)
            {
                if ((users.Count > 0) && !_gathererTicker.isTicking())
                    _gathererTicker.start();
                else if ((users.Count == 0) && _gathererTicker.isTicking())
                    _gathererTicker.stop();
                else if (_gathererTicker.isTicking() && !readyToConsume)
                    readyToConsume = _gathererTicker.tick();
            }
        }//! gather

        void Update()
        { }

        // EVENTS
        public abstract List<CoreEvent> generateEvents();
    }
}
