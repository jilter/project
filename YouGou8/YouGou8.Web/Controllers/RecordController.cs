using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YouGou8.Model;
using YouGou8.Service;
using YouGou8.Web.Helper;

namespace YouGou8.Web.Controllers
{
    [MemberAuthorize]
    public class RecordController : Controller
    {
        // GET: Record
        public ActionResult Index(int freeId=0, int pageIndex = 1, int pageSize = 20)
        {
            int totalCount = 0;
            var productList = FreeRecordService.GetList(freeId, pageSize, pageIndex, out totalCount);

            ViewBag.PageCount = totalCount % pageSize == 0 ? (int)totalCount / pageSize : (int)totalCount / pageSize + 1;
            ViewBag.FreeID = freeId;
            ViewBag.PageIndex = pageIndex;
            return View(productList);
        }

        public ActionResult Add(long? id)
        {
            FreeRecord fr = new FreeRecord();
            if (id.HasValue && id > 0)
            {
                fr = FreeRecordService.GetInfo(id.Value);
                if (fr == null)
                {
                    fr = new FreeRecord();
                }
            }

            return View(fr);
        }

        [HttpPost]
        public ActionResult Add(FreeRecord fr)
        {
            string msg = "";
            int result = 0;
            long id = fr.ID;
            if (fr.ID > 0)
            {
                result = FreeRecordService.Update(fr);
                if (result > 0)
                {
                    msg = "更新成功";
                }
                else
                {
                    msg = "更新失败";
                }
            }
            else
            {
                result = 0;
                if (fr.UserID > 0 && fr.FreeID > 0)
                {
                    Users user = UsersService.GetInfo(fr.UserID);
                    if (user == null || user.ID <= 0)
                    {
                        msg = "用户ID不正确";
                    }
                    else
                    {
                        result = FreeRecordService.CheckAndInsert(fr.UserID, fr.FreeID, out id);
                        switch (result)
                        {
                            case 0: msg = "添加成功"; break;
                            case 1: msg = "赠品ID不正确"; break;
                            case 2: msg = "赠品已送完"; break;
                            case 3: msg = "活动已结束"; break;
                            case 4: msg = "已申请过赠品"; break;
                            case 5: msg = "添加失败"; break;
                            case 6: msg = "更新赠品失败"; break;
                            case 7: msg = "该用户今天已领过赠品"; break;
                            default: msg = "添加失败"; break;
                        }
                    }
                }
                else
                {
                    msg = "用户ID和赠品ID不能为空";
                }
            }
            return Content("<script>alert('"+msg+"');window.location.href='" + Url.Content("~/record/add?id=") + id + "';</script>");
        }

        public ActionResult Delete(string ids)
        {
            string[] idArr = ids.Split(',');
            if (idArr.Length > 0)
            {
                foreach (var s in idArr)
                {
                    FreeRecordService.Delete(Convert.ToInt64(s));
                }
            }
            return Json(new { code = 0 }, JsonRequestBehavior.AllowGet);
        }
    }
}