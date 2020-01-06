using System.Web.Mvc;

namespace AprajitaRetails.Areas.TAS
{
    public class TASAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TAS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TAS_default",
                "TAS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
           
        }
    }
}