using Chloe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouGou8.Model;

namespace YouGou8.Service
{
    public class UsersBLogService
    {
        public static int GetTodayBrowse()
        {
            IQuery<UsersBLog> q = MySqlHelper.context.Query<UsersBLog>();
            DateTime start = DateTime.Now.Date;
            DateTime end = DateTime.Now.AddDays(1).Date;
            q = q.Where(p => p.BrowseTime > start && p.BrowseTime < end);
            return q.Count();
        }

        public static void Insert(UsersBLog log)
        {
             MySqlHelper.context.Insert(log);
        }
    }
}
