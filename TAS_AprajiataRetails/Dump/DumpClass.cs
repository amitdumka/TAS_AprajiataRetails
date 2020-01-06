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





    //html code

    //    <!--Modal: Login with Avatar Form-->
    //<div class="modal fade" id="modalLoginAvatar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
    //  aria-hidden="true">
    //  <div class="modal-dialog cascading-modal modal-avatar modal-sm" role="document">
    //    <!--Content-->
    //    <div class="modal-content">

    //      <!--Header-->
    //      <div class="modal-header">
    //        <img src = "https://mdbootstrap.com/img/Photos/Avatars/img%20%281%29.jpg" alt="avatar" class="rounded-circle img-responsive">
    //      </div>
    //      <!--Body-->
    //      <div class="modal-body text-center mb-1">

    //        <h5 class="mt-1 mb-2">Maria Doe</h5>

    //        <div class="md-form ml-0 mr-0">
    //          <input type = "password" type="text" id="form29" class="form-control form-control-sm validate ml-0">
    //          <label data-error="wrong" data-success="right" for="form29" class="ml-0">Enter password</label>
    //        </div>

    //        <div class="text-center mt-4">
    //          <button class="btn btn-cyan mt-1">Login<i class="fas fa-sign-in ml-1"></i></button>
    //        </div>
    //      </div>

    //    </div>
    //    <!--/.Content-->
    //  </div>
    //</div>

    //<!--Modal: Login with Avatar Form-->

    //<div class="text-center">
    //  <a href = "" class="btn btn-default btn-rounded" data-toggle="modal" data-target="#modalLoginAvatar">Launch
    //      Modal Login with Avatar</a>
    //</div>




    //    <div class="modal fade" id="orangeModalSubscription" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
    //  aria-hidden="true">
    //  <div class="modal-dialog modal-notify modal-warning" role="document">
    //    <!--Content-->
    //    <div class="modal-content">
    //      <!--Header-->
    //      <div class="modal-header text-center">
    //        <h4 class="modal-title white-text w-100 font-weight-bold py-2">Subscribe</h4>
    //        <button type = "button" class="close" data-dismiss="modal" aria-label="Close">
    //          <span aria-hidden="true" class="white-text">&times;</span>
    //        </button>
    //      </div>

    //      <!--Body-->
    //      <div class="modal-body">
    //        <div class="md-form mb-5">
    //          <i class="fas fa-user prefix grey-text"></i>
    //          <input type = "text" id="form3" class="form-control validate">
    //          <label data-error="wrong" data-success="right" for="form3">Your name</label>
    //        </div>

    //        <div class="md-form">
    //          <i class="fas fa-envelope prefix grey-text"></i>
    //          <input type = "email" id="form2" class="form-control validate">
    //          <label data-error="wrong" data-success="right" for="form2">Your email</label>
    //        </div>
    //      </div>

    //      <!--Footer-->
    //      <div class="modal-footer justify-content-center">
    //        <a type = "button" class="btn btn-outline-warning waves-effect">Send<i class="fas fa-paper-plane-o ml-1"></i></a>
    //      </div>
    //    </div>
    //    <!--/.Content-->
    //  </div>
    //</div>

    //<div class="text-center">
    //  <a href = "" class="btn btn-default btn-rounded" data-toggle="modal" data-target="#orangeModalSubscription">Launch
    //      modal Subscription</a>
    //</div>



  //  var query = transactions.GroupBy(t => new { t.Price, t.ItemId, t.CurrencyId }, t => t, (key, g) => new { key.ItemId, key.Price, key.CurrencyId, Quantity = g.Count() });
		//foreach (var transaction in query){
		//	Console.WriteLine("ItemId: {0}, Price {1}, CurrencyId {2}, Quantity {3}", transaction.ItemId, transaction.Price, transaction.CurrencyId, transaction.Quantity);
		//}
}