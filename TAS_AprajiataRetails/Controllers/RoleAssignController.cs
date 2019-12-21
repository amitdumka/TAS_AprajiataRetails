using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TAS_AprajiataRetails.Controllers
{
    public class RoleAssignController : Controller
    {
        // GET: RoleAssign
        public ActionResult Index()
        {
            return View();
        }

        // GET: RoleAssign/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoleAssign/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleAssign/Create
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

        // GET: RoleAssign/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoleAssign/Edit/5
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

        // GET: RoleAssign/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoleAssign/Delete/5
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
