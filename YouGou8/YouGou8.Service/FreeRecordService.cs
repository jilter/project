using Chloe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouGou8.Model;

namespace YouGou8.Service
{
    public class FreeRecordJoin
    {
        public FreeRecord FR { get; set; }
        public Users U { get; set; }
        public FreeProduct FP { get; set; }
    }
    public class FreeRecordService
    {
        public static int Delete(long id)
        {
            return MySqlHelper.context.Delete<FreeRecord>(p => p.ID == id);
        }

        public static List<FreeRecordJoin> GetList(int freeId,int pageSize,int pageIndex,out int totalCount)
        {
            IQuery<FreeRecord> record = MySqlHelper.context.Query<FreeRecord>();
            IJoiningQuery<FreeRecord, Users> record_user = record.InnerJoin<Users>((r, u) => r.UserID == u.ID);
            IJoiningQuery<FreeRecord, Users, FreeProduct> record_user_product = record_user.InnerJoin<FreeProduct>((r, u, p) => r.FreeID == p.ID);
            IQuery<FreeRecordJoin> frj=record_user_product.Select((r, u, p) => new FreeRecordJoin { FR = r, U = u, FP = p });
            if (freeId>0)
            {
                frj = frj.Where(rup => rup.FR.FreeID==freeId);
            }
            frj = frj.OrderBy(f => f.FR.AddedTime);
            totalCount = frj.Count();
            frj = frj.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return frj.ToList();
        }

        public static FreeRecord GetInfo(long id)
        {
            IQuery<FreeRecord> q = MySqlHelper.context.Query<FreeRecord>();
            return q.Where(p => p.ID==id).FirstOrDefault();
        }

        public static FreeRecord GetInfo(long userid, int freeid)
        {
            IQuery<FreeRecord> q = MySqlHelper.context.Query<FreeRecord>();
            return q.Where(p => p.UserID == userid && p.FreeID==freeid).FirstOrDefault();
        }

        public static FreeRecord GetTodayRecord(long userid)
        {
            IQuery<FreeRecord> q = MySqlHelper.context.Query<FreeRecord>();
            DateTime start = DateTime.Now.Date;
            DateTime end = DateTime.Now.AddDays(1).Date;
            return q.Where(p => p.UserID == userid && p.AddedTime > start && p.AddedTime < end && p.Status == true).FirstOrDefault();
        }

        public static long Insert(FreeRecord fr)
        {
            FreeRecord newP = MySqlHelper.context.Insert(fr);
            return newP.ID;
        }

        public static int Update(FreeRecord fr)
        {
            return MySqlHelper.context.Update(fr);
        }

        public static int GetRecordCount(long freeid)
        {
            IQuery<FreeRecord> record = MySqlHelper.context.Query<FreeRecord>();
            record = record.Where(r => r.FreeID == freeid);
            return record.Count();
        }

        public static int CheckAndInsert(long userid,int freeid,out long id)
        {
            id = 0;
            FreeProduct fp = FreeProductService.GetInfo(freeid);
            if (fp == null || fp.ID <= 0)
            {
                return 1;//赠品不存在
            }
            if (fp.RQty <= 0)
            {
                return 2;//赠品已送完
            }
            if (!fp.Status||fp.AddedTime<Convert.ToDateTime(DateTime.Now.ToShortDateString()))
            {
                return 3;//赠品活动已结束
            }
            FreeRecord fr = GetInfo(userid, freeid);
            if (fr != null)
            {
                return 4;//已领取该赠品
            }

            fr = GetTodayRecord(userid);
            if (fr != null)
            {
                return 7;//今天已领过其它赠品了
            }
            
            fr = new FreeRecord();
            fr.UserID = userid;
            fr.FreeID = freeid;
            fr.FreeLink = fp.Link;
            fr.Status = fp.RQty > 0;
            fr.AddedTime = DateTime.Now;
            fr.ID = Insert(fr);
            id = fr.ID;
            if (fr.ID > 0)
            {
                fp.RQty = fp.RQty - 1;
                if (GetRecordCount(freeid) >= fp.Count * 2)
                {
                    fp.Status = true;
                }
                int result = FreeProductService.Update(fp);
                return result > 0 ? 0 : 6;//0成功,6更新赠品信息失败
            }
            else
            {
                return 5;//添加失败
            }      
        }
    }
}
