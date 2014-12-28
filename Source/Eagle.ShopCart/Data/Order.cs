//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eagle.ShopCart.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int Order_Id { get; set; }
        public System.Guid Order_No { get; set; }
        public System.Guid Customer_No { get; set; }
        public System.DateTime OrderDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<decimal> Shipping_Rate { get; set; }
        public string Coupon_Code { get; set; }
        public Nullable<decimal> Coupon_Discount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalFees { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public string CurrencyCode { get; set; }
        public string Comment { get; set; }
        public Nullable<bool> OnlineOrderFlag { get; set; }
        public Nullable<bool> MarkAsRead { get; set; }
        public Nullable<int> OrderStatus { get; set; }
        public string IPLog { get; set; }
        public Nullable<System.DateTime> CreatedOnDate { get; set; }
        public Nullable<System.DateTime> LastModifiedOnDate { get; set; }
        public Nullable<System.Guid> LastModifiedByUserId { get; set; }
    }
}
