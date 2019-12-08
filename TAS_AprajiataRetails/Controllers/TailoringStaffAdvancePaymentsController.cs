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
    public class TailoringStaffAdvancePaymentsController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        // GET: TailoringStaffAdvancePayments
        public ActionResult Index()
        {
            var tailoringStaffAdvancePayments = db.TailoringStaffAdvancePayments.Include(t => t.Employee);
            return View(tailoringStaffAdvancePayments.ToList());
        }

        // GET: TailoringStaffAdvancePayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TailoringStaffAdvancePayment tailoringStaffAdvancePayment = db.TailoringStaffAdvancePayments.Find(id);
            if (tailoringStaffAdvancePayment == null)
            {
                return HttpNotFound();
            }
            return View(tailoringStaffAdvancePayment);
        }

        // GET: TailoringStaffAdvancePayments/Create
        public ActionResult Create()
        {
            ViewBag.TailoringEmployeeId = new SelectList(db.Tailors, "TailoringEmployeeId", "StaffName");
            return View();
        }

        // POST: TailoringStaffAdvancePayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TailoringStaffAdvancePaymentId,TailoringEmployeeId,PaymentDate,Amount,PayMode,Details")] TailoringStaffAdvancePayment tailoringStaffAdvancePayment)
        {
            if (ModelState.IsValid)
            {
                db.TailoringStaffAdvancePayments.Add(tailoringStaffAdvancePayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TailoringEmployeeId = new SelectList(db.Tailors, "TailoringEmployeeId", "StaffName", tailoringStaffAdvancePayment.TailoringEmployeeId);
            return View(tailoringStaffAdvancePayment);
        }

        // GET: TailoringStaffAdvancePayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TailoringStaffAdvancePayment tailoringStaffAdvancePayment = db.TailoringStaffAdvancePayments.Find(id);
            if (tailoringStaffAdvancePayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.TailoringEmployeeId = new SelectList(db.Tailors, "TailoringEmployeeId", "StaffName", tailoringStaffAdvancePayment.TailoringEmployeeId);
            return View(tailoringStaffAdvancePayment);
        }

        // POST: TailoringStaffAdvancePayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TailoringStaffAdvancePaymentId,TailoringEmployeeId,PaymentDate,Amount,PayMode,Details")] TailoringStaffAdvancePayment tailoringStaffAdvancePayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tailoringStaffAdvancePayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TailoringEmployeeId = new SelectList(db.Tailors, "TailoringEmployeeId", "StaffName", tailoringStaffAdvancePayment.TailoringEmployeeId);
            return View(tailoringStaffAdvancePayment);
        }

        // GET: TailoringStaffAdvancePayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TailoringStaffAdvancePayment tailoringStaffAdvancePayment = db.TailoringStaffAdvancePayments.Find(id);
            if (tailoringStaffAdvancePayment == null)
            {
                return HttpNotFound();
            }
            return View(tailoringStaffAdvancePayment);
        }

        // POST: TailoringStaffAdvancePayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TailoringStaffAdvancePayment tailoringStaffAdvancePayment = db.TailoringStaffAdvancePayments.Find(id);
            db.TailoringStaffAdvancePayments.Remove(tailoringStaffAdvancePayment);
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
