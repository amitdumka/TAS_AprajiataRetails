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
    public class PettyCashExpensesController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        // GET: PettyCashExpenses
        public ActionResult Index()
        {
            var cashExpenses = db.CashExpenses.Include(p => p.PaidBy);
            return View(cashExpenses.ToList());
        }

        // GET: PettyCashExpenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PettyCashExpense pettyCashExpense = db.CashExpenses.Find(id);
            if (pettyCashExpense == null)
            {
                return HttpNotFound();
            }
            return View(pettyCashExpense);
        }

        // GET: PettyCashExpenses/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName");
            return View();
        }

        // POST: PettyCashExpenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PettyCashExpenseId,ExpDate,Particulars,Amount,EmployeeId,PaidTo,Remarks")] PettyCashExpense pettyCashExpense)
        {
            if (ModelState.IsValid)
            {
                db.CashExpenses.Add(pettyCashExpense);
                Utils.UpDateCashOutHand(db, pettyCashExpense.ExpDate, pettyCashExpense.Amount); 
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName", pettyCashExpense.EmployeeId);
            return View(pettyCashExpense);
        }

        // GET: PettyCashExpenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PettyCashExpense pettyCashExpense = db.CashExpenses.Find(id);
            if (pettyCashExpense == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName", pettyCashExpense.EmployeeId);
            return View(pettyCashExpense);
        }

        // POST: PettyCashExpenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PettyCashExpenseId,ExpDate,Particulars,Amount,EmployeeId,PaidTo,Remarks")] PettyCashExpense pettyCashExpense)
        {
            if (ModelState.IsValid)
            {
                Utils.UpDateCashOutHand(db, pettyCashExpense.ExpDate, pettyCashExpense.Amount);
                db.Entry(pettyCashExpense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName", pettyCashExpense.EmployeeId);
            return View(pettyCashExpense);
        }

        // GET: PettyCashExpenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PettyCashExpense pettyCashExpense = db.CashExpenses.Find(id);
            if (pettyCashExpense == null)
            {
                return HttpNotFound();
            }
            return View(pettyCashExpense);
        }

        // POST: PettyCashExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PettyCashExpense pettyCashExpense = db.CashExpenses.Find(id);
            db.CashExpenses.Remove(pettyCashExpense);
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
