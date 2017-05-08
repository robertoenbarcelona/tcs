
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
    //[AtLeastOneProperty("UserName", "Pattern", ErrorMessage ="Cannot send empty query")]
    public class UserQuery : QueryObject<User>
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Password { get; set; }

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
        public override Expression<Func<User, bool>> Query() => (x => 
                                (this.UserName == null || x.UserName == UserName) &&
                                (this.Password == null || x.Password == Password) &&
                                (this.Pattern == null || x.UserName.Contains(this.Pattern)));
    }
}