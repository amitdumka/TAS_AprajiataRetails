using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TAS_AprajiataRetails.Models.Data;

namespace TAS_AprajiataRetails.Controllers
{
    public class DailySalesController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        // GET: DailySales
        public ActionResult Index()
        {
            var dailySales = db.DailySales.Include(d => d.Salesman);
            return View(dailySales.ToList());
        }

        // GET: DailySales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailySale dailySale = db.DailySales.Find(id);
            if (dailySale == null)
            {
                return HttpNotFound();
            }
            return View(dailySale);
        }

        // GET: DailySales/Create
        public ActionResult Create()
        {
            ViewBag.SalesmanId = new SelectList(db.Salesmen, "SalesmanId", "SalesmanName");
            return View();
        }

        // POST: DailySales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DailySaleId,SaleDate,InvNo,Amount,PayMode,CashAmount,SalesmanId,IsDue,IsManualBill,IsTailoringBill,Remarks")] DailySale dailySale)
        {
            if (ModelState.IsValid)
            {
                db.DailySales.Add(dailySale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SalesmanId = new SelectList(db.Salesmen, "SalesmanId", "SalesmanName", dailySale.SalesmanId);
            return View(dailySale);
        }

        // GET: DailySales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailySale dailySale = db.DailySales.Find(id);
            if (dailySale == null)
            {
                return HttpNotFound();
            }
            ViewBag.SalesmanId = new SelectList(db.Salesmen, "SalesmanId", "SalesmanName", dailySale.SalesmanId);
            return View(dailySale);
        }

        // POST: DailySales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DailySaleId,SaleDate,InvNo,Amount,PayMode,CashAmount,SalesmanId,IsDue,IsManualBill,IsTailoringBill,Remarks")] DailySale dailySale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dailySale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SalesmanId = new SelectList(db.Salesmen, "SalesmanId", "SalesmanName", dailySale.SalesmanId);
            return View(dailySale);
        }

        // GET: DailySales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailySale dailySale = db.DailySales.Find(id);
            if (dailySale == null)
            {
                return HttpNotFound();
            }
            return View(dailySale);
        }

        // POST: DailySales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DailySale dailySale = db.DailySales.Find(id);
            db.DailySales.Remove(dailySale);
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
