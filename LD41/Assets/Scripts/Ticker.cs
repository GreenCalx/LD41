using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Ticker
    {
        public int frequencyPerMinutes;
        protected float elapsedTime;
        protected float referenceTime;
        protected bool tickerIsOn;

        public bool isTicking() { return tickerIsOn; }

        public Ticker(int iFrequencyPerMinutes)
        {
            elapsedTime = 0;
            frequencyPerMinutes = iFrequencyPerMinutes;
            tickerIsOn = false;
        }

        public Ticker()
        {
            elapsedTime = 0;
            frequencyPerMinutes = 0;
            tickerIsOn = false;
        }


        public void start()
        {
            tickerIsOn = true;
            referenceTime = Time.time;
        }

        public void stop()
        {
            tickerIsOn = false;
        }

        public bool tick()
        {
            bool rc = false;

            if (tickerIsOn && frequencyPerMinutes>=1)
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
