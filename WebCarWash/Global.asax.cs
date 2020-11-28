using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using WebCarWash.Models.Repository;

namespace WebCarWash
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            try
            {
                Database.SetInitializer(new ServiceDbInitializer());
                Models.Repository.ServicesContext db = new Models.Repository.ServicesContext();
                db.Database.Initialize(true);
            }
            catch(Exception e)
            {
  
                string str = e.Message;
            }

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
