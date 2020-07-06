using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EmbedIO;

namespace HitomiApi.Routes.ImageProxy
{
    public class ImageDownloader
    {
        public static async Task Proxy(IHttpContext ctx, string url)
        {
            WebClient wc = new WebClient();
            var h = new WebHeaderCollection();
            h.Add("referer", $"https://hitomi.la/reader/1000000.html");
            wc.Headers = h;
            var b = wc.DownloadData(url);
            ctx.Response.ContentType = "image/jpeg";
            using (Stream s = ctx.OpenResponseStream())
            {
                s.Write(b, 0, b.Length);
            }
        }
    }
}
