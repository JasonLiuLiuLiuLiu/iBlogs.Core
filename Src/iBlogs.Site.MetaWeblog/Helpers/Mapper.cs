using iBlogs.Site.MetaWeblog.Classes;
using iBlogs.Site.MetaWeblog.XmlRpcStructs.Cnblogs;

namespace iBlogs.Site.MetaWeblog.Helpers
{
    internal class Mapper
    {
        internal class From
        {
            internal static Post Post(XmlRpcPost xmlRpcPost)
            {
                return new Post
                {
                    Title = xmlRpcPost.title,
                    Categories = xmlRpcPost.categories,
                    DateCreated = xmlRpcPost.dateCreated,
                    Description = xmlRpcPost.description,
                    Enclosure = new Enclosure
                    {
                        Length = xmlRpcPost.XML_RPC_ENCLOSURE.length,
                        Type = xmlRpcPost.XML_RPC_ENCLOSURE.type,
                        Url = xmlRpcPost.XML_RPC_ENCLOSURE.url
                    },
                    Link = xmlRpcPost.link,
                    MtAllowComments = xmlRpcPost.mt_allow_comments,
                    MtAllowPings = xmlRpcPost.mt_allow_pings,
                    MtConvertBreaks = xmlRpcPost.mt_convert_breaks,
                    MtExcerpt = xmlRpcPost.mt_excerpt,
                    MtKeywords = xmlRpcPost.mt_keywords,
                    WpSlug = xmlRpcPost.wp_slug,
                    Permalink = xmlRpcPost.permalink,
                    MtTextMore = xmlRpcPost.mt_text_more,
                    PostId = xmlRpcPost.postid,
                    Source = new Source
                    {
                        Name = xmlRpcPost.XML_RPC_SOURCE.name,
                        Url = xmlRpcPost.XML_RPC_SOURCE.url
                    }
                };
            }

            internal static BlogInfo BlogInfo(XmlRpcBlogInfo blogInfo)
            {
                return new BlogInfo
                {
                    BlogId = blogInfo.blogid,
                    BlogName = blogInfo.blogName,
                    Url = blogInfo.url
                };
            }

            internal static WpCategory WpCategory(XmlRpcWpCategory wpCategory)
            {
                return new WpCategory
                {
                    Description = wpCategory.description,
                    Name = wpCategory.name,
                    ParentId = wpCategory.parent_id,
                    Slug = wpCategory.slug
                };
            }

            internal static CategoryInfo CategoryInfo(XmlRpcCategoryInfo categoryInfo)
            {
                return new CategoryInfo
                {
                    CategoryId = categoryInfo.categoryid,
                    Description = categoryInfo.description,
                    HtmlUrl = categoryInfo.htmlUrl,
                    RssUrl = categoryInfo.rssUrl,
                    Title = categoryInfo.title
                };
            }
        }

        internal class To
        {
            internal static XmlRpcPost Post(Post post)
            {
                return new XmlRpcPost
                {
                    title = post.Title,
                    categories = post.Categories,
                    dateCreated = post.DateCreated,
                    description = post.Description,
                    XML_RPC_ENCLOSURE  = new XmlRpcEnclosure()
                    {
                        length = post.Enclosure.Length,
                        type = post.Enclosure.Type,
                        url = post.Enclosure.Url
                    },
                    link = post.Link,
                    mt_allow_comments = post.MtAllowComments,
                    mt_allow_pings = post.MtAllowPings,
                    mt_convert_breaks = post.MtConvertBreaks,
                    mt_excerpt = post.MtExcerpt,
                    mt_keywords = post.MtKeywords,
                    wp_slug = post.WpSlug
                };
            }

            internal static XmlRpcWpCategory WpCategory(WpCategory wpCategory)
            {
                return new XmlRpcWpCategory
                {
                    description = wpCategory.Description,
                    name = wpCategory.Name,
                    parent_id = wpCategory.ParentId,
                    slug = wpCategory.Slug
                };
            }
        }
    }
}
