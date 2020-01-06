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
    public class CurrentSalariesController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        // GET: CurrentSalaries
        public ActionResult Index()
        {
            var currentSalaries = db.CurrentSalaries.Include(c => c.Employee);
            return View(currentSalaries.ToList());
        }

        // GET: CurrentSalaries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrentSalary currentSalary = db.CurrentSalaries.Find(id);
            if (currentSalary == null)
            {
                return HttpNotFound();
            }
            return View(currentSalary);
        }

        // GET: CurrentSalaries/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName");
            return View();
        }

        // POST: CurrentSalaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CurrentSalaryId,EmployeeId,BasicSalary,SundaySalary,LPRate,IncentiveRate,IncentiveTarget,WOWBillRate,WOWBillTarget,IsSundayBillable,EffectiveDate,CloseDate,IsEffective")] CurrentSalary currentSalary)
        {
            if (ModelState.IsValid)
            {
                db.CurrentSalaries.Add(currentSalary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName", currentSalary.EmployeeId);
            return View(currentSalary);
        }

        // GET: CurrentSalaries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrentSalary currentSalary = db.CurrentSalaries.Find(id);
            if (currentSalary == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName", currentSalary.EmployeeId);
            return View(currentSalary);
        }

        // POST: CurrentSalaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CurrentSalaryId,EmployeeId,BasicSalary,SundaySalary,LPRate,IncentiveRate,IncentiveTarget,WOWBillRate,WOWBillTarget,IsSundayBillable,EffectiveDate,CloseDate,IsEffective")] CurrentSalary currentSalary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(currentSalary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName", currentSalary.EmployeeId);
            return View(currentSalary);
        }

        // GET: CurrentSalaries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrentSalary currentSalary = db.CurrentSalaries.Find(id);
            if (currentSalary == null)
            {
                return HttpNotFound();
            }
            return View(currentSalary);
        }

        // POST: CurrentSalaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CurrentSalary currentSalary = db.CurrentSalaries.Find(id);
            db.CurrentSalaries.Remove(currentSalary);
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
