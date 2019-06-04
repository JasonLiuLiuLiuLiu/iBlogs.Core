using iBlogs.Site.Core.Content.DTO;
using iBlogs.Site.Core.Content.Service;
using iBlogs.Site.Core.Meta.DTO;
using iBlogs.Site.Web.Attribute;
using iBlogs.Site.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace iBlogs.Site.Web.Controllers
{
    public class IndexController : Controller
    {
        private readonly IContentsService _contentsService;
        public IndexController(IContentsService contentsService)
        {
            _contentsService = contentsService;
        }
        [ViewLayout("~/Views/Layout/Layout.cshtml")]
        public IActionResult Index(ArticleParam articleParam)
        {
            if (articleParam == null)
                articleParam = new ArticleParam();
            return View(new IndexViewModel
            {
                Contents = _contentsService.FindArticles(articleParam)
            });
        }

        [HttpGet("/tag/{tag}")]
        [ViewLayout("~/Views/Layout/Layout.cshtml")]
        public IActionResult Tag(string tag)
        {
            return View("Index", new IndexViewModel
            {
                DisplayType = "标签",
                DisplayMeta = tag,
                Contents = _contentsService.FindContentByMeta(MetaType.Tag,tag,new ArticleParam())
            });
        }

        [HttpGet("/category/{category}")]
        [ViewLayout("~/Views/Layout/Layout.cshtml")]
        public IActionResult Category(string category)
        {
            return View("Index", new IndexViewModel
            {
                DisplayType = "分类",
                DisplayMeta = category,
                Contents = _contentsService.FindContentByMeta(MetaType.Category, category, new ArticleParam())
            });
        }

    }
}