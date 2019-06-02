using iBlogs.Site.Core.Content.DTO;
using iBlogs.Site.Core.Content.Service;
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
            return View(_contentsService.FindArticles(articleParam));
        }
    }
}