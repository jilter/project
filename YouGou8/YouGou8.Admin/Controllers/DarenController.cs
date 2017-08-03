using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YouGou8.Admin.Helper;
using YouGou8.Model;
using YouGou8.Service;

namespace YouGou8.Admin.Controllers
{
    [MemberAuthorize]
    public class DarenController : Controller
    {
        // GET: Daren
        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportP(HttpPostedFileBase excelFile)
        {
            string fileExt = Path.GetExtension(excelFile.FileName).ToLower();
            if (fileExt != ".xls" && fileExt != ".xlsx")
            {
                return Json(new { code = 1, msg = "只能上传后缀为xls或xlsx的文件" }, JsonRequestBehavior.DenyGet);
            }
            string path = ProductService.SaveExcle(excelFile, "~/Upload/Daren/");
            if (string.IsNullOrEmpty(path))
            {
                return Json(new { code = 2, msg = "保存文件失败" }, JsonRequestBehavior.DenyGet);
            }
            DataTable dt = ProductService.GetDataFromExcelByConn(path);
            if (dt == null)
            {
                return Json(new { code = 3, msg = "读取Excel数据失败" }, JsonRequestBehavior.DenyGet);
            }
            List<DarenPro> successList = new List<DarenPro>();
            List<DarenPro> failList = new List<DarenPro>();
            List<DarenPro> repeatList = new List<DarenPro>();
            int result = DarenProService.Import(dt,out successList,out failList,out repeatList);
            if (result == 0)
            {
                return Json(new { code = 4, msg = "导入数据失败" }, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(new { code = 0, msg = "导入数据成功",data=new { slist = successList, flist = failList, rlist = repeatList } }, JsonRequestBehavior.DenyGet);
            }
        }
    }
}