using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EmbedIO;
using EmbedIO.Actions;
using HitomiApi.Routes;
using HitomiApi.Routes.ImageProxy;
using HitomiApi.Routes.Index;
using HitomiApi.Routes.Info;

namespace HitomiApi
{
    public class ApiServer
    {
        public static WebServer server;
        public static List<string> ActiveModules = new List<string>();
        public static void Initialize()
        {
            server = new WebServer(x =>
            {
                x.WithUrlPrefix($"http://+:{Program.Port}");
            });
            server.WithApiRoute<Endpoints>("/endpoints");
            server.WithApiRoute<ImageProxyRoute>("/image");
            server.WithApiRoute<InfoRoute>("/info");
            server.WithApiRoute<IndexRoute>("/index");
            server.HandleHttpException(async (s, e) => await ServerException.Handle(s, e));
            server.HandleUnhandledException(async (s, e) => await ServerException.HandleU(s, e));

            server.WithModule(new ActionModule("/", HttpVerbs.Any, ctx => ctx.SendDataAsync(new { Message = "goto /endpoints" })));
        }

        public static void Start()
        {
            server.Start();
        }
    }
}
