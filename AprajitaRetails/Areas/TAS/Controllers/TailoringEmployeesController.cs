using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AprajitaRetails.Areas.TAS.Models.Data;

namespace AprajitaRetails.Areas.TAS.Controllers
{
    [Authorize]
    public class TailoringEmployeesController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        // GET: TailoringEmployees
        public ActionResult Index()
        {
            return View(db.Tailors.ToList());
        }

        // GET: TailoringEmployees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TailoringEmployee tailoringEmployee = db.Tailors.Find(id);
            if (tailoringEmployee == null)
            {
                return HttpNotFound();
            }
            return PartialView(tailoringEmployee);
        }

        // GET: TailoringEmployees/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: TailoringEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TailoringEmployeeId,StaffName,MobileNo,JoiningDate,LeavingDate,IsWorking")] TailoringEmployee tailoringEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Tailors.Add(tailoringEmployee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tailoringEmployee);
        }

        // GET: TailoringEmployees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TailoringEmployee tailoringEmployee = db.Tailors.Find(id);
            if (tailoringEmployee == null)
            {
                return HttpNotFound();
            }
            return View(tailoringEmployee);
        }

        // POST: TailoringEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TailoringEmployeeId,StaffName,MobileNo,JoiningDate,LeavingDate,IsWorking")] TailoringEmployee tailoringEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tailoringEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tailoringEmployee);
        }

        // GET: TailoringEmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TailoringEmployee tailoringEmployee = db.Tailors.Find(id);
            if (tailoringEmployee == null)
            {
                return HttpNotFound();
            }
            return View(tailoringEmployee);
        }

        // POST: TailoringEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TailoringEmployee tailoringEmployee = db.Tailors.Find(id);
            db.Tailors.Remove(tailoringEmployee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
