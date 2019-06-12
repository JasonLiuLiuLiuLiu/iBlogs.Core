using iBlogs.Site.Core.Common.Request;
using iBlogs.Site.Core.Content.DTO;
using iBlogs.Site.Core.Content.Service;
using iBlogs.Site.Core.Meta.DTO;
using iBlogs.Site.Core.Meta.Service;
using iBlogs.Site.Web.Attribute;
using iBlogs.Site.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace iBlogs.Site.Web.Controllers
{
    public class IndexController : Controller
    {
        private readonly IContentsService _contentsService;
        private readonly IMetasService _metasService;
        public IndexController(IContentsService contentsService, IMetasService metasService)
        {
            _contentsService = contentsService;
            _metasService = metasService;
        }
        [HttpGet("")]
        [HttpGet("/index/index/{index}")]
        [ViewLayout("~/Views/Layout/Layout.cshtml")]
        public IActionResult Index(int index)
        {
            var articleParam = new ArticleParam
            {
                Page = index==0?1:index
            };
            return View(new IndexViewModel
            {
                OrderType = "Index",
                Contents = _contentsService.FindArticles(articleParam)
            });
        }

        [HttpGet("hot")]
        [HttpGet("/index/hot/{index}")]
        [ViewLayout("~/Views/Layout/Layout.cshtml")]
        public IActionResult Hot(int index)
        {
            var articleParam = new ArticleParam {Page = index == 0 ? 1 : index, OrderBy = "Hits"};
            return View("Index", new IndexViewModel
            {
                Contents = _contentsService.FindArticles(articleParam),
                OrderType = "Hot"
            });
        }

        [HttpGet("random")]
        [HttpGet("/index/Random/{index}")]
        [ViewLayout("~/Views/Layout/Layout.cshtml")]
        public IActionResult Random(int index)
        {
            var articleParam = new ArticleParam {Page = index == 0 ? 1 : index, OrderBy = "Random"};
            return View("Index", new IndexViewModel
            {
                Contents = _contentsService.FindArticles(articleParam),
                OrderType = "Random"
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
                Contents = _contentsService.FindContentByMeta(MetaType.Tag, tag, new ArticleParam())
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

        [HttpGet("/archives")]
        [ViewLayout("~/Views/Layout/Layout.cshtml")]
        public IActionResult Archive(PageParam param)
        {
            return View(_contentsService.GetArchive(param ?? new PageParam()));
        }

        [HttpGet("AllTags")]
        [ViewLayout("~/Views/Layout/Layout.cshtml")]
        public IActionResult AllTags()
        {
            return View("AllTags", _metasService.LoadMetaDataViewModel(MetaType.Tag));
        }

        [HttpGet("AllCategories")]
        [ViewLayout("~/Views/Layout/Layout.cshtml")]
        public IActionResult AllCategories()
        {
            return View("AllCategories", _metasService.LoadMetaDataViewModel(MetaType.Category));
        }
    }
}