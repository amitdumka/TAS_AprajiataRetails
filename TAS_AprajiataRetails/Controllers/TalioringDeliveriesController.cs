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
    [Authorize]
    public class TalioringDeliveriesController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        // GET: TalioringDeliveries
        public ActionResult Index()
        {
            var deliveries = db.Deliveries.Include(t => t.Booking);
            return View(deliveries.ToList());
        }

        // GET: TalioringDeliveries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TalioringDelivery talioringDelivery = db.Deliveries.Find(id);
            if (talioringDelivery == null)
            {
                return HttpNotFound();
            }
            return PartialView(talioringDelivery);
        }

        // GET: TalioringDeliveries/Create
        public ActionResult Create()
        {
            ViewBag.TalioringBookingId = new SelectList(db.Bookings, "TalioringBookingId", "CustName");
            return PartialView();
        }

        // POST: TalioringDeliveries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TalioringDeliveryId,DeliveryDate,TalioringBookingId,InvNo,Amount,Remarks")] TalioringDelivery talioringDelivery)
        {
            if (ModelState.IsValid)
            {
                db.Deliveries.Add(talioringDelivery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TalioringBookingId = new SelectList(db.Bookings, "TalioringBookingId", "CustName", talioringDelivery.TalioringBookingId);
            return View(talioringDelivery);
        }

        // GET: TalioringDeliveries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TalioringDelivery talioringDelivery = db.Deliveries.Find(id);
            if (talioringDelivery == null)
            {
                return HttpNotFound();
            }
            ViewBag.TalioringBookingId = new SelectList(db.Bookings, "TalioringBookingId", "CustName", talioringDelivery.TalioringBookingId);
            return View(talioringDelivery);
        }

        // POST: TalioringDeliveries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TalioringDeliveryId,DeliveryDate,TalioringBookingId,InvNo,Amount,Remarks")] TalioringDelivery talioringDelivery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(talioringDelivery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TalioringBookingId = new SelectList(db.Bookings, "TalioringBookingId", "CustName", talioringDelivery.TalioringBookingId);
            return View(talioringDelivery);
        }

        // GET: TalioringDeliveries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TalioringDelivery talioringDelivery = db.Deliveries.Find(id);
            if (talioringDelivery == null)
            {
                return HttpNotFound();
            }
            return View(talioringDelivery);
        }

        // POST: TalioringDeliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TalioringDelivery talioringDelivery = db.Deliveries.Find(id);
            db.Deliveries.Remove(talioringDelivery);
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
