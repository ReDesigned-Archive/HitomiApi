using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Microsoft.VisualBasic.CompilerServices;

namespace HitomiApi.Routes.Info
{
    public class InfoRoute : WebApiController
    {
        [Route(HttpVerbs.Get, "/{id}", true)]
        public async Task Get(int id)
        {
            var json = await HitomiRequester.GetJson(id);
            var model = await Builder.BuildResponse(id, json);
            await HttpContext.SendDataAsync(model);
        }
    }
}
