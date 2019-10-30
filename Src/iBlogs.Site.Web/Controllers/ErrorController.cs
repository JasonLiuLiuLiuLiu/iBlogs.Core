using Microsoft.AspNetCore.Mvc;

namespace iBlogs.Site.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(int errorCode)
        {
            return View(errorCode);
        }

        public IActionResult Loading()
        {
            return View();
        }

        public IActionResult Error404()
        {
            return View();
        }

        public IActionResult Error500()
        {
            return View();
        }
    }
}