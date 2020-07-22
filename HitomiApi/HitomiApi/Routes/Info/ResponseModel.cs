using System;
using System.Collections.Generic;
using System.Text;

namespace HitomiApi.Routes.Info
{
    public class ResponseModel
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public IReadOnlyCollection<string> Tags { get; set; }
        public string Language_Localname { get; set; }
        public string Language { get; set; }
        public IReadOnlyCollection<string> Images { get; set; }
        public IReadOnlyCollection<string> ProxiedImages { get; set; }

    }
}
