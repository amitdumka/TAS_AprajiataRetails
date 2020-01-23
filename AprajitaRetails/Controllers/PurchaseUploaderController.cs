using AprajitaRetails.Models.Data.Voyagers;
using AprajitaRetails.Ops.TAS;
using AprajitaRetailsOps.TAS;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Net;

namespace AprajitaRetails.Controllers
{
    public class PurchaseUploaderController : Controller
    {
        private VoyagerContext db = new VoyagerContext();
        // GET: PurchaseUploader
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadPurchase(string BillType, string InterState, HttpPostedFileBase FileUpload)
        {
            ExcelUploaders uploader = new ExcelUploaders();
            bool IsVat = false;
            bool IsLocal = false;

            if (BillType == "VAT")
            {
                IsVat = true;
            }


            if (InterState == "WithInState")
            {
                IsLocal = true;
            }

            UploadReturns response = uploader.UploadExcel(UploadTypes.Purchase, FileUpload, Server.MapPath("~/Doc/"), IsVat, IsLocal);

            ViewBag.Status = response.ToString();
            if (response == UploadReturns.Success)
            {
                return RedirectToAction("ListUpload");
            }

            return View();

        }
        // GET: PurchaseUploader/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            if (id > 0)
            {
                var productPurchases1 = db.ProductPurchases.Include(p => p.Supplier).Include(c => c.PurchaseItems).Where(c => c.ProductPurchaseId == id);
                if (productPurchases1 == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Details = "Invoice No: " + productPurchases1.FirstOrDefault().InvoiceNo;
                return View(productPurchases1.ToList());
            }
            ViewBag.Details = "";
            var productPurchases = db.ProductPurchases.Include(p => p.Supplier).Include(c => c.PurchaseItems);
            if (productPurchases == null)
            {
                return HttpNotFound();
            }
            return View(productPurchases.ToList());
        }
        public ActionResult ListUpload(int? id)
        {

            if (id == 101)
            {
                var md1 = db.ImportPurchases.Where(c => c.IsDataConsumed == true).OrderByDescending(c => c.GRNDate);
                return View(md1);
            }
            else if (id == 100)
            {
                var md1 = db.ImportPurchases.OrderByDescending(c => c.GRNDate).ThenBy(c => c.IsDataConsumed);
                return View(md1);
            }
            var md = db.ImportPurchases.Where(c => c.IsDataConsumed == false).OrderByDescending(c => c.GRNDate);
            return View(md);



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessPurchase(string dDate)
        {
            DateTime ddDate = DateTime.Parse(dDate).Date;

            InventoryManger iManage = new InventoryManger();
            int a = iManage.ProcessPurchaseInward(ddDate, false);
            //TODO: instead of product item . it should list purchase invoice with item

            if (a > 0)
            {


                var dm = db.ProductPurchases.Include(c => c.PurchaseItems).Where(c => DbFunctions.TruncateTime(c.InWardDate) == DbFunctions.TruncateTime(ddDate));
                ViewBag.MessageHead = "Invoices added and No. Of Items Added are " + a;
                return View(dm.ToList());


            }
            else
            {
                //TODO: In view Check for Model is null or not
                ViewBag.MessageHead = "No Product items added. Some error might has been occured. a=" + a;
                return View(new List<ProductPurchase>());
            }
        }

        //// GET: PurchaseUploader/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: PurchaseUploader/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PurchaseUploader/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: PurchaseUploader/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
