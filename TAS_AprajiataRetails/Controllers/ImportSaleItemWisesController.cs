using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TAS_AprajiataRetails.Models.Data.Voyagers;

namespace TAS_AprajiataRetails.Controllers
{
    public class ImportSaleItemWisesController : Controller
    {
        private VoyagerContext db = new VoyagerContext();

        // GET: ImportSaleItemWises
        public ActionResult Index()
        {
            return View(db.ImportSaleItemWises.ToList());
        }

        // GET: ImportSaleItemWises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportSaleItemWise importSaleItemWise = db.ImportSaleItemWises.Find(id);
            if (importSaleItemWise == null)
            {
                return HttpNotFound();
            }
            return View(importSaleItemWise);
        }

        // GET: ImportSaleItemWises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImportSaleItemWises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImportSaleItemWiseId,InvoiceDate,InvoiceNo,InvoiceType,BrandName,ProductName,ItemDesc,HSNCode,Barcode,StyleCode,Quantity,MRP,Discount,BasicRate,Tax,SGST,CGST,LineTotal,RoundOff,BillAmnt,PaymentType,Saleman,IsDataConsumed,ImportTime")] ImportSaleItemWise importSaleItemWise)
        {
            if (ModelState.IsValid)
            {
                db.ImportSaleItemWises.Add(importSaleItemWise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(importSaleItemWise);
        }

        // GET: ImportSaleItemWises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportSaleItemWise importSaleItemWise = db.ImportSaleItemWises.Find(id);
            if (importSaleItemWise == null)
            {
                return HttpNotFound();
            }
            return View(importSaleItemWise);
        }

        // POST: ImportSaleItemWises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImportSaleItemWiseId,InvoiceDate,InvoiceNo,InvoiceType,BrandName,ProductName,ItemDesc,HSNCode,Barcode,StyleCode,Quantity,MRP,Discount,BasicRate,Tax,SGST,CGST,LineTotal,RoundOff,BillAmnt,PaymentType,Saleman,IsDataConsumed,ImportTime")] ImportSaleItemWise importSaleItemWise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(importSaleItemWise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(importSaleItemWise);
        }

        // GET: ImportSaleItemWises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportSaleItemWise importSaleItemWise = db.ImportSaleItemWises.Find(id);
            if (importSaleItemWise == null)
            {
                return HttpNotFound();
            }
            return View(importSaleItemWise);
        }

        // POST: ImportSaleItemWises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImportSaleItemWise importSaleItemWise = db.ImportSaleItemWises.Find(id);
            db.ImportSaleItemWises.Remove(importSaleItemWise);
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
