using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentsRooms.Models;
using System.Web.Security;

namespace StudentsRooms.Views.Home
{
    public class UserController : Controller
    {
        StudentsRoomsEntities db = new StudentsRoomsEntities();

        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Owner_Table objUser)
        {
            if (ModelState.IsValid)
            {
                using (StudentsRoomsEntities db = new StudentsRoomsEntities())
                {
                    var obj = db.Owner_Table.Where(a => a.owner_mobile2.Equals(objUser.owner_mobile1) || a.owner_mobile1.Equals(objUser.owner_mobile1) && a.owner_password.Equals(objUser.owner_password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.owner_id.ToString();
                        Session["UserName"] = obj.owner_name.ToString();
                        Session["UserMobile1"] = obj.owner_mobile1.ToString();

                        return RedirectToAction("Dashboard", "Landlord");
                    }
                }
            }
            return View(objUser);
        }
        public ActionResult signup()
        {
            return View();
        }
        [HttpPost]

        public ActionResult signup(Owner_Table obj)
        {
            if (ModelState.IsValid)
            {
                var isMobile1AlreadyExists = db.Owner_Table.Any(x => x.owner_mobile1 == obj.owner_mobile1);
                var isMobile2AlreadyExists = db.Owner_Table.Any(x => x.owner_mobile2 == obj.owner_mobile2);
                var isEmail1AlreadyExists = db.Owner_Table.Any(x => x.owner_email == obj.owner_email);

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

                db.Owner_Table.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Login", "User", "User");
            }
            return View(obj);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["UserID"] = null;
            Session["UserName"] = null;
            Session["UserMobile1"] = null;
            return RedirectToAction("Index", "Home");
        }

    }
}