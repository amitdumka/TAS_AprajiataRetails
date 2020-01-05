using LinqToExcel.Attributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace AprajitaRetails.Models.Data.Voyagers
{

    //TODO: Need to Create View So String Date error problem will be solved
    public class ImportSaleItemWise
    {
        //Invoice No	Invoice Date	Invoice Type	Brand Name	Product Name	Item Desc	HSN Code	BAR CODE	Style Code	Quantity	MRP	Discount Amt	Basic Amt	Tax Amt	SGST Amt	CGST Amt	Line Total	Round Off	Bill Amt	Payment Mode	SalesMan Name	Coupon %	Coupon Amt	SUB TYPE	Bill Discount	LP Flag	Inst Order CD	TAILORING FLAG


        public int ImportSaleItemWiseId { get; set; }

        [ExcelColumn("Invoice Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        [ExcelColumn("Invoice No")]
        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }

        [ExcelColumn("Invoice Type")]
        [Display(Name = "Invoice Type")]
        public string InvoiceType { get; set; }

        [ExcelColumn("Brand Name")]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        [ExcelColumn("Product Name")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [ExcelColumn("Item Desc")]
        [Display(Name = "Item Desc")]
        public string ItemDesc { get; set; }

        [ExcelColumn("HSN Code")]
        [Display(Name = "HSN Code")]
        public string HSNCode { get; set; }

        [ExcelColumn("BAR CODE")]
        public string Barcode { get; set; }

        [ExcelColumn("Style Code")]
        [Display(Name = "Style Code")]
        public string StyleCode { get; set; }

        [ExcelColumn("Quantity")]
        public double Quantity { get; set; }

        [ExcelColumn("MRP")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MRP { get; set; }

        [ExcelColumn("Discount Amt")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Discount { get; set; }

        [ExcelColumn("Basic Amt")]
        [Display(Name = "Basic Rate")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal BasicRate { get; set; }

        [ExcelColumn("Tax Amt")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Tax { get; set; } // Can be use for IGST

        [ExcelColumn("SGST Amt")]

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal SGST { get; set; }

        [ExcelColumn("CGST Amt")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CGST { get; set; }

        [ExcelColumn("Line Total")]
        [Display(Name = "Line Total")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal LineTotal { get; set; }

        [ExcelColumn("Round Off")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal RoundOff { get; set; }

        [ExcelColumn("Bill Amt")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        [Display(Name = "Bill Amount")]
        public decimal BillAmnt { get; set; }

        [ExcelColumn("Payment Mode")]

        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        [ExcelColumn("SalesMan Name")]
        public string Saleman { get; set; }

        [DefaultValue(false)]
        public bool IsDataConsumed { get; set; } = false;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? ImportTime { get; set; } = DateTime.Now; // Date of Import
                                                                  // is data imported to relevent table


    }

    public class ImportSaleItemWiseVM
    {
        //Invoice No	Invoice Date	Invoice Type	Brand Name	Product Name	Item Desc	HSN Code	BAR CODE	Style Code	Quantity	MRP	Discount Amt	Basic Amt	Tax Amt	SGST Amt	CGST Amt	Line Total	Round Off	Bill Amt	Payment Mode	SalesMan Name	Coupon %	Coupon Amt	SUB TYPE	Bill Discount	LP Flag	Inst Order CD	TAILORING FLAG
        public static ImportSaleItemWise ToTable(ImportSaleItemWiseVM data)
        {
           

            ImportSaleItemWise item = new ImportSaleItemWise
            {
                Barcode = data.Barcode,
                BasicRate = data.BasicRate,
                BillAmnt = data.BillAmnt,
                BrandName = data.BrandName,
                CGST = data.CGST,
                Discount = data.Discount,
                HSNCode = data.HSNCode,
                InvoiceDate = DateTime.ParseExact(data.InvoiceDate.Trim(),"dd-MM-yyyy", CultureInfo.InvariantCulture),
                InvoiceNo = data.InvoiceNo,
                InvoiceType = data.InvoiceType,
                IsDataConsumed = data.IsDataConsumed,
                ImportSaleItemWiseId = data.ImportSaleItemWiseId,
                Quantity = data.Quantity,
                ItemDesc = data.ItemDesc,
                LineTotal = data.LineTotal,
                MRP = data.MRP,
                PaymentType = data.PaymentType,
                ImportTime = data.ImportTime,
                ProductName = data.ProductName,
                RoundOff = data.RoundOff,
                Saleman = data.Saleman,
                SGST = data.SGST,
                StyleCode = data.StyleCode,
                Tax = data.Tax
            };
            return item;
        }

        public int ImportSaleItemWiseId { get; set; }

        [ExcelColumn("Invoice Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string InvoiceDate { get; set; }

        [ExcelColumn("Invoice No")]
        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }

        [ExcelColumn("Invoice Type")]
        [Display(Name = "Invoice Type")]
        public string InvoiceType { get; set; }

        [ExcelColumn("Brand Name")]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        [ExcelColumn("Product Name")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [ExcelColumn("Item Desc")]
        [Display(Name = "Item Desc")]
        public string ItemDesc { get; set; }

        [ExcelColumn("HSN Code")]
        [Display(Name = "HSN Code")]
        public string HSNCode { get; set; }

        [ExcelColumn("BAR CODE")]
        public string Barcode { get; set; }

        [ExcelColumn("Style Code")]
        [Display(Name = "Style Code")]
        public string StyleCode { get; set; }

        [ExcelColumn("Quantity")]
        public double Quantity { get; set; }

        [ExcelColumn("MRP")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MRP { get; set; }

        [ExcelColumn("Discount Amt")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Discount { get; set; }

        [ExcelColumn("Basic Amt")]
        [Display(Name = "Basic Rate")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal BasicRate { get; set; }

        [ExcelColumn("Tax Amt")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Tax { get; set; } // Can be use for IGST

        [ExcelColumn("SGST Amt")]

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal SGST { get; set; }

        [ExcelColumn("CGST Amt")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CGST { get; set; }

        [ExcelColumn("Line Total")]
        [Display(Name = "Line Total")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal LineTotal { get; set; }

        [ExcelColumn("Round Off")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal RoundOff { get; set; }

        [ExcelColumn("Bill Amt")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        [Display(Name = "Bill Amount")]
        public decimal BillAmnt { get; set; }

        [ExcelColumn("Payment Mode")]

        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        [ExcelColumn("SalesMan Name")]
        public string Saleman { get; set; }

        [DefaultValue(false)]
        public bool IsDataConsumed { get; set; } = false;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? ImportTime { get; set; } = DateTime.Now; // Date of Import
                                                                  // is data imported to relevent table


    }
}

