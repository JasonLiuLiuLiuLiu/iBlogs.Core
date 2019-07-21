using System;
using System.Collections.Generic;
using System.Text;
using iBlogs.Site.MetaWeblog.Classes;
using iBlogs.Site.MetaWeblog.CnBlogs.XmlRpcStructs;

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
                    WpSlug = xmlRpcPost.wp_slug
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
        }

        internal class To
        {
            internal static XmlRpcPost Post(Post post)
            {
                return new XmlRpcPost
                {
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
        }
    }
}
