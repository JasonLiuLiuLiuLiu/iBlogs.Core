﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace iBlogs.Site.Web.Controllers
{
    public class IndexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}