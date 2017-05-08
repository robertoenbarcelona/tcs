
namespace Proton.VendorPortalAPI
{
    using Microsoft.Web.Http.Routing;
    using System.Web.Http;

    /// <summary>
    /// This class is used to register application special routes
    /// </summary>
    public static class WebApiConfigExtension
    {
        /// <summary>
        /// Registers the routes in the Http configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {

            // Web API configuration and services others than default
            //config.Routes.MapHttpRoute(
            //    "newRouteNotDefault",
            //    "api/v{apiVersion}/{controller}/test/{id}",
            //    defaults: null,
            //    constraints: new { apiVersion = new ApiVersionRouteConstraint() }
            //);
            
        }
    }
}



// just refere the base version
// using V1.Controllers;

////Configure.ConfigureDefaultRoutes(config), 
////    config.AddApiVersioning(
////        options =>
////        {
////            // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
////            options.ReportApiVersions = true;

////            // set always a V1 
////            options.AssumeDefaultVersionWhenUnspecified = true;
////            options.DefaultApiVersion = new ApiVersion(1, 0);

////            options.Conventions.Controller<BillController>()
////                       .HasApiVersion(2, 0)
////                       .HasApiVersion(3, 0)
////                       .Action(c => c.GetV3()).MapToApiVersion(3, 0);
////        });