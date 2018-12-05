using System.Collections.Generic;
using System;
namespace MercuryLogger
{
    public abstract class Logger
    {
        
        List<Logger> loggers;
        public Logger()
        {
            loggers = new List<Logger>();
        }
        public void AddLogger(Logger logger)
        {
            loggers.Add(logger);
        }
        public abstract void Log(string value);
        protected void LogAll(string message)
        {
            foreach (var item in loggers)
            {
                item.Log(message);
            }
        }
        
    }
}
