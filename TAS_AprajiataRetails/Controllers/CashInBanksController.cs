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
    public class CashInBanksController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        // GET: CashInBanks
        public ActionResult Index()
        {
            return View(db.CashInBanks.ToList());
        }

        // GET: CashInBanks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashInBank cashInBank = db.CashInBanks.Find(id);
            if (cashInBank == null)
            {
                return HttpNotFound();
            }
            return View(cashInBank);
        }

        // GET: CashInBanks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CashInBanks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CashInBankId,CIBDate,OpenningBalance,ClosingBalance,CashIn,CashOut")] CashInBank cashInBank)
        {
            if (ModelState.IsValid)
            {
                db.CashInBanks.Add(cashInBank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cashInBank);
        }

        // GET: CashInBanks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashInBank cashInBank = db.CashInBanks.Find(id);
            if (cashInBank == null)
            {
                return HttpNotFound();
            }
            return View(cashInBank);
        }

        // POST: CashInBanks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CashInBankId,CIBDate,OpenningBalance,ClosingBalance,CashIn,CashOut")] CashInBank cashInBank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cashInBank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cashInBank);
        }

        // GET: CashInBanks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashInBank cashInBank = db.CashInBanks.Find(id);
            if (cashInBank == null)
            {
                return HttpNotFound();
            }
            return View(cashInBank);
        }

        // POST: CashInBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CashInBank cashInBank = db.CashInBanks.Find(id);
            db.CashInBanks.Remove(cashInBank);
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
