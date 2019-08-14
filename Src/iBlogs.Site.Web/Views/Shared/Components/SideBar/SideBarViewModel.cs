using System.Collections.Generic;

namespace iBlogs.Site.Web.Views.Shared.Components.SideBar
{
    public class SideBarViewModel
    {
        public List<KeyValuePair<string, int>> Tags { get; set; }
        public List<KeyValuePair<string, int>> Categories { get; set; }
        public string ContentCount { get; set; }
        public string CommentCount { get; set; }
        public string RunTime { get; set; }
        public string LastActiveTime { get; set; }
    }
}
