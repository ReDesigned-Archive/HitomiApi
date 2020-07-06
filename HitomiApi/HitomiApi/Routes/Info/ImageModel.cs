using System;
using System.Collections.Generic;
using System.Text;

namespace HitomiApi.Routes.Info
{
    public class ImageModel
    {
        public int Width { get; set; }
        public string Hash { get; set; }
        public bool HasWebp { get; set; } = true;
        public string Name { get; set; }
        public int Height { get; set; }
    }
}
