using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Swashbuckle.Extension.Mvc;
using System.Web.Http;
using System.Web.Http.Description;

namespace DefaultMvcProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var assembly = typeof(DefaultMvcProject.MvcApplication).Assembly;
            var apiExplorer = new MvcApiExplorer(assembly, GlobalConfiguration.Configuration);
            GlobalConfiguration.Configuration.Services.Replace(typeof(IApiExplorer), apiExplorer);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
