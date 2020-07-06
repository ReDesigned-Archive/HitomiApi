using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Swan.Logging;

namespace HitomiApi
{
    public class GCTimer
    {
        private static Timer T = new Timer(10000);

        public static void Initialize()
        {
            T.AutoReset = true;
            T.Elapsed += async(s,e) => await RunGC(s,e);
        }

        public static void Start()
        {
            T.Start();
        }

        public static void Stop()
        {
            T.Stop();
        }

        private static async Task RunGC(object sender, ElapsedEventArgs e)
        {
            GC.Collect();
            "GC Done".Info("GCTimer");
        }
    }
}
