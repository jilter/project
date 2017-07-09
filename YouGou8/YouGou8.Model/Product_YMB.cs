/**  版本信息模板在安装目录下，可自行修改。
* Product_YMB.cs
*
* 功 能： N/A
* 类 名： Product_YMB
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/6/9 15:44:23   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace YouGou8.Model
{
	/// <summary>
	/// Product_YMB:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Product_YMB
	{
		public Product_YMB()
		{}
		#region Model
		private string _id;
		private string _no;
		private decimal? _amount;
		private string _hid;
		private int? _tk;
		private int? _status;
		private int? _p_status;
		private string _url;
		private string _link;
		private string _title;
		private string _maill_type;
		private string _mall_pid;
		private int? _history_sales;
		private int? _history_30d_sales;
		private int? _totalsellcount;
		private bool _plan_is_auto;
		private bool _plan_type;
		private bool _is_promise;
		private bool _plan_is_que;
		private string _image_path;
		private string _image_path_300;
		private string _front_image_path;
		private string _back_image_path;
		private int? _quantity;
		private decimal? _redpack_price;
		private int? _redpack_quantity;
		private long? _redpack_id;
		private string _arrearagefee;
		private string _hot_type;
		private int? _published;
		private long? _publishs;
		private string _h_expired;
		private string _h_created;
		private decimal? _service_price;
		private string _commission;
		private string _commission_price;
		private string _price;
		private string _sell_price;
		private string _coupon_url;
		private string _coupon_price;
		private decimal? _coupon_spare;
		private decimal? _coupon_amount;
		private string _coupon_expired;
		private int? _coupon_spare_day;
		private string _created;
		private string _updated;
		private int? _online;
		private string _reason;
		private int? _tk_join;
		private string _zfb_password;
		private int? _views;
		private string _intro;
		private bool _added= false;
		private DateTime? _addedtime;
		/// <summary>
		/// 
		/// </summary>
		public string id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string no
		{
			set{ _no=value;}
			get{return _no;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? amount
		{
			set{ _amount=value;}
			get{return _amount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string hid
		{
			set{ _hid=value;}
			get{return _hid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? tk
		{
			set{ _tk=value;}
			get{return _tk;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? p_status
		{
			set{ _p_status=value;}
			get{return _p_status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string url
		{
			set{ _url=value;}
			get{return _url;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string link
		{
			set{ _link=value;}
			get{return _link;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string maill_type
		{
			set{ _maill_type=value;}
			get{return _maill_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string mall_pid
		{
			set{ _mall_pid=value;}
			get{return _mall_pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? history_sales
		{
			set{ _history_sales=value;}
			get{return _history_sales;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? history_30d_sales
		{
			set{ _history_30d_sales=value;}
			get{return _history_30d_sales;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? totalSellCount
		{
			set{ _totalsellcount=value;}
			get{return _totalsellcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool plan_is_auto
		{
			set{ _plan_is_auto=value;}
			get{return _plan_is_auto;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool plan_type
		{
			set{ _plan_type=value;}
			get{return _plan_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool is_promise
		{
			set{ _is_promise=value;}
			get{return _is_promise;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool plan_is_que
		{
			set{ _plan_is_que=value;}
			get{return _plan_is_que;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string image_path
		{
			set{ _image_path=value;}
			get{return _image_path;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string image_path_300
		{
			set{ _image_path_300=value;}
			get{return _image_path_300;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string front_image_path
		{
			set{ _front_image_path=value;}
			get{return _front_image_path;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string back_image_path
		{
			set{ _back_image_path=value;}
			get{return _back_image_path;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? quantity
		{
			set{ _quantity=value;}
			get{return _quantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? redpack_price
		{
			set{ _redpack_price=value;}
			get{return _redpack_price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? redpack_quantity
		{
			set{ _redpack_quantity=value;}
			get{return _redpack_quantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? redpack_id
		{
			set{ _redpack_id=value;}
			get{return _redpack_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string arrearageFee
		{
			set{ _arrearagefee=value;}
			get{return _arrearagefee;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string hot_type
		{
			set{ _hot_type=value;}
			get{return _hot_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? published
		{
			set{ _published=value;}
			get{return _published;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? publishs
		{
			set{ _publishs=value;}
			get{return _publishs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string h_expired
		{
			set{ _h_expired=value;}
			get{return _h_expired;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string h_created
		{
			set{ _h_created=value;}
			get{return _h_created;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? service_price
		{
			set{ _service_price=value;}
			get{return _service_price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string commission
		{
			set{ _commission=value;}
			get{return _commission;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string commission_price
		{
			set{ _commission_price=value;}
			get{return _commission_price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string sell_price
		{
			set{ _sell_price=value;}
			get{return _sell_price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string coupon_url
		{
			set{ _coupon_url=value;}
			get{return _coupon_url;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string coupon_price
		{
			set{ _coupon_price=value;}
			get{return _coupon_price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? coupon_spare
		{
			set{ _coupon_spare=value;}
			get{return _coupon_spare;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? coupon_amount
		{
			set{ _coupon_amount=value;}
			get{return _coupon_amount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string coupon_expired
		{
			set{ _coupon_expired=value;}
			get{return _coupon_expired;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? coupon_spare_day
		{
			set{ _coupon_spare_day=value;}
			get{return _coupon_spare_day;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string created
		{
			set{ _created=value;}
			get{return _created;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string updated
		{
			set{ _updated=value;}
			get{return _updated;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? online
		{
			set{ _online=value;}
			get{return _online;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string reason
		{
			set{ _reason=value;}
			get{return _reason;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? tk_join
		{
			set{ _tk_join=value;}
			get{return _tk_join;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string zfb_password
		{
			set{ _zfb_password=value;}
			get{return _zfb_password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? views
		{
			set{ _views=value;}
			get{return _views;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string intro
		{
			set{ _intro=value;}
			get{return _intro;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool added
		{
			set{ _added=value;}
			get{return _added;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? addedtime
		{
			set{ _addedtime=value;}
			get{return _addedtime;}
		}
		#endregion Model

	}
}

