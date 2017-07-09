using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utility;

namespace YouGou8.Service
{
    public class UtilityService
    {
        public static decimal GetMinPriceFromStr(string str)
        {
            decimal min = 0;
            if (str != null && str != string.Empty)
            {
                str = Regex.Replace(str, @"[^\d.\d]", " ");
                str = str.Trim(' ');
                str = Regex.Replace(str, @"\s+", " ");
                if (!string.IsNullOrWhiteSpace(str))
                {
                    string[] nums = str.Split(' ');
                    decimal.TryParse(nums[0], out min);
                    if (nums.Length > 1)
                    {
                        decimal p = 0;
                        for (int i = 1; i < nums.Length; i++)
                        {
                            decimal.TryParse(nums[i], out p);
                            min = min > p ? p : min;
                        }
                    }
                }
            }
            return min;
        }

        public static decimal GetRedPack(decimal price, decimal coupon, decimal percent)
        {
            int max = ConfigService.MaxRedPack;
            decimal p = Math.Floor((price - coupon) * (percent / 100));
            return p > max ? max : p;
        }

        public static string GetYoumaibaData(string path)
        {
            string responseStr = string.Empty;
            Uri url = new Uri(path);
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                request.Headers.Add("Accept-Encoding", "gzip, deflate, sdch");
                request.Accept = "application/json, text/plain, */*";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
                request.KeepAlive = true;
                request.Host = "www.youmaiba.com";
                request.Timeout = 30000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string contentype = response.Headers["Content-Type"];
                Regex regex = new Regex("charset\\s*=\\s*[\\W]?\\s*([\\w-]+)", RegexOptions.IgnoreCase);
                if (response.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压
                {
                    using (Stream streamReceive = response.GetResponseStream())
                    {
                        using (var zipStream = new GZipStream(streamReceive, CompressionMode.Decompress))
                        {
                            //匹配编码格式
                            if (regex.IsMatch(contentype))
                            {
                                Encoding ending = Encoding.GetEncoding(regex.Match(contentype).Groups[1].Value.Trim());
                                using (StreamReader sr = new StreamReader(zipStream, ending))
                                {
                                    responseStr = sr.ReadToEnd();
                                }
                            }
                            else
                            {
                                using (StreamReader sr = new StreamReader(zipStream, Encoding.UTF8))
                                {
                                    responseStr = sr.ReadToEnd();
                                }
                            }
                        }
                    }
                }
                else
                {
                    using (Stream streamReceive = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(streamReceive, Encoding.Default))
                        {
                            responseStr = sr.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                responseStr = "";
                ErrorLog.WriteTextLog("", e.ToString(), DateTime.Now);
            }
            Regex reg = new Regex("\"title\":[.*]");
            responseStr = responseStr.Replace("\\\"", "").Replace("\\[","").Replace("\\}", "").Replace("\\{", "").Replace("\\}", "");
            responseStr = Regex.Unescape(responseStr).Replace("\n", "").Replace("\r", "").Replace("\r\n", "");
            responseStr = responseStr.Replace("<em class=\"hl\">", "").Replace("</em>", "");
            return Regex.Replace(responseStr, "\"title\":\\[(\"[^\\]]+\")\\]", "\"title\":$1");
        }
    }
}
