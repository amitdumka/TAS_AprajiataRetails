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
    public class CashInHandsController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        // GET: CashInHands
        public ActionResult Index()
        {
            return View(db.CashInHands.ToList());
        }

        // GET: CashInHands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashInHand cashInHand = db.CashInHands.Find(id);
            if (cashInHand == null)
            {
                return HttpNotFound();
            }
            return View(cashInHand);
        }

        // GET: CashInHands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CashInHands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CashInHandId,CIHDate,OpenningBalance,ClosingBalance,CashIn,CashOut")] CashInHand cashInHand)
        {
            if (ModelState.IsValid)
            {
                db.CashInHands.Add(cashInHand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cashInHand);
        }

        // GET: CashInHands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashInHand cashInHand = db.CashInHands.Find(id);
            if (cashInHand == null)
            {
                return HttpNotFound();
            }
            return View(cashInHand);
        }

        // POST: CashInHands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CashInHandId,CIHDate,OpenningBalance,ClosingBalance,CashIn,CashOut")] CashInHand cashInHand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cashInHand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cashInHand);
        }

        // GET: CashInHands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashInHand cashInHand = db.CashInHands.Find(id);
            if (cashInHand == null)
            {
                return HttpNotFound();
            }
            return View(cashInHand);
        }

        // POST: CashInHands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CashInHand cashInHand = db.CashInHands.Find(id);
            db.CashInHands.Remove(cashInHand);
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
