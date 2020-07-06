using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;

namespace HitomiApi.Routes.Index
{
    public class IndexRoute : WebApiController
    {
        [Route(HttpVerbs.Get, "/list")]
        public async Task GetList([QueryField] string langcode = "all")
        {
            WebClient wc = new WebClient();
            await HttpContext.SendStringAsync(wc.DownloadString(Path.Combine(Program.Upstream, langcode)), "application/json", Encoding.UTF8);
        }
    }
}
