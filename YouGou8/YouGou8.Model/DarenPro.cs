using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace YouGou8.Model{
	 	//DarenPro
		public class DarenPro
	{
   		     
      	/// <summary>
		/// ID
        /// </summary>		
		private int _id;
        public int ID
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// ItemID
        /// </summary>		
		private long _itemid;
        public long ItemID
        {
            get{ return _itemid; }
            set{ _itemid = value; }
        }        
		/// <summary>
		/// Link
        /// </summary>		
		private string _link;
        public string Link
        {
            get{ return _link; }
            set{ _link = value; }
        }        
		/// <summary>
		/// AddedDate
        /// </summary>		
		private DateTime _addeddate;
        public DateTime AddedDate
        {
            get{ return _addeddate; }
            set{ _addeddate = value; }
        }        
		   
	}
}

