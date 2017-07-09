using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YouGou8.Model;
using YouGou8.Service;
using YouGou8.API.Models;
using YouGou8.API.Helper;

namespace YouGou8.API.Controllers
{
    public class ProductController : Controller
    {
        #region 匹配方法 新版上线后删除
        public ActionResult GetList(string name, int type = 0, int page = 1, int size = 5)
        {
            List<ProductModel> list = ExcelHelper.GetProductFromDataTable();
            if (type == 0 && !String.IsNullOrWhiteSpace(name))
            {
                list = list.FindAll(l => l.Name.Contains(name));
            }
            if (type == 1)
            {
                list = list.FindAll(l => (l.Price - UtilityService.GetMinPriceFromStr(l.CouponDesc)) < 9.9m);
            }
            int totalPage = 0;
            if (list.Count > 0)
            {
                totalPage = list.Count % size == 0 ? Convert.ToInt16(list.Count / size) : Convert.ToInt16(list.Count / size) + 1;
            }
            if (page > totalPage)
            {
                return Json(new { code = 0, result = new List<ProductModel>(), total = totalPage }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var objList = new List<object>();
                list = list.Take(size * page).Skip(size * (page - 1)).ToList<ProductModel>();
                var coupon = 0.00m;
                var redpack = 0.00m;
                foreach (var item in list)
                {
                    coupon = UtilityService.GetMinPriceFromStr(item.CouponDesc);
                    redpack = UtilityService.GetRedPack(item.Price, coupon, item.ActivityPercent);
                    objList.Add(new
                    {
                        id = item.ID,
                        name = item.Name,
                        img = item.Picture,
                        price = item.Price - coupon - redpack,
                        coupon = coupon,
                        sales = item.Sales,
                        redpack = redpack
                    });
                }
                return Json(new { code = 0, result = objList, total = totalPage }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetFree()
        {
            List<FreeModel> freeList = ExcelHelper.GetFreeFromDataTable();
            if (freeList.Count > 0)
            {
                var objList = new List<object>();
                foreach (var item in freeList)
                {
                    List<string> strList = new List<string>();
                    if (!string.IsNullOrWhiteSpace(item.Picture0))
                    {
                        strList.Add(item.Picture0);
                    }
                    if (!string.IsNullOrWhiteSpace(item.Picture1))
                    {
                        strList.Add(item.Picture1);
                    }
                    if (!string.IsNullOrWhiteSpace(item.Picture2))
                    {
                        strList.Add(item.Picture2);
                    }
                    if (!string.IsNullOrWhiteSpace(item.Picture3))
                    {
                        strList.Add(item.Picture3);
                    }
                    if (!string.IsNullOrWhiteSpace(item.Picture4))
                    {
                        strList.Add(item.Picture4);
                    }
                    objList.Add(new
                    {
                        id = item.ID,
                        name = item.Name,
                        imgs = strList,
                        count = item.Count,
                        time = item.EndDateTime.ToString("MM/dd HH:mm")
                    });
                }
                return Json(new { code = 0, result = objList }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 0, result = new List<FreeModel>() }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetDetail(long id)
        {
            List<ProductModel> list = ExcelHelper.GetProductFromDataTable();
            ProductModel product = list.Find(l => l.ID == id);
            if (product != null)
            {
                var coupon = UtilityService.GetMinPriceFromStr(product.CouponDesc);
                var redpack = UtilityService.GetRedPack(product.Price, coupon, product.ActivityPercent);
                return Json(new
                {
                    code = 0,
                    result = new
                    {
                        id = product.ID,
                        name = product.Name,
                        img = product.Picture,
                        price = product.Price - coupon - redpack,
                        coupon = coupon,
                        redpack = redpack,
                        sales = product.Sales,
                        key = string.IsNullOrWhiteSpace(product.CouponCommand) ? product.Command : product.CouponCommand
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 1, result = new ProductModel() }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}