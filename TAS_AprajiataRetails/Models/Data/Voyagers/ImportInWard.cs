using LinqToExcel.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAS_AprajiataRetails.Models.Data.Voyagers
{
    //Inward Imports
    public class ImportInWard
    {
        //Inward No	Inward Date	Invoice No	Invoice Date	Party Name	Total Qty	Total MRP Value	Total Cost

        public int ImportInWardId { get; set; }

        [ExcelColumn("Inward No")]
        public string InWardNo { get; set; }

        // 4/4/2018  5:34:56 PM
       // [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{dd/MM/yyyy HH:mm:ss tt}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [ExcelColumn("Inward Date")]
        [Column(TypeName = "DateTime2")]
        public DateTime InWardDate { get; set; }

        [ExcelColumn("Invoice No")]
        public string InvoiceNo { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [ExcelColumn("Invoice Date")]
        [Column(TypeName = "DateTime2")]
        public DateTime InvoiceDate { get; set; }
        
        [ExcelColumn("Party Name")]
        public string PartyName { get; set; }

        [ExcelColumn("Total Qty")]
        public decimal TotalQty { get; set; }
        
        [ExcelColumn("Total MRP Value")]
        public decimal TotalMRPValue { get; set; }
        
        [ExcelColumn("Total Cost")]
        public decimal TotalCost { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? ImportDate { get; set; } = DateTime.Now;


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
