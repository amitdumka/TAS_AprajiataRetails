using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAS_AprajiataRetails.Models.Data;
using TAS_AprajiataRetails.Models.Helpers;
using TAS_AprajiataRetails.Models.Views;

namespace TAS_AprajiataRetails.Controllers
{
    public class CashBookController : Controller
    {
       // private AprajitaRetailsContext db = new AprajitaRetailsContext();
        // GET: CashBook
        public ActionResult Index(DateTime? EDate)
        {
            DateTime date = EDate ?? DateTime.Today.AddDays(-1);

            List<CashBook> book = Reports.GetMontlyCashBook(date);
            if (book != null)
                return View(book);
            else
                return View();
        }

        // GET: CashBook/Details/5
        public ActionResult Details(int? id, DateTime? EDate)
        {
            DateTime date = EDate ?? DateTime.Today.AddDays(-1);

            if (id != null && id == 101)
            {
                List<CashBook> book1 = Reports.CorrectCashInHand_OnDB(date);
                if (book1 != null)
                    return View(book1);
                else
                    return View();
            }


            List<CashBook> book = Reports.GetMontlyCashBook(date);
            if (book != null)
                return View(book);
            else
                return View();


            
        }

        // GET: CashBook/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CashBook/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CashBook/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CashBook/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CashBook/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CashBook/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
