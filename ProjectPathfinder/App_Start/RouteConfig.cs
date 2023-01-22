using ProjectPathfinder.Controllers;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectPathfinder
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var namespaces = new[] { typeof(AccountController).Namespace };

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Home", "", new { controller = "Home", action = "Index" }, namespaces);
            routes.MapRoute("About", "about_us", new { controller = "Home", action = "About_Us" }, namespaces);
            routes.MapRoute("Pricing", "pricing", new { controller = "Home", action = "Pricing" }, namespaces);

            routes.MapRoute("Error500", "errors/500", new { controller = "Errors", action = "Error" }, namespaces);
            routes.MapRoute("Error404", "errors/404", new { controller = "Errors", action = "NotFound" }, namespaces);

            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional }, namespaces);
        }
    }
}
