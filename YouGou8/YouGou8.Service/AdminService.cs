using Chloe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouGou8.Model;

namespace YouGou8.Service
{
    public class AdminService
    {
        public static Admin GetInfo(string name,string pwd)
        {
            IQuery<Admin> q = MySqlHelper.context.Query<Admin>();
            Admin admin = q.Where(a => a.UserName.ToLower()==name.ToLower()&&a.PWD==pwd).FirstOrDefault();
            return admin;
        }

        public static int Insert()
        {
            int id = (int)MySqlHelper.context.Insert<Admin>(() => new Admin() { UserName = "test", PWD = "abcd123", Role = 2 });
            return id;
        }

        public static int Update()
        {
            int id=MySqlHelper.context.Update<Admin>(a => a.ID == 1, a => new Admin() { UserName="admin" });
            return id;
        }

        public static int Delete()
        {
            int id = MySqlHelper.context.Delete<Admin>(a => a.ID == 2);
            return id;
        }
    }
}
