namespace iBlogs.Site.Core.Common.Request
{
    public class PageParam
    {
        public int Page { get; set; }
        public int Limit { get; set; } = 20;
        public string OrderBy { get; set; }
        public OrderType OrderType { get; set; }
    }

    public enum OrderType
    {
        Desc,
        Asc
    }
}
