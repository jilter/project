using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;
using YouGou8.Service;

namespace YouGou8.Web.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList(string c,string o,int p,string q)
        {
            string url = "http://www.youmaiba.com/?";
            if (!string.IsNullOrWhiteSpace(q))
            {
                url = "http://www.youmaiba.com/search?q=" + q + "&";
            }
            if (!string.IsNullOrWhiteSpace(c))
            {
                url += "i=" + c + "&";
            }
            if (!string.IsNullOrWhiteSpace(o))
            {
                url += "o=" + o + "&";
            }
            if (p > 1)
            {
                url += "page=" + p + "&";
            }
            if (url.EndsWith("?") || url.EndsWith("&"))
            {
                url = url.Substring(0, url.Length - 1);
            }
            string response = UtilityService.GetYoumaibaData(url);
            return Content(response);
        }

        public ActionResult Goto(string itemId,string activityId)
        {
            string pid = ConfigService.PID;
            string redirectUrl = "https://uland.taobao.com/coupon/edetail?activityId={0}&pid={1}&itemId={2}&src=ld_ldto&dx=1";
            ViewBag.RediredUrl = string.Format(redirectUrl, activityId, pid, itemId);
            return View();
        }

        public ActionResult Test()
        {
            string url = "http://gw.api.taobao.com/router/rest";
            //string url = "http://gw.api.tbsandbox.com/router/rest";
            ITopClient client = new DefaultTopClient(url, "24526506", "5a9e071dacf3f6f925eccce6f7b99602");
            WirelessShareTpwdCreateRequest req = new WirelessShareTpwdCreateRequest();
            WirelessShareTpwdCreateRequest.GenPwdIsvParamDtoDomain obj1 = new WirelessShareTpwdCreateRequest.GenPwdIsvParamDtoDomain();
            obj1.Ext = "{\"ext\":\"xx\"}";
            obj1.Logo = "https://img.alicdn.com/bao/uploaded/i4/TB1poH_RVXXXXc5aXXXXXXXXXXX_!!0-item_pic.jpg_430x430q90.jpg";
            obj1.Url = "https://uland.taobao.com/coupon/edetail?activityId=740a1b5a164843bbbf059d12cb588a7c&pid=mm_28646890_25482821_94980646&itemId=545081345755&dx=1";
            obj1.Text = "蜂之语百花蜂蜜 纯净天然农家自产野生土蜂蜜峰蜂巢蜜洋槐花蜜";
            obj1.UserId = 28646890L;
            req.TpwdParam_ = obj1;
            WirelessShareTpwdCreateResponse rsp = client.Execute(req);
            return Content(rsp.Body);
        }
    }
}