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
    /// Expose all operation about <see cref="INVOICE"/>
    /// </summary>
    /// <seealso cref="Foundation.Web.Controllers.WritableController{INVOICE}" />
    //[ApiVersion("1")]
    [ControllerName("invoices")]
    public class InvoiceController : WritableController<INVOICE>
    {
        private readonly ITopicManager<INVOICE> topicManager;
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="policy"></param>
        public InvoiceController(IWritableService<INVOICE> service, IUnitOfWork unitOfWork, ISerializationConfigurePolicy<INVOICE> policy, ITopicManager<INVOICE> topicManager, ILogger logger) : base(service, unitOfWork, policy, logger)
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
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<INVOICE>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(IEnumerable<object>))]
        [APISearchRoute("invoices")]
        public IHttpActionResult GetAll([FromUri] InvoiceQuery query, [FromUri] SerializationMode? mode = null) => base.Query(query, mode);

        /// <summary>
        /// Gets the INVOICE for specified id />.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="mode">Serialization mode</param>
        /// <returns>
        /// A single instance of <see cref="INVOICE" /> with the specified <paramref name="id"/>
        /// </returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(INVOICE))]
        [APIBaseRoute("invoices")]
        public IHttpActionResult GetByKey([FromUri] string id, [FromUri] SerializationMode? mode = null) => base.Get(id, mode.Value);

    }
}