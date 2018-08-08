using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CoreLib
{
    public static class Runner
    {
        public enum RunnerState
        {
            Paused, Running, Iddle
        }

        #region fields
        static Stopwatch watch = new Stopwatch();
       
        #endregion

        #region Properties

        public static RunnerState State { get; private set; } = RunnerState.Iddle;
        public static Statistics Statistics { get; set; }
      

        public static TimeSpan RunningTime
        {
            get { return watch.Elapsed; }
        }

        #endregion
        
        #region public methods

        public static void Pause()
        {
            watch.Stop();
            State = RunnerState.Paused;

        }

        public static void Start()
        {
            watch.Start();
            State = RunnerState.Running;
        }

        public static void Stop()
        {
            watch.Reset();
            State = RunnerState.Iddle;
        }

        #endregion
    }
}
