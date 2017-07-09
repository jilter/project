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
    [MemberAuthorize]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index(string name,int pageIndex = 1, int pageSize = 20)
        {
            int totalCount = 0;
            var userList = UsersService.GetAllList(name,pageSize, pageIndex, out totalCount);
            ViewBag.PageCount = totalCount % pageSize == 0 ? (int)totalCount / pageSize : (int)totalCount / pageSize + 1;
            ViewBag.PageIndex = pageIndex;
            ViewBag.Name = name;
            return View(userList);
        }

        public ActionResult Delete(string ids)
        {
            string[] idArr = ids.Split(',');
            if (idArr.Length > 0)
            {
                foreach (var s in idArr)
                {
                    UsersService.Delete(Convert.ToInt32(s));
                }
            }
            return Json(new { code = 0 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(int? id)
        {
            Users user = new Users();
            if (id.HasValue && id > 0)
            {
                user = UsersService.GetInfo(id.Value);
                if (user == null)
                {
                    user = new Users();
                }
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult Add(Users user)
        {
            if (user.ID > 0)
            {
                int result = UsersService.Update(user);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(user.AvatarUrl))
                {
                    user.AvatarUrl = "";
                }
                user.LastAccessTime = DateTime.Now;
                user.ID = UsersService.Insert(user);
            }
            return Content("<script>alert('操作成功');window.location.href='" + Url.Content("~/User/Add?id=") + user.ID + "';</script>");
        }
    }
}