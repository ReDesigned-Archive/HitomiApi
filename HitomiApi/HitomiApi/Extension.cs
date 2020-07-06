using System;
using System.Collections.Generic;
using System.Text;
using EmbedIO;
using EmbedIO.WebApi;
using Swan.Logging;

namespace HitomiApi
{
    public static class Extension
    {
        public static void WithApiRoute<TController>(this WebServer server, string baseroute) where TController : WebApiController, new()
        {
            ApiServer.ActiveModules.Add(baseroute);
            $"Added Route: {baseroute}".Info("Bootstrap");
            server.WithWebApi(baseroute, x => x.WithController<TController>());
        }
    }
}
