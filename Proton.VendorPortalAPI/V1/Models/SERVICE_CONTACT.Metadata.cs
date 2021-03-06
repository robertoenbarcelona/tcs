﻿
namespace Proton.VendorPortalAPI.V1.Models
{
    using System;
    using System.Collections.Generic;
    using Foundation.Core.Data;
    using Newtonsoft.Json;
    using System.ComponentModel;
    using System.Reflection;

    [Serializable]
    [JsonObject]
    public partial class SERVICE_CONTACT : IIdentityEntity
    {
        public DateTime CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public DateTime? EditDate { get; set; }

        public List<ExternalReferenceInfo> ExternalReferenceIds { get; set; }

        public string Id { get; set; }

        public bool IsDeleted { get; set; }

        public List<RestLink> Links { get; set; }

        public string ModifiedBy { get; set; }

        public string SystemCaller { get; set; }

        public string TimeStamp { get; set; }
    }
}