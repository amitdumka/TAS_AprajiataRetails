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
    public class ChequesLogsController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        // GET: ChequesLogs
        public ActionResult Index()
        {
            return View(db.Cheques.ToList());
        }

        // GET: ChequesLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChequesLog chequesLog = db.Cheques.Find(id);
            if (chequesLog == null)
            {
                return HttpNotFound();
            }
            return PartialView(chequesLog);
        }

        // GET: ChequesLogs/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: ChequesLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChequesLogId,BankName,AccountNumber,ChequesDate,DepositDate,ClearedDate,IssuedBy,IssuedTo,Amount,IsPDC,IsIssuedByAprajitaRetails,IsDepositedOnAprajitaRetails,Remarks")] ChequesLog chequesLog)
        {
            if (ModelState.IsValid)
            {
                db.Cheques.Add(chequesLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return PartialView(chequesLog);
        }

        // GET: ChequesLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChequesLog chequesLog = db.Cheques.Find(id);
            if (chequesLog == null)
            {
                return HttpNotFound();
            }
            return PartialView(chequesLog);
        }

        // POST: ChequesLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChequesLogId,BankName,AccountNumber,ChequesDate,DepositDate,ClearedDate,IssuedBy,IssuedTo,Amount,IsPDC,IsIssuedByAprajitaRetails,IsDepositedOnAprajitaRetails,Remarks")] ChequesLog chequesLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chequesLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chequesLog);
        }

        // GET: ChequesLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChequesLog chequesLog = db.Cheques.Find(id);
            if (chequesLog == null)
            {
                return HttpNotFound();
            }
            return PartialView(chequesLog);
        }

        // POST: ChequesLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChequesLog chequesLog = db.Cheques.Find(id);
            db.Cheques.Remove(chequesLog);
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
