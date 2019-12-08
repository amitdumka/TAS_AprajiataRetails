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
    public class SuspenseAccountsController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        // GET: SuspenseAccounts
        public ActionResult Index()
        {
            return View(db.Suspenses.ToList());
        }

        // GET: SuspenseAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspenseAccount suspenseAccount = db.Suspenses.Find(id);
            if (suspenseAccount == null)
            {
                return HttpNotFound();
            }
            return View(suspenseAccount);
        }

        // GET: SuspenseAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SuspenseAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SuspenseAccountId,EntryDate,ReferanceDetails,InAmount,OutAmount,IsCleared,ClearedDetails,ReviewBy")] SuspenseAccount suspenseAccount)
        {
            if (ModelState.IsValid)
            {
                db.Suspenses.Add(suspenseAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(suspenseAccount);
        }

        // GET: SuspenseAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspenseAccount suspenseAccount = db.Suspenses.Find(id);
            if (suspenseAccount == null)
            {
                return HttpNotFound();
            }
            return View(suspenseAccount);
        }

        // POST: SuspenseAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SuspenseAccountId,EntryDate,ReferanceDetails,InAmount,OutAmount,IsCleared,ClearedDetails,ReviewBy")] SuspenseAccount suspenseAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suspenseAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(suspenseAccount);
        }

        // GET: SuspenseAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspenseAccount suspenseAccount = db.Suspenses.Find(id);
            if (suspenseAccount == null)
            {
                return HttpNotFound();
            }
            return View(suspenseAccount);
        }

        // POST: SuspenseAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SuspenseAccount suspenseAccount = db.Suspenses.Find(id);
            db.Suspenses.Remove(suspenseAccount);
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
