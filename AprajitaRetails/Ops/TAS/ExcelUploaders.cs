using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

namespace AprajitaRetails.Ops.TAS
{
   
    public class ExcelUploaders
    {
        public UploadReturns UploadExcel( UploadTypes UploadType, HttpPostedFileBase FileUpload, string targetpath, bool IsVat, bool IsLocal)
        {
            using (VoyagerContext db = new VoyagerContext())
            {
                //UploadType = "InWard";
                List<string> data = new List<string>();
                if (FileUpload != null)
                {
                    // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                    if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        string filename = FileUpload.FileName;
                        //TODO: pass from calling function of http post
                       // string targetpath = Server.MapPath("~/Doc/"); 
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
                       
                        if (UploadType == UploadTypes.Purchase)
                        {
                            var currentImports = from a in excelFile.Worksheet<ImportPurchase>(sheetName) select a;
                            foreach (var a in currentImports)
                            {
                                try
                                {
                                    a.ImportTime = DateTime.Now;
                                    //Vat & Local 
                                    a.IsLocal = IsLocal; a.IsVatBill = IsVat;
                                    
                                    db.ImportPurchases.Add(a);
                                    db.SaveChanges();
                                }
                                catch (DbEntityValidationException)
                                {
                                    //TODO: need to handel this
                                    return UploadReturns.Error;
                                }
                            }
                        }
                        else if (UploadType == UploadTypes.SaleItemWise)
                        {
                            var currentImports = from a in excelFile.Worksheet<ImportSaleItemWiseVM>(sheetName) select a;
                            foreach (var a in currentImports)
                            {
                                try
                                {
                                    a.ImportTime = DateTime.Now;
                                    a.IsDataConsumed = false;
                                    db.ImportSaleItemWises.Add(ImportSaleItemWiseVM.ToTable(a));
                                    db.SaveChanges();
                                }
                                catch (DbEntityValidationException)
                                {
                                    //TODO: need to handel this
                                    return UploadReturns.Error;
                                }
                            }
                        }
                        else if (UploadType == UploadTypes.SaleRegister)
                        {
                            var currentImports = from a in excelFile.Worksheet<ImportSaleRegister>(sheetName) select a;
                            foreach (var a in currentImports)
                            {
                                try
                                {
                                    a.ImportTime = DateTime.Now;
                                    a.IsConsumed = false;
                                    db.ImportSaleRegisters.Add(a);
                                    db.SaveChanges();
                                }
                                catch (DbEntityValidationException)
                                {
                                    //TODO: need to handel this
                                    return UploadReturns.Error;
                                }
                            }
                        }
                        else if (UploadType == UploadTypes.InWard)
                        {
                            var currentImports = from a in excelFile.Worksheet<ImportInWard>(sheetName) select a;
                            foreach (var a in currentImports)
                            {
                                try
                                {
                                    // Inward No   Inward Date Invoice No  Invoice Date    Party Name  Total Qty   Total MRP Value Total Cost
                                    ImportInWard inw = new ImportInWard
                                    {
                                        ImportDate = DateTime.Today,
                                        InvoiceDate = a.InvoiceDate,
                                        InvoiceNo = a.InvoiceNo,
                                        InWardDate = a.InWardDate,
                                        InWardNo = a.InWardNo,
                                        PartyName = a.PartyName,
                                        TotalCost = a.TotalCost,
                                        TotalMRPValue = a.TotalMRPValue,
                                        TotalQty = a.TotalQty
                                    };
                                    db.ImportInWards.Add(inw);
                                    db.SaveChanges();


                                }
                                catch (DbEntityValidationException)
                                {
                                    //TODO: need to handel this

                                    return UploadReturns.Error;
                                }
                            }
                        }
                        else
                        {
                            return UploadReturns.ImportNotSupported;
                        }

                        //deleting excel file from folder  


                        if ((System.IO.File.Exists(pathToExcelFile)))
                        {
                            System.IO.File.Delete(pathToExcelFile);
                        }
                        return UploadReturns.Success;



                    }//end of if contexttype
                    else
                    {
                        return UploadReturns.NotExcelType;
                    }

                }//end of if fileupload
                else
                {
                    return UploadReturns.FileNotFound;
                }
            }

        }//end of function
    }
}