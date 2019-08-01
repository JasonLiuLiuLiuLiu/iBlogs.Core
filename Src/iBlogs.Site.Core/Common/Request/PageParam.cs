using iBlogs.Site.Core.Option;

namespace iBlogs.Site.Core.Common.Request
{
    public class PageParam
    {
        private int? _pageSize;
        public int Page { get; set; } = 1;
        public int Limit
        {
            get => _pageSize ?? int.Parse(ConfigData.Get(ConfigKey.PageSize, "20"));

            set => _pageSize = value;
        }

        public string OrderBy { get; set; }
        public OrderType OrderType { get; set; }
    }

    public enum OrderType
    {
        Desc,
        Asc
    }
}
