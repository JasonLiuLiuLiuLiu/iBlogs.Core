using CookComputing.XmlRpc;

namespace iBlogs.Site.MetaWeblog.Classes
{
    public class WpCategory
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public int ParentId { get; set; }
        public string Description { get; set; }
    }
}