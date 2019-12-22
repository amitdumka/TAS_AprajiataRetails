using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAS_AprajiataRetails.Models.Data.Voyagers
{
    public class ImportSaleRegister
    {
        public int ImportSaleRegisterId { get; set; }

        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }
        [Display(Name = "Invoice Type")]
        public string InvoiceType { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public string InvoiceDate { get; set; }
        public double Quantity { get; set; }

        public decimal MRP { get; set; }
        public decimal Discount { get; set; }
        [Display(Name = "Basic Rate")]
        public decimal BasicRate { get; set; }
        public decimal Tax { get; set; }
        public decimal RoundOff { get; set; }
        [Display(Name = "Bill Amount")]
        public decimal BillAmnt { get; set; }

        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ImportTime { get; set; } = DateTime.Now; // Date of Import
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
