//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eagle.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vendor_Currency
    {
        public int VendorCurrencyId { get; set; }
        public int VendorId { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public Nullable<int> Decimals { get; set; }
        public string DecimalSymbol { get; set; }
        public string ThousandSeparator { get; set; }
        public string PositiveFormat { get; set; }
        public string NegativeFormat { get; set; }
    }
}
