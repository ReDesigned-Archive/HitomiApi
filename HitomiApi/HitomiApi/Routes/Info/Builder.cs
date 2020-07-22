using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HitomiApi.Routes.Info
{
    public class Builder
    {
        public static async Task<ResponseModel> BuildResponse(int id, string json)
        {
            var model = new ResponseModel();
            var obj = JObject.Parse(json);

            model.Language_Localname = obj["language_localname"].Value<string>();
            model.Language = obj["language"].Value<string>();

            HashDecode decoder = new HashDecode();
            List<string> images = new List<string>();
            List<string> ProxiedImages = new List<string>();

            foreach (var img in obj["files"])
            {
                var imgmodel = new ImageModel();
                imgmodel.Width = img["width"].Value<int>();
                imgmodel.Hash = img["hash"].Value<string>();
                imgmodel.HasWebp = Convert.ToBoolean(img["haswebp"].Value<int>());
                imgmodel.Name = img["name"].Value<string>();
                imgmodel.Height = img["height"].Value<int>();

                images.Add(decoder.Image_Url_From_Image(id, imgmodel, false));
                ProxiedImages.Add($"{Program.ApiUrl}/image/proxy?url=" + decoder.Image_Url_From_Image(id, imgmodel, false));
            }

            List<string> Tags = new List<string>();
            foreach (JObject t in obj["tags"])
            {
                JToken a;
                if (!t.TryGetValue("female", out a) && !t.TryGetValue("male", out a))
                {
                    Tags.Add(t["tag"].Value<string>());
                    continue;
                }
                if (t.TryGetValue("female", out a) && a.Value<string>() == "1")
                {
                    Tags.Add($"female:{t["tag"].Value<string>()}");
                }
                if (t.TryGetValue("male", out a) && a.Value<string>() == "1")
                {
                    Tags.Add($"male:{t["tag"].Value<string>()}");
                }
            }

            // List<TagModel> Tags = new List<TagModel>();
            // foreach (JObject t in obj["tags"])
            // {
            //     var tagmodel = new TagModel();
            //     JToken a;
            //     if (t.TryGetValue("female", out a) && a.Value<string>() == "1")
            //     {
            //         tagmodel.Female = true;
            //     }
            //     if (t.TryGetValue("male", out a) && a.Value<string>() == "1")
            //     {
            //         tagmodel.Male = true;
            //     }
            //     tagmodel.Name = t["tag"].Value<string>();
            //     Tags.Add(tagmodel);
            // }

            model.Title = obj["title"].Value<string>();
            model.Type = obj["type"].Value<string>();

            model.Tags = new ReadOnlyCollection<string>(Tags);
            model.Images = new ReadOnlyCollection<string>(images);
            model.ProxiedImages = new ReadOnlyCollection<string>(ProxiedImages);
            return model;
        }
    }
}
