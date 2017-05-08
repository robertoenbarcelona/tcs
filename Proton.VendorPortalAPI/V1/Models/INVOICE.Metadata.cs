
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
    public partial class INVOICE : IIdentityEntity
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
        public string StatusDescription
        {
            get
            {
                if (Allocation == "A" || Allocation == "P")
                    return GetEnumDescription(InvoiceStatus.PAID);
                else if (Allocation == "T" || Allocation == "4")
                    return GetEnumDescription(InvoiceStatus.PAYMENTINPROGRESS);
                else if (Allocation == "W")
                    return GetEnumDescription(InvoiceStatus.HOLD);
                //else if (JournalType == "PICRE")
                //    return GetEnumDescription(InvoiceStatus.CREDITNOTE);
                else if (DueDate > DateTime.UtcNow)
                    return GetEnumDescription(InvoiceStatus.NOTYETDUE);
                else if ((string.IsNullOrEmpty(Allocation) || Allocation == " ") && DueDate < DateTime.UtcNow)
                    return GetEnumDescription(InvoiceStatus.DUEFORPAYMENT);
                else return GetEnumDescription(InvoiceStatus.INVALID);
            }
        }
        public InvoiceStatus StatusId
        {
            get
            {
                if (Allocation == "A" || Allocation == "P")
                    return InvoiceStatus.PAID;
                else if (Allocation == "T" || Allocation == "4")
                    return InvoiceStatus.PAYMENTINPROGRESS;
                else if (Allocation == "W")
                    return InvoiceStatus.HOLD;
                //else if (JournalType == "PICRE")
                //    return InvoiceStatus.CREDITNOTE;
                else if (DueDate > DateTime.UtcNow)
                    return InvoiceStatus.NOTYETDUE;
                else if ((string.IsNullOrEmpty(Allocation) || Allocation == " ") && DueDate < DateTime.UtcNow)
                    return InvoiceStatus.DUEFORPAYMENT;
                else return InvoiceStatus.INVALID;
            }
        }
        private string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
    public enum InvoiceStatus
    {
        [Description("Invalid")]
        INVALID = -1,
        [Description("Paid")]
        PAID,
        [Description("Payment In Progress")]
        PAYMENTINPROGRESS,
        [Description("Not Yet Due")]
        NOTYETDUE,
        [Description("Due For Payment")]
        DUEFORPAYMENT,
        [Description("Credit Note")]
        CREDITNOTE,
        [Description("Hold")]
        HOLD
    }
}