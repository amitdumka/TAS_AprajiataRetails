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

    public class Item { }
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? SubCategoryId { get; set; }
    }
    public class Stock { }
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
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