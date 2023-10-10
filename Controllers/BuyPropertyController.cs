using StudentsRooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentsRooms.Controllers
{
    public class BuyPropertyController : BaseController
    {
        StudentsRoomsEntities db = new StudentsRoomsEntities();

        // GET: BuyProperty
        public ActionResult Index(string Sort, string property_type, string property_state, string property_district, string property_city, string property_kitchen_no, string property_balcony_no, string property_bathroom_no, string property_room_no, string min_area, string max_area, string min_price, string max_price)
        {
            if (Request.QueryString.Count == 0)
            {
                var PropertiesData = (from p in db.Property_Table
                                      join o in db.Owner_Table on p.owner_id equals o.owner_id
                                      join st in db.States on p.property_state equals st.StateId
                                      join dt in db.Districts on p.property_district equals dt.DistrictId
                                      join ct in db.Tehsiles on p.property_city equals ct.TehsilId
                                      where p.property_purpose == "Sale"
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

                List<State> StateList = db.States.ToList();
                ViewBag.StateList = new SelectList(db.States.ToList().Select(x => new { StateId = x.StateId, StateName = x.StateName }), "StateId", "StateName", Request.QueryString["property_state"]);

                List<District> DistrictList = db.Districts.ToList();
                ViewBag.DistrictList = new SelectList(db.Districts.ToList().Select(x => new { DistrictId = x.DistrictId, DistrictName = x.DistrictName }), "DistrictId", "DistrictName", Request.QueryString["property_district"]);

                List<Tehsile> TehsileList = db.Tehsiles.ToList();
                ViewBag.TehsileList = new SelectList(db.Tehsiles.ToList().Select(x => new { TehsilId = x.TehsilId, TehsilName = x.TehsilName }), "TehsilId", "TehsilName", Request.QueryString["property_city"]);

                ViewBag.PropertiesJoinData = PropertiesData;
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
                                      }).Where(x => x.property_purpose == "Sale").ToList();

                if (property_state == null || property_state == "" && property_type != "" && property_type != "0")
                {
                    if (!string.IsNullOrEmpty(property_type) || property_type != "0")
                    {
                        if (property_type == "1")
                        {
                            var Properties = PropertiesData.Where(p => p.property_type == property_type).ToList();
                        }
                        if (property_type == "2")
                        {
                            var Properties = PropertiesData.Where(p => p.property_type == property_type).ToList();
                        }
                        if (property_type == "3")
                        {
                            var Properties = PropertiesData.Where(p => p.property_type == property_type).ToList();
                        }
                        if (property_type == "4")
                        {
                            var Properties = PropertiesData.Where(p => p.property_type == property_type).ToList();
                        }
                    }
                }
                else
                {
                    var Properties = PropertiesData.Where(p => p.property_state.ToString() == property_state).ToList();
                    Properties = PropertiesData.Where(p => p.property_district.ToString() == property_district).ToList();
                    Properties = PropertiesData.Where(p => p.property_city.ToString() == property_city).ToList();

                    if (!string.IsNullOrEmpty(property_type) || property_type != "0")
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
                    if (!string.IsNullOrEmpty(property_room_no) || property_room_no != "0")
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
                    if (!string.IsNullOrEmpty(property_bathroom_no) || property_bathroom_no != "0")
                    {
                        if (property_bathroom_no == "1")
                        {
                            Properties = Properties.Where(p => p.property_bathroom_no.ToString() == property_bathroom_no).ToList();
                        }
                        if (property_bathroom_no == "2")
                        {
                            Properties = Properties.Where(p => p.property_bathroom_no.ToString() == property_bathroom_no).ToList();
                        }
                        if (property_bathroom_no == "3")
                        {
                            Properties = Properties.Where(p => p.property_bathroom_no.ToString() == property_bathroom_no).ToList();
                        }
                        if (property_bathroom_no == "4")
                        {
                            Properties = Properties.Where(p => p.property_bathroom_no.ToString() == property_bathroom_no).ToList();
                        }
                        if (property_bathroom_no == "5")
                        {
                            Properties = Properties.Where(p => p.property_bathroom_no.ToString() == property_bathroom_no).ToList();
                        }
                    }
                    if (!string.IsNullOrEmpty(property_kitchen_no) || property_kitchen_no != "0")
                    {
                        if (property_kitchen_no == "1")
                        {
                            Properties = Properties.Where(p => p.property_kitchen_no.ToString() == property_kitchen_no).ToList();
                        }
                        if (property_kitchen_no == "2")
                        {
                            Properties = Properties.Where(p => p.property_kitchen_no.ToString() == property_kitchen_no).ToList();
                        }
                        if (property_kitchen_no == "3")
                        {
                            Properties = Properties.Where(p => p.property_kitchen_no.ToString() == property_kitchen_no).ToList();
                        }
                    }
                    if (!string.IsNullOrEmpty(property_balcony_no) || property_balcony_no != "0")
                    {
                        if (property_balcony_no == "1")
                        {
                            Properties = Properties.Where(p => p.property_balcony_no.ToString() == property_balcony_no).ToList();
                        }
                        if (property_balcony_no == "2")
                        {
                            Properties = Properties.Where(p => p.property_balcony_no.ToString() == property_balcony_no).ToList();
                        }
                        if (property_balcony_no == "3")
                        {
                            Properties = Properties.Where(p => p.property_balcony_no.ToString() == property_balcony_no).ToList();
                        }
                    }

                    if (Sort == "Smaller")
                    {
                        Properties = Properties.OrderBy(p => p.property_size).ToList();
                    }
                    if (Sort == "Larger")
                    {
                        Properties = Properties.OrderByDescending(p => p.property_size).ToList();
                    }
                    if (Sort == "Low To High")
                    {
                        Properties = Properties.OrderBy(p => p.property_price).ToList();
                    }
                    if (Sort == "High To Low")
                    {
                        Properties = Properties.OrderByDescending(p => p.property_price).ToList();
                    }
                    if (Request.QueryString["min_area"] != null)
                    {
                        Properties = Properties.Where(p => Convert.ToInt32(p.property_size) >= Convert.ToInt32(min_area)).ToList();
                    }
                    if (Request.QueryString["max_area"] != null)
                    {
                        Properties = Properties.Where(p => Convert.ToInt32(p.property_size) <= Convert.ToInt32(max_area)).ToList();
                    }
                    if (Request.QueryString["min_price"] != null)
                    {
                        Properties = Properties.Where(p => Convert.ToInt32(p.property_price) >= Convert.ToInt32(min_price)).ToList();
                    }
                    if (Request.QueryString["max_price"] != null)
                    {
                        Properties = Properties.Where(p => Convert.ToInt32(p.property_price) <= Convert.ToInt32(max_price)).ToList();
                    }
                    ViewBag.PropertiesJoinData = Properties;

                }

                List<State> StateList = db.States.ToList();
                ViewBag.StateList = new SelectList(db.States.ToList().Select(x => new { StateId = x.StateId, StateName = x.StateName }), "StateId", "StateName", Request.QueryString["property_state"]);

                List<District> DistrictList = db.Districts.ToList();
                ViewBag.DistrictList = new SelectList(db.Districts.ToList().Select(x => new { DistrictId = x.DistrictId, DistrictName = x.DistrictName }), "DistrictId", "DistrictName", Request.QueryString["property_district"]);

                List<Tehsile> TehsileList = db.Tehsiles.ToList();
                ViewBag.TehsileList = new SelectList(db.Tehsiles.ToList().Select(x => new { TehsilId = x.TehsilId, TehsilName = x.TehsilName }), "TehsilId", "TehsilName", Request.QueryString["property_city"]);

                return View();
            }
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
                                      property_map = p.property_map,
                                      rts = p.rts
                                  }).ToList();
            ViewBag.PropertiesJoinData = PropertiesData;

            Property_Table pr = db.Property_Table.Where(x => x.property_id == id).SingleOrDefault();
            ViewBag.cityname = db.Tehsiles.Where(x => x.TehsilId == pr.property_city).Select(x => x.TehsilName).SingleOrDefault();
            ViewBag.districtname = db.Districts.Where(x => x.DistrictId == pr.property_district).Select(x => x.DistrictName).SingleOrDefault();
            ViewBag.statename = db.States.Where(x => x.StateId == pr.property_state).Select(x => x.StateName).SingleOrDefault();

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
        public JsonResult DistrictList(int Id)
        {
            var categories = (from b in db.Districts where b.StateId == Id select b).ToList();
            return Json(new SelectList(categories, "DistrictId", "DistrictName"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult TehsilList(int Id)
        {
            var categorieslist = (from c in db.Tehsiles where c.DistrictId == Id select c).ToList();
            return Json(new SelectList(categorieslist, "TehsilId", "TehsilName"), JsonRequestBehavior.AllowGet);
        }
    }

}
