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
                Page = index == 0 ? 1 : index
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
            var articleParam = new ArticleParam { Page = index == 0 ? 1 : index, OrderBy = "Hits" };
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
            var articleParam = new ArticleParam { Page = index == 0 ? 1 : index, OrderBy = "Random" };
            return View("Index", new IndexViewModel
            {
                Contents = _contentsService.FindArticles(articleParam),
                OrderType = "Random"
            });
        }

        [HttpGet("/index/tag/{tag}/{index}")]
        [HttpGet("/tag/{tag}")]
        [ViewLayout("~/Views/Layout/Layout.cshtml")]
        public IActionResult Tag(string tag, int index)
        {
            return View("Index", new IndexViewModel
            {
                DisplayType = "标签",
                DisplayMeta = tag,
                OrderType = "tag",
                Contents = _contentsService.FindContentByMeta(MetaType.Tag, tag, new ArticleParam { Page = index == 0 ? 1 : index })
            });
        }

        [HttpGet("/index/category/{category}/{index}")]
        [HttpGet("/category/{category}")]
        [ViewLayout("~/Views/Layout/Layout.cshtml")]
        public IActionResult Category(string category, int index)
        {
            return View("Index", new IndexViewModel
            {
                DisplayType = "分类",
                DisplayMeta = category,
                OrderType = "category",
                Contents = _contentsService.FindContentByMeta(MetaType.Category, category, new ArticleParam
                {
                    Page = index == 0 ? 1 : index
                })
            });
        }

        [HttpGet("/archives")]
        [HttpGet("/index/archives/{index}")]
        [ViewLayout("~/Views/Layout/Layout.cshtml")]
        public IActionResult Archive(int index)
        {
            return View(_contentsService.GetArchive(new PageParam
            {
                Page = index == 0 ? 1 : index
            }));
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

        [HttpGet("search")]
        [HttpGet("/search/{keyword}")]
        [HttpGet("/search/{keyword}/{index}")]
        [ViewLayout("~/Views/Layout/Layout.cshtml")]
        public IActionResult Search(string keyword, int index)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return View("Index", new IndexViewModel
                {
                    OrderType = "Index",
                    Contents = _contentsService.FindArticles(new ArticleParam { Page = index == 0 ? 1 : index })
                });

            var articleParam = new ArticleParam { Page = index == 0 ? 1 : index, Title = keyword };
            return View("Search", new SearchViewModel
            {
                KeyWord = keyword,
                Contents = _contentsService.FindArticles(articleParam)
            });
        }
    }
}