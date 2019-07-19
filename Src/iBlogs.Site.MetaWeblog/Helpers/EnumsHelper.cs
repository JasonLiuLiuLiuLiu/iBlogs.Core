using System;
using iBlogs.Site.MetaWeblog.Classes;

namespace iBlogs.Site.MetaWeblog.Helpers
{
    static public class EnumsHelper
    {
        public static string GetCommentStatusName(CommentStatus status)
        {
            return Enum.GetName(typeof(CommentStatus), status);
        }
        public static CommentStatus GetCommentStatus(string commentStatus)
        {
            try
            {
                return (CommentStatus)Enum.Parse(typeof(CommentStatus), commentStatus, true);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException(
                    $"Status value of '{commentStatus}' doesn't exist in the CommentStatus enum");
            }
        }
    }
}