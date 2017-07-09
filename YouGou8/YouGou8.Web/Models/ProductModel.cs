using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YouGou8.Web.Models
{
    public class ProductModel
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 商品主图
        /// </summary>
        public string Picture { get; set; }
        /// <summary>
        /// 商品详情页链接地址
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// 商品价格(单位：元)
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 商品月销量
        /// </summary>
        public int Sales { get; set; }
        /// <summary>
        /// 通用收入比率(%)
        /// </summary>
        public decimal NormalPercent { get; set; }
        /// <summary>
        /// 通用佣金
        /// </summary>
        public decimal NormalComm { get; set; }
        /// <summary>
        /// 活动状态
        /// </summary>
        public string ActivityStatus { get; set; }
        /// <summary>
        /// 活动收入比率(%)
        /// </summary>
        public decimal ActivityPercent { get; set; }
        /// <summary>
        /// 活动佣金
        /// </summary>
        public decimal ActivityComm { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime ActivityStart { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime ActivityEnd { get; set; }
        /// <summary>
        /// 卖家旺旺
        /// </summary>
        public string Seller { get; set; }
        /// <summary>
        /// 淘宝客短链接(300天内有效)
        /// </summary>
        public string GuestShortLink { get; set; }
        /// <summary>
        /// 淘宝客链接
        /// </summary>
        public string GuestLink { get; set; }
        /// <summary>
        /// 淘口令(300天内有效)
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// 优惠券总量
        /// </summary>
        public int CouponCount { get; set; }
        /// <summary>
        /// 优惠券剩余量
        /// </summary>
        public int CouponRemain { get; set; }
        /// <summary>
        /// 优惠券面额
        /// </summary>
        public string CouponDesc { get; set; }
        /// <summary>
        /// 优惠券开始时间
        /// </summary>
        public DateTime CouponStart { get; set; }
        /// <summary>
        /// 优惠券结束时间
        /// </summary>
        public DateTime CouponEnd { get; set; }
        /// <summary>
        /// 优惠券链接
        /// </summary>
        public string CouponLink { get; set; }
        /// <summary>
        /// 优惠券淘口令(300天内有效)
        /// </summary>
        public string CouponCommand { get; set; }
        /// <summary>
        /// 优惠券短链接(300天内有效)
        /// </summary>
        public string CouponShortLink { get; set; }

    }
}