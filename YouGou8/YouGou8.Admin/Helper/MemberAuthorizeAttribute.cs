using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YouGou8.Service;

namespace YouGou8.Admin.Helper
{
    public class MemberAuthorizeAttribute: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (ConfigService.LoginUser == null)
            {
                return false;
            }
            return true;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (ConfigService.LoginUser == null)
            {
                filterContext.Result= new RedirectResult("/Home/Login");
                return;
            }
            base.OnAuthorization(filterContext);
        }
    }
}