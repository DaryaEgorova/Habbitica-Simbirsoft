using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Habbitica.Models
{
    public class HomeViewModel
    {
        public List<PostForHomeSliderModel> PostsSlider { get; set; } 
        public List<LatestPostModel> LatestPosts { get; set; }
    }

    public class PostForHomeSliderModel 
    {
        public string Name { get; set; }
        public DateTime PostedOn { get; set; }
    }

    public class LatestPostModel
    {
        public string Name { get; set; }
        public DateTime PostedOn { get; set; }
    }
}
