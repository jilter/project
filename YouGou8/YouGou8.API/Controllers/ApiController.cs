using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YouGou8.Model;
using YouGou8.Service;

namespace YouGou8.API.Controllers
{
    public class ApiController : Controller
    {
        // GET: Api
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMemberInfo(Users u)
        {
            if (string.IsNullOrWhiteSpace(u.NickName) || string.IsNullOrWhiteSpace(u.AvatarUrl))
            {
                return Json(new { code = 1, msg = "参数错误" }, JsonRequestBehavior.AllowGet);
            }
            Users user = UsersService.GetInfo(u.NickName,u.AvatarUrl);
            if (user == null)
            {
                u.LastAccessTime = DateTime.Now;
                u.AccessCount = 1;
                u.ID = UsersService.Insert(u);
                user = u;
            }
            else
            {
                user.AccessCount = user.AccessCount + 1;
                UsersService.Update(user);
            }
            UsersLLogService.Insert(user.ID);
            return Json(new { code = 0, data = new { id = user.ID } }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetList(string name, string types = "0",int stype=1, int page = 1, int size = 5)
        {
            int totalPage = 0;
            int totalCount = 0;
            List<Product> list = new List<Product>();
            if (stype == 1)
            {
                list = ProductService.GetList(types, name, size, page, out totalCount);
            }
            else if (stype == 2)
            {
                list = ProductService.GetListBig(size, page, out totalCount);
            }
            else if (stype == 3)
            {
                list = ProductService.GetListPrice(10, size, page, out totalCount);
            }

            if (list.Count > 0)
            {
                totalPage = totalCount % size == 0 ? Convert.ToInt16(totalCount / size) : Convert.ToInt16(totalCount / size) + 1;
            }
            if (list == null || list.Count == 0)
            {
                return Json(new { code = 0, data = new List<Product>(), total = totalPage }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<object> objList = new List<object>();
                foreach (var item in list)
                {
                    objList.Add(new
                    {
                        id = item.ID,
                        name = item.Name,
                        img = item.ImgUrls.Split('|')[0],
                        price = item.Price - item.CouponMoney - item.Redpack,
                        coupon = item.CouponMoney,
                        sales = item.Sales,
                        redpack = item.Redpack
                    });
                }
                return Json(new { code = 0, data = objList, total = totalPage }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetDetail(long id,long userid=0)
        {
            Product product = ProductService.GetInfo(id);
            if (product != null)
            {
                product.ClickCount = product.ClickCount + 1;
                ProductService.Update(product);
                UsersBLog log = new UsersBLog();
                log.BrowseTime = DateTime.Now;
                log.CategoryID = product.CategoryID;
                log.Name = product.Name;
                log.ProductID = product.ID;
                log.ProductLink = product.Link;
                log.UserID = userid;
                UsersBLogService.Insert(log);
                return Json(new
                {
                    code = 0,
                    data = new
                    {
                        id = product.ID,
                        name = product.Name,
                        imgs = product.ImgUrls.Split('|'),
                        price = product.Price - product.CouponMoney - product.Redpack,
                        coupon = product.CouponMoney,
                        desc = product.Desc,
                        redpack = product.Redpack,
                        sales = product.Sales,
                        key = product.CouponCommand
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 1, msg="商品ID错误" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetFreeList(int page = 1, int size = 20)
        {
            int totalPage = 0;
            int totalCount = 0;
            List<FreeProduct> freeList = FreeProductService.GetList(size, page, out totalCount);
            if (freeList.Count > 0)
            {
                totalPage = totalCount % size == 0 ? Convert.ToInt16(totalCount / size) : Convert.ToInt16(totalCount / size) + 1;
                var objList = new List<object>();
                foreach (var item in freeList)
                {
                    objList.Add(new
                    {
                        id = item.ID,
                        name = item.Name,
                        img = item.ImgUrls.Split('|')[0],
                        count = item.Count,
                        remain = item.RQty,
                        time = item.AddedTime.Value.ToString("MM/dd 23:59")
                    });
                }
                return Json(new { code = 0, data = objList,total=totalPage }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 0, data = new List<FreeProduct>(), total = totalPage }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetFreeDetail(long userid,int freeid)
        {
            var product = FreeProductService.GetInfo(freeid);
            var frecordToday = FreeRecordService.GetTodayRecord(userid);
            var frecord = FreeRecordService.GetInfo(userid, freeid);
            if (product!=null)
            {
                var obj = new
                {
                    id = product.ID,
                    name = product.Name,
                    imgs = product.ImgUrls.Split('|'),
                    desc = product.Desc,
                    count = product.Count,
                    remain = product.RQty,
                    isget = frecord != null,
                    isgetother = frecordToday != null,
                    status=product.Status,
                    time = product.AddedTime.Value.ToString("MM/dd 23:59")
                };
                return Json(new { code = 0, data = obj }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 1, msg="赠品不存在" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetFree(long userid,string wechat,int freeid)
        {
            var code = 101;
            var msg = "101-系统错误,请联系客服";
            if (string.IsNullOrWhiteSpace(wechat))
            {
                code = 102;
                msg = "请填写微信号";
            }
            else if (userid <= 0)
            {
                code = 103;
                msg = "103-用户不存在";
            }
            else
            {
                Users u = UsersService.GetInfo(userid);
                if (u == null)
                {
                    code = 104;
                    msg = "104-用户不存在";
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(u.WeChat) || u.WeChat.ToLower() != wechat.ToLower())
                    {
                        u.WeChat = wechat;
                        UsersService.Update(u);
                    }

                    long frid = 0;
                    int result = FreeRecordService.CheckAndInsert(userid, freeid, out frid);
                    switch (result)
                    {
                        case 0: code = 0; msg = "领取成功"; break;
                        case 1: code = 1; msg = "赠品ID错误"; break;
                        case 2: code = 2; msg = "赠品已送完"; break;
                        case 3: code = 3; msg = "活动已结束"; break;
                        case 4: code = 4; msg = "您已领过该赠品"; break;
                        case 5: code = 5; msg = "赠品领取失败,请联系客服"; break;
                        case 6: code = 6; msg = "数据出错,请联系客服"; break;
                        case 7: code = 7; msg = "您今天已经领过其它赠品了"; break;
                        default: code = 101; msg = "数据出错,请联系客服"; break;
                    }
                }
            }
            return Json(new { code = code, msg = msg }, JsonRequestBehavior.AllowGet);
        }
    }
}