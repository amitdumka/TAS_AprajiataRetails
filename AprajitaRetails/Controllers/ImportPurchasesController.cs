using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AprajitaRetails.Models.Data.Voyagers;

namespace AprajitaRetailsControllers
{
    public class ImportPurchasesController : Controller
    {
        private VoyagerContext db = new VoyagerContext();

        // GET: ImportPurchases
        public ActionResult Index()
        {
            return View(db.ImportPurchases.ToList());
        }

        // GET: ImportPurchases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportPurchase importPurchase = db.ImportPurchases.Find(id);
            if (importPurchase == null)
            {
                return HttpNotFound();
            }
            return View(importPurchase);
        }

        // GET: ImportPurchases/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImportPurchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImportPurchaseId,GRNNo,GRNDate,InvoiceNo,InvoiceDate,SupplierName,Barcode,ProductName,StyleCode,ItemDesc,Quantity,MRP,MRPValue,Cost,CostValue,TaxAmt,IsDataConsumed,ImportTime")] ImportPurchase importPurchase)
        {
            if (ModelState.IsValid)
            {
                db.ImportPurchases.Add(importPurchase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(importPurchase);
        }

        // GET: ImportPurchases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportPurchase importPurchase = db.ImportPurchases.Find(id);
            if (importPurchase == null)
            {
                return HttpNotFound();
            }
            return View(importPurchase);
        }

        // POST: ImportPurchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImportPurchaseId,GRNNo,GRNDate,InvoiceNo,InvoiceDate,SupplierName,Barcode,ProductName,StyleCode,ItemDesc,Quantity,MRP,MRPValue,Cost,CostValue,TaxAmt,IsDataConsumed,ImportTime")] ImportPurchase importPurchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(importPurchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(importPurchase);
        }

        // GET: ImportPurchases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportPurchase importPurchase = db.ImportPurchases.Find(id);
            if (importPurchase == null)
            {
                return HttpNotFound();
            }
            return View(importPurchase);
        }

        // POST: ImportPurchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImportPurchase importPurchase = db.ImportPurchases.Find(id);
            db.ImportPurchases.Remove(importPurchase);
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
