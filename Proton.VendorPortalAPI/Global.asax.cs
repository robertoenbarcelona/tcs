
namespace Proton.VendorPortalAPI
{
    using Foundation.Web.Installation;
    using System.Web.Http;
    using Swashbuckle.Application;
    using System;
    using App_Start;
    using System.Web.Routing;
    using System.IO;
    /// <summary>
    /// The application class
    /// </summary>
    /// <seealso cref="Proton.Foundation.Web.Installation.ProtonWebApiApplication" />
    public class WebApiApplication : ProtonWebApiApplication
    {
        /// <summary>
        /// Called when the application the start.
        /// </summary>
        protected override void Application_Start()
        {
            // set here Cors permitted sites (read it from config or file)
            //base.permitedCors.Add("mywebaap.azurewebsites.net");
            //base.permitedCors.Add("mypartnerwebapp.azurewebsites.net");

            //Call Foundation registration
            base.Application_Start();

            // Make your registrations
            IoCConfigExtension.Register();
        }

        /// <summary>
        /// Gets the version information actions to build the multiversion API.
        /// </summary>
        /// <returns>Ther operations to execute for build the version system for Swagger</returns>
        protected override Action<VersionInfoBuilder> GetVersionInfo() =>
            (versionbuilder) =>
                    {
                        versionbuilder.Version("v2", "A sample API for testing and prototyping Foundation features v2")
                            .Description("A sample API for testing and prototyping Foundation features")
                            .TermsOfService("Some terms")
                            .Contact(cc => cc
                            .Name("Some contact")
                            .Url("http://tempuri.org/contact")
                            .Email("some.contact@tempuri.org"))
                            .License(lc => lc
                            .Name("Some License")
                            .Url("http://tempuri.org/license"));
                        versionbuilder.Version("v1", "A sample API for testing and prototyping Foundation features V1")
                            .Description("A sample API for testing and prototyping Foundation features")
                            .TermsOfService("Some terms")
                            .Contact(cc => cc
                            .Name("Some contact")
                            .Url("http://tempuri.org/contact")
                            .Email("some.contact@tempuri.org"))
                            .License(lc => lc
                            .Name("Some License")
                            .Url("http://tempuri.org/license"));
                    };

        /// <summary>
        /// Registers the web API extension.
        /// </summary>
        /// <param name="config">The configuration.</param>
        protected override Action<HttpConfiguration> RegisterWebApiExtension(HttpConfiguration config) => (x) => { WebApiConfigExtension.Register(x); };

        /// <summary>
        /// Returns the current assembly file name
        /// </summary>
        /// <returns>Current assembly file name, without extension</returns>
        protected override string GetApplicationAssemblyName()
        {
            string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            return Path.GetFileNameWithoutExtension(assemblyPath);
        }
    }
}
