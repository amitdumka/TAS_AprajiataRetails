using AprajitaRetails.Models.Data.Voyagers;
using AprajitaRetails.Ops.TAS;
using AprajitaRetailsOps.TAS;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

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

            if (BillType == "VAT") {
                IsVat = true;
            }
                     

            if (InterState == "WithInState") {
                IsLocal = true;
            }

            UploadReturns response= uploader.UploadExcel(UploadTypes.Purchase, FileUpload, Server.MapPath("~/Doc/") , IsVat, IsLocal);

            ViewBag.Status = response.ToString();
            if (response ==UploadReturns.Success)
            {
                return RedirectToAction("ListUpload");
            }

            return View();

        }
        // GET: PurchaseUploader/Details/5
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
               
               
                    var dm = db.ProductItems.Include(c => c.MainCategory);
                    ViewBag.MessageHead = "No of Product Item added and stock is created are " + a;
                    return View(dm);
               
                
            }
            else
            {
                ViewBag.MessageHead = "No Product items added. Some error might has been occured. a=" + a;
                return View(new ProductItem());
            }
        }

        // GET: PurchaseUploader/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PurchaseUploader/Edit/5
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

        // GET: PurchaseUploader/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PurchaseUploader/Delete/5
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
