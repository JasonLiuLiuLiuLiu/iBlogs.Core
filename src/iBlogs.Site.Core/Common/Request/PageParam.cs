namespace iBlogs.Site.Core.Common.Request
{
    public class PageParam
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 5;
        public string OrderBy { get; set; }
        public OrderType OrderType { get; set; }
    }

    public enum OrderType
    {
        Desc,
        Asc
    }
}
