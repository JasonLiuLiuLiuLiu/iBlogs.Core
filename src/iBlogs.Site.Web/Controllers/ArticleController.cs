using iBlogs.Site.Core.Service.Content;
using iBlogs.Site.Web.Attribute;
using iBlogs.Site.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace iBlogs.Site.Web.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IContentsService contentsService;

        public ArticleController(IContentsService contentsService)
        {
            this.contentsService = contentsService;
        }

        [HttpGet("/article/{url}")]
        [ViewLayout("~/Views/Layout/Layout.cshtml")]
        public IActionResult Index(string url)
        {
            contentsService.getContents(url);
            return View(new ViewBaseModel());
        }
    }
}