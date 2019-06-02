using iBlogs.Site.Core.Content.Service;
using iBlogs.Site.Web.Attribute;
using iBlogs.Site.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace iBlogs.Site.Web.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IContentsService _contentsService;

        public ArticleController(IContentsService contentsService)
        {
            _contentsService = contentsService;
        }

        [HttpGet("/article/{url}")]
        [ViewLayout("~/Views/Layout/Layout.cshtml")]
        public IActionResult Index(string url)
        {
            var content = _contentsService.GetContents(url);
            var pre = _contentsService.GetPre(content.Id);
            var next = _contentsService.GetNext(content.Id);
            return View(new ArticleViewModel
            {
                Content = content,
                Pre = pre,
                Next = next
            });
        }
    }
}