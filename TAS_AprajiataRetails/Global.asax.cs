using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TAS_AprajiataRetails.works;
using System.Web.Http;
using TAS_AprajiataRetails.Models.Data.Voy;

namespace TAS_AprajiataRetails
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Code that runs on application startup
            Scheduler objmyScheduler = new Scheduler();
            objmyScheduler.Scheduler_Start();

            
            
        }
    }
}
