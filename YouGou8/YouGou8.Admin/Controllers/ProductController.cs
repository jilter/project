using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YouGou8.Model;
using YouGou8.Service;
using YouGou8.Admin.Helper;

namespace YouGou8.Admin.Controllers
{
    [MemberAuthorize]
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int productType = 0,string productName = "",int pageIndex=1,int pageSize=20)
        {
            List<Category> categoryList = CategoryService.GetList();
            var selectItemList = new List<SelectListItem>() {
                new SelectListItem(){Value="0",Text="全部",Selected=true}
            };
            var selectList = new SelectList(categoryList, "ID", "Name");
            selectItemList.AddRange(selectList);
            ViewBag.CategoryList = selectItemList;

            int totalCount = 0;
            var productList = ProductService.GetAllList(productType, productName, pageSize, pageIndex, out totalCount);

            ViewBag.PageCount = totalCount % pageSize == 0 ? (int)totalCount / pageSize : (int)totalCount / pageSize + 1;
            ViewBag.ProductName = productName;
            ViewBag.PageIndex = pageIndex;
            return View(productList);
        }

        public ActionResult Delete(string ids)
        {
            string[] idArr = ids.Split(',');
            if (idArr.Length > 0)
            {
                foreach(var s in idArr)
                {
                    ProductService.Delete(Convert.ToInt64(s));
                }                
            }
            return Json(new { code=0},JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteAll()
        {
            int result=ProductService.Delete();
            return Json(new { code = 0, data = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteDisable()
        {
            int result = ProductService.DeleteDisable();
            return Json(new { code = 0, data = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Import()
        {
            List<Category> categoryList = CategoryService.GetList();
            var selectItemList = new List<SelectListItem>() {
                new SelectListItem(){Value="0",Text="未知",Selected=true}
            };
            var selectList = new SelectList(categoryList, "ID", "Name");
            selectItemList.AddRange(selectList);
            ViewBag.CategoryList = selectItemList;
            return View();
        }

        [HttpPost]
        public JsonResult ImportP(HttpPostedFileBase excelFile, short excelType,int productType)
        {
            string fileExt = Path.GetExtension(excelFile.FileName).ToLower();
            if (fileExt != ".xls" && fileExt != ".xlsx")
            {
                return Json(new { code=1,msg="只能上传后缀为xls或xlsx的文件"}, JsonRequestBehavior.DenyGet);
            }
            string path = ProductService.SaveExcle(excelFile);
            if (string.IsNullOrEmpty(path))
            {
                return Json(new { code = 2, msg = "保存文件失败" }, JsonRequestBehavior.DenyGet);
            }
            DataTable dt = ProductService.GetDataFromExcelByConn(path);
            if (dt == null)
            {
                return Json(new { code = 2, msg = "读取Excel数据失败" }, JsonRequestBehavior.DenyGet);
            }
            List<Product_MY> pList = new List<Product_MY>();
            if(excelType==1)
            {
                if (dt.Columns.Count != 22)
                    return Json(new { code = 3, msg = "普通商品Excel列数不等于22,请检查" }, JsonRequestBehavior.DenyGet);
                else
                    pList = ProductService.GetNormalList(dt, productType);
            }
            if (excelType == 2)
            {
                if (dt.Columns.Count != 26)
                    return Json(new { code = 3, msg = "高佣商品Excel列数不等于26,请检查格式" }, JsonRequestBehavior.DenyGet);
                else
                    pList = ProductService.GetHightList(dt, productType);
            }
            if (excelType == 3)
            {
                if (dt.Columns.Count != 22 && dt.Columns.Count != 23)
                    return Json(new { code = 3, msg = "优质商品Excel列数不等于22或23,请检查格式" }, JsonRequestBehavior.DenyGet);
                else
                    pList = ProductService.GetExtList(dt, productType);
            }
            if (pList!=null && pList.Count > 0)
            {
                foreach(Product_MY p in pList)
                {
                    ProductService.Add(p);
                }
                return Json(new { code = 0, msg = "导入成功" }, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(new { code = 4, msg = "导入的商品为空" }, JsonRequestBehavior.DenyGet);
            }            
        }

        public ActionResult Add()
        {
            List<Category> categoryList = CategoryService.GetList();
            var selectItemList = new List<SelectListItem>() {
                new SelectListItem(){Value="0",Text="全部"}
            };
            var selectList = new SelectList(categoryList, "ID", "Name");
            selectItemList.AddRange(selectList);

            Product_MY p = new Product_MY();
            
            ViewBag.CategoryList = selectItemList;
            return View(p);
        }

        [HttpPost]
        public ActionResult Add(Product_MY p)
        {
            p.AddedID = ConfigService.LoginUser == null ? 1 : ConfigService.LoginUser.ID;
            p.PlanID = ConfigService.LoginUser == null ? "0" : ConfigService.LoginUser.PID;
            p.AddedType = 3;
            p.AddedTime = DateTime.Now;
            if (!p.CouponEndTime.HasValue)
            {
                p.CouponEndTime = DateTime.Now.AddDays(3);
            }
            else
            {
                p.CouponEndTime = p.CouponEndTime.Value.AddDays(1);
            }
            if (!p.PSales.HasValue)
            {
                p.PSales = 0;
            }
            if (!p.PRate.HasValue)
            {
                p.PRate = 0;
            }
            if (!p.CouponStartTime.HasValue)
            {
                p.CouponStartTime = DateTime.Now;
            }
            if (!p.CouponCount.HasValue)
            {
                p.CouponCount = 100;
            }
            if (!p.CouponRemain.HasValue)
            {
                p.CouponRemain = 100;
            }
            if (p.PPrice.HasValue && p.CouponMoney.HasValue && p.PRate.HasValue)
            {
                p.PCommission = (p.PPrice - p.CouponMoney) * (p.PRate / 100);
                p.PRedPack = UtilityService.GetRedPack(p.PPrice.Value, p.CouponMoney.Value, p.PRate.Value);
            }
            p.ID = ProductService.Add(p);
            return Content("<script>alert('添加成功');window.location.href='" + Url.Content("~/Product/Add")+"';</script>");
        }

        public ActionResult GetProductInfo(string url)
        {
            TaoModel model = TaoService.GetItemByUrl(url);
            if (model != null && !string.IsNullOrWhiteSpace(model.ProductName))
            {
                return Json(new { code=0,data=model}, JsonRequestBehavior.AllowGet);
            }
            return Json(new { code=1,msg="获取信息失败"}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetProduct(long id,bool isbig)
        {
            int result = ProductService.UpdateBig(id, isbig);
            if (result > 0)
            {
                return Json(new { code = 0, msg = "设置成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 1, msg = "设置失败" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Update(long id)
        {
            List<Category> categoryList = CategoryService.GetList();
            var selectItemList = new List<SelectListItem>() {
                new SelectListItem(){Value="0",Text="未知"}
            };
            var selectList = new SelectList(categoryList, "ID", "Name");
            selectItemList.AddRange(selectList);

            Product_MY p = new Product_MY();
            if (id > 0)
            {
                p = ProductService.GetInfo(id);
                if (p == null)
                {
                    p = new Product_MY();
                }
                else
                {
                    selectItemList.Find(s => s.Value == p.PCID.ToString()).Selected = true;
                }
            }

            ViewBag.CategoryList = selectItemList;
            return View(p);
        }

        [HttpPost]
        public ActionResult Update(Product_MY p)
        {
            if (p.ID > 0)
            {
                int result = ProductService.Update(p);
                return Content("<script>alert('修改成功');window.location.href='" + Url.Content("~/Product/Update?id=") + p.ID + "';</script>");
            }
            else
            {
                return Content("<script>alert('修改失败');window.location.href='" + Url.Content("~/Product/Update?id=") + p.ID + "';</script>");
            }
            
        }
    }
}