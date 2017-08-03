using Chloe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utility;
using YouGou8.Model;

namespace YouGou8.Service
{
    public class ProductMyService
    {
        public static int QTKInput(string url,string text,int cid)
        {
            Regex titleReg = new Regex("(?<=今日推荐：).+(?=\\n)");
            Regex priceReg = new Regex("(?<=（售价 ).+(?=元）)");
            Regex introReg = new Regex("(?<=【推荐理由】).+(?=\\n)");
            Regex salesReg = new Regex("(?<=已抢).+(?=件)");
            Regex couponReg = new Regex("(?<=领).+(?=元券包邮秒杀)");
            Regex urlReg = new Regex("(?<=商品链接：).+(?=；)");
            Regex cmdReg = new Regex("(?<=或复制这条信息).+(?=，)");

            try
            {
                Product_MY product = new Product_MY();
                product.AddedID = ConfigService.LoginUser==null?1: ConfigService.LoginUser.ID;
                product.AddedTime = DateTime.Now;
                product.AddedType = 3;
                product.ClickCount = 0;
                product.CouponCommand = cmdReg.Match(text).Value;
                product.CouponCount = 100;
                product.CouponEndTime = DateTime.Now.AddDays(2);
                product.CouponLink = "";
                product.CouponMoney = Convert.ToDecimal(couponReg.Match(text).Value);
                product.CouponRemain = 100;
                product.CouponShortLink = urlReg.Match(text).Value;
                product.CouponStartTime = DateTime.Now;
                product.Desc = "";
                product.DescImgs = "";
                product.IsBig = true;
                product.PCID = cid;
                product.PCommission = 0;
                product.PID = 0;
                product.PImgUrls = url;
                product.PIntro = introReg.Match(text).Value;
                product.PlanID = ConfigService.LoginUser == null ? "" : ConfigService.LoginUser.PID;
                product.PLink = product.CouponLink;
                product.PPrice = Convert.ToDecimal(priceReg.Match(text).Value);
                product.PRate = 0;
                product.PSales = Convert.ToInt32(salesReg.Match(text).Value);
                product.PTitle = titleReg.Match(text).Value;
                product.Remark = "";
                if (string.IsNullOrEmpty(product.PTitle) || 
                    string.IsNullOrWhiteSpace(product.CouponCommand) || 
                    !product.PPrice.HasValue || 
                    !product.PSales.HasValue || 
                    !product.CouponMoney.HasValue ||
                    string.IsNullOrWhiteSpace(product.CouponShortLink))
                {
                    return 1;
                }
                IQuery<Product_MY> q = MySqlHelper.context.Query<Product_MY>();
                var pmy=q.Where(p => p.CouponCommand == product.CouponCommand || p.CouponShortLink == product.CouponShortLink).FirstOrDefault();
                if (pmy != null)
                {
                    MySqlHelper.context.Delete<Product_MY>(p => p.ID == pmy.ID);
                }

                Product_MY newP = MySqlHelper.context.Insert(product);
            }
            catch(Exception ex)
            {
                ErrorLog.WriteTextLog("QTKInput", ex.ToString(), DateTime.Now);
                return 2;
            }
            return 0;
        }

