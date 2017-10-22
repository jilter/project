using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace YouGou8.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "Html",
               "{controller}/{id}.html",    
               new { controller = "Wechat", action = "Index", id = UrlParameter.Optional }    
            );

            routes.MapRoute(
               "WechatDetail",
               "{controller}/p{id}",
               new { controller = "Wechat", action = "Detail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               "Html2",
               "{controller}/{id}.html",
               new { controller = "Weibo", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Wechat", action = "Index", id = 0 }
            );
        }
    }
}
