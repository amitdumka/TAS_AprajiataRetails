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
    public class ProductItemsController : Controller
    {
        private VoyagerContext db = new VoyagerContext();

        // GET: ProductItems
        public ActionResult Index()
        {
            var productItems = db.ProductItems.Include(p => p.BrandName).Include(c=>c.MainCategory).Include(c=>c.ProductCategory).Include(c=>c.ProductType);
            
            return View(productItems.ToList());
        }

        // GET: ProductItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productItem = db.ProductItems.Find(id);
            if (productItem == null)
            {
                return HttpNotFound();
            }
            return View(productItem);
        }

        // GET: ProductItems/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName");
            return View();
        }

        // POST: ProductItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductItemId,Barcode,BrandId,StyleCode,ProductName,ItemDesc,Categorys,MRP,TaxRate,Cost,Size,Units")] ProductItem productItem)
        {
            if (ModelState.IsValid)
            {
                db.ProductItems.Add(productItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", productItem.BrandId);
            return View(productItem);
        }

        // GET: ProductItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productItem = db.ProductItems.Find(id);
            if (productItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", productItem.BrandId);
            return View(productItem);
        }

        // POST: ProductItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductItemId,Barcode,BrandId,StyleCode,ProductName,ItemDesc,Categorys,MRP,TaxRate,Cost,Size,Units")] ProductItem productItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", productItem.BrandId);
            return View(productItem);
        }

        // GET: ProductItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productItem = db.ProductItems.Find(id);
            if (productItem == null)
            {
                return HttpNotFound();
            }
            return View(productItem);
        }

        // POST: ProductItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductItem productItem = db.ProductItems.Find(id);
            db.ProductItems.Remove(productItem);
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
