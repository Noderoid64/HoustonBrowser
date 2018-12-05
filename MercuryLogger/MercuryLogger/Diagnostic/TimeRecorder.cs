using System;
using System.Diagnostics;

namespace MercuryLogger.Diagnostics
{
    public class TimeRecorder : IDisposable
    {
        private Action<TimeDump> callback;
        private Stopwatch sw;

        public TimeRecorder(Action<TimeDump> action)
        {
            callback = action;            
            sw = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            sw.Stop();
            TimeDump t;
            t.Ticks = sw.ElapsedTicks;
            t.Milliseconds = sw.ElapsedMilliseconds;            
            callback.Invoke(t);
        }
    }
}