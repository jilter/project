using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YouGou8.Web.Models
{
    public class FreeModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Picture0 { get; set; }
        public string Picture1 { get; set; }
        public string Picture2 { get; set; }
        public string Picture3 { get; set; }
        public string Picture4 { get; set; }
        public int Count { get; set; }
        public DateTime EndDateTime { get; set; }
        public string CouponCommand { get; set; }
    }
}