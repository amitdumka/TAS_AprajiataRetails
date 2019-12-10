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
    public class TalioringBookingsController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        // GET: TalioringBookings
        public ActionResult Index()
        {
            return View(db.Bookings.ToList());
        }

        // GET: TalioringBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TalioringBooking talioringBooking = db.Bookings.Find(id);
            if (talioringBooking == null)
            {
                return HttpNotFound();
            }
            return PartialView(talioringBooking);
        }

        // GET: TalioringBookings/Create
        public ActionResult Create()
        {
            TalioringBooking booking = new TalioringBooking();
            booking.BookingDate = DateTime.Today;
            booking.TryDate = DateTime.Today.AddDays(3);
            booking.DeliveryDate = DateTime.Today.AddDays(6);
            return PartialView(booking);
        }

        // POST: TalioringBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TalioringBookingId,BookingDate,CustName,DeliveryDate,TryDate,BookingSlipNo,TotalAmount,TotalQty,ShirtQty,ShirtPrice,PantQty,PantPrice,CoatQty,CoatPrice,KurtaQty,KurtaPrice,BundiQty,BundiPrice,Others,OthersPrice")] TalioringBooking talioringBooking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(talioringBooking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(talioringBooking);
        }

        // GET: TalioringBookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TalioringBooking talioringBooking = db.Bookings.Find(id);
            if (talioringBooking == null)
            {
                return HttpNotFound();
            }
            return View(talioringBooking);
        }

        // POST: TalioringBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TalioringBookingId,BookingDate,CustName,DeliveryDate,TryDate,BookingSlipNo,TotalAmount,TotalQty,ShirtQty,ShirtPrice,PantQty,PantPrice,CoatQty,CoatPrice,KurtaQty,KurtaPrice,BundiQty,BundiPrice,Others,OthersPrice")] TalioringBooking talioringBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(talioringBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(talioringBooking);
        }

        // GET: TalioringBookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TalioringBooking talioringBooking = db.Bookings.Find(id);
            if (talioringBooking == null)
            {
                return HttpNotFound();
            }
            return View(talioringBooking);
        }

        // POST: TalioringBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TalioringBooking talioringBooking = db.Bookings.Find(id);
            db.Bookings.Remove(talioringBooking);
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
