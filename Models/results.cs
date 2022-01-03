using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WrapperService.Models
{
    public class results
    {
        public string title { get; set; }
        public string link { get; set; }
        public object keywords { get; set; }
        public List<string> creator { get; set; }
        public object video_url { get; set; }
        public string description { get; set; }
        public object content { get; set; }
        public string pubDate { get; set; }
        public string image_url { get; set; }
        public string source_id { get; set; }
    }
}
