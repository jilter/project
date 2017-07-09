using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using YouGou8.API.Models;
using YouGou8.Service;

namespace YouGou8.API.Helper
{
    public class ExcelHelper
    {
        #region 匹配方法，新版上线后删除
        public static List<ProductModel> GetProductFromDataTable()
        {
            DataTable dt = ProductService.GetDataFromExcelByConn(HostingEnvironment.MapPath("~/App_Data/Products.xls"));
            List<ProductModel> productList = new List<ProductModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                var ActivityEnd = DateTime.Now.AddDays(1);
                foreach (DataRow item in dt.Rows)
                {
                    if (!String.IsNullOrWhiteSpace(item[13].ToString()))
                    {
                        ActivityEnd = Convert.ToDateTime(item[13]);
                    }

                    if ((ActivityEnd - DateTime.Now).TotalHours > 2)
                    {
                        productList.Add(new ProductModel()
                        {
                            ID = Convert.ToInt64(item[0]),
                            Name = item[1].ToString(),
                            Picture = item[2].ToString(),
                            Link = item[3].ToString(),
                            ShopName = item[4].ToString(),
                            Price = Convert.ToDecimal(item[5]),
                            Sales = Convert.ToInt32(item[6]),
                            NormalPercent = String.IsNullOrWhiteSpace(item[7].ToString()) ? 0 : Convert.ToDecimal(item[7]),
                            NormalComm = String.IsNullOrWhiteSpace(item[8].ToString()) ? 0 : Convert.ToDecimal(item[8]),
                            ActivityStatus = item[9].ToString(),
                            ActivityPercent = Convert.ToDecimal(item[10]),
                            ActivityComm = Convert.ToDecimal(item[11]),
                            ActivityStart = String.IsNullOrWhiteSpace(item[12].ToString()) ? DateTime.Now : Convert.ToDateTime(item[12]),
                            ActivityEnd = ActivityEnd,
                            Seller = item[14].ToString(),
                            GuestShortLink = item[15].ToString(),
                            GuestLink = item[16].ToString(),
                            Command = item[17].ToString(),
                            CouponCount = Convert.ToInt32(item[18]),
                            CouponRemain = Convert.ToInt32(item[19]),
                            CouponDesc = item[20].ToString(),
                            CouponStart = String.IsNullOrWhiteSpace(item[21].ToString()) ? DateTime.Now : Convert.ToDateTime(item[21]),
                            CouponEnd = String.IsNullOrWhiteSpace(item[22].ToString()) ? DateTime.Now : Convert.ToDateTime(item[22]),
                            CouponLink = item[23].ToString(),
                            CouponCommand = item[24].ToString(),
                            CouponShortLink = item[25].ToString()
                        });
                    }
                }
            }
            return productList;
        }

        public static List<FreeModel> GetFreeFromDataTable()
        {
            DataTable dt = ProductService.GetDataFromExcelByConn(HostingEnvironment.MapPath("~/App_Data/free.xls"));
            List<FreeModel> freeList = new List<FreeModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    freeList.Add(new FreeModel()
                    {
                        ID = Convert.ToInt64(item[0]),
                        Name = item[1].ToString(),
                        Link = item[2].ToString(),
                        Picture0 = item[3].ToString(),
                        Picture1 = item[4].ToString(),
                        Picture2 = item[5].ToString(),
                        Picture3 = item[6].ToString(),
                        Picture4 = item[7].ToString(),
                        Count = Convert.ToInt32(item[8]),
                        EndDateTime = Convert.ToDateTime(item[9]),
                        CouponCommand = item[10].ToString()
                    });
                }
            }
            return freeList;
        }
        #endregion
    }
}