
namespace Proton.VendorPortalAPI.V1.Filters
{
    using Foundation.Core.Data;
    using Models;
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Implement query for User
    /// </summary>
    /// <seealso cref="Foundation.Core.Data.QueryObject{User}" />
    [AtLeastOneProperty("UserId", "Pattern", ErrorMessage ="Cannot send empty query")]
    public class UserHistoryQuery : QueryObject<UserHistory>
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the pattern for search in name, it is a Contains.
        /// </summary>
        public string Pattern { get; set; }


        /// <summary>
        /// Get the query expression.
        /// </summary>
        /// <returns>
        /// An instance of the expression tree of the instance.
        /// </returns>
        public override Expression<Func<UserHistory, bool>> Query() => (x => 
                                (this.UserId == null || x.UserId == this.UserId) &&
                                (this.Pattern == null || x.UserId.Contains(this.Pattern)));
    }
}