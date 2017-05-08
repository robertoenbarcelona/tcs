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
    /// Expose all operation about <see cref="VENDOR"/>
    /// </summary>
    /// <seealso cref="Foundation.Web.Controllers.WritableController{VENDOR}" />
    //[ApiVersion("1")]
    [ControllerName("vendors")]
    public class VendorController : WritableController<VENDOR>
    {
        private readonly ITopicManager<VENDOR> topicManager;
        /// <summary>
        /// Initializes a new instance of the <see cref="VendorController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="policy"></param>
        public VendorController(IWritableService<VENDOR> service, IUnitOfWork unitOfWork, ISerializationConfigurePolicy<VENDOR> policy, ITopicManager<VENDOR> topicManager, ILogger logger) : base(service, unitOfWork, policy, logger)
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
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<VENDOR>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(IEnumerable<object>))]
        [APISearchRoute("vendors")]
        public IHttpActionResult GetAll([FromUri] VendorQuery query, [FromUri] SerializationMode? mode = null) => base.Query(query, mode);

        /// <summary>
        /// Gets the VENDOR for specified id />.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="mode">Serialization mode</param>
        /// <returns>
        /// A single instance of <see cref="VENDOR" /> with the specified <paramref name="id"/>
        /// </returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(VENDOR))]
        [APIBaseRoute("vendors")]
        public IHttpActionResult GetByKey([FromUri] string id, [FromUri] SerializationMode? mode = null) => base.Get(id, mode.Value);

        ///// <summary>
        ///// Create an item with the specified value.
        ///// </summary>
        ///// <param name="entity">The values of the entity.</param>
        ///// <returns>A 201 created witrh the new instance or internal error</returns>
        //[HttpPost]
        //[SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(IEnumerable<object>))]
        //[SwaggerResponse(HttpStatusCode.InternalServerError)]
        //[SwaggerResponse(HttpStatusCode.Created, Type = typeof(VENDOR))]
        //[APIPostRoute("vendors")]
        //public IHttpActionResult Create([FromBody]VENDOR entity)
        //{
        //    return base.Post(entity, null, (x) => { topicManager.Send(new OperationEvent<VENDOR>() { Entity = x, Operation = OperationType.POST }); });
        //}

        ///// <summary>
        ///// Update the entity specified by id.
        ///// </summary>
        ///// <param name="id">The id.</param>
        ///// <param name="entity">The entity.</param>
        ///// <returns>A 200 Ok with the entity, not found if not exists or internal error</returns>
        //[HttpPut]
        //[APIBaseRoute("vendors")]
        //[SwaggerResponse(HttpStatusCode.OK, Type = typeof(VENDOR))]
        //[SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(IEnumerable<object>))]
        //[SwaggerResponse(HttpStatusCode.Conflict, Type = typeof(VENDOR))]
        //[SwaggerResponse(HttpStatusCode.NotFound)]
        //[SwaggerResponse(HttpStatusCode.InternalServerError)]
        //public IHttpActionResult Update([FromUri] string id, [FromBody]VENDOR entity)
        //{
        //    return base.Put(id, entity, null, (x) => { topicManager.Send(new OperationEvent<VENDOR>() { Entity = x, Operation = OperationType.PUT }); });
        //}

        ///// <summary>
        ///// Deletes the entity specified by id.
        ///// </summary>
        ///// <param name="id">The id.</param>
        ///// <param name="timeStamp">The time stamp.</param>
        ///// <returns>
        ///// A 200 Ok with no content, not found if not exists or internal error
        ///// </returns>
        //[HttpDelete]
        //[APIBaseRoute("vendors")]
        //[SwaggerResponse(HttpStatusCode.NotFound)]
        //[SwaggerResponse(HttpStatusCode.NoContent)]
        //[SwaggerResponse(HttpStatusCode.Conflict, Type = typeof(VENDOR))]
        //[SwaggerResponse(HttpStatusCode.InternalServerError)]
        //public IHttpActionResult DeleteByKeyAsync([FromUri] string id, [FromUri] string timeStamp)
        //{
        //    return base.Delete(id, timeStamp, null, (x) => { topicManager.Send(new OperationEvent<VENDOR>() { Entity = x, Operation = OperationType.DELETE }); });
        //}

        ///// <summary>
        ///// Gets a list of VENDOR filerted by country.
        ///// </summary>
        ///// <param name="country">The country.</param>
        ///// <returns>A 200 Ok with the list or internal error</returns>
        //[HttpGet]
        //[SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<VENDOR>))]
        //[APISharededRoute("vendors", "surname")]
        //[SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(IEnumerable<object>))]
        //[SwaggerResponse(HttpStatusCode.InternalServerError)]
        //public IHttpActionResult GetAllFamily([FromUri] string surname) => base.Query(new VendorsOfFamily() { Surname = surname }, SerializationMode.Info);
        
    }
}