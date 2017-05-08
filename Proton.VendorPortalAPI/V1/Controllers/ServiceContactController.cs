using Proton.Foundation.Core.Logging;
using Proton.Foundation.Core.Services;
using Proton.Foundation.Core.UnitOfWork;
using Proton.Foundation.Web.Controllers;
using Proton.Foundation.Core.Installation;
using Proton.Foundation.Web.Routing;
using Proton.VendorPortalAPI.V1.Models;
using Proton.VendorPortalAPI.V1.Filters;
using Proton.Foundation.Core.Serialization;
using System.Collections.Generic;
using System.Net;
using Swashbuckle.Swagger.Annotations;
using System.Web.Http;
using Proton.Foundation.Core.ServiceBus;
using Microsoft.Web.Http;

namespace Proton.VendorPortalAPI.V1.Controllers
{
    /// <summary>
    /// Expose all operation about <see cref="SERVICE_CONTACT"/>
    /// </summary>
    /// <seealso cref="Foundation.Web.Controllers.WritableController{SERVICE_CONTACT}" />
    //[ApiVersion("1")]
    [ControllerName("servicecontact")]
    public class ServiceContactController : WritableController<SERVICE_CONTACT>
    {
        private readonly ITopicManager<SERVICE_CONTACT> topicManager;
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceContactController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="policy"></param>
        public ServiceContactController(IWritableService<SERVICE_CONTACT> service, IUnitOfWork unitOfWork, ISerializationConfigurePolicy<SERVICE_CONTACT> policy, ITopicManager<SERVICE_CONTACT> topicManager, ILogger logger) : base(service, unitOfWork, policy, logger)
        {
            this.topicManager = topicManager;
        }

        /// <summary>
        /// Search Customers.
        /// </summary>
        /// <returns>A collection of Customer instances satisfing the criterias</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<SERVICE_CONTACT>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(IEnumerable<object>))]
        [APISearchRoute("servicecontact")]
        public IHttpActionResult GetAll([FromUri] ServiceContactQuery query, [FromUri] SerializationMode? mode = null) => base.Query(query, mode);

        /// <summary>
        /// Gets the SERVICE_CONTACT for specified id />.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="mode">Serialization mode</param>
        /// <returns>
        /// A single instance of <see cref="SERVICE_CONTACT" /> with the specified <paramref name="id"/>
        /// </returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(SERVICE_CONTACT))]
        [APIBaseRoute("servicecontact")]
        public IHttpActionResult GetByKey([FromUri] string id, [FromUri] SerializationMode? mode = null) => base.Get(id, mode.Value);
        
    }
}