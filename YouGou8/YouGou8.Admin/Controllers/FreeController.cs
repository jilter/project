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
    public class FreeController : Controller
    {
        // GET: Free
        public ActionResult Index(int pageIndex = 1, int pageSize = 20)
        {
            int totalCount = 0;
            var productList = FreeProductService.GetAllList(pageSize, pageIndex, out totalCount);
            ViewBag.PageCount = totalCount % pageSize == 0 ? (int)totalCount / pageSize : (int)totalCount / pageSize + 1;
            ViewBag.PageIndex = pageIndex;
            return View(productList);
        }

        public ActionResult Delete(string ids)
        {
            string[] idArr = ids.Split(',');
            if (idArr.Length > 0)
            {
                foreach (var s in idArr)
                {
                    FreeProductService.Delete(Convert.ToInt32(s));
                }
            }
            return Json(new { code = 0 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteAll()
        {
            int result = FreeProductService.Delete();
            return Json(new { code = 0, data = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteDisable()
        {
            int result = FreeProductService.DeleteDisable();
            return Json(new { code = 0, data = result }, JsonRequestBehavior.AllowGet);
        }
        

        public ActionResult Add(int? id)
        {
            FreeProduct p = new FreeProduct();
            if (id.HasValue && id > 0)
            {
                p = FreeProductService.GetInfo(id.Value);
                if (p == null)
                {
                    p = new FreeProduct();
                }
            }

            return View(p);
        }

        [HttpPost]
        public ActionResult Add(FreeProduct p)
        {
            if (p.ID > 0)
            {
                int result = FreeProductService.Update(p);
            }
            else
            {
                p.AddedTime = DateTime.Now;
                p.ID = FreeProductService.Add(p);
            }
            return Content("<script>alert('操作成功');window.location.href='" + Url.Content("~/Free/Add?id=") + p.ID + "';</script>");
        }
    }
}