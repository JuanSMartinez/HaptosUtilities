using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Diagnostics;

namespace HaptosUtilities
{
    public class TimeUtilities
    {

        //Types of timed sessions
        public enum sessions { trainig, testing, response };

        //Singleton instance
        private static TimeUtilities instance = null;
        public static TimeUtilities Instance
        {
            get
            {
                if (instance == null)
                    instance = new TimeUtilities();
                return instance;
            }
        }

        //Stopwatch list for different sessions
        private Stopwatch[] stopWatches;


        //Private constructor
        private TimeUtilities()
        {
            stopWatches = new Stopwatch[3];
            for (int i = 0; i < 3; ++i)
                stopWatches[i] = new Stopwatch();
        }

        //Start a specific session
        public void StartSession(sessions session)
        {
            StartStopWatch(stopWatches[(int)session]);
        }

        //Stop a specific session
        public long StopSession(sessions session)
        {
            return StopStopWatch(stopWatches[(int)session]);
        }

        //Start a stopwatch
        private void StartStopWatch(Stopwatch watch)
        {
            if (!watch.IsRunning)
            {
                watch.Reset();
                watch.Start();
            }
            else
                throw new Exception("Stopwatch is already running");
        }

        //Stop a stopwatch
        private long StopStopWatch(Stopwatch watch)
        {
            if (watch.IsRunning)
            {
                watch.Stop();
                return watch.ElapsedMilliseconds;
            }
            else
                throw new Exception("Stopwatch was not running before being stopped");
        }

        //Finalizer
        ~TimeUtilities()
        {
            
        }
    }
}
