//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dblayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class StockItemReviewTable
    {
        public int ItemReviewID { get; set; }
        public int StockItemID { get; set; }
        public int ReviewBy_UserID { get; set; }
        public System.DateTime ReviewDateTime { get; set; }
        public int Rating { get; set; }
        public string ReviewDetails { get; set; }
    }
}
