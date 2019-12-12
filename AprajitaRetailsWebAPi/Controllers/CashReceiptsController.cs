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
    public class CashReceiptsController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        // GET: CashReceipts
        public ActionResult Index()
        {
            var cashReceipts = db.CashReceipts.Include(c => c.Mode);
            return View(cashReceipts.ToList());
        }

        // GET: CashReceipts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashReceipt cashReceipt = db.CashReceipts.Find(id);
            if (cashReceipt == null)
            {
                return HttpNotFound();
            }
            return PartialView(cashReceipt);
        }

        // GET: CashReceipts/Create
        public ActionResult Create()
        {
            ViewBag.TranscationModeId = new SelectList(db.TranscationModes, "TranscationModeId", "Transcation");
            return PartialView();
        }

        // POST: CashReceipts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CashReceiptId,InwardDate,TranscationModeId,ReceiptFrom,Amount,SlipNo")] CashReceipt cashReceipt)
        {
            if (ModelState.IsValid)
            {
                Utils.UpDateCashInHand(db, cashReceipt.InwardDate, cashReceipt.Amount);
                db.CashReceipts.Add(cashReceipt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TranscationModeId = new SelectList(db.TranscationModes, "TranscationModeId", "Transcation", cashReceipt.TranscationModeId);
            return View(cashReceipt);
        }

        // GET: CashReceipts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashReceipt cashReceipt = db.CashReceipts.Find(id);
            if (cashReceipt == null)
            {
                return HttpNotFound();
            }
            ViewBag.TranscationModeId = new SelectList(db.TranscationModes, "TranscationModeId", "Transcation", cashReceipt.TranscationModeId);
            return View(cashReceipt);
        }

        // POST: CashReceipts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CashReceiptId,InwardDate,TranscationModeId,ReceiptFrom,Amount,SlipNo")] CashReceipt cashReceipt)
        {
            if (ModelState.IsValid)
            {
                Utils.UpDateCashInHand(db, cashReceipt.InwardDate, cashReceipt.Amount);
                db.Entry(cashReceipt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TranscationModeId = new SelectList(db.TranscationModes, "TranscationModeId", "Transcation", cashReceipt.TranscationModeId);
            return View(cashReceipt);
        }

        // GET: CashReceipts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashReceipt cashReceipt = db.CashReceipts.Find(id);
            if (cashReceipt == null)
            {
                return HttpNotFound();
            }
            return View(cashReceipt);
        }

        // POST: CashReceipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CashReceipt cashReceipt = db.CashReceipts.Find(id);
            db.CashReceipts.Remove(cashReceipt);
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
