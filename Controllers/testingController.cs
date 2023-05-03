using StudentsRooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentsRooms.Controllers
{
    public class testingController : Controller
    {
        // GET: testing

        StudentsRoomsEntities db = new StudentsRoomsEntities();
        public ActionResult Index()
        {
            var PropertiesData = (from p in db.Property_Table
                               join o in db.Owner_Table on p.owner_id equals o.owner_id
                               select new mix { owner_name = o.owner_name, property_name = p.property_name, property_purpose = p.property_purpose, property_price = p.property_price, property_rent = p.property_rent, property_address = p.property_address, property_image1 = p.property_image1, property_image2 = p.property_image2, property_image3 = p.property_image3, property_image4 = p.property_image4, property_image5 = p.property_image5, property_parking = p.property_parking, property_wifi = p.property_wifi, property_ac = p.property_ac, property_water_supply = p.property_water_supply, property_window = p.property_window, property_video = p.property_video }).ToList();
            ViewBag.PropertiesJoinData = PropertiesData;
            return View();
        }
    }
}