using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using Utility;
using YouGou8.Model;
using YouGou8.Service;
using YouGou8.Web.Models;

namespace YouGou8.Web.Helper
{
    public class ExcelHelper
    {
        public static string SaveExcle(HttpPostedFileBase file)
        {
            var root = "~/Upload/Product/";
            var phicyPath = HostingEnvironment.MapPath(root);
            if (!Directory.Exists(phicyPath))
            {
                Directory.CreateDirectory(phicyPath);
            }
            try
            {
                var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + file.FileName.Substring(file.FileName.LastIndexOf('.'));
                file.SaveAs(phicyPath + fileName);
                return phicyPath + fileName;
            }
            catch(Exception ex)
            {
                ErrorLog.WriteTextLog("SaveExcel", ex.ToString(), DateTime.Now);
                return "";
            }
        }

        public static DataTable GetDataFromExcelByConn(string path, bool hasTitle = true)
        {
            try
            {
                string fileType = System.IO.Path.GetExtension(path);
                DataTable dt = new DataTable();
                using (DataSet ds = new DataSet())
                {
                    string strCon = string.Format("Provider=Microsoft.Jet.OLEDB.{0}.0;" +
                                    "Extended Properties=\"Excel {1}.0;HDR={2};IMEX=1;\";" +
                                    "data source={3};",
                                    (fileType == ".xls" ? 4 : 12), (fileType == ".xls" ? 8 : 12), (hasTitle ? "Yes" : "NO"), path);
                    string strCom = " SELECT * FROM [Page1$]";
                    using (OleDbConnection myConn = new OleDbConnection(strCon))
                    {
                        using (OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn))
                        {
                            myConn.Open();
                            myCommand.Fill(ds);
                        }
                    }
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteTextLog("GetDataFromExcelByConn", ex.ToString(), DateTime.Now);
                return null;
            }
        }

