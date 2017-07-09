using Chloe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouGou8.Model;

namespace YouGou8.Service
{
    public class FreeProductService
    {
        public static int Delete(int id)
        {
            return MySqlHelper.context.Delete<FreeProduct>(p => p.ID == id);
        }

        public static int Delete()
        {
            return MySqlHelper.context.Delete<FreeProduct>(p => p.ID > 0);
        }

        public static int DeleteDisable()
        {
            return MySqlHelper.context.Delete<FreeProduct>(p => p.Status==false&&p.AddedTime<DateTime.Now.AddDays(-1));
        }

        public static int Add(FreeProduct p)
        {
            FreeProduct newP = MySqlHelper.context.Insert(p);
            return newP.ID;
        }

        public static int Update(FreeProduct p)
        {
            return MySqlHelper.context.Update(p);
        }
        public static FreeProduct GetInfo(int id)
        {
            IQuery<FreeProduct> q = MySqlHelper.context.Query<FreeProduct>();
            return q.Where(p => p.ID == id).FirstOrDefault();
        }

        public static List<FreeProduct> GetList(int pageSize, int pageIndex, out int totalCount)
        {
            IQuery<FreeProduct> q = MySqlHelper.context.Query<FreeProduct>();
            DateTime start = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            q = q.Where(a => a.AddedTime > start).OrderBy(a => a.AddedTime);
            totalCount = q.Count();
            q = q.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return q.ToList();
        }

        public static List<FreeProduct> GetAllList(int pageSize, int pageIndex, out int totalCount)
        {
            IQuery<FreeProduct> q = MySqlHelper.context.Query<FreeProduct>();
            q = q.OrderBy(a => a.AddedTime);
            totalCount = q.Count();
            q = q.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return q.ToList();
        }
    }
}
