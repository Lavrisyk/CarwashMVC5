using Ninject.Modules;
using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebCarWash.Domain.Concrete;

namespace WebCarWash
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
           
              // Database.SetInitializer(new ServiceDbInitializer());
           

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
