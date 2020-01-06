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
    public class PaySlipsController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        // GET: PaySlips
        public ActionResult Index()
        {
            var paySlips = db.PaySlips.Include(p => p.CurrentSalary).Include(p => p.Employee);
            return View(paySlips.ToList());
        }

        // GET: PaySlips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaySlip paySlip = db.PaySlips.Find(id);
            if (paySlip == null)
            {
                return HttpNotFound();
            }
            return View(paySlip);
        }

        // GET: PaySlips/Create
        public ActionResult Create()
        {
            ViewBag.CurrentSalaryId = new SelectList(db.CurrentSalaries, "CurrentSalaryId", "CurrentSalaryId");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName");
            return View();
        }

        // POST: PaySlips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaySlipId,OnDate,Month,Year,EmployeeId,CurrentSalaryId,BasicSalary,NoOfDaysPresent,TotalSale,SaleIncentive,WOWBillAmount,WOWBillIncentive,LastPcsAmount,LastPCsIncentive,OthersIncentive,GrossSalary,StandardDeductions,TDSDeductions,PFDeductions,AdvanceDeducations,OtherDeductions,Remarks")] PaySlip paySlip)
        {
            if (ModelState.IsValid)
            {
                db.PaySlips.Add(paySlip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CurrentSalaryId = new SelectList(db.CurrentSalaries, "CurrentSalaryId", "CurrentSalaryId", paySlip.CurrentSalaryId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName", paySlip.EmployeeId);
            return View(paySlip);
        }

        // GET: PaySlips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaySlip paySlip = db.PaySlips.Find(id);
            if (paySlip == null)
            {
                return HttpNotFound();
            }
            ViewBag.CurrentSalaryId = new SelectList(db.CurrentSalaries, "CurrentSalaryId", "CurrentSalaryId", paySlip.CurrentSalaryId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName", paySlip.EmployeeId);
            return View(paySlip);
        }

        // POST: PaySlips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaySlipId,OnDate,Month,Year,EmployeeId,CurrentSalaryId,BasicSalary,NoOfDaysPresent,TotalSale,SaleIncentive,WOWBillAmount,WOWBillIncentive,LastPcsAmount,LastPCsIncentive,OthersIncentive,GrossSalary,StandardDeductions,TDSDeductions,PFDeductions,AdvanceDeducations,OtherDeductions,Remarks")] PaySlip paySlip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paySlip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrentSalaryId = new SelectList(db.CurrentSalaries, "CurrentSalaryId", "CurrentSalaryId", paySlip.CurrentSalaryId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName", paySlip.EmployeeId);
            return View(paySlip);
        }

        // GET: PaySlips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaySlip paySlip = db.PaySlips.Find(id);
            if (paySlip == null)
            {
                return HttpNotFound();
            }
            return View(paySlip);
        }

        // POST: PaySlips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaySlip paySlip = db.PaySlips.Find(id);
            db.PaySlips.Remove(paySlip);
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
