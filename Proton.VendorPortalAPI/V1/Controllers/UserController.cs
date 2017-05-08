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
using System.Threading.Tasks;
using Microsoft.Web.Http;

namespace Proton.VendorPortalAPI.V1.Controllers
{
    [ControllerName("users")]
    public class UserController : WritableAsyncController<User>
    {
        private readonly ITopicManager<User> topicManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="topicManager">The topic manager.</param>
        /// <param name="policy"></param>
        public UserController(IWritableServiceAsync<User> service, IUnitOfWorkAsync unitOfWork, ISerializationConfigurePolicy<User> policy, ITopicManager<User> topicManager, ILogger logger) : base(service, unitOfWork, policy, logger)
        {
            this.topicManager = topicManager;
        }

        [HttpGet]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<User>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(IEnumerable<object>))]
        [APISearchRoute("users")]
        public Task<IHttpActionResult> GetAll([FromUri] UserQuery query) => base.QueryAsync(query, SerializationMode.Info);


        // POST api/v1.0/Users
        // or prefixing [Route]
        /// <summary>
        /// Create an item with the specified value.
        /// </summary>
        /// <param name="entity">The values of the entity.</param>
        /// <returns>A 201 created witrh the new instance or internal error</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(IEnumerable<object>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(User))]
        [APIPostRoute("users")]
        public async Task<IHttpActionResult> CreateAsync([FromBody]User entity)
        {
            var result = await base.PostAsync(entity).ConfigureAwait(true);
            //var created = result as CreatedContentActionResult<User>;
            //if (created != null)
            //{
            //    await topicManager.SendAsync(new OperationEvent<User>() { Entity = created.Entity, Operation = OperationType.POST }).ConfigureAwait(false);
            //}

            return result;
        }

        // PUT api/v1.0/users
        // or prefixing [Route("{key}")]
        /// <summary>
        /// Update the entity specified by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>A 200 Ok with the entity, not found if not exists or internal error</returns>
        [HttpPut]
        [APIBaseRoute("users")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(User))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(IEnumerable<object>))]
        [SwaggerResponse(HttpStatusCode.Conflict, Type = typeof(User))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> UpdateAsync([FromUri] string key, [FromBody]User entity)
        {
            var result = await base.PutAsync(key, entity).ConfigureAwait(true);
            //var updated = result as GetContentActionResult<User>;
            //if (updated != null)
            //{
            //    await topicManager.SendAsync(new OperationEvent<User>() { Entity = updated.Entity, Operation = OperationType.PUT }).ConfigureAwait(false);
            //}

            return result;
        }

        // DELETE api/v1.0/users/acRD34568
        // or prefixing [Route]
        /// <summary>
        /// Deletes the entity specified by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="timeStamp">The time stamp.</param>
        /// <returns>
        /// A 200 Ok with no content, not found if not exists or internal error
        /// </returns>
        [HttpDelete]
        [APIBaseRoute("users")]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [SwaggerResponse(HttpStatusCode.Conflict, Type = typeof(User))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> DeleteByKeyAsync([FromUri] string key, [FromUri] string timeStamp)
        {
            var result = await base.DeleteAsync(key, timeStamp).ConfigureAwait(true);
            //var httpCodeResult = result as System.Web.Http.Results.StatusCodeResult;
            //if (httpCodeResult != null && httpCodeResult.StatusCode == HttpStatusCode.NoContent)
            //{
            //    await topicManager.SendAsync(new OperationEvent<User>() { Entity = new User() { Key = key }, Operation = OperationType.DELETE }).ConfigureAwait(true);
            //}

            return result;
        }


        //[HttpGet]
        //[SwaggerResponse(HttpStatusCode.NotFound)]
        //[SwaggerResponse(HttpStatusCode.InternalServerError)]
        //[SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<User>))]
        //[SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(IEnumerable<object>))]
        //[APISearchRoute("users")]
        //public Task<IHttpActionResult> Validate([FromUri] UserQuery query)
        //{
        //    var result = base.QueryAsync(query, SerializationMode.Normal);

        //    return result;
        //}

    }
}