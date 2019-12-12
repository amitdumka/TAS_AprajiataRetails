using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TAS_AprajiataRetails.Models.Data;
using TAS_AprajiataRetails.Models.Helpers;

namespace TAS_AprajiataRetails.Controllers
{
    [Authorize]
    public class StaffAdvancePaymentsController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();
        private void ProcessAccounts(StaffAdvancePayment objectName)
        {

            if (objectName.PayMode == PayModes.Cash)
            {
                Utils.UpDateCashOutHand(db, objectName.PaymentDate, objectName.Amount);

            }
            //TODO: in future make it more robust
            if (objectName.PayMode != PayModes.Cash && objectName.PayMode != PayModes.Coupons && objectName.PayMode != PayModes.Points)
            {
                Utils.UpDateCashOutBank(db, objectName.PaymentDate, objectName.Amount);
            }



        }
        // GET: StaffAdvancePayments
        public ActionResult Index()
        {
            var staffAdvancePayments = db.StaffAdvancePayments.Include(s => s.Employee);
            return View(staffAdvancePayments.ToList());
        }

        // GET: StaffAdvancePayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffAdvancePayment staffAdvancePayment = db.StaffAdvancePayments.Find(id);
            if (staffAdvancePayment == null)
            {
                return HttpNotFound();
            }
            return PartialView(staffAdvancePayment);
        }

        // GET: StaffAdvancePayments/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName");
            return PartialView();
        }

        // POST: StaffAdvancePayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffAdvancePaymentId,EmployeeId,PaymentDate,Amount,PayMode,Details")] StaffAdvancePayment staffAdvancePayment)
        {
            if (ModelState.IsValid)
            {
                db.StaffAdvancePayments.Add(staffAdvancePayment);
                ProcessAccounts(staffAdvancePayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName", staffAdvancePayment.EmployeeId);
            return View(staffAdvancePayment);
        }

        // GET: StaffAdvancePayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffAdvancePayment staffAdvancePayment = db.StaffAdvancePayments.Find(id);
            if (staffAdvancePayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName", staffAdvancePayment.EmployeeId);
            return View(staffAdvancePayment);
        }

        // POST: StaffAdvancePayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffAdvancePaymentId,EmployeeId,PaymentDate,Amount,PayMode,Details")] StaffAdvancePayment staffAdvancePayment)
        {
            if (ModelState.IsValid)
            {
                ProcessAccounts(staffAdvancePayment);
                db.Entry(staffAdvancePayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName", staffAdvancePayment.EmployeeId);
            return View(staffAdvancePayment);
        }

        // GET: StaffAdvancePayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffAdvancePayment staffAdvancePayment = db.StaffAdvancePayments.Find(id);
            if (staffAdvancePayment == null)
            {
                return HttpNotFound();
            }
            return View(staffAdvancePayment);
        }

        // POST: StaffAdvancePayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffAdvancePayment staffAdvancePayment = db.StaffAdvancePayments.Find(id);
            db.StaffAdvancePayments.Remove(staffAdvancePayment);
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
