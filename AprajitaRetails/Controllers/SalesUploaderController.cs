using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AprajitaRetails.Models.Data.Voyagers;
using AprajitaRetailsOps.TAS;
using System.Data.Entity;
using AprajitaRetails.Ops.TAS;

namespace AprajitaRetails.Controllers
{
    public class SalesUploaderController : Controller
    {
        private VoyagerContext db = new VoyagerContext();

        // GET: SalesUploader
        public ActionResult Index()
        {
            return View();
        }
        
        // GET: SalesUploader/SaleList/5
        public ActionResult SaleList(int? id)
        {
            var md = db.ImportSaleItemWises.Where(c => c.IsDataConsumed == false).OrderByDescending(c => c.InvoiceDate);
            return View(md);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessSale(string dDate)
        {
            DateTime ddDate = DateTime.Parse(dDate).Date;

            InventoryManger iManage = new InventoryManger();
            int a = iManage.CreateSaleEntry(ddDate);
            if (a > 0)
            {
                var dm = db.SaleInvoices.Include(c => c.PaymentDetail).Where(c => c.OnDate == ddDate);
                ViewBag.MessageHead = "No. Of Sale Invoice Created  and item processed are " + a;
                return View(dm.ToList());
            }
            else
            {
                ViewBag.MessageHead = "No Sale items added. Some error might has been occured. a=" + a;
                return View(new SaleInvoice());
            }

        }
        [HttpPost]
        public ActionResult UploadSales(string BillType, string InterState, string UploadType, HttpPostedFileBase FileUpload)
        {
            ExcelUploaders uploader = new ExcelUploaders();
            bool IsVat = false;
            bool IsLocal = true;

            if (BillType == "VAT")
            {
                IsVat = true;
            }


            if (InterState == "InterState")
            {
                IsLocal = false;
            }

            UploadTypes uType = UploadTypes.SaleItemWise;
            if (UploadType =="Register")
            {
                uType = UploadTypes.SaleRegister;
            }
            UploadReturns response = uploader.UploadExcel(uType, FileUpload, Server.MapPath("~/Doc/"), IsVat, IsLocal);

            ViewBag.Status = response.ToString();
            if (response == UploadReturns.Success)
            {
                return RedirectToAction("SaleList");
            }

            return View();

        }
        // GET: SalesUploader/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalesUploader/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesUploader/Create
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

        // GET: SalesUploader/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SalesUploader/Edit/5
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

        // GET: SalesUploader/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SalesUploader/Delete/5
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
