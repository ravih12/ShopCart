//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopCart
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCustomerDetail
    {
        public int customerId { get; set; }
        public string customerName { get; set; }
        public string customerAddress { get; set; }
        public string mobile { get; set; }
        public Nullable<decimal> totalAmount { get; set; }
        public int shoppingCartId { get; set; }
    }
}