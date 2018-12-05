using System;
using System.Text;

namespace MercuryLogger.Extentions
{
    public class TimeExtention : Logger
    {
        public bool ShowYear { get; set; }
        public bool ShowMonth { get; set; }
        public bool ShowDay { get; set; }
        public bool ShowHour { get; set; }
        public bool ShowMinute { get; set; }
        public bool ShowSecond { get; set; }
        public bool ShowMillisecond { get; set; }

        public override void Log(string value)
        {
            DateTime time = DateTime.Now;
            StringBuilder builder = new StringBuilder(100);
            builder.Append("[");

            if (ShowYear)
                builder.Append(time.Year + ":");
            if (ShowMonth)
                builder.Append(time.Month + ":");
            if (ShowDay)
                builder.Append(time.Day + ":");
            if (ShowHour)
                builder.Append(time.Hour + ":");
            if (ShowMinute)
                builder.Append(time.Minute + ":");
            if (ShowSecond)
                builder.Append(time.Second + ":");
            if (ShowMillisecond)
                builder.Append(time.Millisecond + ":");
            builder.Append("]");
            builder.Replace(":]", "] ");

            value = builder.ToString() + value;

            LogAll(value);
        }
    }
}
