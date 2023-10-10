using StudentsRooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Net;
using System.Data.Entity.Validation;

namespace StudentsRooms.Controllers
{
    public class HomeController : BaseController
    {
        StudentsRoomsEntities db = new StudentsRoomsEntities();

        public ActionResult Index()
        {
            var PropertiesData = (from p in db.Property_Table
                                  join o in db.Owner_Table on p.owner_id equals o.owner_id
                                  join st in db.States on p.property_state equals st.StateId
                                  join dt in db.Districts on p.property_district equals dt.DistrictId
                                  join ct in db.Tehsiles on p.property_city equals ct.TehsilId
                                  select new mix
                                  {
                                      StateName = st.StateName,
                                      DistrictName = dt.DistrictName,
                                      TehsilName = ct.TehsilName,
                                      owner_name = o.owner_name,
                                      owner_mobile1 = o.owner_mobile1,
                                      owner_mobile2 = o.owner_mobile2,
                                      owner_email = o.owner_email,
                                      property_id = p.property_id,
                                      property_name = p.property_name,
                                      property_purpose = p.property_purpose,
                                      property_type = p.property_type,
                                      property_price = p.property_price,
                                      property_rent = p.property_rent,
                                      property_size = p.property_size,
                                      property_room_no = p.property_room_no,
                                      property_bathroom_no = p.property_bathroom_no,
                                      property_kitchen_no = p.property_kitchen_no,
                                      property_balcony_no = p.property_balcony_no,
                                      property_age = p.property_age,
                                      property_image1 = p.property_image1,
                                      property_image2 = p.property_image2,
                                      property_image3 = p.property_image3,
                                      property_image4 = p.property_image4,
                                      property_image5 = p.property_image5,
                                      property_address = p.property_address,
                                      property_city = p.property_city,
                                      property_district = p.property_district,
                                      property_state = p.property_state,
                                      property_postal_code = p.property_postal_code,
                                      property_parking = p.property_parking,
                                      property_wifi = p.property_wifi,
                                      property_ac = p.property_ac,
                                      property_water_supply = p.property_water_supply,
                                      property_window = p.property_window,
                                      property_video = p.property_video,
                                      property_map = p.property_map,
                                      rts = p.rts
                                  }).ToList();


            var LandlordCount = db.Owner_Table.Count();
            var SalepropertyCount = db.Property_Table.Where(x => x.property_purpose == "Sale").Count();
            var RentpropertyCount = db.Property_Table.Where(x => x.property_purpose == "Rent").Count();

            ViewBag.LandlordCount = LandlordCount;
            ViewBag.SalepropertyCount = SalepropertyCount;
            ViewBag.RentpropertyCount = RentpropertyCount;

            ViewBag.PropertiesJoinData = PropertiesData;

            var footer_listings= db.Property_Table.ToList().Take(4);
            ViewBag.footer_listings = footer_listings;

            //ViewBag.TopRatedProducts = TopSoldProducts();

            return View();
        }

        public JsonResult StateAutoComplete(string prefix)
        {
            var customers = (from customer in db.States
                             where customer.StateName.Contains(prefix)
                             select new
                             {
                                 label = customer.StateName,
                                 val = customer.StateId
                             }).ToList();
            return Json(customers);
        }
            
        public JsonResult CityAutoComplete(string prefix)
        {
            var customers = (from customer in db.Tehsiles
                             where customer.TehsilName.Contains(prefix)
                             select new
                             {
                                 label = customer.TehsilName,
                                 val = customer.TehsilId
                             }).ToList();
            return Json(customers);
        }

        public JsonResult AutoComplete(string prefix)
        {
            var customers = (from customer in db.Districts
                             where customer.DistrictName.Contains(prefix)
                             select new
                             {
                                 label = customer.DistrictName,
                                 val = customer.DistrictId
                             }).ToList();
            return Json(customers);
        }

