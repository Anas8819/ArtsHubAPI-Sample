//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_ItemDetail
    {
        public int ItemId { get; set; }
        public int ItemDetailId { get; set; }
        public string ItemImage { get; set; }
    
        public virtual tbl_Item tbl_Item { get; set; }
    }
}
