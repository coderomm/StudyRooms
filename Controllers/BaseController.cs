using StudentsRooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentsRooms.Controllers
{
    public class BaseController : Controller
    {
        StudentsRoomsEntities db = new StudentsRoomsEntities();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var footer_listings = db.Property_Table.ToList().Take(4);
            ViewBag.footer_listings = footer_listings;
        }
    }
}