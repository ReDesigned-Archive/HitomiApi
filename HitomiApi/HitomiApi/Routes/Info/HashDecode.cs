using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HitomiApi.Routes.Info
{
    public class HashDecode
    {
        public string Subdomain_From_GalleryId(int g, int num_of_front)
        {
            var o = g % num_of_front;
            var r = Convert.ToChar(97 + o).ToString();
            return r;
        }

        public string Subdomain_From_Url(string url)
        {
            var retval = "a";
            var number_of_frontends = 3;

            var r = new Regex(@"\/[0-9a-f]\/([0-9a-f]{2})\/");
            var m = r.Matches(url);
            var g = Convert.ToInt16(m[0].Groups[1].Value, 16);
            if (g < 0x30)
            {
                number_of_frontends = 2;
            }
            if (g < 0x09)
            {
                g = 1;
            }
            retval = Subdomain_From_GalleryId(g, number_of_frontends) + retval;
            return retval;
        }

        public string Url_From_Url(string url)
        {
            var r = new Regex(@"\/\/..?\.hitomi\.la\/");
            var s = Subdomain_From_Url(url);
            return r.Replace(url, $"//{s}.hitomi.la/");
        }

        //정상
        public string Full_Path_From_Hash(string hash)
        {
            if (hash.Length < 3)
            {
                Console.WriteLine("HASH LENGTH 3");
                return hash;
            }
            var result = hash.Substring(hash.Length - 3);
            var a = result.Substring(0, 2);
            var b = result.Last().ToString();

            return string.Concat($"{b}/{a}/", hash);
        }

        //정상
        public string Url_From_Hash(int id, ImageModel model, string dir = null, string ext = null)
        {
            string e = model.Name.Split('.').Last();
            if (ext != null)
            {
                e = ext;
            }

            string d = "images";
            if (dir != null)
            {
                e = dir;
                d = dir;
            }

            var r = Full_Path_From_Hash(model.Hash);
            return "https://a.hitomi.la/" + d + "/" + r + "." + e;
        }

        //정상
        public string Url_From_Url_From_Hash(int id, ImageModel model, string dir = null, string ext = null)
        {
            string a = Url_From_Hash(id, model, dir, ext);
            var b = Url_From_Url(a);
            return b;
        }

        public string Image_Url_From_Image(int id, ImageModel model, bool no_webp)
        {
            string webp = null;
            if (model.Hash != null && model.HasWebp && !no_webp)
            {
                webp = "webp";
            }

            return Url_From_Url_From_Hash(id, model, webp);
        }
    }
}
