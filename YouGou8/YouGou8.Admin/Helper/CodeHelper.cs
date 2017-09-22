using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using ThoughtWorks.QRCode.Codec;

namespace YouGou8.Admin.Helper
{
    public class CodeHelper
    {
        private static string downloadPath = HostingEnvironment.MapPath("~/Download/Temp/");
        private static string savePath = HostingEnvironment.MapPath("~/assets/img/qrcode/");

        #region 保存web图片到本地
        /// <summary>
        /// 保存web图片到本地
        /// </summary>
        /// <param name="imgUrl">web图片路径</param>
        /// <returns></returns>
        public static string SaveImageFromWeb(string imgUrl)
        {
            if (!Directory.Exists(downloadPath))
            {
                Directory.CreateDirectory(downloadPath);
            }
            string imgName = imgUrl.ToString().Substring(imgUrl.ToString().LastIndexOf("/") + 1);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imgUrl);
            request.UserAgent = "Mozilla/6.0 (MSIE 6.0; Windows NT 5.1; Natas.Robot)";
            request.Timeout = 3000;

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();

            if (response.ContentType.ToLower().StartsWith("image/"))
            {
                byte[] arrayByte = new byte[1024];
                int imgLong = (int)response.ContentLength;
                int l = 0;

                if(File.Exists(downloadPath + imgName))
                {
                    File.Delete(downloadPath + imgName);
                }

                FileStream fso = new FileStream(downloadPath + imgName, FileMode.Create);
                while (l < imgLong)
                {
                    int i = stream.Read(arrayByte, 0, 1024);
                    fso.Write(arrayByte, 0, i);
                    l += i;
                }

                fso.Close();
                stream.Close();
                response.Close();

                return downloadPath + imgName;
            }
            else
            {
                return "";
            }
        }

        public static Bitmap GetImageFromWeb(string imgUrl)
        {
            Bitmap img = null;
            HttpWebRequest req;
            HttpWebResponse res = null;
            System.Uri httpUrl = new System.Uri(imgUrl);
            req = (HttpWebRequest)(WebRequest.Create(httpUrl));
            req.Timeout = 10000; //设置超时值10秒
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0";
            req.Accept = "text/html, application/xhtml+xml, */*";
            req.Method = "GET";
            res = (HttpWebResponse)(req.GetResponse());
            img = new Bitmap(res.GetResponseStream());//获取图片流                 
            return img;
        }
        #endregion

        #region 生成二维码
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="strData">要生成的文字或者数字，支持中文。如： "4408810820 深圳－广州" 或者：4444444444</param>
        /// <param name="qrEncoding">三种尺寸：BYTE ，ALPHA_NUMERIC，NUMERIC</param>
        /// <param name="level">错误效验、错误更正(有4个等级)：L M Q H</param>
        /// <param name="version">版本：如 8</param>
        /// <param name="scale">比例：如 4</param>
        /// <returns></returns>
        public static string CreateQRCode(string strData, string qrEncoding, string level, int version, int scale)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            string encoding = qrEncoding;
            switch (encoding)
            {
                case "Byte":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;
                case "AlphaNumeric":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                    break;
                case "Numeric":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                    break;
                default:
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;
            }

            qrCodeEncoder.QRCodeScale = scale;
            qrCodeEncoder.QRCodeVersion = version;
            switch (level)
            {
                case "L":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                    break;
                case "M":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                    break;
                case "Q":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                    break;
                default:
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                    break;
            }
            //文字生成图片
            Image image = qrCodeEncoder.Encode(strData);
            string filename = DateTime.Now.ToString("yyyymmddhhmmssfff").ToString() + ".jpg";
            FileStream fs = new FileStream(downloadPath+filename, FileMode.OpenOrCreate, FileAccess.Write);
            image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
            fs.Close();
            image.Dispose();
            return downloadPath + filename;
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="strData">要生成的文字或者数字，支持中文。如： "4408810820 深圳－广州" 或者：4444444444</param>
        /// <param name="qrEncoding">三种尺寸：BYTE ，ALPHA_NUMERIC，NUMERIC</param>
        /// <param name="level">错误效验、错误更正(有4个等级)：L M Q H</param>
        /// <param name="version">版本：如 8</param>
        /// <param name="scale">比例：如 4</param>
        /// <returns></returns>
        public static Bitmap GetQRCode(string strData, string qrEncoding, string level, int version, int scale)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            string encoding = qrEncoding;
            switch (encoding)
            {
                case "Byte":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;
                case "AlphaNumeric":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                    break;
                case "Numeric":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                    break;
                default:
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;
            }

            qrCodeEncoder.QRCodeScale = scale;
            qrCodeEncoder.QRCodeVersion = version;
            switch (level)
            {
                case "L":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                    break;
                case "M":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                    break;
                case "Q":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                    break;
                default:
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                    break;
            }
            return qrCodeEncoder.Encode(strData);
        }

        #endregion

        public static string CombinImage(string imgUrl,long id,string title,decimal price,decimal coupon,int sales)
        {
            string httpUrl = "/assets/img/qrcode/";

            int initialWidth = 322, initialHeight = 450;
            Bitmap theBitmap = new Bitmap(initialWidth, initialHeight);
            Graphics theGraphics = Graphics.FromImage(theBitmap);
            theGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            theGraphics.Clear(Color.FromArgb(255, 255, 255));
            theGraphics.DrawRectangle(new Pen(Color.FromArgb(227, 227, 227), 1), 1, 1, initialWidth - 2,initialHeight - 2);

            string FontType = "微软雅黑";
            Font theFont = new Font(FontType, 10.0f, FontStyle.Regular);
            Font thePriceFont = new Font(FontType, 10.0f, FontStyle.Bold);
            Brush theBrush = new SolidBrush(Color.FromArgb(51, 51, 51));
            Brush thePriceBrush = new SolidBrush(Color.FromArgb(255, 48, 48)); //填充的颜色

            Bitmap imgProduct = GetImageFromWeb(imgUrl);
            theGraphics.DrawImage(imgProduct,0,0,320,320);
            Bitmap imgQRCode = GetQRCode("http://yougou8.com.cn/wechat/p" + id, "Byte", "H", 8, 2);
            theGraphics.DrawImage(imgQRCode, 210, 330, 100, 100);
            theGraphics.DrawString("长按识别二维码", new Font(FontType, 8.0f, FontStyle.Regular), thePriceBrush, 212, 432);
            if (title.Length <= 12)
            {
                theGraphics.DrawString(title, theFont, theBrush, 5, 325);
            }
            else 
            {
                theGraphics.DrawString(title.Substring(0, 12), theFont, theBrush, 5, 325);
                theGraphics.DrawString(title.Substring(12), theFont, theBrush, 5, 345);
            }
            theGraphics.DrawString("现价:"+price, theFont, theBrush, 5, 375);
            theGraphics.DrawString("销量:" + sales, theFont, theBrush, 105, 375);
            theGraphics.DrawString("优惠券:" + coupon, thePriceFont, thePriceBrush, 5, 405);
            theGraphics.DrawString("券后价:" + (price-coupon), thePriceFont, thePriceBrush, 105, 405);

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            string filename = DateTime.Now.ToString("yyyymmddhhmmssfff").ToString() + ".jpg";
            FileStream fs = new FileStream(savePath + filename, FileMode.OpenOrCreate, FileAccess.Write);
            theBitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            fs.Close();
            theBitmap.Dispose();

            return httpUrl+filename;
        }
    }


}