using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YouGou8.Model;
using YouGou8.Service;

namespace YouGou8.Web.Controllers
{
    public class WeiboController : Controller
    {
        // GET: Weibo
        //public ActionResult Index(int? id, DateTime? dt,string title="")
        //{
        //    DateTime dtStart = dt.HasValue ? dt.Value.AddDays(-7) : DateTime.Now.AddDays(-1).Date;
        //    DateTime dtEnd = dt.HasValue ? dt.Value : DateTime.Now.AddDays(1).Date;
        //    ViewBag.Start = dtStart;
        //    ViewBag.End = dtEnd;
        //    ViewBag.ID = id.HasValue ? id.Value : 0;
        //    ViewBag.Title = title;
        //    return View();
        //}

        //public ActionResult GetList(int id,string title,DateTime start,DateTime end,int page=1,int size=100)
        //{
        //    List<Product_MY> plist = ProductMyService.GetList(id, start, end, title, page, size);
        //    List<object> objList = new List<object>();
        //    if (plist.Count > 0)
        //    {
        //        foreach (var item in plist)
        //        {
        //            objList.Add(new
        //            {
        //                title = item.PTitle,
        //                intro = item.PIntro,
        //                img = item.PImgUrls.Split('|')[0],
        //                coupon = item.CouponMoney,
        //                key = item.CouponCommand,
        //                price = item.PPrice,
        //                link = item.CouponShortLink,
        //                sales = item.PSales
        //            });
        //        }
        //    }
        //    return Json(new { code = 0, data = objList }, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Index(int? id, DateTime? dt, string title = "")
        {
            DateTime dtStart = dt.HasValue ? dt.Value.AddDays(-7) : DateTime.Now.AddDays(-2).Date;
            DateTime dtEnd = dt.HasValue ? dt.Value : DateTime.Now.AddDays(1).Date;
            List<Product_MY> plist = ProductMyService.GetList(id.HasValue?id.Value:0, dtStart, dtEnd, title, 1, 90);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (plist != null && plist.Count > 0)
            {
                string key = "";
                string value = "";
                int index = 1;
                int total = 0;
                foreach(var item in plist)
                {
                    total++;
                    key += item.PImgUrls.Split('|')[0]+"|";
                    value += "图" + (index) + item.PTitle + item.CouponShortLink+ "，";
                    if (index % 9 == 0 || total == plist.Count)
                    {
                        key = key.TrimEnd('|');
                        value = "#淘宝大牌优惠#" + value.TrimEnd('，') + "，更多优惠http://yougou8.com.cn/wechat/1.html";
                        dict.Add(key, value);
                        key = "";
                        value = "";
                        index = 1;
                    }else
                    {
                        index++;
                    }
                }
            }
            ViewBag.Dict = dict;
            return View();
        }
    }
}