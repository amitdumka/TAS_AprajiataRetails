using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAS_AprajiataRetails.Models.Data.Voyagers
{
    //TODO: Move all enums to same place
    //Enums


    //Import tables.
    public class ImportPurchase
    {
        //GRNNo	GRNDate	Invoice No	Invoice Date	Supplier Name	Barcode	Product Name	Style Code	Item Desc	Quantity	MRP	MRP Value	Cost	Cost Value	TaxAmt	ExmillCost	Excise1	Excise2	Excise3

        public int ImportPurchaseId { get; set; }

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

        //[DataType(DataType.Currency), Column(TypeName = "money")]
        //public decimal IGSTAmt { get; set; }
        //[DataType(DataType.Currency), Column(TypeName = "money")]
        //public decimal CSGTAmt { get; set; }
        //[DataType(DataType.Currency), Column(TypeName = "money")]
        //public decimal SGSTAmt { get; set; }


        public bool IsDataConsumed { get; set; }// is data imported to relevent table
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
