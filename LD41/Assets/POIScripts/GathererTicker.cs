using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.POIScripts
{
    public class GathererTicker
    {
        public int frequencyPerMinutes;
        private float elapsedTime;
        private float referenceTime;
        private bool collectIsOn;
        public bool isGathering() { return collectIsOn; }

        public GathererTicker(int iFrequencyPerMinutes)
        {
            elapsedTime = 0;
            frequencyPerMinutes = iFrequencyPerMinutes;
            collectIsOn = false;
        }

        public void startCollect()
        {
            collectIsOn = true;
            referenceTime = Time.time;
        }

        public void stopCollect()
        {
            collectIsOn = false;
        }

        public bool tick()
        {
            bool rc = false;

            if ( collectIsOn )
            {
                elapsedTime = Time.time - referenceTime;
                rc = elapsedTime >= (60 / frequencyPerMinutes);
                if (!!rc)
                { elapsedTime = 0; referenceTime = Time.time; }
            }

            return rc;  
        }//! tick

    }
}
