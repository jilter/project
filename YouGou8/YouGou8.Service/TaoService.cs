using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using YouGou8.Model;

namespace YouGou8.Service
{
    public static class TaoService
    {

        #region 根据商品地址获取商品信息+TaoModel GetItemByUrl(string url)
        /// <summary>
        /// 根据商品地址获取商品信息
        /// </summary>
        /// <param name="url">商品地址</param>
        /// <returns>商品信息</returns>
        public static TaoModel GetItemByUrl(string url)
        {
            Regex taobaoReg = new Regex("item.taobao.com");//淘宝url
            Regex tianmaoReg = new Regex("detail.tmall.com");//天猫
            Regex yaoReg = new Regex("detail.yao.95095.com");//天猫药物
            if (taobaoReg.Match(url).Success)
            {
                return GetInfoByTaobaoUrl(url);
            }
            else if (tianmaoReg.Match(url).Success||yaoReg.Match(url).Success)
            {
                return GetInfoByTmallUrl(url);
            }
            return null;
        }
        #endregion

        #region 根据Url获取相应字符串+string GetStrByUrl(string url)
        /// <summary>
        /// 根据Url获取相应字符串
        /// </summary>
        /// <param name="url">网址</param>
        /// <returns>网页html代码</returns>
        private static string GetStrByUrl(string url, string encodeing)
        {
            string str = "";
            Encoding ecd = string.IsNullOrEmpty(encodeing) ? Encoding.Default : Encoding.GetEncoding(encodeing);
            try
            {
                WebRequest request = HttpWebRequest.Create(url);
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encodeing));
                str = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                response.Close();
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            return str;
        }
        #endregion        

        #region 根据天猫商品地址获取商品信息+taoModel GetInfoByTmallUrl(string TmallUrl)
        /// <summary>
        /// 根据天猫商品地址获取商品信息
        /// </summary>
        ///<param name="TmallUrl">天猫地址</param>
        /// <returns>商品实体信息</returns>
        private static TaoModel GetInfoByTmallUrl(string TmallUrl)
        {
            TaoModel taoModel = new TaoModel();
            string Imgstr, str = "";
            str = GetStrByUrl(TmallUrl, "gb2312");
            Regex myRegex = new Regex("(?<=descUrl\":\").+(?=\",\"fetchDcUrl\")");//指定其正则验证式
            Regex imgaeRegex = new Regex("http://img.+60x60q90.jpg|https://img.+60x60q90.jpg|//img.+60x60q90.jpg");//商品图片
            Regex detailimgRegex = new Regex("https://img\\S+jpg|http://img\\S+jpg");//商品详细图片
            Regex ZkPriceRegex = new Regex("(?<=\"comboPrice\":\").+(?=\",\"defaultPromType\")");//折扣后价格
            Regex PriceRegex = new Regex("(?<=\"reservePrice\":\").+(?=\",\"rootCatId\")");//默认价格
            Regex shopNameRegex = new Regex("(?<=\"sellerNickName\":\").+(?=\",\"spuId\")");//店铺名称
            Regex titleRegex = new Regex("(?<=\"title\":\").+(?=\",\"userId\":\")");
            Regex PriceNowRegex = new Regex("");
            string detailImg = myRegex.Match(str).Value;//从指定内容中匹配字符串
            MatchCollection matchs = imgaeRegex.Matches(str, 0);
            string imgUrl = "";
            foreach (Match mat in matchs)
            {
                string tempmat = mat.Value;
                //tempmat = "http:" + tempmat;
                if (!tempmat.StartsWith("http"))
                {
                    tempmat = "http:" + tempmat;
                }
                tempmat = tempmat.Replace("_60x60q90.jpg", "_300x300.jpg");
                imgUrl += tempmat + "|";
            }
            string price = PriceRegex.Match(str).Value;
            string shopname = HttpUtility.UrlDecode(shopNameRegex.Match(str).Value);
            string title = titleRegex.Match(str).Value;
            //imgUrl = "商品图片："+imgUrl.Substring(0,imgUrl.Length-12);
            Imgstr = GetStrByUrl("http:" + detailImg, "gb2312").Substring(10);
            MatchCollection matches1 = detailimgRegex.Matches(Imgstr, 0);
            string DetailimgUrl = "";
            foreach (Match mat in matches1)
            {
                DetailimgUrl += mat.Value + "|";
            }
            taoModel.ProductName = title;
            taoModel.ShopName = shopname;
            taoModel.ProductPrice = price;
            taoModel.ThumImg = imgUrl;
            taoModel.DetailImg = DetailimgUrl;
            taoModel.TypeName = "天猫";
            return taoModel;
        }
        #endregion

        #region 根据淘宝商品地址获取商品信息+string GetInfoByTaobaoUrl(string TaobaoUrl)
        /// <summary>
        /// 根据淘宝商品地址获取商品信息
        /// </summary>
        /// <param name="TaobaoUrl">淘宝商品地址</param>
        /// <returns>淘宝商品信息</returns>
        private static TaoModel GetInfoByTaobaoUrl(string TaobaoUrl)
        {
            TaoModel taoModel = new TaoModel();
            string Imgstr, str = "";
            str = GetStrByUrl(TaobaoUrl, "gb2312");
            Regex myRegex = new Regex("(?<=location\\.protocol===\'http:\' \\? \').+(?=' :)");//指定其正则验证式
            Regex detailimgRegex = new Regex("https://img\\S+jpg|http://img\\S+jpg");//商品详细图片
            Regex imgaeRegex = new Regex("(?<=auctionImages    : \\[).+(?=])");//商品图片
            Regex PriceRegex = new Regex("(?<=price:).+(?=,)");//默认价格
            Regex shopNameRegex = new Regex("(?<=sellerNick:\").+(?=\")");//店铺名称
            Regex titleRegex = new Regex("(?<=title.).+(?=-淘宝网)");//商品名称
            string detailImg = myRegex.Match(str).Value.Replace("\"", "").Replace("'", "").Replace(":", "").Trim();//从指定内容中匹配字符串
            string imgUrl = imgaeRegex.Match(str).Value;
            MatchCollection matchs = imgaeRegex.Matches(str, 0);
            imgUrl = imgUrl.Replace("\"", "").Replace(",", "_300x300.jpg|").Replace("//", "http://").Trim();
            string price = PriceRegex.Match(str).Value;
            string shopname = HttpUtility.UrlDecode(shopNameRegex.Match(str).Value);
            string title = titleRegex.Match(str).Value;
            //imgUrl = "商品图片："+imgUrl.Substring(0,imgUrl.Length-12);
            Imgstr = GetStrByUrl("http:" + detailImg, "gb2312");
            MatchCollection matches1 = detailimgRegex.Matches(Imgstr, 0);
            string DetailimgUrl = "";
            foreach (Match mat in matches1)
            {
                DetailimgUrl += mat.Value + "|";
            }
            taoModel.ProductName = title;
            taoModel.ShopName = shopname;
            taoModel.ProductPrice = price;
            taoModel.ThumImg = imgUrl;
            taoModel.DetailImg = DetailimgUrl;
            taoModel.TypeName = "淘宝";
            return taoModel;
        }
        #endregion
    }
}
