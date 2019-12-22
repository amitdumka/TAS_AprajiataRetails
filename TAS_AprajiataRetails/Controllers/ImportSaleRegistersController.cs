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
    public class ImportSaleRegistersController : Controller
    {
        private VoyagerContext db = new VoyagerContext();

        // GET: ImportSaleRegisters
        public ActionResult Index()
        {
            return View(db.ImportSaleRegisters.ToList());
        }

        // GET: ImportSaleRegisters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportSaleRegister importSaleRegister = db.ImportSaleRegisters.Find(id);
            if (importSaleRegister == null)
            {
                return HttpNotFound();
            }
            return View(importSaleRegister);
        }

        // GET: ImportSaleRegisters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImportSaleRegisters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImportSaleRegisterId,InvoiceNo,InvoiceType,InvoiceDate,Quantity,MRP,Discount,BasicRate,Tax,RoundOff,BillAmnt,PaymentType,ImportTime")] ImportSaleRegister importSaleRegister)
        {
            if (ModelState.IsValid)
            {
                db.ImportSaleRegisters.Add(importSaleRegister);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(importSaleRegister);
        }

        // GET: ImportSaleRegisters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportSaleRegister importSaleRegister = db.ImportSaleRegisters.Find(id);
            if (importSaleRegister == null)
            {
                return HttpNotFound();
            }
            return View(importSaleRegister);
        }

        // POST: ImportSaleRegisters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImportSaleRegisterId,InvoiceNo,InvoiceType,InvoiceDate,Quantity,MRP,Discount,BasicRate,Tax,RoundOff,BillAmnt,PaymentType,ImportTime")] ImportSaleRegister importSaleRegister)
        {
            if (ModelState.IsValid)
            {
                db.Entry(importSaleRegister).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(importSaleRegister);
        }

        // GET: ImportSaleRegisters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportSaleRegister importSaleRegister = db.ImportSaleRegisters.Find(id);
            if (importSaleRegister == null)
            {
                return HttpNotFound();
            }
            return View(importSaleRegister);
        }

        // POST: ImportSaleRegisters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImportSaleRegister importSaleRegister = db.ImportSaleRegisters.Find(id);
            db.ImportSaleRegisters.Remove(importSaleRegister);
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
