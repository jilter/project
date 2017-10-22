using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;
using Utility;
using YouGou8.Model;
using YouGou8.Service;

namespace YouGou8.Web.Controllers
{
    public class WechatController : Controller
    {
        // GET: Wechat
        //public ActionResult Index(int id,DateTime? dt)
        //{
        //    DateTime dtStart = dt.HasValue ? dt.Value.AddDays(-7) : DateTime.Now.AddDays(-1).Date;
        //    DateTime dtEnd = dt.HasValue ? dt.Value : DateTime.Now.AddDays(1).Date;
        //    List<Product_MY> plist=ProductMyService.GetList(id, dtStart, dtEnd, "",1, 100);
        //    return View(plist);
        //}
        public ActionResult Index(int? id, DateTime? dt, string title = "")
        {
            DateTime dtStart = dt.HasValue ? dt.Value.AddDays(-7) : DateTime.Now.AddDays(-2).Date;
            DateTime dtEnd = dt.HasValue ? dt.Value : DateTime.Now.AddDays(1).Date;
            ViewBag.Start = dtStart;
            ViewBag.End = dtEnd;
            ViewBag.ID = id.HasValue ? id.Value : 0;
            ViewBag.Title = title;
            return View();
        }

        public ActionResult GetList(int id, string title, DateTime start, DateTime end, int page = 1, int size = 100)
        {
            List<Product_MY> plist = ProductMyService.GetList(id, start, end, title, page, size);
            List<object> objList = new List<object>();
            if (plist.Count > 0)
            {
                foreach (var item in plist)
                {
                    objList.Add(new
                    {
                        id = item.ID,
                        title = item.PTitle,
                        intro = item.PIntro,
                        img = item.PImgUrls.Split('|')[0],
                        coupon = item.CouponMoney,
                        key = item.CouponCommand,
                        price = item.PPrice,
                        link = item.CouponShortLink,
                        sales = item.PSales
                    });
                }
            }
            return Json(new { code = 0, data = objList }, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration =300, VaryByParam="id")]
        public ActionResult Detail(long id,bool isjson=false)
        {
            Product_MY product = ProductMyService.GetInfo(id);
            if(string.IsNullOrWhiteSpace(product.CouponCommand))
            {
                ITopClient client = new DefaultTopClient(ConfigService.TaoBaoApiUrl, "24526506", "5a9e071dacf3f6f925eccce6f7b99602", "json");
                TbkTpwdCreateRequest req = new TbkTpwdCreateRequest();
                req.UserId = "98776048";
                req.Text = product.PTitle;
                req.Url = product.CouponLink;
                req.Logo = product.PImgUrls.Replace("300x300", "100x100");
                req.Ext = "{\"test\":\"testv\"}";
                TbkTpwdCreateResponse rsp = client.Execute(req);
                JObject obj = JObject.Parse(rsp.Body);
                if(obj["tbk_tpwd_create_response"]!=null&&
                    obj["tbk_tpwd_create_response"]["data"]!=null&&
                    obj["tbk_tpwd_create_response"]["data"]["model"] != null)
                {
                    product.CouponCommand = obj["tbk_tpwd_create_response"]["data"]["model"].ToString();
                    ProductMyService.Update(product);
                    if (isjson)
                    {
                        return Json(new { key = product.CouponCommand }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    ErrorLog.WriteTextLog("", rsp.Body, DateTime.Now);
                    if (isjson)
                    {
                        return Json(new { key = "" }, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            else if(isjson)
            {
                return Json(new { key = product.CouponCommand }, JsonRequestBehavior.AllowGet);
            }
            return View(product);
        }
    }
}