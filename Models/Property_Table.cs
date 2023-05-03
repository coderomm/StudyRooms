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
    
    public partial class Property_Table
    {
        public int property_id { get; set; }
        public Nullable<int> owner_id { get; set; }
        public string property_name { get; set; }
        public string property_purpose { get; set; }
        public string property_type { get; set; }
        public Nullable<decimal> property_price { get; set; }
        public Nullable<decimal> property_rent { get; set; }
        public string property_size { get; set; }
        public Nullable<int> property_room_no { get; set; }
        public Nullable<int> property_bathroom_no { get; set; }
        public Nullable<int> property_kitchen_no { get; set; }
        public Nullable<int> property_balcony_no { get; set; }
        public Nullable<decimal> property_age { get; set; }
        public string property_image1 { get; set; }
        public string property_image2 { get; set; }
        public string property_image3 { get; set; }
        public string property_image4 { get; set; }
        public string property_image5 { get; set; }
        public string property_address { get; set; }
        public Nullable<int> property_city { get; set; }
        public Nullable<int> property_district { get; set; }
        public Nullable<int> property_state { get; set; }
        public Nullable<int> property_postal_code { get; set; }
        public Nullable<bool> property_parking { get; set; }
        public Nullable<bool> property_wifi { get; set; }
        public Nullable<bool> property_ac { get; set; }
        public Nullable<bool> property_water_supply { get; set; }
        public Nullable<bool> property_window { get; set; }
        public string property_video { get; set; }
        public string property_map { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<System.DateTime> rts { get; set; }
    }
}