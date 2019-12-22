using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

    //TODO: Move all enums to same place
    //Enums
    public enum Genders { Male, Female, TransGender }
    public enum Units { Meters, Nos, Pcs, Packets }
    public enum TaxType { Vat, GST, SGST, CGST, IGST }
    public enum SalePayMode { Cash, Card, Mix }
    internal class ProductCategory
    {//TODO: Make is enum
        //TODO: Add option of trims, promo items, coupons, others
        public static readonly int Fabric = 1;
        public static readonly int RMZ = 2;
        public static readonly int Accessiories = 3;
        public static readonly int Tailoring = 4;
    }

    //Import tables.
    public class Purchase
    {
        //GRNNo	GRNDate	Invoice No	Invoice Date	Supplier Name	Barcode	Product Name	Style Code	Item Desc	Quantity	MRP	MRP Value	Cost	Cost Value	TaxAmt	ExmillCost	Excise1	Excise2	Excise3

        public int PurchaseId { get; set; }

        public string GRNNo { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime GRNDate { get; set; }
        public string InvoiceNo { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        public string SupplierName { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public string StyleCode { get; set; }
        public string ItemDesc { get; set; }
        public double Quantity { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MRP { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MRPValue { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Cost { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CostValue { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TaxAmt { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal IGSTAmt { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CSGTAmt { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal SGSTAmt { get; set; }


        public bool IsDataConsumed { get; set; }// is data imported to relevent table
        [Timestamp]
        public byte[] ImportTime { get; set; } // Date of Import



    }
    public class SaleItemWise
    {
        //Invoice No	Invoice Date	Invoice Type	Brand Name	Product Name	Item Desc	HSN Code	BAR CODE	Style Code	Quantity	MRP	Discount Amt	Basic Amt	Tax Amt	SGST Amt	CGST Amt	Line Total	Round Off	Bill Amt	Payment Mode	SalesMan Name	Coupon %	Coupon Amt	SUB TYPE	Bill Discount	LP Flag	Inst Order CD	TAILORING FLAG


        public int ID { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceType { get; set; }

        public string BrandName { get; set; }
        public string ProductName { get; set; }
        public string ItemDesc { get; set; }
        public string HSNCode { get; set; }

        public string Barcode { get; set; }
        public string StyleCode { get; set; }
        public double Quantity { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MRP { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Discount { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal BasicRate { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Tax { get; set; } // Can be use for IGST

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal SGST { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CGST { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal LineTotal { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal RoundOff { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal BillAmnt { get; set; }

        public string PaymentType { get; set; }
        public string Saleman { get; set; }

        public bool IsDataConsumed { get; set; }

        [Timestamp]
        public byte[] ImportTime { get; set; } // Date of Import
                                               // is data imported to relevent table


    }

    public class SaleRegister
    {
        public int SaleRegisterId { get; set; }

        public string InvoiceNo { get; set; }
        public string InvoiceType { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string InvoiceDate { get; set; }
        public double Quantity { get; set; }

        public decimal MRP { get; set; }
        public decimal Discount { get; set; }
        public decimal BasicRate { get; set; }
        public decimal Tax { get; set; }
        public decimal RoundOff { get; set; }
        public decimal BillAmnt { get; set; }

        public string PaymentType { get; set; }
        [Timestamp]
        public byte[] ImportTime { get; set; } // Date of Import
    }


    //Inward Imports

    public class InWard
    {
        //Inward No	Inward Date	Invoice No	Invoice Date	Party Name	Total Qty	Total MRP Value	Total Cost

        public int InWardId { get; set; }

        public string InWardNo { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InWardDate { get; set; }

        public string InvoiceNo { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        public string PartyName { get; set; }

        public double TotalQty { get; set; }

        public decimal TotalMRPValue { get; set; }
        public decimal TotalCost { get; set; }
        [Timestamp]
        public byte[] ImportDate { get; set; }


    }




    //Processed Tables

    public class Item {
        public int ID { set; get; }
        public string StyleCode { get; set; }
        public string Barcode { get; set; }

        public string SupplierId { get; set; }

        public string BrandName { get; set; }
        public string ProductName { get; set; }
        public string ItemDesc { get; set; }

        public double MRP { get; set; }
        public double Tax { get; set; }    // TODO:Need to Review in final Edition
        public double Cost { get; set; }

        public string Size { get; set; }
        public double Qty { get; set; }

        //GST Implementation    Version 1.0
        //TODO: GST implementation should use Taxes Class
        //public double HSNCode { get; set; }
        //public int PreGST { get; set; }
        //public double SGST { get; set; }
        //public double CGST { get; set; }
        //public double IGST { get; set; }
    }
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? SubCategoryId { get; set; }
    }
    public class Stock
    {
        public int StockID { set; get; }
        public int ProductId { set; get; }
        public double QTY { set; get; }
    }
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
    class Salesman
    {
        public int ID
        {
            set; get;
        }

        public string SMCode { get; set; }
        public string SalesmanName { get; set; }
    }

    internal class Supplier
    {
        public int ID { get; set; }
        public string SuppilerName { get; set; }
        public string Warehouse { get; set; }
    }

    internal class SaleReturnInvoice
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string SaleInvoiceNo { get; set; }
        public string ReturnInvoiceNo { get; set; }
        public double TotalQty { get; set; }
        public int TotalReturnItem { get; set; }
        public double Amount { get; set; }
        public double TaxAmount { get; set; }
        public double DiscountAmount { get; set; }
        public DateTime OnDate { get; set; }
        public string NewSaleInvoiceNo { get; set; }
        public string Debit_CreditNotesNo { get; set; }
        public int SalesmanId { get; set; }
    }

    internal class SaleInvoice
    {
        public int ID { get; set; }
        public int CustomerId { get; set; }
        public DateTime OnDate { get; set; }
        public string InvoiceNo { get; set; }
        public int TotalItems { get; set; }
        public double TotalQty { get; set; }
        public double TotalBillAmount { get; set; }
        public double TotalDiscountAmount { get; set; }
        public double RoundOffAmount { get; set; }
        public double TotalTaxAmount { get; set; }
    }

    internal class PaymentDetails
    {
        public int ID { get; set; }
        public string InvoiceNo { get; set; }
        public int PayMode { get; set; }
        public double CashAmount { get; set; }
        public double CardAmount { get; set; }
        public int CardDetailsID { get; set; }
    }

    internal class SalePaymentDetails
    {
        public int ID { get; set; }
        public string InvoiceNo { get; set; }
        public int PayMode { get; set; }
        public double CashAmount { get; set; }
        public double CardAmount { get; set; }
        public CardPaymentDetails CardDetails { get; set; }
    }

    internal class CardMode
    {
        public static readonly int DebitCard = 1;
        public static readonly int CreditCard = 2;
        public static readonly int AmexCard = 3;
    }

    internal class CardType
    {
        public static readonly int Visa = 1;
        public static readonly int MasterCard = 2;
        public static readonly int Mastro = 3;
        public static readonly int Amex = 4;
        public static readonly int Dinners = 5;
        public static readonly int Rupay = 6;
    }

    internal class CardPaymentDetails
    {
        public int ID { get; set; }
        public string InvoiceNo { get; set; }
        public int CardType { get; set; }
        public double Amount { get; set; }
        public int AuthCode { get; set; }
        public int LastDigit { get; set; }
    }

    internal class SaleItem
    {
        public int ID { get; set; }
        public string InvoiceNo { get; set; }
        public string BarCode { get; set; }
        public double Qty { get; set; }
        public double MRP { get; set; }
        public double BasicAmount { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
        public double BillAmount { get; set; }
        public int SalesmanID { get; set; }
    }


    //Others Tables

    public class Store
    {
        public int StoreID { get; set; }
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string PhoneNo { get; set; }
        public string StoreManagerName { get; set; }
        public string StoreManagerPhoneNo { get; set; }
        public string PanNo { get; set; }
        public string GSTNO { get; set; }
        public int NoOfEmployees { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Opening Date")]
        public DateTime OpeningDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Closing Date")]
        public DateTime ClosingDate { get; set; }
        [Display(Name = "Operative")]
        public bool Status { get; set; }
    }

    public class Customer
    {
        public int CustomerID { set; get; }

        public string FirstName { set; get; }
        public string LastName { set; get; }
        public int Age { set; get; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
        public string City { set; get; }
        public string MobileNo { set; get; }
        public Genders Gender { set; get; }
        public int NoOfBills { set; get; }
        public decimal TotalAmount { set; get; }
        [Timestamp]
        public byte[] CreatedDate { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
    }

}

//future code for reference 
/*public InvoiceNo GenerateInvoiceNo()
{
    InvoiceNo inv = new InvoiceNo(ManualSeries);
    //TODO: series Start should be changed every Finnical Year;
    //TODO: Should have option to check data and based on that generate it
    string iNo = sDB.GetLastInvoiceNo();
    if (iNo.Length > 0)
    {
        if (iNo != "0")
        {
            Logs.LogMe("Inv=" + iNo);
            iNo = iNo.Substring(5);
            Logs.LogMe("Inv=" + iNo);
            long i = long.Parse(iNo);
            Logs.LogMe("Inv=" + i);
            inv.TP = i + 1;
        }
        else
        { inv.TP = SeriesStart + 1; }
    }
    else
    {
        //TODO: check future what happens and what condtion  come here
        inv.TP = SeriesStart + 1;
        Logs.LogMe("Future check :Inv=" + inv.TP);
    }
    return inv;
}*/