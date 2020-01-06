using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AprajitaRetails.Models.Data.Voyagers;

namespace AprajitaRetails.Controllers
{
    public class PurchaseTaxTypesController : Controller
    {
        private VoyagerContext db = new VoyagerContext();

        // GET: PurchaseTaxTypes
        public ActionResult Index()
        {
            return View(db.PurchaseTaxTypes.ToList());
        }

        // GET: PurchaseTaxTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseTaxType purchaseTaxType = db.PurchaseTaxTypes.Find(id);
            if (purchaseTaxType == null)
            {
                return HttpNotFound();
            }
            return View(purchaseTaxType);
        }

        // GET: PurchaseTaxTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PurchaseTaxTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PurchaseTaxTypeId,TaxName,TaxType,CompositeRate")] PurchaseTaxType purchaseTaxType)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseTaxTypes.Add(purchaseTaxType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(purchaseTaxType);
        }

        // GET: PurchaseTaxTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseTaxType purchaseTaxType = db.PurchaseTaxTypes.Find(id);
            if (purchaseTaxType == null)
            {
                return HttpNotFound();
            }
            return View(purchaseTaxType);
        }

        // POST: PurchaseTaxTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PurchaseTaxTypeId,TaxName,TaxType,CompositeRate")] PurchaseTaxType purchaseTaxType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseTaxType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchaseTaxType);
        }

        // GET: PurchaseTaxTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseTaxType purchaseTaxType = db.PurchaseTaxTypes.Find(id);
            if (purchaseTaxType == null)
            {
                return HttpNotFound();
            }
            return View(purchaseTaxType);
        }

        // POST: PurchaseTaxTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseTaxType purchaseTaxType = db.PurchaseTaxTypes.Find(id);
            db.PurchaseTaxTypes.Remove(purchaseTaxType);
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
