using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAS_AprajiataRetails.Models.Data.Voyagers
{
    public class Sales
    {
    }

    public class SaleInvoice
    {
        public int SaleInvoiceID { get; set; }
        
        public int CustomerId { get; set; }
        
        public DateTime OnDate { get; set; }
        
        public string InvoiceNo { get; set; }
        
        public int TotalItems { get; set; }
        
        public double TotalQty { get; set; }
        
        public decimal TotalBillAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal RoundOffAmount { get; set; }
        public decimal TotalTaxAmount { get; set; }

        public virtual ICollection<SaleItem> SaleItems { get; set; }   
    }

    public class Salesman
    {
        public int SalesmanId { set; get; }
        public string SMCode { get; set; }
        public string SalesmanName { get; set; }
        public virtual ICollection<SaleItem> SaleItems { get; set; }
    }


    public class SaleItem
    {
        public int SaleItemID { get; set; }

        public int SaleInvoiceID { get; set; }

        public string BarCode { get; set; }
        public double Qty { get; set; }
        public Units Units { get; set; }

        public double MRP { get; set; }
        public double BasicAmount { get; set; }
        public double Discount { get; set; }
        public double TaxAmount { get; set; }
        public double BillAmount { get; set; }
        // SaleTax options and it will Done
       
        public int SalesmanID { get; set; }

        public virtual SaleInvoice SaleInvoice { get; set; }
        public virtual Salesman Salesman { get; set; }
    }
}