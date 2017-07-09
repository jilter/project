
using Chloe.Entity;
/**  版本信息模板在安装目录下，可自行修改。
* Product_QTK.cs
*
* 功 能： N/A
* 类 名： Product_QTK
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/6/8 15:30:55   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace YouGou8.Model
{
    /// <summary>
    /// Product_QTK:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    [TableAttribute("Product_QTK")]
    public partial class Product_QTK
    {
        public Product_QTK()
        { }
        #region Model
        private long _goods_id;
        private string _goods_pic;
        private string _goods_title;
        private int? _goods_cat;
        private decimal? _goods_price;
        private int? _goods_sales;
        private string _goods_introduce;
        private int? _is_tmall;
        private decimal? _commission;
        private int? _commission_type;
        private string _commission_link;
        private int? _coupon_is_check;
        private int? _coupon_type;
        private string _seller_id;
        private string _coupon_id;
        private decimal? _coupon_price;
        private int? _coupon_number;
        private int? _coupon_limit;
        private int? _coupon_over;
        private decimal? _coupon_condition;
        private DateTime? _coupon_start_time;
        private DateTime? _coupon_end_time;
        private DateTime? _addedtime;
        /// <summary>
        /// auto_increment
        /// </summary>
        [Column(IsPrimaryKey = true)]
        [NonAutoIncrement]
        public long goods_id
        {
            set { _goods_id = value; }
            get { return _goods_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string goods_pic
        {
            set { _goods_pic = value; }
            get { return _goods_pic; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string goods_title
        {
            set { _goods_title = value; }
            get { return _goods_title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? goods_cat
        {
            set { _goods_cat = value; }
            get { return _goods_cat; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? goods_price
        {
            set { _goods_price = value; }
            get { return _goods_price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? goods_sales
        {
            set { _goods_sales = value; }
            get { return _goods_sales; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string goods_introduce
        {
            set { _goods_introduce = value; }
            get { return _goods_introduce; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? is_tmall
        {
            set { _is_tmall = value; }
            get { return _is_tmall; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? commission
        {
            set { _commission = value; }
            get { return _commission; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? commission_type
        {
            set { _commission_type = value; }
            get { return _commission_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string commission_link
        {
            set { _commission_link = value; }
            get { return _commission_link; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? coupon_is_check
        {
            set { _coupon_is_check = value; }
            get { return _coupon_is_check; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? coupon_type
        {
            set { _coupon_type = value; }
            get { return _coupon_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string seller_id
        {
            set { _seller_id = value; }
            get { return _seller_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string coupon_id
        {
            set { _coupon_id = value; }
            get { return _coupon_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? coupon_price
        {
            set { _coupon_price = value; }
            get { return _coupon_price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? coupon_number
        {
            set { _coupon_number = value; }
            get { return _coupon_number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? coupon_limit
        {
            set { _coupon_limit = value; }
            get { return _coupon_limit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? coupon_over
        {
            set { _coupon_over = value; }
            get { return _coupon_over; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? coupon_condition
        {
            set { _coupon_condition = value; }
            get { return _coupon_condition; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? coupon_start_time
        {
            set { _coupon_start_time = value; }
            get { return _coupon_start_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? coupon_end_time
        {
            set { _coupon_end_time = value; }
            get { return _coupon_end_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? addedtime
        {
            set { _addedtime = value; }
            get { return _addedtime; }
        }
        #endregion Model

    }
}

