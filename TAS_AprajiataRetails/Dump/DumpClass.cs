using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAS_AprajiataRetails.Dump
{
    //Links 
    //https://www.dotnetcurry.com/aspnet-mvc/1102/aspnet-mvc-role-based-security
    //https://www.codeproject.com/Articles/875547/Custom-Roles-Based-Access-Control-RBAC-in-ASP-NET
    //https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/authentication-and-authorization-in-aspnet-web-api
    //https://stackoverflow.com/questions/39249111/mvc-authentication-in-controller
    //https://stackoverflow.com/questions/2319157/how-can-we-set-authorization-for-a-whole-area-in-asp-net-mvc
    // https://weblogs.asp.net/jhallal/building-secure-asp-net-mvc-web-applications
    //https://gooroo.io/GoorooTHINK/Article/17333/Custom-user-roles-and-rolebased-authorization-in-ASPNET-core/28352#.XeiYyegzaM8

    public class DumpClass
    {
        //[LoggedOrAuthorizedAttribute(Roles = "admin", Users = "Bob")]
        //public ActionResult Index()
        //{

        //}
        //RegisterGlobalFilters(GlobalFilterCollection filters)
        //{
        //    filters.Add(new HandleErrorAttribute());
        //    filters.Add(new System.Web.Mvc.AuthorizeAttribute());
        //}
        //Here is a snippet of the OnAuthorization Method:

        //if (!filterContext.ActionDescriptor.IsDefined
        //    (typeof(AllowAnonymousAttribute), inherit) &&
        //  !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined
        //    (typeof(AllowAnonymousAttribute), true)) {
        //      // Check for authorization
        //}

    }


    //public class LoggedOrAuthorizedAttribute : AuthorizeAttribute
    //{
    //    public LoggedOrAuthorizedAttribute()
    //    {
    //        View = "error";
    //        Master = String.Empty;
    //    }

    //    public String View { get; set; }
    //    public String Master { get; set; }

    //    public override void OnAuthorization(AuthorizationContext filterContext)
    //    {
    //        base.OnAuthorization(filterContext);
    //        CheckIfUserIsAuthenticated(filterContext);
    //    }

    //    private void CheckIfUserIsAuthenticated(AuthorizationContext filterContext)
    //    {
    //        // If Result is null, we're OK: the user is authenticated and authorized. 
    //        if (filterContext.Result == null)
    //            return;

    //        // If here, you're getting an HTTP 401 status code. In particular,
    //        // filterContext.Result is of HttpUnauthorizedResult type. Check Ajax here. 
    //        if (filterContext.HttpContext.User.Identity.IsAuthenticated)
    //        {
    //            if (String.IsNullOrEmpty(View))
    //                return;
    //            var result = new ViewResult { ViewName = View, MasterName = Master };
    //            filterContext.Result = result;
    //        }
    //    }
    //}



}