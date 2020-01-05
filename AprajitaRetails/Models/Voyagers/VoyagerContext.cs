using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace AprajitaRetails.Models.Data.Voyagers
{
    public class VoyagerContext : DbContext
    {
        public VoyagerContext() : base("Voyager")
        {
            Database.SetInitializer<VoyagerContext>(new CreateDatabaseIfNotExists<VoyagerContext>());
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<VoyagerContext, Migrations.Configuration>());
        }


        public DbSet<Store> Stores { get; set; }
        public DbSet<Customer> Customers { get; set; }

        //Import Table
        public DbSet<ImportInWard> ImportInWards { get; set; }
        public DbSet<ImportPurchase> ImportPurchases { get; set; }
        public DbSet<ImportSaleItemWise> ImportSaleItemWises { get; set; }
        public DbSet<ImportSaleRegister> ImportSaleRegisters { get; set; }



        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<Brand> Brands { get; set; }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Stock> Stocks { get; set; }


        //Purchase Entry System
        public DbSet<ProductPurchase> ProductPurchases { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<PurchaseTaxType> PurchaseTaxTypes { get; set; }


        // Sale Entry System

        public DbSet<Salesman> Salesmen { get; set; }
        public DbSet<SaleInvoice> SaleInvoices { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<SaleTaxType> SaleTaxTypes { get; set; }


        public DbSet<SalePaymentDetail> SalePaymentDetails { get; set; }
        public DbSet<CardPaymentDetail> CardPaymentDetails { get; set; }
    }




    //Processed Tables
    

    internal class SaleReturnInvoice
    {
        public int SaleReturnInvoiceID { get; set; }
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

    

    internal class PaymentDetails
    {
        public int PaymentDetailsID { get; set; }
        public string InvoiceNo { get; set; }
        public SalePayMode PayMode { get; set; }
        public decimal CashAmount { get; set; }
        public decimal CardAmount { get; set; }
        public int CardDetailsID { get; set; }
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
