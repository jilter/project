﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YouGou8.Admin.Helper;

namespace YouGou8.Admin.Controllers
{
    [MemberAuthorize]
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
    }
}