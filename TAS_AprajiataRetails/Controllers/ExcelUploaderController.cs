using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAS_AprajiataRetails.Models.Data.Voyagers;

namespace TAS_AprajiataRetails.Controllers
{
    public class ExcelUploaderController : Controller
    {
        private VoyagerContext db = new VoyagerContext();

        // GET: ExcelUploader
        public ActionResult Index()
        {
            var vm = db.TestInwards.OrderByDescending(c => c.ImportInWardVMId);
            return View(vm);
        }

        [HttpPost]
        public JsonResult UploadExcel(ImportInWard inWard, HttpPostedFileBase FileUpload)
        {
            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Doc/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");

                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var currentImports = from a in excelFile.Worksheet<ImportInWardVM>(sheetName) select a;
                    foreach (var a in currentImports)
                    {
                        try
                        {
                            // Inward No   Inward Date Invoice No  Invoice Date    Party Name  Total Qty   Total MRP Value Total Cost
                            ImportInWardVM inw = new ImportInWardVM { 
                                 InvoiceDate=a.InvoiceDate, InvoiceNo=a.InvoiceNo,InWardDate=a.InWardDate, InWardNo=a.InWardNo,
                                 PartyName=a.PartyName, TotalCost=a.TotalCost, TotalMRPValue=a.TotalMRPValue, TotalQty=a.TotalQty
                            };
                            db.TestInwards.Add(inw);
                            db.SaveChanges();


                        }
                        catch (DbEntityValidationException ex)
                        {

                            throw;
                        }
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return Json("success", JsonRequestBehavior.AllowGet);



                }//end of if contexttype
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li>Only Excel file format is allowed</li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

            }//end of if fileupload
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }//end of function
    }//end of class
}//end of namespace