using Chloe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using Utility;
using YouGou8.Model;
using System.Web.Hosting;
using System.IO;

namespace YouGou8.Service
{
    public class ProductService
    {
        public static int Delete(long id)
        {
            return MySqlHelper.context.Delete<Product_MY>(p => p.ID == id);
        }

        public static int Delete()
        {
            return MySqlHelper.context.Delete<Product_MY>(p => p.ID > 0);
        }

        public static int DeleteDisable()
        {
            return MySqlHelper.context.Delete<Product_MY>(p => p.CouponEndTime<DateTime.Now);
        }

        public static long Add(Product_MY p)
        {
            Product_MY newP = MySqlHelper.context.Insert(p);
            return newP.ID;
        }

        public static int Update(Product_MY p)
        {
            return MySqlHelper.context.Update(p);
        }

        public static int UpdateBig(long id,bool isbig)
        {
            return MySqlHelper.context.Update<Product_MY>(a => a.ID == id, a => new Product_MY() { IsBig=isbig });
        }
        public static Product_MY GetInfo(long id)
        {
            IQuery<Product_MY> q = MySqlHelper.context.Query<Product_MY>();
            return q.Where(p => p.ID == id).FirstOrDefault();
        }

        public static List<Product_MY> GetList(string types,string name,int pageSize,int pageIndex,out int totalCount)
        {
            IQuery<Product_MY> q = MySqlHelper.context.Query<Product_MY>();
            q = q.Where(p => 
                        p.PTitle!=null && p.PTitle != "" &&
                        p.PPrice!=null &&
                        p.CouponCommand !=null && p.CouponCommand!="" &&
                        p.CouponEndTime !=null && p.CouponEndTime > DateTime.Now &&
                        p.PImgUrls!=null&&p.PImgUrls!=""
                    );
            if (!string.IsNullOrWhiteSpace(types) && types != "0")
            {
                List<string> typeList = types.Split(',').ToList();
                if (typeList.Count == 1)
                {
                    int c1 = Convert.ToInt32(typeList[0]);
                    q = q.Where(p => p.PCID==c1);
                }
                else if (typeList.Count == 2)
                {
                    int c1 = Convert.ToInt32(typeList[0]);
                    int c2 = Convert.ToInt32(typeList[1]);
                    q = q.Where(p => p.PCID == c1 || p.PCID == c2);
                }
                else if (typeList.Count == 3)
                {
                    int c1 = Convert.ToInt32(typeList[0]);
                    int c2 = Convert.ToInt32(typeList[1]);
                    int c3 = Convert.ToInt32(typeList[2]);
                    q = q.Where(p => p.PCID == c1 || p.PCID == c2 || p.PCID == c3);
                }
                else if (typeList.Count == 4)
                {
                    int c1 = Convert.ToInt32(typeList[0]);
                    int c2 = Convert.ToInt32(typeList[1]);
                    int c3 = Convert.ToInt32(typeList[2]);
                    int c4 = Convert.ToInt32(typeList[3]);
                    q = q.Where(p => p.PCID == c1 || p.PCID == c2 || p.PCID == c3 || p.PCID == c4);
                }
                else if (typeList.Count == 5)
                {
                    int c1 = Convert.ToInt32(typeList[0]);
                    int c2 = Convert.ToInt32(typeList[1]);
                    int c3 = Convert.ToInt32(typeList[2]);
                    int c4 = Convert.ToInt32(typeList[3]);
                    int c5 = Convert.ToInt32(typeList[4]);
                    q = q.Where(p => p.PCID == c1 || p.PCID == c2 || p.PCID == c3 || p.PCID == c4 || p.PCID == c5);
                }
            }
            if(!string.IsNullOrWhiteSpace(name))
            {
                q=q.Where(p => p.PTitle.Contains(name));
            }
            totalCount = q.Count();
            return q.OrderByDesc(p => p.CouponMoney).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public static List<Product_MY> GetListBig(int pageSize, int pageIndex, out int totalCount)
        {
            IQuery<Product_MY> q = MySqlHelper.context.Query<Product_MY>();
            q = q.Where(p =>
                        p.PTitle != null && p.PTitle != "" &&
                        p.PPrice != null &&
                        p.CouponCommand != null && p.CouponCommand != "" &&
                        p.CouponEndTime != null && p.CouponEndTime > DateTime.Now &&
                        p.PImgUrls != null && p.PImgUrls != "" &&
                        p.IsBig==true
                    );
            totalCount = q.Count();
            return q.OrderByDesc(p => p.CouponMoney).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public static List<Product_MY> GetListPrice(decimal price,int pageSize, int pageIndex, out int totalCount)
        {
            IQuery<Product_MY> q = MySqlHelper.context.Query<Product_MY>();
            q = q.Where(p =>
                        p.PTitle != null && p.PTitle != "" &&
                        p.PPrice != null && (p.PPrice-p.CouponMoney)<price &&
                        p.CouponCommand != null && p.CouponCommand != "" &&
                        p.CouponEndTime != null && p.CouponEndTime > DateTime.Now &&
                        p.PImgUrls != null && p.PImgUrls != ""
                    );            
            totalCount = q.Count();
            return q.OrderByDesc(p => p.CouponMoney).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public static List<Product_MY> GetAllList(int categoryID,string name,int pageSize, int pageIndex, out int totalCount)
        {
            IQuery<Product_MY> q = MySqlHelper.context.Query<Product_MY>();
            if (categoryID != 0)
            {
                q = q.Where(p => p.PCID == categoryID);
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                q = q.Where(p => p.PTitle.Contains(name));
            }
            q = q.OrderByDesc(a => a.AddedTime);
            totalCount = q.Count();
            q = q.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            
            return q.ToList();
        }

        public static List<Product_MY> GetTopByClick()
        {
            IQuery<Product_MY> q = MySqlHelper.context.Query<Product_MY>();
            q = q.OrderByDesc(a => a.ClickCount);
            q = q.Skip(0).Take(10);
            return q.ToList();
        }

        public static string SaveExcle(HttpPostedFileBase file,string root= "~/Upload/Product/")
        {
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
            catch (Exception ex)
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
                    string strCom = " SELECT * FROM [{0}]";
                    using (OleDbConnection myConn = new OleDbConnection(strCon))
                    {
                        myConn.Open();
                        DataTable sheetNames = myConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                        string tableName = sheetNames.Rows[0][2].ToString();
                        strCom = string.Format(strCom, tableName);
                        using (OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn))
                        {
                            myCommand.Fill(ds);
                        }
                        myConn.Close();
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

        public static List<Product_MY> GetNormalList(DataTable dt, int productType)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                List<Product_MY> pList = new List<Product_MY>();
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrWhiteSpace(dr[0].ToString()))
                    {
                        Product_MY product = new Product_MY();
                        product.AddedID = ConfigService.LoginUser == null ? 1 : ConfigService.LoginUser.ID;
                        product.PlanID = ConfigService.LoginUser == null ? "0" : ConfigService.LoginUser.PID;
                        product.AddedType = 3;
                        product.PID = Convert.ToInt64(dr[0]);
                        product.PTitle = dr[1].ToString();
                        product.PImgUrls = dr[2].ToString()+ "_300x300.jpg";
                        product.PLink = dr[3].ToString();
                        product.PCID = productType;
                        product.PPrice = Convert.ToDecimal(dr[5]);
                        product.PSales = Convert.ToInt32(dr[6]);
                        product.PRate = Convert.ToDecimal(dr[7]);
                        product.CouponMoney = UtilityService.GetMinPriceFromStr(dr[15].ToString());
                        product.PCommission = (product.PPrice - product.CouponMoney) * (product.PRate / 100);
                        product.PRedPack = UtilityService.GetRedPack(product.PPrice.Value, product.CouponMoney.Value, product.PRate.Value);
                        product.CouponEndTime = string.IsNullOrWhiteSpace(dr[17].ToString()) ? DateTime.Now.AddDays(7) : Convert.ToDateTime(dr[17]).AddDays(1);
                        product.CouponCount = Convert.ToInt32(dr[13]);
                        product.CouponRemain = Convert.ToInt32(dr[14]);
                        product.CouponShortLink = string.IsNullOrWhiteSpace(dr[20].ToString()) ? dr[10].ToString() : dr[20].ToString();
                        product.CouponLink = string.IsNullOrWhiteSpace(dr[18].ToString()) ? dr[11].ToString() : dr[18].ToString();
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

        public static List<Product_MY> GetHightList(DataTable dt, int productType)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                List<Product_MY> pList = new List<Product_MY>();
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrWhiteSpace(dr[0].ToString()))
                    {
                        Product_MY product = new Product_MY();
                        product.AddedID = ConfigService.LoginUser == null ? 1 : ConfigService.LoginUser.ID;
                        product.PlanID = ConfigService.LoginUser == null ? "0" : ConfigService.LoginUser.PID;
                        product.AddedType = 3;
                        product.PID = Convert.ToInt64(dr[0]);
                        product.PTitle = dr[1].ToString();
                        product.PImgUrls = dr[2].ToString() + "_300x300.jpg";
                        product.PLink = dr[3].ToString();
                        product.PCID = productType;
                        product.PPrice = Convert.ToDecimal(dr[5]);
                        product.PSales = Convert.ToInt32(dr[6]);
                        product.PRate = string.IsNullOrWhiteSpace(dr[10].ToString()) ? Convert.ToDecimal(dr[7]) : Convert.ToDecimal(dr[10]);
                        product.CouponMoney = UtilityService.GetMinPriceFromStr(dr[20].ToString());
                        product.PCommission = (product.PPrice - product.CouponMoney) * (product.PRate / 100);
                        product.PRedPack = UtilityService.GetRedPack(product.PPrice.Value, product.CouponMoney.Value, product.PRate.Value);
                        product.CouponEndTime = string.IsNullOrWhiteSpace(dr[22].ToString()) ? Convert.ToDateTime(dr[13]) : Convert.ToDateTime(dr[22]).AddDays(1);
                        product.CouponCount = Convert.ToInt32(dr[18]);
                        product.CouponRemain = Convert.ToInt32(dr[19]);
                        product.CouponShortLink = string.IsNullOrWhiteSpace(dr[25].ToString()) ? dr[15].ToString() : dr[25].ToString();
                        product.CouponLink = string.IsNullOrWhiteSpace(dr[23].ToString()) ? dr[16].ToString() : dr[23].ToString();
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

        public static List<Product_MY> GetExtList(DataTable dt, int productType)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                List<Category> cList = CategoryService.GetList();
                Category matchCategory = new Category();
                List<Product_MY> pList = new List<Product_MY>();
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrWhiteSpace(dr[0].ToString()))
                    {
                        matchCategory = cList.Find(c => c.MappingName.Contains(dr[4].ToString()));

                        Product_MY product = new Product_MY();
                        product.AddedID = ConfigService.LoginUser == null ? 1 : ConfigService.LoginUser.ID;
                        product.PlanID = ConfigService.LoginUser == null ? "0" : ConfigService.LoginUser.PID;
                        product.AddedType = 3;
                        product.PID = Convert.ToInt64(dr[0]);
                        product.PTitle = dr[1].ToString();
                        product.PImgUrls = dr[2].ToString() + "_300x300.jpg";
                        product.PLink = dr[3].ToString();
                        product.PCID = matchCategory == null || matchCategory.ID == 0 ? 15 : matchCategory.ID;
                        product.PPrice = Convert.ToDecimal(dr[6]);
                        product.PSales = Convert.ToInt32(dr[7]);
                        product.PRate = Convert.ToDecimal(dr[8]);
                        product.CouponMoney = UtilityService.GetMinPriceFromStr(dr[17].ToString());
                        product.PCommission = (product.PPrice - product.CouponMoney) * (product.PRate / 100);
                        product.PRedPack = UtilityService.GetRedPack(product.PPrice.Value, product.CouponMoney.Value, product.PRate.Value);
                        product.CouponEndTime = string.IsNullOrWhiteSpace(dr[19].ToString()) ? DateTime.Now.AddDays(7) : Convert.ToDateTime(dr[19]).AddDays(1);
                        product.CouponCount = Convert.ToInt32(dr[15]);
                        product.CouponRemain = Convert.ToInt32(dr[16]);
                        product.CouponShortLink = string.IsNullOrWhiteSpace(dr[21].ToString()) ? dr[5].ToString() : dr[21].ToString();
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

    }
}
