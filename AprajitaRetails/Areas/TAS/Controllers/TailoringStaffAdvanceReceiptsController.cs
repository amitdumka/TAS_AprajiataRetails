using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AprajitaRetails.Areas.TAS.Models.Data;
using AprajitaRetails.Areas.TAS.Models.Helpers;

namespace AprajitaRetails.Areas.TAS.Controllers
{
    [Authorize]
    public class TailoringStaffAdvanceReceiptsController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();


        private void ProcessAccounts(TailoringStaffAdvanceReceipt objectName, bool isIn)
        {
            if (isIn)
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
            else
            {
                if (objectName.PayMode == PayModes.Cash)
                {
                    Utils.UpDateCashInHand(db, objectName.ReceiptDate, 0 - objectName.Amount);

                }
                //TODO: in future make it more robust
                if (objectName.PayMode != PayModes.Cash && objectName.PayMode != PayModes.Coupons && objectName.PayMode != PayModes.Points)
                {
                    Utils.UpDateCashInBank(db, objectName.ReceiptDate, 0 - objectName.Amount);
                }
            }


        }


        // GET: TailoringStaffAdvanceReceipts
        public ActionResult Index()
        {
            var tailoringStaffAdvanceReceipts = db.TailoringStaffAdvanceReceipts.Include(t => t.Employee);
            return View(tailoringStaffAdvanceReceipts.ToList());
        }

        // GET: TailoringStaffAdvanceReceipts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TailoringStaffAdvanceReceipt tailoringStaffAdvanceReceipt = db.TailoringStaffAdvanceReceipts.Find(id);
            if (tailoringStaffAdvanceReceipt == null)
            {
                return HttpNotFound();
            }
            return PartialView(tailoringStaffAdvanceReceipt);
        }

        // GET: TailoringStaffAdvanceReceipts/Create
        public ActionResult Create()
        {
            ViewBag.TailoringEmployeeId = new SelectList(db.Tailors, "TailoringEmployeeId", "StaffName");
            return PartialView();
        }

        // POST: TailoringStaffAdvanceReceipts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TailoringStaffAdvanceReceiptId,TailoringEmployeeId,ReceiptDate,Amount,PayMode,Details")] TailoringStaffAdvanceReceipt tailoringStaffAdvanceReceipt)
        {
            if (ModelState.IsValid)
            {
                db.TailoringStaffAdvanceReceipts.Add(tailoringStaffAdvanceReceipt);
                ProcessAccounts(tailoringStaffAdvanceReceipt, true);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TailoringEmployeeId = new SelectList(db.Tailors, "TailoringEmployeeId", "StaffName", tailoringStaffAdvanceReceipt.TailoringEmployeeId);
            return View(tailoringStaffAdvanceReceipt);
        }

        // GET: TailoringStaffAdvanceReceipts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TailoringStaffAdvanceReceipt tailoringStaffAdvanceReceipt = db.TailoringStaffAdvanceReceipts.Find(id);
            if (tailoringStaffAdvanceReceipt == null)
            {
                return HttpNotFound();
            }
            ViewBag.TailoringEmployeeId = new SelectList(db.Tailors, "TailoringEmployeeId", "StaffName", tailoringStaffAdvanceReceipt.TailoringEmployeeId);
            return View(tailoringStaffAdvanceReceipt);
        }

        // POST: TailoringStaffAdvanceReceipts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TailoringStaffAdvanceReceiptId,TailoringEmployeeId,ReceiptDate,Amount,PayMode,Details")] TailoringStaffAdvanceReceipt tailoringStaffAdvanceReceipt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tailoringStaffAdvanceReceipt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TailoringEmployeeId = new SelectList(db.Tailors, "TailoringEmployeeId", "StaffName", tailoringStaffAdvanceReceipt.TailoringEmployeeId);
            return View(tailoringStaffAdvanceReceipt);
        }

        // GET: TailoringStaffAdvanceReceipts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TailoringStaffAdvanceReceipt tailoringStaffAdvanceReceipt = db.TailoringStaffAdvanceReceipts.Find(id);
            if (tailoringStaffAdvanceReceipt == null)
            {
                return HttpNotFound();
            }
            return View(tailoringStaffAdvanceReceipt);
        }

        // POST: TailoringStaffAdvanceReceipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TailoringStaffAdvanceReceipt tailoringStaffAdvanceReceipt = db.TailoringStaffAdvanceReceipts.Find(id);
            ProcessAccounts(tailoringStaffAdvanceReceipt, false);
            db.TailoringStaffAdvanceReceipts.Remove(tailoringStaffAdvanceReceipt);
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
