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
    public class ImportInWardsController : Controller
    {
        private VoyagerContext db = new VoyagerContext();

        // GET: ImportInWards
        public ActionResult Index()
        {
            return View(db.ImportInWards.ToList());
        }

        // GET: ImportInWards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportInWard importInWard = db.ImportInWards.Find(id);
            if (importInWard == null)
            {
                return HttpNotFound();
            }
            return View(importInWard);
        }

        // GET: ImportInWards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImportInWards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImportInWardId,InWardNo,InWardDate,InvoiceNo,InvoiceDate,PartyName,TotalQty,TotalMRPValue,TotalCost,ImportDate")] ImportInWard importInWard)
        {
            if (ModelState.IsValid)
            {
                db.ImportInWards.Add(importInWard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(importInWard);
        }

        // GET: ImportInWards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportInWard importInWard = db.ImportInWards.Find(id);
            if (importInWard == null)
            {
                return HttpNotFound();
            }
            return View(importInWard);
        }

        // POST: ImportInWards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImportInWardId,InWardNo,InWardDate,InvoiceNo,InvoiceDate,PartyName,TotalQty,TotalMRPValue,TotalCost,ImportDate")] ImportInWard importInWard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(importInWard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(importInWard);
        }

        // GET: ImportInWards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportInWard importInWard = db.ImportInWards.Find(id);
            if (importInWard == null)
            {
                return HttpNotFound();
            }
            return View(importInWard);
        }

        // POST: ImportInWards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImportInWard importInWard = db.ImportInWards.Find(id);
            db.ImportInWards.Remove(importInWard);
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
