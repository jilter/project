using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouGou8.Model
{
    public class TaoModel
    {
        /// <summary>
        /// 商店名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        public string ProductPrice { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        public string ThumImg { get; set; }

        /// <summary>
        /// 商品详细介绍图片
        /// </summary>
        public string DetailImg { get; set; }

        /// <summary>
        /// 商品类型
        /// </summary>
        public string TypeName { get; set; }

    }
}
