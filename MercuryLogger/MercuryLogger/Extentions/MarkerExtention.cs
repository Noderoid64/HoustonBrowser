using System;
using System.Text;

namespace MercuryLogger.Extentions
{
    public class MarkerExtention : Logger
    {
        private string marker;
        public MarkerExtention(string marker)
        {
            this.marker = marker;
        }
        public override void Log(string value)
        {

            LogAll("[" + marker + "] " + value);
        }
    }
}