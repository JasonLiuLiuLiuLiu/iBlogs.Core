using iBlogs.Site.Core.Option;

namespace iBlogs.Site.Core.Common.Request
{
    public class PageParam
    {
        public int Page { get; set; } = 1;
        public int Limit => int.Parse(ConfigData.Get(ConfigKey.PageSize, "20"));
        public string OrderBy { get; set; }
        public OrderType OrderType { get; set; }
    }

    public enum OrderType
    {
        Desc,
        Asc
    }
}
