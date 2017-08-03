using Chloe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using YouGou8.Model;

namespace YouGou8.Service
{
    public class DarenProService
    {
        public static int Add(DarenPro p)
        {
            DarenPro newP = MySqlHelper.context.Insert(p);
            return newP.ID;
        }

        public static DarenPro GetInfo(long id)
        {
            IQuery<DarenPro> q = MySqlHelper.context.Query<DarenPro>();
            return q.Where(p => p.ItemID == id).FirstOrDefault();
        }

        public static int Import(DataTable dt,out List<DarenPro> successList,out List<DarenPro> failList,out List<DarenPro> repeatList)
        {
            successList = new List<DarenPro>();
            failList = new List<DarenPro>();
            repeatList = new List<DarenPro>();
            if (dt == null || dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrWhiteSpace(dr[0].ToString()))
                    {
                        DarenPro p = new DarenPro();
                        p.Link = dr[0].ToString();
                        p.AddedDate = DateTime.Now;
                        try
                        {
                            string itemid = HttpUtility.ParseQueryString(dr[0].ToString().Split('?')[1]).Get("id");
                            p.ItemID = Convert.ToInt64(itemid);
                            if (GetInfo(p.ItemID) != null)
                            {
                                repeatList.Add(p);
                            }else
                            {
                                p.ID=Add(p);
                                if (p.ID > 0)
                                {
                                    successList.Add(p);
                                }else
                                {
                                    failList.Add(p);
                                }
                            }
                        }
                        catch
                        {
                            failList.Add(p);
                        }                     
                    }
                }
                return 1;
            }

        }
    }
}
