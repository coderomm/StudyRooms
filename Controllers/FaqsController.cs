﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentsRooms.Controllers
{
    public class FaqsController : BaseController
    {
        // GET: Faqs
        public ActionResult Index()
        {
            return View();
        }

        // GET: Faqs/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Faqs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Faqs/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Faqs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Faqs/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Faqs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Faqs/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
