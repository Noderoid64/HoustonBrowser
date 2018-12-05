using System;

namespace MercuryLogger
{
    public class MainLogger : Logger
    {
        #region Singleton
        static object SyncObject = new object();
        static MainLogger instance;
        private MainLogger() { }

        public static MainLogger GetInstance()
        {
            lock (SyncObject)
            {
                if (instance == null)
                    instance = new MainLogger();
            }
            return instance;
        }
        #endregion
        public override void Log(string value)
        {
            lock (SyncObject)
            {
                try
                {
                    LogAll(value);
                }
                catch (Exception e)
                {
                    // exception handle must be there 
                }

            }
        }
    }
}