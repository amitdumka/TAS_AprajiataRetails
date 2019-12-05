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
    public class CashPaymentsController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        // GET: CashPayments
        public ActionResult Index()
        {
            var cashPayments = db.CashPayments.Include(c => c.Mode);
            return View(cashPayments.ToList());
        }

        // GET: CashPayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashPayment cashPayment = db.CashPayments.Find(id);
            if (cashPayment == null)
            {
                return HttpNotFound();
            }
            return View(cashPayment);
        }

        // GET: CashPayments/Create
        public ActionResult Create()
        {
            ViewBag.TranscationModeId = new SelectList(db.TranscationModes, "TranscationModeId", "Transcation");
            return View();
        }

        // POST: CashPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CashPaymentId,PaymentDate,TranscationModeId,PaidTo,Amount,SlipNo")] CashPayment cashPayment)
        {
            if (ModelState.IsValid)
            {
                
                db.CashPayments.Add(cashPayment);
                Utils.UpDateCashOutHand(db, cashPayment.PaymentDate, cashPayment.Amount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TranscationModeId = new SelectList(db.TranscationModes, "TranscationModeId", "Transcation", cashPayment.TranscationModeId);
            return View(cashPayment);
        }

        // GET: CashPayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashPayment cashPayment = db.CashPayments.Find(id);
            if (cashPayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.TranscationModeId = new SelectList(db.TranscationModes, "TranscationModeId", "Transcation", cashPayment.TranscationModeId);
            return View(cashPayment);
        }

        // POST: CashPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CashPaymentId,PaymentDate,TranscationModeId,PaidTo,Amount,SlipNo")] CashPayment cashPayment)
        {
            if (ModelState.IsValid)
            {
                Utils.UpDateCashOutHand(db, cashPayment.PaymentDate, cashPayment.Amount);
                db.Entry(cashPayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TranscationModeId = new SelectList(db.TranscationModes, "TranscationModeId", "Transcation", cashPayment.TranscationModeId);
            return View(cashPayment);
        }

        // GET: CashPayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashPayment cashPayment = db.CashPayments.Find(id);
            if (cashPayment == null)
            {
                return HttpNotFound();
            }
            return View(cashPayment);
        }

        // POST: CashPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CashPayment cashPayment = db.CashPayments.Find(id);
            db.CashPayments.Remove(cashPayment);
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
