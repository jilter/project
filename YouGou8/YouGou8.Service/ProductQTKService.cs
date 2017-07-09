using Chloe;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using YouGou8.Model;

namespace YouGou8.Service
{
    public class ProductQTKService
    {
        public static Product_QTK GetInfo(Int64 goods_id)
        {
            IQuery<Product_QTK> q = MySqlHelper.context.Query<Product_QTK>();
            return q.Where(p => p.goods_id == goods_id).FirstOrDefault();
        }

        public static DateTime? GetMaxTime()
        {
            IQuery<Product_QTK> q = MySqlHelper.context.Query<Product_QTK>();
            return q.Max(p => p.addedtime);
        }

        public static int Delete(Int64 goods_id)
        {
            return MySqlHelper.context.Delete<Product_QTK>(p => p.goods_id == goods_id);
        }

        public static long Add(Product_QTK p)
        {
            Product_QTK newP = MySqlHelper.context.Insert(p);
            return newP.goods_id;
        }

        public static int Update(Product_QTK p)
        {
            return MySqlHelper.context.Update(p);
        }

        public static int GetQTKData(out int success,out int error)
        {
            DateTime dtStart = DateTime.Now;
            string appkey = ConfigService.QTKAppKey;
            int maxPage = 120;
            long result = 0;
            success = 0;
            error = 0;
            string url = "http://openapi.qingtaoke.com/qingsoulist?sort=2&page={0}&app_key="+appkey+"&v=1.0";
            string response = "";
            List<Product_QTK> pList = new List<Product_QTK>();
            Product_QTK oldProduct = new Product_QTK();
            JObject first;
            JObject data;

            for (int i = 1; i <= maxPage; i++)
            {
                response = RequestUtility.GetData(string.Format(url, i));
                if (!string.IsNullOrEmpty(response))
                {
                    first = JObject.Parse(response);
                    if (first["er_code"]!=null && Convert.ToInt32(first["er_code"]) == (int)Product_QTK_Code.Success)
                    {
                        data = JObject.Parse(first["data"].ToString());
                        pList = JsonUtility.JsonDeserialize<Product_QTK>(data["list"].ToString());
                        foreach (Product_QTK item in pList)
                        {
                            result = 0;
                            if (item != null && item.goods_id > 0)
                            {
                                item.addedtime = DateTime.Now;
                                oldProduct = GetInfo(item.goods_id);
                                if (oldProduct != null)
                                {
                                    result = Update(item);
                                }
                                else
                                {
                                    result = Add(item);
                                }

                                if (result > 0)
                                {
                                    success++;
                                }
                                else
                                {
                                    error++;
                                }
                            }
                        }

                    }
                }
            }
            return 0;
        }
    }
}
