using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using iBlogs.Site.Core.Blog.Content.Service;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Option.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Atom;
using Microsoft.SyndicationFeed.Rss;

namespace iBlogs.Site.Web.Controllers
{
    
    [ApiController]
    public class XmlController : ControllerBase
    {
        private readonly IContentsService _contentsService;
        private readonly IOptionService _optionService;

        public XmlController(IContentsService contentsService, IOptionService optionService)
        {
            _contentsService = contentsService;
            _optionService = optionService;
        }

        [HttpGet("/robots.txt")]
        public string RobotsTxt()
        {
            string host = Request.Scheme + "://" + Request.Host;
            var sb = new StringBuilder();
            sb.AppendLine("User-agent: *");
            sb.AppendLine("Disallow:");
            sb.AppendLine($"sitemap: {host}/sitemap.xml");

            return sb.ToString();
        }

        [HttpGet("/sitemap.xml")]
        public async Task SitemapXml()
        {
            string host = Request.Scheme + "://" + Request.Host;

            Response.ContentType = "application/xml";

            using (var xml = XmlWriter.Create(Response.Body, new XmlWriterSettings { Indent = true }))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                var posts = await _contentsService.GetContent(int.MaxValue);

                foreach (var post in posts)
                {
                    var lastMod = new[] { post.Created, post.Modified };

                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", host + post.Permalink());
                    xml.WriteElementString("lastmod", lastMod.Max().ToString("yyyy-MM-ddThh:mmzzz"));
                    xml.WriteEndElement();
                }

                xml.WriteEndElement();
            }
        }

        [HttpGet("/rsd.xml")]
        public void RsdXml()
        {
            string host = Request.Scheme + "://" + Request.Host;

            Response.ContentType = "application/xml";
            Response.Headers["cache-control"] = "no-cache, no-store, must-revalidate";

            using (var xml = XmlWriter.Create(Response.Body, new XmlWriterSettings { Indent = true }))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("rsd");
                xml.WriteAttributeString("version", "1.0");

                xml.WriteStartElement("service");

                xml.WriteElementString("enginename", "iBlogs");
                xml.WriteElementString("enginelink", "https://github.com/liuzhenyulive/iblogs");
                xml.WriteElementString("homepagelink", host);

                xml.WriteStartElement("apis");
                xml.WriteStartElement("api");
                xml.WriteAttributeString("name", "MetaWeblog");
                xml.WriteAttributeString("preferred", "true");
                xml.WriteAttributeString("apilink", host + "/metaweblog");
                xml.WriteAttributeString("blogid", "1");

                xml.WriteEndElement(); // api
                xml.WriteEndElement(); // apis
                xml.WriteEndElement(); // service
                xml.WriteEndElement(); // rsd
            }
        }

        [HttpGet("feed")]
        [HttpGet("/feed/{type}")]
        public async Task Rss(string type)
        {
            if (string.IsNullOrEmpty(type))
                type = "rss";

            Response.ContentType = "application/xml";
            string host = Request.Scheme + "://" + Request.Host;

            using (XmlWriter xmlWriter = XmlWriter.Create(Response.Body, new XmlWriterSettings() { Async = true, Indent = true, Encoding = new UTF8Encoding(false) }))
            {
                var posts = await _contentsService.GetContent(10);
                var writer = await GetWriter(type, xmlWriter, posts.Max(p => p.Created));

                foreach (var post in posts)
                {
                    var item = new AtomEntry
                    {
                        Title = post.Title,
                        Description = post.Content,
                        Id = host + post.Permalink(),
                        Published = post.Modified,
                        LastUpdated = post.Modified,
                        ContentType = "html",
                    };

                    foreach (string category in post.Categories.Split(','))
                    {
                        item.AddCategory(new SyndicationCategory(category));
                    }

                    item.AddContributor(new SyndicationPerson("liuzhenyu", "liuzhenyulive@live.com"));
                    item.AddLink(new SyndicationLink(new Uri(item.Id)));

                    await writer.Write(item);
                }
            }
        }

        private async Task<ISyndicationFeedWriter> GetWriter(string type, XmlWriter xmlWriter, DateTime updated)
        {
            string host = Request.Scheme + "://" + Request.Host + "/";

            if (type.Equals("rss", StringComparison.OrdinalIgnoreCase))
            {
                var rss = new RssFeedWriter(xmlWriter);
                await rss.WriteTitle("iBlogs");
                await rss.WriteDescription("iBlogs");
                await rss.WriteGenerator("iBlogs");
                await rss.WriteValue("link", host);
                return rss;
            }

            var atom = new AtomFeedWriter(xmlWriter);
            await atom.WriteTitle(_optionService.Get(ConfigKey.SiteTitle,"iBlogs"));
            await atom.WriteId(host);
            await atom.WriteSubtitle(_optionService.Get(ConfigKey.SiteDescription,"iBlogs"));
            await atom.WriteGenerator("iBlogs", "https://github.com/liuzhenyulive/iblogs", "1.0");
            await atom.WriteValue("updated", updated.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            return atom;
        }
    }
}