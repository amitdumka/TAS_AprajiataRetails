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
    public class ProductPurchasesController : Controller
    {
        private VoyagerContext db = new VoyagerContext();


        public ActionResult PurchaseDetails(int? id)
        {
            if (id > 0)
            {
                var productPurchases1 = db.ProductPurchases.Include(p => p.Supplier).Include(c => c.PurchaseItems).Where(c => c.ProductPurchaseId == id);
                ViewBag.Details = "Invoice No: "+productPurchases1.FirstOrDefault().InvoiceNo;
                return View(productPurchases1.ToList());
            }
            ViewBag.Details = "";
          var productPurchases = db.ProductPurchases.Include(p => p.Supplier).Include(c=>c.PurchaseItems);
            return View(productPurchases.ToList());
        }
        // GET: ProductPurchases
        public ActionResult Index()
        {
            var productPurchases = db.ProductPurchases.Include(p => p.Supplier);
           
            return View(productPurchases.ToList()); 
        }

        // GET: ProductPurchases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPurchase productPurchase = db.ProductPurchases.Find(id);
            if (productPurchase == null)
            {
                return HttpNotFound();
            }
            return View(productPurchase);
        }

        // GET: ProductPurchases/Create
        public ActionResult Create()
        {
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SuppilerName");
            return View();
        }

        // POST: ProductPurchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductPurchaseId,InWardNo,InWardDate,PurchaseDate,InvoiceNo,TotalQty,TotalBasicAmount,ShippingCost,TotalTax,TotalAmount,Remarks,SupplierID,IsPaid")] ProductPurchase productPurchase)
        {
            if (ModelState.IsValid)
            {
                db.ProductPurchases.Add(productPurchase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SuppilerName", productPurchase.SupplierID);
            return View(productPurchase);
        }

        // GET: ProductPurchases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPurchase productPurchase = db.ProductPurchases.Find(id);
            if (productPurchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SuppilerName", productPurchase.SupplierID);
            return View(productPurchase);
        }

        // POST: ProductPurchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductPurchaseId,InWardNo,InWardDate,PurchaseDate,InvoiceNo,TotalQty,TotalBasicAmount,ShippingCost,TotalTax,TotalAmount,Remarks,SupplierID,IsPaid")] ProductPurchase productPurchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productPurchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SuppilerName", productPurchase.SupplierID);
            return View(productPurchase);
        }

        // GET: ProductPurchases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPurchase productPurchase = db.ProductPurchases.Find(id);
            if (productPurchase == null)
            {
                return HttpNotFound();
            }
            return View(productPurchase);
        }

        // POST: ProductPurchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductPurchase productPurchase = db.ProductPurchases.Find(id);
            db.ProductPurchases.Remove(productPurchase);
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
