
namespace Proton.VendorPortalAPI.V1.Filters
{
    using Foundation.Core.Data;
    using Models;
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Implement query for SERVICE_CONTACT
    /// </summary>
    /// <seealso cref="Foundation.Core.Data.QueryObject{SERVICE_CONTACT}" />
    [AtLeastOneProperty("CountryISOCode", "Pattern", ErrorMessage ="Cannot send empty query")]
    public class ServiceContactQuery : QueryObject<SERVICE_CONTACT>
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string CountryISOCode { get; set; }

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
        public override Expression<Func<SERVICE_CONTACT, bool>> Query() => (x => 
                                (this.CountryISOCode == null || x.CountryISOCode == this.CountryISOCode) &&
                                (this.Pattern == null || x.CountryISOCode.Contains(this.Pattern)));
    }
}