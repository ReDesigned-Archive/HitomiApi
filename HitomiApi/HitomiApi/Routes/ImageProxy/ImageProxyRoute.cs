using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Swan.Logging;

namespace HitomiApi.Routes.ImageProxy
{
    public class ImageProxyRoute : WebApiController
    {
        private static string LogSource = "ImageProxy";
        [Route(HttpVerbs.Get, "/proxy")]
        public async Task ProxyGet([QueryField] string url = null)
        {
            if (url is null)
            {
                $"Invalid Request: {HttpContext.RemoteEndPoint.Address} {HttpContext.RequestedPath}".Warn(LogSource);
                await HttpContext.SendDataAsync(new {Message = "BadRequest"});
                return;
            }
            $"Proxy Request: {HttpContext.RemoteEndPoint.Address} {url}".Info(LogSource);
            await ImageDownloader.Proxy(HttpContext, url);
        }
    }
}
