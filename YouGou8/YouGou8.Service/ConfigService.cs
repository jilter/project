using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using YouGou8.Model;

namespace YouGou8.Service
{
    public class ConfigService
    {
        private static string _PID=null;
        private static int? _MaxRedPack=null;
        private static string _QTKAppKey=null;
        private static string _TaoBaoApiUrl = null;
        private static Admin _LoginUser=null;

        public static string PID
        {
            get
            {
                if (string.IsNullOrEmpty(_PID))
                {
                    if (ConfigurationManager.AppSettings["PID"] != null)
                    {
                        _PID = ConfigurationManager.AppSettings["PID"].ToString();
                    }
                }
                if (string.IsNullOrEmpty(_PID))
                {
                    _PID = "mm_28646890_25482821_94980646";
                }
                return _PID;
            }
        }

        public static int MaxRedPack
        {
            get
            {
                if (!_MaxRedPack.HasValue)
                {
                    if (ConfigurationManager.AppSettings["MaxRedPack"] != null)
                    {
                        _MaxRedPack = Convert.ToInt32(ConfigurationManager.AppSettings["MaxRedPack"]);
                    }
                    else
                    {
                        _MaxRedPack = 0;
                    }
                }
                return _MaxRedPack.Value;
            }
        }

        public static string QTKAppKey
        {
            get
            {
                if (string.IsNullOrEmpty(_QTKAppKey))
                {
                    if (ConfigurationManager.AppSettings["QTKAppKey"] != null)
                    {
                        _QTKAppKey = ConfigurationManager.AppSettings["QTKAppKey"].ToString();
                    }
                }
                return _QTKAppKey;
            }
        }

        public static string TaoBaoApiUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_TaoBaoApiUrl))
                {
                    if (ConfigurationManager.AppSettings["TaoBaoApiUrl"] != null)
                    {
                        _TaoBaoApiUrl = ConfigurationManager.AppSettings["TaoBaoApiUrl"].ToString();
                    }
                }
                return _TaoBaoApiUrl;
            }
        }

        public static Admin LoginUser
        {
            get
            {
                if (_LoginUser == null)
                {
                    if (HttpContext.Current.Session["LoginUser"] != null)
                    {
                        _LoginUser = HttpContext.Current.Session["LoginUser"] as Admin;
                    }
                }
                return _LoginUser;
            }
            set
            {
                HttpContext.Current.Session["LoginUser"] = value;
            }
        }

        public static void Logout()
        {
            _LoginUser = null;
            LoginUser = null;
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }
    }
}
