//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentsRooms.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Review_Table
    {
        public int review_id { get; set; }
        public Nullable<int> tenant_id { get; set; }
        public Nullable<int> property_id { get; set; }
        public string review_comment { get; set; }
        public Nullable<int> review_rating { get; set; }
    }
}