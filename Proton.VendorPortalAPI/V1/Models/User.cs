using Proton.Foundation.Core.Data;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Proton.VendorPortalAPI.V1.Models
{
    [Serializable]
    [JsonObject]
    public class User : IIdentityEntity
    {
        ///// <summary> User Id Unique Key </summary>
        //public string UserId { get; set; }
        /// <summary> User Name </summary>
        public string UserName { get; set; }
        /// <summary> Password </summary>
        public string Password { get; set; }
        /// <summary> Last Time Logon </summary>
        public Nullable<System.DateTime> LastLogon { get; set; }
        /// <summary> Is Active </summary>
        public bool IsActive { get; set; }
        /// <summary> User's ERP Type (SUN / PS) </summary>
        public string ERPType { get; set; }

        public string Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public DateTime? EditDate { get; set; }

        public List<ExternalReferenceInfo> ExternalReferenceIds { get; set; }

        public bool IsDeleted { get; set; }

        public string ModifiedBy { get; set; }

        public string SystemCaller { get; set; }

        public List<RestLink> Links { get; set; }

        public string TimeStamp { get; set; }

        ///// <summary>
        ///// Gets or sets the create date.
        ///// </summary>
        //public DateTime CreateDate { get; set; }

        ///// <summary>
        ///// Gets or sets the edit date.
        ///// </summary>
        //public DateTime EditDate { get; set; }

        ///// <summary>
        ///// Gets or sets the key that identify the graph od the Customer.
        ///// </summary>
        //public string Key { get; set; }

        ///// <summary>
        ///// Gets or sets a value indicating whether this instance is deleted.
        ///// </summary>
        //public bool? IsDeleted { get; set; }

        ///// <summary>
        ///// Gets or sets the links Hateos.
        ///// </summary>
        //public List<RestLink> Links { get; set; }

        ///// <summary>
        ///// Gets or sets the related system ids of this instance in each system.
        ///// </summary>
        //public List<ExternalReferenceInfo> ExternalReferenceIds { get; set; }

        ///// <summary>
        ///// Gets or sets the time stamp.
        ///// </summary>
        //public string TimeStamp { get; set; }
    }
}