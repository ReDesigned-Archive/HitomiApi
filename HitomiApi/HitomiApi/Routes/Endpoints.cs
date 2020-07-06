using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;

namespace HitomiApi.Routes
{
    public class Endpoints : WebApiController
    {
        [Route(HttpVerbs.Get, "/")]
        public async Task Main()
        {
            await HttpContext.SendDataAsync(new {Modules = ApiServer.ActiveModules});
        }
    }
}
