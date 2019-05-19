namespace iBlogs.Site.Core.Utils.Extensions
{
    public static class InitExtension
    {
        public static int ValueOrDefault(this int? source)
        {
            return source ?? 0;
        }

        public static long ValueOrDefault(this long? source)
        {
            return source ?? 0;
        }
    }
}
