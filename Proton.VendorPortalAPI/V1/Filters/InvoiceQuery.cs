
namespace Proton.VendorPortalAPI.V1.Filters
{
    using Foundation.Core.Data;
    using Models;
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Implement query for INVOICE
    /// </summary>
    /// <seealso cref="Foundation.Core.Data.QueryObject{INVOICE}" />
    [AtLeastOneProperty("VendorCode", "CountryISO", "Pattern", ErrorMessage = "Cannot send empty query")]
    public class InvoiceQuery : QueryObject<INVOICE>
    {
        public InvoiceType InvoiceType { get; set; }
        /// <summary>
        /// Gets or sets the VendorCode.
        /// </summary>
        public string VendorCode { get; set; }

        /// <summary>
        /// Gets or sets the CountryISO.
        /// </summary>
        public string CountryISO { get; set; }

        /// <summary>
        /// Gets or sets the pattern for search in name, it is a Contains.
        /// </summary>
        public string Pattern { get; set; }

        public DateTime? FromUTCDate { get; set; }
        public DateTime? EndUTCDate { get; set; }

        /// <summary>
        /// Get the query expression.
        /// </summary>
        /// <returns>
        /// An instance of the expression tree of the instance.
        /// </returns>
        //public override Expression<Func<INVOICE, bool>> Query() => (x =>
        //                        (this.VendorCode == null || x.ACCNT_CODE == this.VendorCode) &&
        //                        (this.CountryISO == null || x.ISO_CODE_3 == this.CountryISO) &&
        //                        (this.FromUTCDate == null || x.DUE_DATETIME >= this.FromUTCDate) &&
        //                        (this.EndUTCDate == null || x.DUE_DATETIME <= this.EndUTCDate) 
        //                        //(this.Pattern == null || x.ACCNT_CODE.Contains(this.Pattern))
        //);

        public override Expression<Func<INVOICE, bool>> Query()
        {
            Expression<Func<INVOICE, bool>> predicate = PredicateBuilder.True<INVOICE>();
            Expression<Func<INVOICE, bool>> subPredicate = PredicateBuilder.True<INVOICE>();
            if (this.VendorCode != null)
            {
                subPredicate = subPredicate.And(x => x.VendorCode == this.VendorCode);
            }
            if (this.CountryISO != null)
            {
                subPredicate = subPredicate.And(x => x.CountryISOCode == this.CountryISO);
            }
            if (this.FromUTCDate != null)
            {
                subPredicate = subPredicate.And(x => x.InvoiceDate >= this.FromUTCDate);
            }
            if (this.EndUTCDate != null)
            {
                subPredicate = subPredicate.And(x => x.InvoiceDate <= this.EndUTCDate);
            }

            if (this.InvoiceType == InvoiceType.PAID)
            {
                subPredicate = subPredicate.And(x => (x.JournalType == "ZRENT" || x.JournalType == "ZPREP"
                       || x.JournalType.StartsWith("P") || x.JournalType.StartsWith("F"))
                       && (!(x.JournalType == "PAYR" || x.JournalType == "PEXPS")));
                subPredicate = subPredicate.And(x => x.Allocation == "A" || x.Allocation == "P");
            }


            else if (this.InvoiceType == InvoiceType.OPEN)
            {
                subPredicate = subPredicate.And(x => (x.JournalType == "ZRENT" || x.JournalType == "ZPREP"
                       || x.JournalType.StartsWith("P") || x.JournalType.StartsWith("F"))
                       && (!(x.JournalType == "PAYR" || x.JournalType == "PEXPS")));
                subPredicate = subPredicate.And((x => x.Allocation != "A" && x.Allocation != "P"));
            }

            else if (this.InvoiceType == InvoiceType.ALL)
            {
                subPredicate = subPredicate.And(x => (x.JournalType == "ZRENT" || x.JournalType == "ZPREP" || x.JournalSource =="PAYMT"
                       || x.JournalType.StartsWith("P") || x.JournalType.StartsWith("F") || x.JournalType.StartsWith("B") || x.JournalType.StartsWith("C"))
                       && (!(x.JournalType == "PAYR" || x.JournalType == "PEXPS")));
            }

            else if (this.InvoiceType == InvoiceType.PAYMENT)
            {
                subPredicate = subPredicate.And(x => (x.JournalType.StartsWith("B") || x.JournalType.StartsWith("C") || x.JournalType == "SYSTM")//|| x.JournalSource == "PAYMT")
                      );
                
            }
            predicate = predicate.And(subPredicate);

            return predicate;
        }
    }

    public enum InvoiceType
    {
       
       ALL,
       PAID,
       OPEN,
       PAYMENT
    }


}