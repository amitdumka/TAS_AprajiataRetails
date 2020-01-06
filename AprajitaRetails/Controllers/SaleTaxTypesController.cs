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
    public class SaleTaxTypesController : Controller
    {
        private VoyagerContext db = new VoyagerContext();

        // GET: SaleTaxTypes
        public ActionResult Index()
        {
            return View(db.SaleTaxTypes.ToList());
        }

        // GET: SaleTaxTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleTaxType saleTaxType = db.SaleTaxTypes.Find(id);
            if (saleTaxType == null)
            {
                return HttpNotFound();
            }
            return View(saleTaxType);
        }

        // GET: SaleTaxTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SaleTaxTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaleTaxTypeId,TaxName,TaxType,CompositeRate")] SaleTaxType saleTaxType)
        {
            if (ModelState.IsValid)
            {
                db.SaleTaxTypes.Add(saleTaxType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(saleTaxType);
        }

        // GET: SaleTaxTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleTaxType saleTaxType = db.SaleTaxTypes.Find(id);
            if (saleTaxType == null)
            {
                return HttpNotFound();
            }
            return View(saleTaxType);
        }

        // POST: SaleTaxTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaleTaxTypeId,TaxName,TaxType,CompositeRate")] SaleTaxType saleTaxType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saleTaxType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(saleTaxType);
        }

        // GET: SaleTaxTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleTaxType saleTaxType = db.SaleTaxTypes.Find(id);
            if (saleTaxType == null)
            {
                return HttpNotFound();
            }
            return View(saleTaxType);
        }

        // POST: SaleTaxTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SaleTaxType saleTaxType = db.SaleTaxTypes.Find(id);
            db.SaleTaxTypes.Remove(saleTaxType);
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