        public static List<Product> GetNormalList(DataTable dt,int productType)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }else
            {
                List<Product> pList = new List<Product>();
                foreach(DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrWhiteSpace(dr[0].ToString()))
                    {
                        Product product = new Product();
                        product.PID = Convert.ToInt64(dr[0]);
                        product.Name = dr[1].ToString();
                        product.ImgUrls = dr[2].ToString();
                        product.Link = dr[3].ToString();
                        product.CategoryID = productType;
                        product.Price = Convert.ToDecimal(dr[5]);
                        product.Sales = Convert.ToInt32(dr[6]);
                        product.Percent = Convert.ToDecimal(dr[7]);
                        product.CouponMoney = UtilityHelper.GetMinPriceFromStr(dr[15].ToString());
                        product.Money = (product.Price - product.CouponMoney) * (product.Percent / 100);
                        product.Redpack = UtilityHelper.GetRedPack(product.Price.Value, product.CouponMoney.Value, product.Percent.Value);
                        product.CouponEndTime = string.IsNullOrWhiteSpace(dr[17].ToString()) ? DateTime.Now.AddDays(7) : Convert.ToDateTime(dr[17]).AddDays(1);
                        product.CouponCount = Convert.ToInt32(dr[13]);
                        product.CouponRemain = Convert.ToInt32(dr[14]);
                        product.CouponLink = string.IsNullOrWhiteSpace(dr[20].ToString()) ? dr[10].ToString() : dr[20].ToString();
                        product.CouponCommand = string.IsNullOrWhiteSpace(dr[19].ToString()) ? dr[12].ToString() : dr[19].ToString();
                        product.AddedTime = DateTime.Now;
                        if (product.CouponEndTime > DateTime.Now)
                        {
                            pList.Add(product);
                        }
                    }
                }
                return pList;
            }
            
        }

        public static List<Product> GetHightList(DataTable dt, int productType)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                List<Product> pList = new List<Product>();
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrWhiteSpace(dr[0].ToString()))
                    {
                        Product product = new Product();
                        product.PID = Convert.ToInt64(dr[0]);
                        product.Name = dr[1].ToString();
                        product.ImgUrls = dr[2].ToString();
                        product.Link = dr[3].ToString();
                        product.CategoryID = productType;
                        product.Price = Convert.ToDecimal(dr[5]);
                        product.Sales = Convert.ToInt32(dr[6]);
                        product.Percent = string.IsNullOrWhiteSpace(dr[10].ToString())? Convert.ToDecimal(dr[7]) : Convert.ToDecimal(dr[10]);
                        product.CouponMoney = UtilityHelper.GetMinPriceFromStr(dr[20].ToString());
                        product.Money = (product.Price - product.CouponMoney) * (product.Percent / 100);
                        product.Redpack = UtilityHelper.GetRedPack(product.Price.Value, product.CouponMoney.Value, product.Percent.Value);
                        product.CouponEndTime = string.IsNullOrWhiteSpace(dr[22].ToString()) ? Convert.ToDateTime(dr[13]) : Convert.ToDateTime(dr[22]).AddDays(1);
                        product.CouponCount = Convert.ToInt32(dr[18]);
                        product.CouponRemain = Convert.ToInt32(dr[19]);
                        product.CouponLink = string.IsNullOrWhiteSpace(dr[25].ToString()) ? dr[15].ToString() : dr[25].ToString();
                        product.CouponCommand = string.IsNullOrWhiteSpace(dr[24].ToString()) ? dr[17].ToString() : dr[24].ToString();
                        product.AddedTime = DateTime.Now;
                        if (product.CouponEndTime > DateTime.Now)
                        {
                            pList.Add(product);
                        }
                    }
                }
                return pList;
            }
        }

        public static List<Product> GetExtList(DataTable dt, int productType)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                List<Category> cList = CategoryService.GetList();
                Category matchCategory = new Category();
                List<Product> pList = new List<Product>();
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrWhiteSpace(dr[0].ToString()))
                    {
                        matchCategory = cList.Find(c => c.MappingName.Contains(dr[4].ToString()));

                        Product product = new Product();
                        product.PID = Convert.ToInt64(dr[0]);
                        product.Name = dr[1].ToString();
                        product.ImgUrls = dr[2].ToString();
                        product.Link = dr[3].ToString();
                        product.CategoryID = matchCategory == null || matchCategory.ID == 0 ? 15 : matchCategory.ID;
                        product.Price = Convert.ToDecimal(dr[6]);
                        product.Sales = Convert.ToInt32(dr[7]);
                        product.Percent = Convert.ToDecimal(dr[8]);
                        product.CouponMoney = UtilityHelper.GetMinPriceFromStr(dr[17].ToString());
                        product.Money = (product.Price - product.CouponMoney) * (product.Percent / 100);
                        product.Redpack = UtilityHelper.GetRedPack(product.Price.Value, product.CouponMoney.Value, product.Percent.Value);
                        product.CouponEndTime = string.IsNullOrWhiteSpace(dr[19].ToString()) ? DateTime.Now.AddDays(7) : Convert.ToDateTime(dr[19]).AddDays(1);
                        product.CouponCount = Convert.ToInt32(dr[15]);
                        product.CouponRemain = Convert.ToInt32(dr[16]);
                        product.CouponLink = string.IsNullOrWhiteSpace(dr[21].ToString()) ? dr[5].ToString() : dr[21].ToString();
                        product.CouponCommand = dt.Columns.Count == 23 ? dr[22].ToString() : "";
                        product.AddedTime = DateTime.Now;
                        if (product.CouponEndTime > DateTime.Now)
                        {
                            pList.Add(product);
                        }
                    }
                }
                return pList;
            }
        }

        #region 匹配方法，新版上线后删除
        public static List<ProductModel> GetProductFromDataTable()
        {
            DataTable dt = GetDataFromExcelByConn(HostingEnvironment.MapPath("~/App_Data/Products.xls"));
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
            DataTable dt = GetDataFromExcelByConn(HostingEnvironment.MapPath("~/App_Data/free.xls"));
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