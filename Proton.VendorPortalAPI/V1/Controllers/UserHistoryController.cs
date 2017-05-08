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
    [ControllerName("userhistories")]
    public class UserHistoryController : WritableAsyncController<UserHistory>
    {
        private readonly ITopicManager<UserHistory> topicManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserHistoryController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="topicManager">The topic manager.</param>
        /// <param name="policy"></param>
        public UserHistoryController(IWritableServiceAsync<UserHistory> service, IUnitOfWorkAsync unitOfWork, ISerializationConfigurePolicy<UserHistory> policy, ITopicManager<UserHistory> topicManager, ILogger logger) : base(service, unitOfWork, policy, logger)
        {
            this.topicManager = topicManager;
        }

        [HttpGet]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<UserHistory>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(IEnumerable<object>))]
        [APISearchRoute("userhistories")]
        public Task<IHttpActionResult> GetAll([FromUri] UserHistoryQuery query) => base.QueryAsync(query, SerializationMode.Info);


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
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(UserHistory))]
        [APIPostRoute("userhistories")]
        public async Task<IHttpActionResult> CreateAsync([FromBody]UserHistory entity)
        {
            var result = await base.PostAsync(entity).ConfigureAwait(true);
            //var created = result as CreatedContentActionResult<UserHistory>;
            //if (created != null)
            //{
            //    await topicManager.SendAsync(new OperationEvent<UserHistory>() { Entity = created.Entity, Operation = OperationType.POST }).ConfigureAwait(false);
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
        [APIBaseRoute("userhistories")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(UserHistory))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(IEnumerable<object>))]
        [SwaggerResponse(HttpStatusCode.Conflict, Type = typeof(UserHistory))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> UpdateAsync([FromUri] string key, [FromBody]UserHistory entity)
        {
            var result = await base.PutAsync(key, entity).ConfigureAwait(true);
            //var updated = result as GetContentActionResult<UserHistory>;
            //if (updated != null)
            //{
            //    await topicManager.SendAsync(new OperationEvent<UserHistory>() { Entity = updated.Entity, Operation = OperationType.PUT }).ConfigureAwait(false);
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
        [SwaggerResponse(HttpStatusCode.Conflict, Type = typeof(UserHistory))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> DeleteByKeyAsync([FromUri] string key, [FromUri] string timeStamp)
        {
            var result = await base.DeleteAsync(key, timeStamp).ConfigureAwait(true);
            //var httpCodeResult = result as System.Web.Http.Results.StatusCodeResult;
            //if (httpCodeResult != null && httpCodeResult.StatusCode == HttpStatusCode.NoContent)
            //{
            //    await topicManager.SendAsync(new OperationEvent<UserHistory>() { Entity = new UserHistory() { Key = key }, Operation = OperationType.DELETE }).ConfigureAwait(true);
            //}

            return result;
        }
    }
}