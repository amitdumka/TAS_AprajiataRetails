using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AprajitaRetails.Areas.TAS.Models.Data;
using AprajitaRetails.Areas.TAS.Models.Helpers;

namespace AprajitaRetails.Areas.TAS.Controllers
{
    [Authorize]
    public class SalaryPaymentsController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        private void ProcessAccounts(SalaryPayment salary)
        {

            if (salary.PayMode == PayModes.Cash)
            {
                Utils.UpDateCashOutHand(db, salary.PaymentDate, salary.Amount);

            }
            //TODO: in future make it more robust
            if (salary.PayMode != PayModes.Cash && salary.PayMode != PayModes.Coupons && salary.PayMode != PayModes.Points)
            {
                Utils.UpDateCashOutBank(db, salary.PaymentDate, salary.Amount);
            }



        }
        // GET: SalaryPayments
        public ActionResult Index()
        {
            var salaries = db.Salaries.Include(s => s.Employee);
            return View(salaries.ToList());
        }

        // GET: SalaryPayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryPayment salaryPayment = db.Salaries.Find(id);
            if (salaryPayment == null)
            {
                return HttpNotFound();
            }
            return PartialView(salaryPayment);
        }

        // GET: SalaryPayments/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName");
            return PartialView();
        }

        // POST: SalaryPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalaryPaymentId,EmployeeId,SalaryMonth,SalaryComponet,PaymentDate,Amount,PayMode,Details")] SalaryPayment salaryPayment)
        {
            if (ModelState.IsValid)
            {
                db.Salaries.Add(salaryPayment);
                ProcessAccounts(salaryPayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName", salaryPayment.EmployeeId);
            return View(salaryPayment);
        }

        // GET: SalaryPayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryPayment salaryPayment = db.Salaries.Find(id);
            if (salaryPayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName", salaryPayment.EmployeeId);
            return View(salaryPayment);
        }

        // POST: SalaryPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalaryPaymentId,EmployeeId,SalaryMonth,SalaryComponet,PaymentDate,Amount,PayMode,Details")] SalaryPayment salaryPayment)
        {
            if (ModelState.IsValid)
            {
                ProcessAccounts(salaryPayment);
                db.Entry(salaryPayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName", salaryPayment.EmployeeId);
            return View(salaryPayment);
        }

        // GET: SalaryPayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryPayment salaryPayment = db.Salaries.Find(id);
            if (salaryPayment == null)
            {
                return HttpNotFound();
            }
            return View(salaryPayment);
        }

        // POST: SalaryPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalaryPayment salaryPayment = db.Salaries.Find(id);
            db.Salaries.Remove(salaryPayment);
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
