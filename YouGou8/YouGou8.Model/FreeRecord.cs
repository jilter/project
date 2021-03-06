﻿/**  版本信息模板在安装目录下，可自行修改。
* FreeRecord.cs
*
* 功 能： N/A
* 类 名： FreeRecord
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
	/// FreeRecord:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class FreeRecord
	{
		public FreeRecord()
		{}
		#region Model
		private long _id;
		private long _userid;
		private int _freeid;
        private string _freelink;
		private bool _status;
		private string _remark;
		private DateTime _addedtime;
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
		public int FreeID
		{
			set{ _freeid=value;}
			get{return _freeid;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string FreeLink
        {
            set { _freelink = value; }
            get { return _freelink; }
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
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime AddedTime
		{
			set{ _addedtime=value;}
			get{return _addedtime;}
		}
		#endregion Model

	}
}