        public static int TaobaoAppInput(string text,int cid)
        {
            text = text.Trim('\n');
            Regex titleReg = new Regex("(?<=.?).+(?=【包邮】)");
            Regex priceReg = new Regex("(?<=【在售价】).+(?=元)");
            Regex price2Reg = new Regex("(?<=【券后价】).+(?=元)");
            Regex urlReg = new Regex("(?<=【下单链接】).+(?=\\n)");
            Regex cmdReg = new Regex("(?<=复制这条信息，).+(?=，)");

            Regex imgReg = new Regex("(?<=var extraData = {\"pic\":\").+(?=\",\"title)");
            Regex couponLinkReg = new Regex("(?<=var url = ').+(?=';)");
            
            try
            {
                string shoplink = urlReg.Match(text).Value;
                if (string.IsNullOrEmpty(shoplink))
                {
                    return 1;
                }
                string response = RequestUtility.GetData(shoplink);
                if (string.IsNullOrEmpty(response))
                {
                    return 2;
                }
                string imgpath = imgReg.Match(response).Value;
                if (string.IsNullOrEmpty(imgpath))
                {
                    return 3;
                }
                imgpath = "https:" + imgpath + "_300x300";
                decimal price2 = Convert.ToDecimal(price2Reg.Match(text).Value);

                Product_MY product = new Product_MY();
                product.AddedID = ConfigService.LoginUser == null ? 1 : ConfigService.LoginUser.ID;
                product.AddedTime = DateTime.Now;
                product.AddedType = 3;
                product.ClickCount = 0;
                product.CouponCommand = cmdReg.Match(text).Value;
                product.CouponCount = 100;
                product.CouponEndTime = DateTime.Now.AddDays(2);
                product.CouponLink = couponLinkReg.Match(response).Value;
                product.PPrice = Convert.ToDecimal(priceReg.Match(text).Value);
                product.CouponMoney = product.PPrice - price2;
                product.CouponRemain = 100;
                product.CouponShortLink = shoplink;
                product.CouponStartTime = DateTime.Now;
                product.Desc = "";
                product.DescImgs = "";
                product.IsBig = true;
                product.PCID = cid;
                product.PCommission = 0;
                product.PID = 0;
                product.PImgUrls = imgpath;
                product.PIntro = "";
                product.PlanID = ConfigService.LoginUser == null ? "" : ConfigService.LoginUser.PID;
                product.PLink = product.CouponLink;
                product.PRate = 0;
                product.PSales = 0;
                product.PTitle = titleReg.Match(text).Value;
                product.Remark = "";
                if (string.IsNullOrEmpty(product.PTitle) ||
                    string.IsNullOrWhiteSpace(product.CouponCommand) ||
                    !product.PPrice.HasValue ||
                    !product.CouponMoney.HasValue ||
                    string.IsNullOrWhiteSpace(product.CouponShortLink))
                {
                    return 4;
                }
                IQuery<Product_MY> q = MySqlHelper.context.Query<Product_MY>();
                var pmy = q.Where(p => p.CouponCommand == product.CouponCommand || p.CouponShortLink == product.CouponShortLink).FirstOrDefault();
                if (pmy != null)
                {
                    product.ID = pmy.ID;
                    MySqlHelper.context.Update(product);
                }
                else
                {
                    Product_MY newP = MySqlHelper.context.Insert(product);
                }                
            }
            catch (Exception ex)
            {
                ErrorLog.WriteTextLog("TaobaoAppInput", ex.ToString(), DateTime.Now);
                return 5;
            }
            return 0;
        }

        public static long Insert(Product_MY p)
        {
            Product_MY newP = MySqlHelper.context.Insert(p);
            return newP.ID;
        }

        public static int Update(Product_MY p)
        {
            return MySqlHelper.context.Update(p);
        }

        public static Product_MY GetInfo(long id)
        {
            IQuery<Product_MY> q = MySqlHelper.context.Query<Product_MY>();
            return q.Where(p => p.ID == id).FirstOrDefault();
        }

        public static List<Product_MY> GetList(int addedID,DateTime dtStart,DateTime dtEnd,string tilte="",int pageIndex=1,int pageSize=100)
        {
            IQuery<Product_MY> q = MySqlHelper.context.Query<Product_MY>();
            q = q.Where(p =>
                        p.PTitle != null && p.PTitle != "" &&
                        p.PPrice != null &&
                        p.CouponEndTime != null && p.CouponEndTime > DateTime.Now &&
                        p.PImgUrls != null && p.PImgUrls != ""&&
                        p.AddedTime>dtStart&&p.AddedTime<dtEnd
                    );
            if (addedID > 0)
            {
                q = q.Where(p => p.AddedID == addedID);
            }
            if (!string.IsNullOrWhiteSpace(tilte))
            {
                q = q.Where(p => p.PTitle.Contains(tilte));
            }
            return q.OrderByDesc(p => p.AddedTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
