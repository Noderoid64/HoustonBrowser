using System;
using System.Reflection;
using Xunit;
using MercuryLogger;
using MercuryLogger.Loggers;
using MercuryLogger.Extentions;
using MercuryLogger.Diagnostics;

namespace MercuryLogger.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string a = "";
            Action<TimeDump> s = t =>
            {
                a += "Ticks: " + t.Ticks + "\r\n";
            };

            MainLogger logger;
            MarkerExtention m1;
            DebugLevelExtention levelExtention;
            TimeExtention timeExtention;
            FileLogger fileLogger;

            using (TimeRecorder r = new TimeRecorder(s))
            {
                logger = MainLogger.GetInstance();
            }
            using (TimeRecorder r = new TimeRecorder(s))
            {
                m1 = new MarkerExtention("Net");
            }
            using (TimeRecorder r = new TimeRecorder(s))
            {
                levelExtention = new DebugLevelExtention(DebugLevelExtention.Levels.Trace);
            }
            using (TimeRecorder r = new TimeRecorder(s))
            {
                timeExtention = new TimeExtention()
                {
                    ShowHour = true,
                    ShowMinute = true,
                    ShowSecond = true,
                    ShowMillisecond = true
                };
            }
            using (TimeRecorder r = new TimeRecorder(s))
            {
                fileLogger = new FileLogger("./", "Log.txt");
            }
            using (TimeRecorder r = new TimeRecorder(s))
            {
                m1.AddLogger(logger);
                levelExtention.AddLogger(logger);
                logger.AddLogger(timeExtention);
                timeExtention.AddLogger(fileLogger);
            }
            using (TimeRecorder r = new TimeRecorder(s))
            {
                m1.Log("M1Logger");
            }
            using (TimeRecorder r = new TimeRecorder(s))
            {
                levelExtention.Log("M2Logger");
            }
            using (TimeRecorder r = new TimeRecorder(s))
            {
                m1.Log("M1Logger");
            }
            using (TimeRecorder r = new TimeRecorder(s))
            {
                levelExtention.Log(a);
            }




        }
    }
}
