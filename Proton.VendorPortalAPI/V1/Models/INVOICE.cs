//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proton.VendorPortalAPI.V1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class INVOICE
    {
        public string LegalEntityName { get; set; }
        public string BUCode { get; set; }
        public string CentreCode { get; set; }
        public string InvoiceReference { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string CurrencyId { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string Allocation { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string VendorCode { get; set; }
        public string Country { get; set; }
        public string JournalType { get; set; }
        public string JournalSource { get; set; }
        public string CountryISOCode { get; set; }
    }
}
