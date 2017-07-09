using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouGou8.Model;

namespace YouGou8.Service
{
    public class CategoryService
    {
        public static List<Category> GetList()
        {
            List<Category> categoryList = new List<Category>();
            categoryList=MySqlHelper.context.Query<Category>().OrderByDesc(c=>c.Sort).ToList();
            return categoryList;
        }
    }
}
