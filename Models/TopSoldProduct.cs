using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsRooms.Models
{
    public class TopSoldProduct
    {
        public Property_Table product { get; set; }
        public int CountSold { get; set; }
    }
}