        public ActionResult PropertyDetail(int id)
        {
            var PropertiesData = (from p in db.Property_Table
                                  join o in db.Owner_Table on p.owner_id equals o.owner_id
                                  join st in db.States on p.property_state equals st.StateId
                                  join dt in db.Districts on p.property_district equals dt.DistrictId
                                  join ct in db.Tehsiles on p.property_city equals ct.TehsilId
                                  select new mix
                                  {
                                      StateName = st.StateName,
                                      DistrictName = dt.DistrictName,
                                      TehsilName = ct.TehsilName,
                                      owner_name = o.owner_name,
                                      owner_mobile1 = o.owner_mobile1,
                                      owner_mobile2 = o.owner_mobile2,
                                      owner_email = o.owner_email,
                                      property_name = p.property_name,
                                      property_purpose = p.property_purpose,
                                      property_type = p.property_type,
                                      property_price = p.property_price,
                                      property_rent = p.property_rent,
                                      property_size = p.property_size,
                                      property_room_no = p.property_room_no,
                                      property_bathroom_no = p.property_bathroom_no,
                                      property_kitchen_no = p.property_kitchen_no,
                                      property_balcony_no = p.property_balcony_no,
                                      property_age = p.property_age,
                                      property_image1 = p.property_image1,
                                      property_image2 = p.property_image2,
                                      property_image3 = p.property_image3,
                                      property_image4 = p.property_image4,
                                      property_image5 = p.property_image5,
                                      property_address = p.property_address,
                                      property_city = p.property_city,
                                      property_district = p.property_district,
                                      property_state = p.property_state,
                                      property_postal_code = p.property_postal_code,
                                      property_parking = p.property_parking,
                                      property_wifi = p.property_wifi,
                                      property_ac = p.property_ac,
                                      property_water_supply = p.property_water_supply,
                                      property_window = p.property_window,
                                      property_video = p.property_video,
                                      property_map = p.property_map
                                  }).ToList();
            ViewBag.PropertiesJoinData = PropertiesData;

            Property_Table pr = db.Property_Table.Where(x => x.property_id == id).SingleOrDefault();
            ViewBag.cityname = db.Tehsiles.Where(x => x.TehsilId == pr.property_city).Select(x => x.TehsilName).SingleOrDefault();
            ViewBag.districtname = db.Districts.Where(x => x.DistrictId == pr.property_district).Select(x => x.DistrictName).SingleOrDefault();
            ViewBag.statename = db.States.Where(x => x.StateId == pr.property_state).Select(x => x.StateName).SingleOrDefault();
            ViewBag.gmap = db.Property_Table.Where(x => x.property_id == id).Select(x => x.property_map).SingleOrDefault();

            ViewBag.ownername = db.Owner_Table.Where(x => x.owner_id == pr.owner_id).Select(x => x.owner_name).SingleOrDefault();
            ViewBag.ownermob1 = db.Owner_Table.Where(x => x.owner_id == pr.owner_id).Select(x => x.owner_mobile1).SingleOrDefault();
            ViewBag.ownermob2 = db.Owner_Table.Where(x => x.owner_id == pr.owner_id).Select(x => x.owner_mobile2).SingleOrDefault();

            var reviews = db.Review_Table.Where(r => r.property_id == id).ToList();
            ViewBag.displayReviews = reviews;
            return View(pr);
        }

        [HttpPost]
        public ActionResult PropertyDetail(Review_Table rt, int id)
        {
            if (Session["UserId"] != null)
            {
                Review_Table Review_Table = new Review_Table();
                rt.user_id = Convert.ToInt16(Session["UserId"]);
                rt.property_id = id;
                rt.review_rts = DateTime.Now;
                Review_Table.review_rating = rt.review_rating;
                db.Review_Table.Add(rt);
                db.SaveChanges();
                return RedirectToAction("PropertyDetail");

            }
            return RedirectToAction("Login", "User");
        }
    }
}