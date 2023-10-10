using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using StudentsRooms.Models;

namespace StudentsRooms.Controllers
{
    //[Authorize]
    public class LandlordController : BaseController
    {
        StudentsRoomsEntities db = new StudentsRoomsEntities();
        // GET: Landlord
        public ActionResult Dashboard()
        {
            if (Session["UserID"] != null)
            {
                int owner_id = Convert.ToInt32(Session["UserID"]);
                Owner_Table ot = db.Owner_Table.Where(x => x.owner_id == owner_id).SingleOrDefault();
                return View(ot);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        public ActionResult Dashboard(Owner_Table obj, int owner_id)
        {
            if (ModelState.IsValid)
            {
                Owner_Table ot = db.Owner_Table.Where(x => x.owner_id == owner_id).SingleOrDefault();

                var isMobile1AlreadyExists = db.Owner_Table.Any(x => x.owner_mobile1 == obj.owner_mobile1 && x.owner_id != obj.owner_id);
                var isMobile2AlreadyExists = db.Owner_Table.Any(x => x.owner_mobile2 == obj.owner_mobile2 && x.owner_id != obj.owner_id);
                var isEmail1AlreadyExists = db.Owner_Table.Any(x => x.owner_email == obj.owner_email && x.owner_id != obj.owner_id);

                if (isMobile1AlreadyExists)
                {
                    ModelState.AddModelError("owner_mobile1", "User with this mobile already exists");
                    return View(obj);
                }
                if (isMobile2AlreadyExists)
                {
                    ModelState.AddModelError("owner_mobile2", "User with this mobile 2 already exists");
                    return View(obj);
                }
                if (isEmail1AlreadyExists)
                {
                    ModelState.AddModelError("owner_email", "User with this email already exists");
                    return View(obj);
                }
                ot.owner_name = obj.owner_name;
                ot.owner_mobile1 = obj.owner_mobile1;
                ot.owner_mobile2 = obj.owner_mobile2;
                ot.owner_email = obj.owner_email;
                ot.status = 1.ToString();
                ot.rts = (DateTime.Now).ToString();

                db.SaveChanges();
            }
            return View();
        }

        public ActionResult UserProperties()
        {
            List<Property_Table> list = db.Property_Table.OrderByDescending(p => p.owner_id).ToList();
            Property_Table ptt = new Property_Table();
            Session["PID"] = ptt.property_id;
            var pid = Session["PID"];
            return View(list);
        }

        public ActionResult UpdateProperty(int id)
        {
            if (Session["UserID"] != null)
            {
                int owner_id = Convert.ToInt32(Session["UserID"]);
                Property_Table ot = db.Property_Table.Where(x => x.property_id == id).SingleOrDefault();

                var PropertiesData = (from p in db.Property_Table where p.property_id == id select p).ToList();
                ViewBag.PropertiesJoinData = PropertiesData;

                List<State> StateList = db.States.ToList();
                ViewBag.StateList = new SelectList(db.States.ToList().Select(x => new { StateId = x.StateId, StateName = x.StateName }), "StateId", "StateName", ot.property_state);

                List<District> DistrictList = db.Districts.ToList();
                ViewBag.DistrictList = new SelectList(db.Districts.ToList().Select(x => new { DistrictId = x.DistrictId, DistrictName = x.DistrictName }), "DistrictId", "DistrictName", ot.property_district);

                List<Tehsile> TehsileList = db.Tehsiles.ToList();
                ViewBag.TehsileList = new SelectList(db.Tehsiles.ToList().Select(x => new { TehsilId = x.TehsilId, TehsilName = x.TehsilName }), "TehsilId", "TehsilName", ot.property_city);

                //List<SelectListItem> options = new List<SelectListItem>
                //{
                //    new SelectListItem { Text = "Sale", Value = "Sale" },
                //    new SelectListItem { Text = "Rent", Value = "Rent" }
                //};
                //ViewBag.selectPurpose = new SelectList(options, "Value", "Text");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult UpdateProperty(Property_Table property, HttpPostedFileBase property_video, HttpPostedFileBase property_image1, HttpPostedFileBase property_image2, HttpPostedFileBase property_image3, HttpPostedFileBase property_image4, HttpPostedFileBase property_image5)

        {
            if (Session["UserID"] != null)
            {
                Property_Table pt = db.Property_Table.Where(x => x.property_id == property.property_id).SingleOrDefault();
                pt.property_name = property.property_name;
                pt.property_purpose = property.property_purpose;
                if (property.property_purpose == "Sale")
                {
                    pt.property_price = property.property_price;
                    pt.property_rent = null;
                }
                if (property.property_purpose == "Rent")
                {
                    pt.property_price = null;
                    pt.property_rent = property.property_rent;
                }
                pt.property_type = property.property_type;
                pt.property_size = property.property_size;
                pt.property_room_no = property.property_room_no;
                pt.property_bathroom_no = property.property_bathroom_no;
                pt.property_kitchen_no = property.property_kitchen_no;
                pt.property_balcony_no = property.property_balcony_no;
                pt.property_age = property.property_age;
                pt.property_address = property.property_address;
                pt.property_city = property.property_city;
                pt.property_district = property.property_district;
                pt.property_state = property.property_state;
                pt.property_postal_code = property.property_postal_code;

                pt.property_parking = property.property_parking;
                pt.property_wifi = property.property_wifi;
                pt.property_ac = property.property_ac;
                pt.property_water_supply = property.property_water_supply;
                pt.property_window = property.property_window;
                pt.property_map = property.property_map;
                pt.status = true;
                pt.rts = DateTime.Now;
                pt.owner_id = Convert.ToInt32(Session["UserID"]);

                if (property_image1 != null && property_image1.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(property_image1.FileName);
                    property_image1.SaveAs(Path.Combine(Server.MapPath("~/Content/images/propertyImages"), fileName));
                    pt.property_image1 = fileName;
                }

                if (property_image2 != null && property_image2.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(property_image2.FileName);
                    property_image2.SaveAs(Path.Combine(Server.MapPath("~/Content/images/propertyImages"), fileName));
                    pt.property_image2 = fileName;
                }

                if (property_image3 != null && property_image3.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(property_image3.FileName);
                    property_image3.SaveAs(Path.Combine(Server.MapPath("~/Content/images/propertyImages"), fileName));
                    pt.property_image3 = fileName;
                }

                if (property_image4 != null && property_image4.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(property_image4.FileName);
                    property_image4.SaveAs(Path.Combine(Server.MapPath("~/Content/images/propertyImages"), fileName));
                    pt.property_image4 = fileName;
                }

                if (property_image5 != null && property_image5.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(property_image5.FileName);
                    property_image5.SaveAs(Path.Combine(Server.MapPath("~/Content/images/propertyImages"), fileName));
                    pt.property_image5 = fileName;
                }

                if (property_video != null)
                {
                    string fileName = Path.GetFileName(property_video.FileName);
                    property_video.SaveAs(Path.Combine(Server.MapPath("~/Content/VideoFileUpload"), fileName));
                    pt.property_video = fileName;
                }

                db.SaveChanges();
                return RedirectToAction("UpdateProperty", "Landlord", new { id = property.property_id });
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult AddProperty()
        {
            if (Session["UserID"] != null)
            {
                List<State> stateList = db.States.ToList();
                ViewBag.stateList = new SelectList(stateList, "StateId", "StateName");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AddProperty(Property_Table property, HttpPostedFileBase property_video, HttpPostedFileBase property_image1, HttpPostedFileBase property_image2, HttpPostedFileBase property_image3, HttpPostedFileBase property_image4, HttpPostedFileBase property_image5)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (property_image1 != null && property_image1.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(property_image1.FileName);
                        property_image1.SaveAs(Path.Combine(Server.MapPath("~/Content/images/propertyImages"), fileName));
                        property.property_image1 = fileName;
                    }

                    if (property_image2 != null && property_image2.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(property_image2.FileName);
                        property_image2.SaveAs(Path.Combine(Server.MapPath("~/Content/images/propertyImages"), fileName));
                        property.property_image2 = fileName;
                    }

                    if (property_image3 != null && property_image3.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(property_image3.FileName);
                        property_image3.SaveAs(Path.Combine(Server.MapPath("~/Content/images/propertyImages"), fileName));
                        property.property_image3 = fileName;
                    }

                    if (property_image4 != null && property_image4.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(property_image4.FileName);
                        property_image4.SaveAs(Path.Combine(Server.MapPath("~/Content/images/propertyImages"), fileName));
                        property.property_image4 = fileName;
                    }

                    if (property_image5 != null && property_image5.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(property_image5.FileName);
                        property_image5.SaveAs(Path.Combine(Server.MapPath("~/Content/images/propertyImages"), fileName));
                        property.property_image5 = fileName;
                    }

                    if (property_video != null)
                    {
                        string fileName = Path.GetFileName(property_video.FileName);
                        property_video.SaveAs(Path.Combine(Server.MapPath("~/Content/VideoFileUpload"), fileName));
                        property.property_video = fileName;
                    }

                    int userId = Convert.ToInt16(Session["UserId"]);
                    property.owner_id = userId;
                    property.status = true;
                    property.rts = DateTime.Now;

                    db.Property_Table.Add(property);
                    db.SaveChanges();
                    return RedirectToAction("AddProperty");
                }

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return View();
        }

        public ActionResult DeleteProperty(int id)
        {
            db.Property_Table.Remove(db.Property_Table.Where(x => x.property_id == id).SingleOrDefault());
            db.SaveChanges();
            return RedirectToAction("UserProperties");
        }

        public ActionResult changePassword()
        {
            if (Session["UserID"] != null)
            {
                //int owner_id = Convert.ToInt32(Session["UserID"]);
                //Owner_Table ot = db.Owner_Table.Where(x => x.owner_id == owner_id).SingleOrDefault();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
        [HttpPost]
        public ActionResult changePassword(string current_owner_password, string owner_password, string confirm_owner_password)
        {
            int owner_id = Convert.ToInt32(Session["UserID"]);
            Owner_Table ot = db.Owner_Table.Where(x => x.owner_id == owner_id).SingleOrDefault();
            if (ot.owner_password == current_owner_password)
            {
                if (owner_password == confirm_owner_password)
                {
                    ot.owner_password = owner_password;
                    db.SaveChanges();
                    TempData["msg"] = "<script>alert('Password has been changed successfully !!!');</script>";
                }
                else
                {
                    TempData["msg"] = "<script>alert('New password match !!! Please check');</script>";
                }
            }
            else
            {
                TempData["msg"] = "<script>alert('Old password not match !!! Please check entered old password');</script>";
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["UserID"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
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