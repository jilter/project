using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YouGou8.Model;
using YouGou8.Service;
using YouGou8.Admin.Helper;

namespace YouGou8.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [MemberAuthorize]
        public ActionResult Index()
        {
            var newUserCount = UsersService.GetTodayUser();
            var loginUserCount = UsersLLogService.GetTodayLogin();
            var browseCount = UsersBLogService.GetTodayBrowse();
            List<Product_MY> pList = ProductService.GetTopByClick();
            List<Category> categoryList = CategoryService.GetList();
            var selectItemList = new List<SelectListItem>() {
                new SelectListItem(){Value="0",Text="全部",Selected=true}
            };
            var selectList = new SelectList(categoryList, "ID", "Name");
            selectItemList.AddRange(selectList);
            ViewBag.NewCount = newUserCount;
            ViewBag.LoginCount = loginUserCount;
            ViewBag.BrowseCount = browseCount;
            ViewBag.ProductList = pList;
            ViewBag.CategoryList = selectItemList;
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName,string pwd)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(pwd))
            {
                return Content("<script>alert('请输入用户名和密码');window.location.href='/home/login';</script>");
            }
            YouGou8.Model.Admin admin = AdminService.GetInfo(userName, pwd);
            if (admin != null)
            {
                ConfigService.LoginUser = admin;
                return Content("<script>window.location.href='/home/index';</script>");
            }
            else
            {
                return Content("<script>alert('用户名或密码错误');window.location.href='/home/login';</script>");
            }
        }

        public ActionResult logout()
        {
            ConfigService.Logout();
            return Redirect("/home/login");
        }
    }
}