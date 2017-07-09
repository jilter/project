/**  版本信息模板在安装目录下，可自行修改。
* Product_MY.cs
*
* 功 能： N/A
* 类 名： Product_MY
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/6/9 15:44:21   N/A    初版
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
	/// Product_MY:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Product_MY
	{
		public Product_MY()
		{}
		#region Model
		private long _id;
		private string _planid= "0";
		private long? _pid;
		private string _ptitle;
		private string _pimgurls;
		private string _plink;
		private int _pcid=0;
		private decimal? _pprice=00000000.00M;
		private int? _psales=0;
		private decimal? _prate;
		private decimal? _pcommission;
		private string _pintro;
        private decimal? _predpack = 0;
        private decimal? _couponmoney;
		private DateTime? _couponstarttime;
		private DateTime? _couponendtime;
		private int? _couponcount;
		private int? _couponremain;
		private string _couponcommand;
		private string _couponlink;
		private string _couponshortlink;
		private string _descimgs;
		private string _desc;
		private bool _isbig= false;
		private int? _clickcount=0;
		private string _remark;
		private int? _addedid;
		private int? _addedtype;
		private bool _isshow= true;
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
		public string PlanID
		{
			set{ _planid=value;}
			get{return _planid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? PID
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PTitle
		{
			set{ _ptitle=value;}
			get{return _ptitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PImgUrls
		{
			set{ _pimgurls=value;}
			get{return _pimgurls;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PLink
		{
			set{ _plink=value;}
			get{return _plink;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int PCID
		{
			set{ _pcid=value;}
			get{return _pcid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? PPrice
		{
			set{ _pprice=value;}
			get{return _pprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PSales
		{
			set{ _psales=value;}
			get{return _psales;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? PRate
		{
			set{ _prate=value;}
			get{return _prate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? PCommission
		{
			set{ _pcommission=value;}
			get{return _pcommission;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PIntro
		{
			set{ _pintro=value;}
			get{return _pintro;}
		}
        public decimal? PRedPack
        {
            set { _predpack = value; }
            get { return _predpack; }
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
		public DateTime? CouponStartTime
		{
			set{ _couponstarttime=value;}
			get{return _couponstarttime;}
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
		public string CouponShortLink
		{
			set{ _couponshortlink=value;}
			get{return _couponshortlink;}
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
		public int? AddedID
		{
			set{ _addedid=value;}
			get{return _addedid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AddedType
		{
			set{ _addedtype=value;}
			get{return _addedtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsShow
		{
			set{ _isshow=value;}
			get{return _isshow;}
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

