using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HitomiApi.Routes.Info
{
    public class HitomiRequester
    {
        public static string BaseUrl = "https://ltn.hitomi.la/galleries/";

        public static async Task<string> GetJson(int id)
        {
            WebClient client = new WebClient();
            var res = client.DownloadString(Path.Combine(BaseUrl, $"{id}.js"));
            var a = res.Replace("var galleryinfo = ", string.Empty);
            return a;
        }
    }
}
