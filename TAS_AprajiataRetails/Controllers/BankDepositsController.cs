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
    public class BankDepositsController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();
        private void ProcessAccounts(BankDeposit objectName)
        {

            if (objectName.PayMode == BankPayModes.Cash)
            {
                Utils.UpDateCashOutHand(db, objectName.DepoDate, objectName.Amount);
                Utils.UpDateCashInBank(db, objectName.DepoDate, objectName.Amount);
            }
            //TODO: in future make it more robust
            if (objectName.PayMode != BankPayModes.Cash )
            {
                Utils.UpDateCashInBank(db, objectName.DepoDate, objectName.Amount);
            }



        }

        // GET: BankDeposits
        public ActionResult Index()
        {
            var bankDeposits = db.BankDeposits.Include(b => b.Account);
            return View(bankDeposits.ToList());
        }

        // GET: BankDeposits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankDeposit bankDeposit = db.BankDeposits.Find(id);
            if (bankDeposit == null)
            {
                return HttpNotFound();
            }
            return View(bankDeposit);
        }

        // GET: BankDeposits/Create
        public ActionResult Create()
        {
            ViewBag.AccountNumberId = new SelectList(db.BankAccounts, "AccountNumberId", "Account");
            return View();
        }

        // POST: BankDeposits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BankDepositId,DepoDate,AccountNumberId,Amount,PayMode,Details,Remarks")] BankDeposit bankDeposit)
        {
            if (ModelState.IsValid)
            {
                db.BankDeposits.Add(bankDeposit);
                ProcessAccounts(bankDeposit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountNumberId = new SelectList(db.BankAccounts, "AccountNumberId", "Account", bankDeposit.AccountNumberId);
            return View(bankDeposit);
        }

        // GET: BankDeposits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankDeposit bankDeposit = db.BankDeposits.Find(id);
            if (bankDeposit == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountNumberId = new SelectList(db.BankAccounts, "AccountNumberId", "Account", bankDeposit.AccountNumberId);
            return View(bankDeposit);
        }

        // POST: BankDeposits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BankDepositId,DepoDate,AccountNumberId,Amount,PayMode,Details,Remarks")] BankDeposit bankDeposit)
        {
            if (ModelState.IsValid)
            {
                //TODO: Rectifed This ProcessAccounts(bankDeposit);
                bankDeposit.Remarks = bankDeposit.Remarks + " Edit, Match with original data";
                db.Entry(bankDeposit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountNumberId = new SelectList(db.BankAccounts, "AccountNumberId", "Account", bankDeposit.AccountNumberId);
            return View(bankDeposit);
        }

        // GET: BankDeposits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankDeposit bankDeposit = db.BankDeposits.Find(id);
            if (bankDeposit == null)
            {
                return HttpNotFound();
            }
            return View(bankDeposit);
        }

        // POST: BankDeposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BankDeposit bankDeposit = db.BankDeposits.Find(id);
            db.BankDeposits.Remove(bankDeposit);
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
