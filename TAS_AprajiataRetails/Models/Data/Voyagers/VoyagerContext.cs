using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TAS_AprajiataRetails.Models.Data.Voyagers
{
    public class VoyagerContext : DbContext
    {
        public VoyagerContext() : base("VoyagerDB")
        {
            Database.SetInitializer<VoyagerContext>(new CreateDatabaseIfNotExists<VoyagerContext>());
           // Database.SetInitializer(new MigrateDatabaseToLatestVersion<VoyagerContext, Migrations.Configuration>());
        }
    }

    public class Customer
    {
        public int ID { set; get; }
        public int Age { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string City { set; get; }
        public string MobileNo { set; get; }
        public int Gender { set; get; }
        public int NoOfBills { set; get; }
        public double TotalAmount { set; get; }
    }

    public class Purchase
    {
        public string GRNNo { get; set; }
        public DateTime GRNDate { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string SupplierName { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public string StyleCode { get; set; }
        public string ItemDesc { get; set; }
        public double Quantity { get; set; }
        public double MRP { get; set; }
        public double MRPValue { get; set; }
        public double Cost { get; set; }
        public double CostValue { get; set; }
        public double TaxAmt { get; set; }
        public double IGSTAmt { get; set; }
        public double CSGTAmt { get; set; }
        public double SGSTAmt { get; set; }
        public DateTime ImportTime { get; set; } // Date of Import
        public string IsDataConsumed { get; set; }// is data imported to relevent table

        public Purchase()
        {
            IsDataConsumed = "NO";
            ImportTime = DateTime.Now;
        }
    }

    public class SaleItemWise
    {
        public string Barcode { get; set; }
        public double BasicRate { get; set; }
        public double BillAmnt { get; set; }
        public string BrandName { get; set; }
        public double CGST { get; set; }
        public double Discount { get; set; }
        public string HSNCode { get; set; }
        public int ID { get; set; }
        public string InvDate { get; set; }
        public string InvoiceNo { get; set; }
        public string InvType { get; set; }
        public string ItemDesc { get; set; }
        public double LineTotal { get; set; }
        public string LP { get; set; }
        public double MRP { get; set; }
        public string PaymentType { get; set; }
        public string ProductName { get; set; }
        public int QTY { get; set; }
        public double RoundOff { get; set; }
        public string Saleman { get; set; }
        public double SGST { get; set; }
        public string StyleCode { get; set; }
        public double Tax { get; set; } // Can be use for IGST
        public DateTime ImportTime { get; set; } // Date of Import
        public string IsDataConsumed { get; set; }// is data imported to relevent table

        public SaleItemWise()
        {
            HSNCode = "";
            CGST = SGST = Tax = 0.00;
            LineTotal = 0.00;
            PaymentType = "";
            IsDataConsumed = "NO";
            ImportTime = DateTime.Now;
        }
    }

    public class SaleRegister
    {
        public int ID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvType { get; set; }
        public string InvDate { get; set; }
        public int QTY { get; set; }
        public double MRP { get; set; }
        public double Discount { get; set; }
        public double BasicRate { get; set; }
        public double Tax { get; set; }
        public double RoundOff { get; set; }
        public double BillAmnt { get; set; }
        public string paymentType { get; set; }
    }
    internal class Stores
    {
        public int _ID { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string PhoneNo { get; set; }
        public string StoreManagerName { get; set; }
        public string StoreManagerPhoneNo { get; set; }
        public string GSTNO { get; set; }
        public int NoOfEmployees { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public string Status { get; set; }
    }
}