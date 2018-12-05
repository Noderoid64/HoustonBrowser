using System;
using System.Text;

namespace MercuryLogger.Extentions
{
    public class DebugLevelExtention : MarkerExtention
    {
        public enum Levels {Fatal, Error, Warning, Info, Debug, Trace}
        public DebugLevelExtention(Levels marker) : base(marker.ToString())
        {
        }
    }
}