using System;
using System.Threading.Tasks;
using Swan.Logging;

namespace HitomiApi
{
    public class Program
    {
        public static string ApiUrl;
        public static int Port;
        public static string Upstream;
        private static string LogSource = "Bootstrap";
        static async Task Main(string[] args)
        {
            "Initializing config.json".Info(LogSource);
            Config.Initialize();
            "Setting up variable".Info(LogSource);
            ApiUrl = Config.GetConfig("publish_url");
            Port = int.Parse(Config.GetConfig("port"));
            Upstream = Config.GetConfig("upstream");
            "Initializing Api Server".Info(LogSource);
            ApiServer.Initialize();
            "Starting Api Server".Info(LogSource);
            ApiServer.Start();
            "Initializing GC".Info(LogSource);
            GCTimer.Initialize();
            "Starting GC".Info(LogSource);
            GCTimer.Stop();
            "Bootstrap Done.".Info(LogSource);

            await Task.Delay(-1);
        }
    }
}
