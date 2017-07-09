/**  版本信息模板在安装目录下，可自行修改。
* Product.cs
*
* 功 能： N/A
* 类 名： Product
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/5/22 0:32:39   N/A    初版
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
	/// Product:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Product
	{
		public Product()
		{}
		#region Model
		private long _id;
		private long _pid=0;
		private int _planid=0;
		private string _name;
		private string _imgurls;
		private string _link;
		private int _categoryid=0;
		private decimal? _price=00000000.00M;
		private int? _sales=0;
		private decimal? _percent;
		private decimal? _money;
		private decimal? _redpack;
		private decimal? _couponmoney;
		private DateTime? _couponendtime;
		private int? _couponcount;
		private int? _couponremain;
		private string _couponcommand;
		private string _couponlink;
		private string _descimgs;
		private string _desc;
		private bool _isbig= false;
		private int? _clickcount=0;
		private string _remark;
		private DateTime? _addedtime;
		/// <summary>
		/// auto_increment
		/// </summary>
		public long ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long PID
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int PlanID
		{
			set{ _planid=value;}
			get{return _planid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImgUrls
		{
			set{ _imgurls=value;}
			get{return _imgurls;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Link
		{
			set{ _link=value;}
			get{return _link;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CategoryID
		{
			set{ _categoryid=value;}
			get{return _categoryid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sales
		{
			set{ _sales=value;}
			get{return _sales;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Percent
		{
			set{ _percent=value;}
			get{return _percent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Money
		{
			set{ _money=value;}
			get{return _money;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Redpack
		{
			set{ _redpack=value;}
			get{return _redpack;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CouponMoney
		{
			set{ _couponmoney=value;}
			get{return _couponmoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CouponEndTime
		{
			set{ _couponendtime=value;}
			get{return _couponendtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CouponCount
		{
			set{ _couponcount=value;}
			get{return _couponcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CouponRemain
		{
			set{ _couponremain=value;}
			get{return _couponremain;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CouponCommand
		{
			set{ _couponcommand=value;}
			get{return _couponcommand;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CouponLink
		{
			set{ _couponlink=value;}
			get{return _couponlink;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DescImgs
		{
			set{ _descimgs=value;}
			get{return _descimgs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Desc
		{
			set{ _desc=value;}
			get{return _desc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsBig
		{
			set{ _isbig=value;}
			get{return _isbig;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ClickCount
		{
			set{ _clickcount=value;}
			get{return _clickcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AddedTime
		{
			set{ _addedtime=value;}
			get{return _addedtime;}
		}
		#endregion Model

	}
}

