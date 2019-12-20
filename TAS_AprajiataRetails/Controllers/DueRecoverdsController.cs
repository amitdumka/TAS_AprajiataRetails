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
    public class DueRecoverdsController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        private void ProcessAccounts(DueRecoverd objects)
        {
            DuesList duesList = db.DuesLists.Find(objects.DuesListId);
            if (objects.Modes == PaymentModes.Cash)
            {
                Utils.UpDateCashInHand(db, objects.PaidDate, objects.AmountPaid);
            }
            else
            {
                Utils.UpDateCashInBank(db, objects.PaidDate, objects.AmountPaid);
            }
         
            if (objects.IsPartialPayment)
            {
                duesList.IsPartialRecovery = true;
            }
            else
            {
                duesList.IsRecovered = true;
                duesList.RecoveryDate = objects.PaidDate;
            }

        }

        private void ProcessAccountEdit(DueRecoverd objects)
        {
            DueRecoverd dr = db.Recoverds.Find(objects.DueRecoverdId);
            DuesList duesList = db.DuesLists.Find(objects.DuesListId);
            

            if (dr.AmountPaid != objects.AmountPaid)
            {
                //Remove Amount from In-Hands
                if (dr.Modes == PaymentModes.Cash)
                {
                    Utils.UpDateCashInHand(db, objects.PaidDate, 0-dr.AmountPaid);
                }
                else
                {
                    Utils.UpDateCashInBank(db, objects.PaidDate, 0-dr.AmountPaid);
                }
                //Add Amount
                if (objects.Modes == PaymentModes.Cash)
                {
                    Utils.UpDateCashInHand(db, objects.PaidDate, objects.AmountPaid);
                }
                else
                {
                    Utils.UpDateCashInBank(db, objects.PaidDate, objects.AmountPaid);
                }
            }

            if (dr.IsPartialPayment != objects.IsPartialPayment)
            {
                if (objects.IsPartialPayment && dr.IsPartialPayment == false)
                {
                    duesList.IsPartialRecovery = true;
                    duesList.IsRecovered = false;
                    duesList.RecoveryDate = null;
                }
                else if (dr.IsPartialPayment && objects.IsPartialPayment == false)
                {
                    duesList.IsPartialRecovery = false;
                    duesList.IsRecovered = true;
                    duesList.RecoveryDate = objects.PaidDate;
                }
            }

            db.Entry(dr).State = EntityState.Detached;
        
        }

        private void ProcessAccountDelete(DueRecoverd objects)
        {
            DuesList duesList = db.DuesLists.Find(objects.DuesListId);
            if (objects.Modes == PaymentModes.Cash)
            {
                Utils.UpDateCashInHand(db, objects.PaidDate, 0-objects.AmountPaid);
            }
            else
            {
                Utils.UpDateCashInBank(db, objects.PaidDate, 0-objects.AmountPaid);
                
            }

            if (objects.IsPartialPayment)
            {
                duesList.IsPartialRecovery = false;
            }
            else
            {
                duesList.IsRecovered = false;
                duesList.RecoveryDate = null;
            }

        }
        // GET: DueRecoverds
        public ActionResult Index()
        {
            var recoverds = db.Recoverds.Include(d => d.DuesList).Include(d=>d.DuesList.DailySale);
            
            return View(recoverds.ToList());
        }

        // GET: DueRecoverds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DueRecoverd dueRecoverd = db.Recoverds.Find(id);
            if (dueRecoverd == null)
            {
                return HttpNotFound();
            }
            return View(dueRecoverd);
        }

        // GET: DueRecoverds/Create
        public ActionResult Create()
        {
            var dueList = db.DuesLists.Include(c=>c.DailySale).Where(c => !c.IsRecovered).ToList();
            
            ViewBag.DuesListId = new SelectList (dueList, "DuesListId", "DailySale.InvNo");
            //dueList.First().DailySale.InvNo;
            return View();
        }

        // POST: DueRecoverds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DueRecoverdId,PaidDate,DuesListId,AmountPaid,IsPartialPayment,Modes,Remarks")] DueRecoverd dueRecoverd)
        {
            if (ModelState.IsValid)
            {
                db.Recoverds.Add(dueRecoverd);
                ProcessAccounts(dueRecoverd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DuesListId = new SelectList(db.DuesLists, "DuesListId", "DuesListId", dueRecoverd.DuesListId);
            return View(dueRecoverd);
        }

        // GET: DueRecoverds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DueRecoverd dueRecoverd = db.Recoverds.Find(id);
            if (dueRecoverd == null)
            {
                return HttpNotFound();
            }
            ViewBag.DuesListId = new SelectList(db.DuesLists, "DuesListId", "DuesListId", dueRecoverd.DuesListId);
            return View(dueRecoverd);
        }

        // POST: DueRecoverds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DueRecoverdId,PaidDate,DuesListId,AmountPaid,IsPartialPayment,Modes,Remarks")] DueRecoverd dueRecoverd)
        {
            if (ModelState.IsValid)
            {
                ProcessAccountEdit(dueRecoverd);
                db.Entry(dueRecoverd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DuesListId = new SelectList(db.DuesLists, "DuesListId", "DuesListId", dueRecoverd.DuesListId);
            return View(dueRecoverd);
        }

        // GET: DueRecoverds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DueRecoverd dueRecoverd = db.Recoverds.Find(id);
            if (dueRecoverd == null)
            {
                return HttpNotFound();
            }
            return View(dueRecoverd);
        }

        // POST: DueRecoverds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DueRecoverd dueRecoverd = db.Recoverds.Find(id);
            ProcessAccountDelete(dueRecoverd);
            db.Recoverds.Remove(dueRecoverd);
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
