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
    /// Expose all operation about <see cref="CENTRE_ENTITY_LIST"/>
    /// </summary>
    /// <seealso cref="Foundation.Web.Controllers.WritableController{CENTRE_ENTITY_LIST}" />
    //[ApiVersion("1")]
    [ControllerName("centrelist")]
    public class CentreEntityListController : WritableController<CENTRE_ENTITY_LIST>
    {
        private readonly ITopicManager<CENTRE_ENTITY_LIST> topicManager;
        /// <summary>
        /// Initializes a new instance of the <see cref="CentreEntityListController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="policy"></param>
        public CentreEntityListController(IWritableService<CENTRE_ENTITY_LIST> service, IUnitOfWork unitOfWork, ISerializationConfigurePolicy<CENTRE_ENTITY_LIST> policy, ITopicManager<CENTRE_ENTITY_LIST> topicManager, ILogger logger) : base(service, unitOfWork, policy, logger)
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
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<CENTRE_ENTITY_LIST>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(IEnumerable<object>))]
        [APISearchRoute("centrelist")]
        public IHttpActionResult GetAll([FromUri] CentreEntityListQuery query, [FromUri] SerializationMode? mode = null) => base.Query(query, mode);

        /// <summary>
        /// Gets the CENTRE_ENTITY_LIST for specified id />.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="mode">Serialization mode</param>
        /// <returns>
        /// A single instance of <see cref="CENTRE_ENTITY_LIST" /> with the specified <paramref name="id"/>
        /// </returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(CENTRE_ENTITY_LIST))]
        [APIBaseRoute("centrelist")]
        public IHttpActionResult GetByKey([FromUri] string id, [FromUri] SerializationMode? mode = null) => base.Get(id, mode.Value);
        
    }
}