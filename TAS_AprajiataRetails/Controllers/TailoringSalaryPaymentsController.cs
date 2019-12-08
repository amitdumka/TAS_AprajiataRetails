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
    public class TailoringSalaryPaymentsController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        // GET: TailoringSalaryPayments
        public ActionResult Index()
        {
            var tailoringSalaries = db.TailoringSalaries.Include(t => t.Employee);
            return View(tailoringSalaries.ToList());
        }

        // GET: TailoringSalaryPayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TailoringSalaryPayment tailoringSalaryPayment = db.TailoringSalaries.Find(id);
            if (tailoringSalaryPayment == null)
            {
                return HttpNotFound();
            }
            return View(tailoringSalaryPayment);
        }

        // GET: TailoringSalaryPayments/Create
        public ActionResult Create()
        {
            ViewBag.TailoringEmployeeId = new SelectList(db.Tailors, "TailoringEmployeeId", "StaffName");
            return View();
        }

        // POST: TailoringSalaryPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TailoringSalaryPaymentId,TailoringEmployeeId,SalaryMonth,SalaryComponet,PaymentDate,Amount,PayMode,Details")] TailoringSalaryPayment tailoringSalaryPayment)
        {
            if (ModelState.IsValid)
            {
                db.TailoringSalaries.Add(tailoringSalaryPayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TailoringEmployeeId = new SelectList(db.Tailors, "TailoringEmployeeId", "StaffName", tailoringSalaryPayment.TailoringEmployeeId);
            return View(tailoringSalaryPayment);
        }

        // GET: TailoringSalaryPayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TailoringSalaryPayment tailoringSalaryPayment = db.TailoringSalaries.Find(id);
            if (tailoringSalaryPayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.TailoringEmployeeId = new SelectList(db.Tailors, "TailoringEmployeeId", "StaffName", tailoringSalaryPayment.TailoringEmployeeId);
            return View(tailoringSalaryPayment);
        }

        // POST: TailoringSalaryPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TailoringSalaryPaymentId,TailoringEmployeeId,SalaryMonth,SalaryComponet,PaymentDate,Amount,PayMode,Details")] TailoringSalaryPayment tailoringSalaryPayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tailoringSalaryPayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TailoringEmployeeId = new SelectList(db.Tailors, "TailoringEmployeeId", "StaffName", tailoringSalaryPayment.TailoringEmployeeId);
            return View(tailoringSalaryPayment);
        }

        // GET: TailoringSalaryPayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TailoringSalaryPayment tailoringSalaryPayment = db.TailoringSalaries.Find(id);
            if (tailoringSalaryPayment == null)
            {
                return HttpNotFound();
            }
            return View(tailoringSalaryPayment);
        }

        // POST: TailoringSalaryPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TailoringSalaryPayment tailoringSalaryPayment = db.TailoringSalaries.Find(id);
            db.TailoringSalaries.Remove(tailoringSalaryPayment);
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
