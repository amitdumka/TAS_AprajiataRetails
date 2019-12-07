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
    public class BankWithdrawalsController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        private void ProcessAccounts(BankWithdrawal objectName)
        {

            
                Utils.UpDateCashInHand(db, objectName.DepoDate, objectName.Amount);
                Utils.UpDateCashOutBank(db, objectName.DepoDate, objectName.Amount);
            
            



        }
        // GET: BankWithdrawals
        public ActionResult Index()
        {
            var withdrawals = db.Withdrawals.Include(b => b.Account);
            return View(withdrawals.ToList());
        }

        // GET: BankWithdrawals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankWithdrawal bankWithdrawal = db.Withdrawals.Find(id);
            if (bankWithdrawal == null)
            {
                return HttpNotFound();
            }
            return View(bankWithdrawal);
        }

        // GET: BankWithdrawals/Create
        public ActionResult Create()
        {
            ViewBag.AccountNumberId = new SelectList(db.BankAccounts, "AccountNumberId", "Account");
            return View();
        }

        // POST: BankWithdrawals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BankWithdrawalId,DepoDate,AccountNumberId,Amount,ChequeNo,SignedBy,ApprovedBy,InNameOf")] BankWithdrawal bankWithdrawal)
        {
            if (ModelState.IsValid)
            {
                db.Withdrawals.Add(bankWithdrawal);
                ProcessAccounts(bankWithdrawal);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountNumberId = new SelectList(db.BankAccounts, "AccountNumberId", "Account", bankWithdrawal.AccountNumberId);
            return View(bankWithdrawal);
        }

        // GET: BankWithdrawals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankWithdrawal bankWithdrawal = db.Withdrawals.Find(id);
            if (bankWithdrawal == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountNumberId = new SelectList(db.BankAccounts, "AccountNumberId", "Account", bankWithdrawal.AccountNumberId);
            return View(bankWithdrawal);
        }

        // POST: BankWithdrawals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BankWithdrawalId,DepoDate,AccountNumberId,Amount,ChequeNo,SignedBy,ApprovedBy,InNameOf")] BankWithdrawal bankWithdrawal)
        {
            if (ModelState.IsValid)
            {
                //TODO: Rectifed This  ProcessAccounts(bankWithdrawal);
                db.Entry(bankWithdrawal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountNumberId = new SelectList(db.BankAccounts, "AccountNumberId", "Account", bankWithdrawal.AccountNumberId);
            return View(bankWithdrawal);
        }

        // GET: BankWithdrawals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankWithdrawal bankWithdrawal = db.Withdrawals.Find(id);
            if (bankWithdrawal == null)
            {
                return HttpNotFound();
            }
            return View(bankWithdrawal);
        }

        // POST: BankWithdrawals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BankWithdrawal bankWithdrawal = db.Withdrawals.Find(id);
            db.Withdrawals.Remove(bankWithdrawal);
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
