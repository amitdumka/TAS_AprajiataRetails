using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAS_AprajiataRetails.Models.Data.Voyagers
{
    public class ImportSaleItemWise
    {
        //Invoice No	Invoice Date	Invoice Type	Brand Name	Product Name	Item Desc	HSN Code	BAR CODE	Style Code	Quantity	MRP	Discount Amt	Basic Amt	Tax Amt	SGST Amt	CGST Amt	Line Total	Round Off	Bill Amt	Payment Mode	SalesMan Name	Coupon %	Coupon Amt	SUB TYPE	Bill Discount	LP Flag	Inst Order CD	TAILORING FLAG


        public int ImportSaleItemWiseId { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string InvoiceDate { get; set; }
        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }
        [Display(Name = "Invoice Type")]
        public string InvoiceType { get; set; }

        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Item Desc")]
        public string ItemDesc { get; set; }
        [Display(Name = "HSN Code")]
        public string HSNCode { get; set; }

        public string Barcode { get; set; }
        [Display(Name = "Style Code")]
        public string StyleCode { get; set; }
        public double Quantity { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MRP { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Discount { get; set; }
        [Display(Name = "Basic Rate")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal BasicRate { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Tax { get; set; } // Can be use for IGST

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal SGST { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CGST { get; set; }
        [Display(Name = "Line Total")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal LineTotal { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal RoundOff { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        [Display(Name = "Bill Amount")]
        public decimal BillAmnt { get; set; }

        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }
        public string Saleman { get; set; }

        public bool IsDataConsumed { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ImportTime { get; set; } = DateTime.Now; // Date of Import
                                                                 // is data imported to relevent table


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
