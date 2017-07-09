using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YouGou8.Web.Helper
{
    public class MemberAuthorizeAttribute: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (System.Web.HttpContext.Current.Session["Admin"] == null)
            {
                return false;
            }
            return true;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (System.Web.HttpContext.Current.Session["Admin"] == null)
            {
                filterContext.Result= new RedirectResult("/Home/Login");
                return;
            }
            base.OnAuthorization(filterContext);
        }
    }
}