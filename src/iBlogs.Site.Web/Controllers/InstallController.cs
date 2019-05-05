using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace iBlogs.Site.Web.Controllers
{
    public class InstallController : Controller
    {
        public IActionResult Index()
        {
            return View(new ViewBaseModel());
        }
    }
}