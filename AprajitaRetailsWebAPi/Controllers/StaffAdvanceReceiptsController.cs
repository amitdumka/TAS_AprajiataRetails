using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TAS_AprajiataRetails.Models.Data;
using TAS_AprajiataRetails.Models.Helpers;

namespace TAS_AprajiataRetails.Controllers
{
    [Authorize]
    public class StaffAdvanceReceiptsController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        private void ProcessAccounts(StaffAdvanceReceipt objectName)
        {

            if (objectName.PayMode == PayModes.Cash)
            {
                Utils.UpDateCashInHand(db, objectName.ReceiptDate, objectName.Amount);

            }
            //TODO: in future make it more robust
            if (objectName.PayMode != PayModes.Cash && objectName.PayMode != PayModes.Coupons && objectName.PayMode != PayModes.Points)
            {
                Utils.UpDateCashInBank(db, objectName.ReceiptDate, objectName.Amount);
            }



        }
        // GET: StaffAdvanceReceipts
        public ActionResult Index()
        {
            var staffAdvanceReceipts = db.StaffAdvanceReceipts.Include(s => s.Employee);
            return View(staffAdvanceReceipts.ToList());
        }

        // GET: StaffAdvanceReceipts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffAdvanceReceipt staffAdvanceReceipt = db.StaffAdvanceReceipts.Find(id);
            if (staffAdvanceReceipt == null)
            {
                return HttpNotFound();
            }
            return PartialView(staffAdvanceReceipt);
        }

        // GET: StaffAdvanceReceipts/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName");
            return PartialView();
        }

        // POST: StaffAdvanceReceipts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffAdvanceReceiptId,EmployeeId,ReceiptDate,Amount,PayMode,Details")] StaffAdvanceReceipt staffAdvanceReceipt)
        {
            if (ModelState.IsValid)
            {
                db.StaffAdvanceReceipts.Add(staffAdvanceReceipt);
                ProcessAccounts(staffAdvanceReceipt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName", staffAdvanceReceipt.EmployeeId);
            return View(staffAdvanceReceipt);
        }

        // GET: StaffAdvanceReceipts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffAdvanceReceipt staffAdvanceReceipt = db.StaffAdvanceReceipts.Find(id);
            if (staffAdvanceReceipt == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName", staffAdvanceReceipt.EmployeeId);
            return View(staffAdvanceReceipt);
        }

        // POST: StaffAdvanceReceipts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffAdvanceReceiptId,EmployeeId,ReceiptDate,Amount,PayMode,Details")] StaffAdvanceReceipt staffAdvanceReceipt)
        {
            if (ModelState.IsValid)
            {
                ProcessAccounts(staffAdvanceReceipt);
                db.Entry(staffAdvanceReceipt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "StaffName", staffAdvanceReceipt.EmployeeId);
            return View(staffAdvanceReceipt);
        }

        // GET: StaffAdvanceReceipts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffAdvanceReceipt staffAdvanceReceipt = db.StaffAdvanceReceipts.Find(id);
            if (staffAdvanceReceipt == null)
            {
                return HttpNotFound();
            }
            return View(staffAdvanceReceipt);
        }

        // POST: StaffAdvanceReceipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffAdvanceReceipt staffAdvanceReceipt = db.StaffAdvanceReceipts.Find(id);
            db.StaffAdvanceReceipts.Remove(staffAdvanceReceipt);
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
