using iBlogs.Site.Web.Attribute;
using iBlogs.Site.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace iBlogs.Site.Web.Controllers
{
    public class IndexController : Controller
    {
        [ViewLayout("~/Views/Layout/Layout.cshtml")]
        public IActionResult Index()
        {
            return View(new ViewBaseModel());
        }
    }
}