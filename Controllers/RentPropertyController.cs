using StudentsRooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentsRooms.Controllers
{
    public class RentPropertyController : Controller
    {
        // GET: RentProperty
        public ActionResult Index(string SortBy, string DistrictId, string property_type, string property_room_no)
        {
            StudentsRoomsEntities db = new StudentsRoomsEntities();

            if (Request.QueryString.Count > 0 && Request.QueryString["DistrictId"] != null)
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
                                      }).Where(x => x.property_purpose == "Rent").ToList();

                ViewBag.PropertiesJoinData = PropertiesData;

                var Properties = PropertiesData.Where(p => p.property_district.ToString() == DistrictId).ToList();

                if (!string.IsNullOrEmpty(property_type))
                {
                    if (property_type == "1")
                    {
                        Properties = Properties.Where(p => p.property_type == property_type).ToList();
                    }
                    if (property_type == "2")
                    {
                        Properties = Properties.Where(p => p.property_type == property_type).ToList();
                    }
                    if (property_type == "3")
                    {
                        Properties = Properties.Where(p => p.property_type == property_type).ToList();
                    }
                    if (property_type == "4")
                    {
                        Properties = Properties.Where(p => p.property_type == property_type).ToList();
                    }
                }

                if (!string.IsNullOrEmpty(property_room_no))
                {
                    if (property_room_no == "1")
                    {
                        Properties = Properties.Where(p => p.property_room_no.ToString() == property_room_no).ToList();
                    }
                    if (property_room_no == "2")
                    {
                        Properties = Properties.Where(p => p.property_room_no.ToString() == property_room_no).ToList();
                    }
                    if (property_room_no == "3")
                    {
                        Properties = Properties.Where(p => p.property_room_no.ToString() == property_room_no).ToList();
                    }
                    if (property_room_no == "4")
                    {
                        Properties = Properties.Where(p => p.property_room_no.ToString() == property_room_no).ToList();
                    }
                    if (property_room_no == "5")
                    {
                        Properties = Properties.Where(p => p.property_room_no.ToString() == property_room_no).ToList();
                    }
                }
                ViewBag.PropertiesJoinData = Properties;

                switch (SortBy)
                {
                    case "Recent":
                        PropertiesData = PropertiesData.OrderBy(p => p.rts).ToList();
                        break;
                    case "Oldest":
                        PropertiesData = PropertiesData.OrderByDescending(p => p.rts).ToList();
                        break;
                    case "LowToHigh":
                        PropertiesData = PropertiesData.OrderBy(p => p.property_price).ToList();
                        break;
                    case "HighToLow":
                        PropertiesData = PropertiesData.OrderByDescending(p => p.property_name).ToList();
                        break;
                    default:
                        PropertiesData = PropertiesData.OrderBy(p => p.property_price).ToList();
                        break;
                }
                return View();
            }
            else
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
                                      }).Where(x => x.property_purpose == "Rent").ToList();
                ViewBag.PropertiesJoinData = PropertiesData;

                switch (SortBy)
                {
                    case "Recent":
                        PropertiesData = PropertiesData.OrderBy(p => p.rts).ToList();
                        break;
                    case "Oldest":
                        PropertiesData = PropertiesData.OrderByDescending(p => p.rts).ToList();
                        break;
                    case "LowToHigh":
                        PropertiesData = PropertiesData.OrderBy(p => p.property_price).ToList();
                        break;
                    case "HighToLow":
                        PropertiesData = PropertiesData.OrderByDescending(p => p.property_name).ToList();
                        break;
                    default:
                        PropertiesData = PropertiesData.OrderBy(p => p.property_price).ToList();
                        break;
                }
                return View();
            }
        }
    }
}