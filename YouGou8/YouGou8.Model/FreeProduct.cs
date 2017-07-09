/**  版本信息模板在安装目录下，可自行修改。
* FreeProduct.cs
*
* 功 能： N/A
* 类 名： FreeProduct
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/5/22 0:32:38   N/A    初版
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
	/// FreeProduct:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class FreeProduct
	{
		public FreeProduct()
		{}
		#region Model
		private int _id;
		private string _name;
		private string _link;
		private string _imgurls;
		private int _count;
		private int _rqty;
		private string _desc;
		private string _descurls;
		private string _remark;
		private string _command;
		private bool _status= true;
		private DateTime? _addedtime;
		/// <summary>
		/// auto_increment
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
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
		public string Link
		{
			set{ _link=value;}
			get{return _link;}
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
		public int Count
		{
			set{ _count=value;}
			get{return _count;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int RQty
		{
			set{ _rqty=value;}
			get{return _rqty;}
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
		public string DescUrls
		{
			set{ _descurls=value;}
			get{return _descurls;}
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
		public string Command
		{
			set{ _command=value;}
			get{return _command;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Status
		{
			set{ _status=value;}
			get{return _status;}
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

