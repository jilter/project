/**  版本信息模板在安装目录下，可自行修改。
* UsersBLog.cs
*
* 功 能： N/A
* 类 名： UsersBLog
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/5/22 0:32:40   N/A    初版
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
	/// UsersBLog:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UsersBLog
	{
		public UsersBLog()
		{}
		#region Model
		private long _id;
		private long _userid;
		private int? _categoryid;
		private long? _productid;
		private string _name;
		private string _productlink;
		private DateTime? _browsetime;
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
		public long UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CategoryID
		{
			set{ _categoryid=value;}
			get{return _categoryid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? ProductID
		{
			set{ _productid=value;}
			get{return _productid;}
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
		public string ProductLink
		{
			set{ _productlink=value;}
			get{return _productlink;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? BrowseTime
		{
			set{ _browsetime=value;}
			get{return _browsetime;}
		}
		#endregion Model

	}
}

