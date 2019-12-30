using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Models.Data.Voyagers
{
    public class Customer
    {
        public int CustomerID { set; get; }

        [Display(Name = "First Name")]
        public string FirstName { set; get; }
        [Display(Name = " Last Name")]
        public string LastName { set; get; }
        public int Age { set; get; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
        public string City { set; get; }
        [Display(Name = "Contact No")]
        public string MobileNo { set; get; }
        public Genders Gender { set; get; }
        [Display(Name = "Bill Count")]
        public int NoOfBills { set; get; }
        [Display(Name = "Purchase Amount")]
        public decimal TotalAmount { set; get; }

        [ DatabaseGenerated(DatabaseGeneratedOption.Computed)]
         public DateTime? CreatedDate { get; set; }
        [Display(Name = "Full Name")]
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
