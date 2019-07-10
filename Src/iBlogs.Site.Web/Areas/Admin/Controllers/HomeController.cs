using iBlogs.Site.Core.Attach.Service;
using iBlogs.Site.Core.Comment.Service;
using iBlogs.Site.Core.Content.DTO;
using iBlogs.Site.Core.Content.Service;
using iBlogs.Site.Web.Attribute;
using iBlogs.Site.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iBlogs.Site.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IContentsService _contentsService;
        private readonly ICommentsService _commentsService;
        private readonly IAttachService _attachService;

        public HomeController(IContentsService contentsService, ICommentsService commentsService, IAttachService attachService)
        {
            _contentsService = contentsService;
            _commentsService = commentsService;
            _attachService = attachService;
        }

        [ViewLayout("~/Areas/Admin/Views/Layout/Layout.cshtml")]
        public IActionResult Index()
        {
            var articles = _contentsService.FindArticles(new ArticleParam());
            return View(new AdminIndexModel
            {
                Articles = articles.Rows,
                ArticlesCount = (int)articles.TotalRows,
                CommentsCount = _commentsService.GetTotalCount(),
                AttachCount = _attachService.GetTotalCount()
            });
        }
    }
}