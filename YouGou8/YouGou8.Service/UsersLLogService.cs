using Chloe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouGou8.Model;

namespace YouGou8.Service
{
    public class UsersLLogService
    {
        public static int GetTodayLogin()
        {
            IQuery<UsersLLog> q = MySqlHelper.context.Query<UsersLLog>();
            q.Where(p => p.LoginTime > Convert.ToDateTime(DateTime.Now.ToShortDateString()) && p.LoginTime < Convert.ToDateTime(DateTime.Now.AddDays(1).ToShortDateString()));
            return q.Count();
        }

        public static void Insert(long userid)
        {
            UsersLLog log = new UsersLLog();
            log.LoginTime = DateTime.Now;
            log.UserID = userid;
            MySqlHelper.context.Insert(log);
        }
    }
}
