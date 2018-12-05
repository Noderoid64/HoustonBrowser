using System;
using MercuryLogger;
using MercuryLogger.Extentions;
using MercuryLogger.Loggers;
using Avalonia;
using Avalonia.Logging.Serilog;

namespace HoustonBrowser
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigLogger();
            BuildAvaloniaApp().Start<MainWindow>();
        }

        static void ConfigLogger()
        { // remove in release
            MainLogger logger = MainLogger.GetInstance();
            TimeExtention timeExtention = new TimeExtention()
            {
                ShowHour = true,
                ShowMinute = true,
                ShowSecond = true,
                ShowMillisecond = true
            };
            FileLogger fileLogger = new FileLogger("./", "GlobalLog.txt");
            logger.AddLogger(timeExtention);
            timeExtention.AddLogger(fileLogger);
            logger.Log("<--------Logger-Start-------->");
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToDebug();
    }
}
