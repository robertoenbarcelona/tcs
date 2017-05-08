
namespace Proton.VendorPortalAPI.V1.Filters
{
    using Foundation.Core.Data;
    using Models;
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Implement query for VENDOR
    /// </summary>
    /// <seealso cref="Foundation.Core.Data.QueryObject{VENDOR}" />
    [AtLeastOneProperty("Email", "Pattern", ErrorMessage ="Cannot send empty query")]
    public class VendorQuery : QueryObject<VENDOR>
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

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
        public override Expression<Func<VENDOR, bool>> Query() => (x => 
                                (this.Email == null || x.Email.Contains(this.Email)) &&
                                (this.Pattern == null || x.Email.Contains(this.Pattern)));

    }
}