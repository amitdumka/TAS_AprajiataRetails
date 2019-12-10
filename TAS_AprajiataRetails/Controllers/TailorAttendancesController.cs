using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TAS_AprajiataRetails.Models.Data;

namespace TAS_AprajiataRetails.Controllers
{
    [Authorize]
    public class TailorAttendancesController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        // GET: TailorAttendances
        public ActionResult Index()
        {
            var tailorAttendances = db.TailorAttendances.Include(t => t.Employee);
            return View(tailorAttendances.ToList());
        }

        // GET: TailorAttendances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TailorAttendance tailorAttendance = db.TailorAttendances.Find(id);
            if (tailorAttendance == null)
            {
                return HttpNotFound();
            }
            return PartialView(tailorAttendance);
        }

        // GET: TailorAttendances/Create
        public ActionResult Create()
        {
            ViewBag.TailoringEmployeeId = new SelectList(db.Tailors, "TailoringEmployeeId", "StaffName");
            return PartialView();
        }

        // POST: TailorAttendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TailorAttendanceId,TailoringEmployeeId,AttDate,EntryTime,Status,Remarks")] TailorAttendance tailorAttendance)
        {
            if (ModelState.IsValid)
            {
                db.TailorAttendances.Add(tailorAttendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TailoringEmployeeId = new SelectList(db.Tailors, "TailoringEmployeeId", "StaffName", tailorAttendance.TailoringEmployeeId);
            return View(tailorAttendance);
        }

        // GET: TailorAttendances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TailorAttendance tailorAttendance = db.TailorAttendances.Find(id);
            if (tailorAttendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.TailoringEmployeeId = new SelectList(db.Tailors, "TailoringEmployeeId", "StaffName", tailorAttendance.TailoringEmployeeId);
            return View(tailorAttendance);
        }

        // POST: TailorAttendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TailorAttendanceId,TailoringEmployeeId,AttDate,EntryTime,Status,Remarks")] TailorAttendance tailorAttendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tailorAttendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TailoringEmployeeId = new SelectList(db.Tailors, "TailoringEmployeeId", "StaffName", tailorAttendance.TailoringEmployeeId);
            return View(tailorAttendance);
        }

        // GET: TailorAttendances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TailorAttendance tailorAttendance = db.TailorAttendances.Find(id);
            if (tailorAttendance == null)
            {
                return HttpNotFound();
            }
            return View(tailorAttendance);
        }

        // POST: TailorAttendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TailorAttendance tailorAttendance = db.TailorAttendances.Find(id);
            db.TailorAttendances.Remove(tailorAttendance);
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
