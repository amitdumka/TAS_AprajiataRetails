using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AprajitaRetails.Models.Data.Accounts;

namespace AprajitaRetailsControllers
{
    public class LedgerEntriesController : Controller
    {
        private AccountsContext db = new AccountsContext();

        // GET: LedgerEntries
        public ActionResult Index()
        {
            var ledgerEntries = db.LedgerEntries.Include(l => l.PartyName);
            return View(ledgerEntries.ToList());
        }

        // GET: LedgerEntries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LedgerEntry ledgerEntry = db.LedgerEntries.Find(id);
            if (ledgerEntry == null)
            {
                return HttpNotFound();
            }
            return View(ledgerEntry);
        }

        // GET: LedgerEntries/Create
        public ActionResult Create()
        {
            ViewBag.PartyId = new SelectList(db.Parties, "PartyId", "PartyName");
            return View();
        }

        // POST: LedgerEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LedgerEntryId,PartyId,EntryDate,Particulars,AmountIn,AmountOut,Balance")] LedgerEntry ledgerEntry)
        {
            if (ModelState.IsValid)
            {
                db.LedgerEntries.Add(ledgerEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PartyId = new SelectList(db.Parties, "PartyId", "PartyName", ledgerEntry.PartyId);
            return View(ledgerEntry);
        }

        // GET: LedgerEntries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LedgerEntry ledgerEntry = db.LedgerEntries.Find(id);
            if (ledgerEntry == null)
            {
                return HttpNotFound();
            }
            ViewBag.PartyId = new SelectList(db.Parties, "PartyId", "PartyName", ledgerEntry.PartyId);
            return View(ledgerEntry);
        }

        // POST: LedgerEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LedgerEntryId,PartyId,EntryDate,Particulars,AmountIn,AmountOut,Balance")] LedgerEntry ledgerEntry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ledgerEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PartyId = new SelectList(db.Parties, "PartyId", "PartyName", ledgerEntry.PartyId);
            return View(ledgerEntry);
        }

        // GET: LedgerEntries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LedgerEntry ledgerEntry = db.LedgerEntries.Find(id);
            if (ledgerEntry == null)
            {
                return HttpNotFound();
            }
            return View(ledgerEntry);
        }

        // POST: LedgerEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LedgerEntry ledgerEntry = db.LedgerEntries.Find(id);
            db.LedgerEntries.Remove(ledgerEntry);
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
