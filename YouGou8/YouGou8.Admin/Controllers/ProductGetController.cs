using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;
using YouGou8.Admin.Helper;
using YouGou8.Model;
using YouGou8.Service;

namespace YouGou8.Admin.Controllers
{
    [MemberAuthorize]
    public class ProductGetController : Controller
    {
        // GET: ProductGet
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Import()
        {
            return View();
        }

        public ActionResult QTK()
        {
            DateTime? dt = ProductQTKService.GetMaxTime();
            ViewBag.LastUpdate = dt.HasValue ? dt.Value.ToString("yy-MM-dd HH:mm") : "---";
            return View();
        }

        [HttpPost]
        public ActionResult UpdateQTK()
        {
            int success = 0;
            int error = 0;
            int result = ProductQTKService.GetQTKData(out success, out error);
            return Json(new { code = result, data = new { error = error, success = success } }, JsonRequestBehavior.DenyGet);
        }

        public ActionResult YMB()
        {
            return View();
        }

        public ActionResult Hand()
        {
            List<Category> categoryList = CategoryService.GetList();
            var selectItemList = new List<SelectListItem>() {
                new SelectListItem(){Value="0",Text="全部",Selected=true}
            };
            var selectList = new SelectList(categoryList, "ID", "Name");
            selectItemList.AddRange(selectList);
            ViewBag.CategoryList = selectItemList;
            return View();
        }

        [HttpPost]
        public ActionResult HandP(string imgUrl, string cmdText, int cid = 0)
        {
            int code = 0;
            string msg = "添加成功";
            if (string.IsNullOrWhiteSpace(imgUrl) || string.IsNullOrWhiteSpace(cmdText))
            {
                code = 107;
                msg = "参数错误";
                return Json(new { code = code, msg = msg }, JsonRequestBehavior.AllowGet);
            }
            int result = ProductMyService.QTKInput(imgUrl, cmdText,cid);
            switch (result)
            {
                case 0: code = 0; msg = "添加成功"; break;
                case 1: code = 1; msg = "数据转换出错"; break;
                case 2: code = 2; msg = "系统出错"; break;
            }
            return Json(new { code = code, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Taobao()
        {
            List<Category> categoryList = CategoryService.GetList();
            var selectItemList = new List<SelectListItem>() {
                new SelectListItem(){Value="0",Text="全部",Selected=true}
            };
            var selectList = new SelectList(categoryList, "ID", "Name");
            selectItemList.AddRange(selectList);
            ViewBag.CategoryList = selectItemList;
            return View();
        }

        [HttpPost]
        public ActionResult TaobaoP(string cmdText, int cid = 0)
        {
            int code = 0;
            string msg = "添加成功";
            if (string.IsNullOrWhiteSpace(cmdText))
            {
                code = 107;
                msg = "参数错误";
                return Json(new { code = code, msg = msg }, JsonRequestBehavior.AllowGet);
            }
            int result = ProductMyService.TaobaoAppInput(cmdText, cid);
            switch (result)
            {
                case 0: code = 0; msg = "添加成功"; break;
                case 1: code = 1; msg = "无法获取短链接"; break;
                case 2: code = 2; msg = "无法访问短连接"; break;
                case 3: code = 3; msg = "无法获取图片地址"; break;
                case 4: code = 4; msg = "数据转换出错"; break;
                case 5: code = 5; msg = "系统出错"; break;
            }
            return Json(new { code = code, msg = msg }, JsonRequestBehavior.AllowGet);
        }
    }
}