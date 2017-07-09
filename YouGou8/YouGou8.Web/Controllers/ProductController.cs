using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YouGou8.Model;
using YouGou8.Service;

namespace YouGou8.Web.Controllers
{
    public class ProductController : Controller
    {
        #region 匹配方法 新版上线后删除
        public ActionResult GetList(string name, int type = 0, int page = 1, int size = 5)
        {
            List<Product_MY> list = ProductMyService.GetList(0,DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1));
            if (type == 0 && !String.IsNullOrWhiteSpace(name))
            {
                list = list.FindAll(l => l.PTitle.Contains(name));
            }
            if (type == 1)
            {
                list = list.FindAll(l => (l.PPrice - l.CouponMoney)< 9.9m);
            }
            int totalPage = 0;
            if (list.Count > 0)
            {
                totalPage = list.Count % size == 0 ? Convert.ToInt16(list.Count / size) : Convert.ToInt16(list.Count / size) + 1;
            }
            if (page > totalPage)
            {
                return Json(new { code = 0, result = new { }, total = totalPage }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var objList = new List<object>();
                list = list.Take(size * page).Skip(size * (page - 1)).ToList<Product_MY>();
                var redpack = 0.00m;
                foreach (var item in list)
                {
                    redpack = UtilityService.GetRedPack(item.PPrice.Value, item.CouponMoney.Value, 10);
                    objList.Add(new
                    {
                        id = item.ID,
                        name = item.PTitle,
                        img = item.PImgUrls.Split('|')[0],
                        price = item.PPrice - item.CouponMoney - redpack,
                        coupon = item.CouponMoney,
                        sales = item.PSales,
                        redpack = redpack
                    });
                }
                return Json(new { code = 0, result = objList, total = totalPage }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetFree()
        {
            int totalCount = 0;
            List<FreeProduct> freeList = FreeProductService.GetList(100, 1, out totalCount);
            if (freeList.Count > 0)
            {
                var objList = new List<object>();
                foreach (var item in freeList)
                {
                    objList.Add(new
                    {
                        id = item.ID,
                        name = item.Name,
                        imgs = item.ImgUrls.Split('|'),
                        count = item.Count,
                        time = item.AddedTime.Value.ToString("MM/dd 23:59")
                    });
                }
                return Json(new { code = 0, result = objList }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 0, result = new { } }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetDetail(long id)
        {
            Product_MY product = ProductMyService.GetInfo(id);
            if (product != null)
            {
                var redpack = UtilityService.GetRedPack(product.PPrice.Value, product.CouponMoney.Value, 10);
                return Json(new
                {
                    code = 0,
                    result = new
                    {
                        id = product.ID,
                        name = product.PTitle,
                        img = product.PImgUrls.Split('|')[0],
                        price = product.PPrice - product.CouponMoney - redpack,
                        coupon = product.CouponMoney,
                        redpack = redpack,
                        sales = product.PSales,
                        key = product.CouponCommand
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 1, result = new { } }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}