using Chloe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouGou8.Model;

namespace YouGou8.Service
{
    public class UsersService
    {
        public static Users GetInfo(long id)
        {
            IQuery<Users> q = MySqlHelper.context.Query<Users>();
            return q.Where(p => p.ID == id).FirstOrDefault();
        }

        public static Users GetInfo(string nickname,string avatarUrl)
        {
            IQuery<Users> q = MySqlHelper.context.Query<Users>();
            Users user = q.Where(ur =>ur.NickName==nickname&& ur.AvatarUrl == avatarUrl).FirstOrDefault();
            if (user == null)
            {
                user = q.Where(ur => ur.AvatarUrl == avatarUrl).FirstOrDefault();
            }
            return user;
        }

        public static int Delete(long id)
        {
            return MySqlHelper.context.Delete<Users>(p => p.ID == id);
        }

        public static int Update(Users p)
        {
            return MySqlHelper.context.Update(p);
        }

        public static long Insert(Users user)
        {
            Users newP = MySqlHelper.context.Insert(user);
            return newP.ID;
        }

        public static List<Users> GetAllList(string name, int pageSize, int pageIndex, out int totalCount)
        {
            IQuery<Users> q = MySqlHelper.context.Query<Users>();
            if (!string.IsNullOrWhiteSpace(name))
            {
                q = q.Where(p => p.WeChat.Contains(name)||p.NickName.Contains(name));
            }
            q = q.OrderBy(a => a.LastAccessTime);
            totalCount = q.Count();
            q = q.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return q.ToList();
        }

        public static int GetTodayUser()
        {
            IQuery<Users> q = MySqlHelper.context.Query<Users>();
            DateTime start = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            DateTime end = Convert.ToDateTime(DateTime.Now.AddDays(1).ToShortDateString());
            q = q.Where(p => p.LastAccessTime > start && p.LastAccessTime < end);
            return q.Count();
        }
    }
}
