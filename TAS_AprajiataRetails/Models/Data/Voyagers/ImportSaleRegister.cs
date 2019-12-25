using LinqToExcel.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAS_AprajiataRetails.Models.Data.Voyagers
{
    public class ImportSaleRegister
    {
        public int ImportSaleRegisterId { get; set; }

        [ExcelColumn("Invoice No")]
        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }

        [ExcelColumn("Invoice Type")]
        [Display(Name = "Invoice Type")]
        public string InvoiceType { get; set; }

        [ExcelColumn("Invoice Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public string InvoiceDate { get; set; }

        [ExcelColumn("Quantity")]
        public double Quantity { get; set; }

        [ExcelColumn("MRP")]
        public decimal MRP { get; set; }

        [ExcelColumn("Discount")]
        public decimal Discount { get; set; }
        [ExcelColumn("Basic Amt")]
        [Display(Name = "Basic Rate")]
        public decimal BasicRate { get; set; }

        [ExcelColumn("Tax Amt")]
        public decimal Tax { get; set; }

        [ExcelColumn("Round Off")]
        public decimal RoundOff { get; set; }

        [ExcelColumn("Bill Amt")]
        [Display(Name = "Bill Amount")]
        public decimal BillAmnt { get; set; }

        [ExcelColumn("Payment Mode")]
        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        public bool IsConsumed { get; set; } = false;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? ImportTime { get; set; } = DateTime.Now; // Date of Import
    }

}
