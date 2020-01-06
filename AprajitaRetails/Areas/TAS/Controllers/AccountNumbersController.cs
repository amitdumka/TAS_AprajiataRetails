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
    public class AccountNumbersController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        // GET: AccountNumbers
        public ActionResult Index()
        {
            var bankAccounts = db.BankAccounts.Include(a => a.Bank);
            return View(bankAccounts.ToList());
        }

        // GET: AccountNumbers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountNumber accountNumber = db.BankAccounts.Find(id);
            if (accountNumber == null)
            {
                return HttpNotFound();
            }
            return PartialView(accountNumber);
        }

        // GET: AccountNumbers/Create
        public ActionResult Create()
        {
            ViewBag.BankId = new SelectList(db.Banks, "BankId", "BankName");
            return PartialView();
        }

        // POST: AccountNumbers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountNumberId,BankId,Account")] AccountNumber accountNumber)
        {
            if (ModelState.IsValid)
            {
                db.BankAccounts.Add(accountNumber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BankId = new SelectList(db.Banks, "BankId", "BankName", accountNumber.BankId);
            return View(accountNumber);
        }

        // GET: AccountNumbers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountNumber accountNumber = db.BankAccounts.Find(id);
            if (accountNumber == null)
            {
                return HttpNotFound();
            }
            ViewBag.BankId = new SelectList(db.Banks, "BankId", "BankName", accountNumber.BankId);
            return PartialView(accountNumber);
        }

        // POST: AccountNumbers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountNumberId,BankId,Account")] AccountNumber accountNumber)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountNumber).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BankId = new SelectList(db.Banks, "BankId", "BankName", accountNumber.BankId);
            return View(accountNumber);
        }

        // GET: AccountNumbers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountNumber accountNumber = db.BankAccounts.Find(id);
            if (accountNumber == null)
            {
                return HttpNotFound();
            }
            return PartialView(accountNumber);
        }

        // POST: AccountNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountNumber accountNumber = db.BankAccounts.Find(id);
            db.BankAccounts.Remove(accountNumber);
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
