using System;
using System.Data.Entity;

namespace TAS_AprajiataRetails.Models.Data.Voyagers
{
    public class VoyagerContext : DbContext
    {
        public VoyagerContext() : base("Voyager")
        {
            Database.SetInitializer<VoyagerContext>(new CreateDatabaseIfNotExists<VoyagerContext>());
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<VoyagerContext, Migrations.Configuration>());
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Customer>()
        //        .Property(b => b.CreatedDate)
        //        .HasDefaultValueSql("getdate()");

        //    modelBuilder.Entity<ImportInWard>()
        //        .Property(b => b.ImportDate)
        //        .HasDefaultValueSql("getdate()");

        //    modelBuilder.Entity<ImportPurchase>()
        //        .Property(b => b.InvoiceDate)
        //        .HasDefaultValueSql("getdate()");

        //    modelBuilder.Entity<ImportSaleRegister>()
        //        .Property(b => b.ImportTime)
        //        .HasDefaultValueSql("getdate()");

        //    modelBuilder.Entity<ImportSaleItemWise>()
        //        .Property(b => b.ImportTime)
        //        .HasDefaultValueSql("getdate()");
        //}

        public DbSet<Store> Stores { get; set; }
        public DbSet<Customer> Customers { get; set; }

        //Import Table
        public DbSet<ImportInWard> ImportInWards { get; set; }
        public DbSet<ImportPurchase> ImportPurchases { get; set; }
        public DbSet<ImportSaleItemWise> ImportSaleItemWises { get; set; }
        public DbSet<ImportSaleRegister> ImportSaleRegisters { get; set; }

        public DbSet<ImportInWardVM> TestInwards { get; set; }

    }




    //Processed Tables

    public class Item
    {
        public int ItemId { set; get; }
        public string Barcode { get; set; }

        public int BrandId { get; set; }
        public virtual Brand BrandName { get; set; }

        public string StyleCode { get; set; }
        public string ProductName { get; set; }
        public string ItemDesc { get; set; }

        public string SupplierId { get; set; }

        public ProductCategorys Categorys { get; set; }


        public decimal MRP { get; set; }
        public decimal Tax { get; set; }    // TODO:Need to Review in final Edition
        public decimal Cost { get; set; }

        public Sizes Size { get; set; }
        public double Qty { get; set; } //TODO: Check for use

        //GST Implementation    Version 1.0
        //TODO: GST implementation should use Taxes Class
        //public double HSNCode { get; set; }
        //public int PreGST { get; set; }
        //public double SGST { get; set; }
        //public double CGST { get; set; }
        //public double IGST { get; set; }
    }
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ProductCategorys PrimaryCategorys { get; set; }
    }
    public class Stock
    {
        public int StockID { set; get; }
        public int ItemId { set; get; }
        public double Quantity { set; get; }
    }
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
    class Salesman
    {
        public int SalesmanId { set; get; }
        public string SMCode { get; set; }
        public string SalesmanName { get; set; }
    }

    internal class Supplier
    {
        public int SupplierID { get; set; }
        public string SuppilerName { get; set; }
        public string Warehouse { get; set; }
    }

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

    internal class SaleInvoice
    {
        public int SaleInvoiceID { get; set; }
        public int CustomerId { get; set; }
        public DateTime OnDate { get; set; }
        public string InvoiceNo { get; set; }
        public int TotalItems { get; set; }
        public double TotalQty { get; set; }
        public double TotalBillAmount { get; set; }
        public double TotalDiscountAmount { get; set; }
        public double RoundOffAmount { get; set; }
        public double TotalTaxAmount { get; set; }
    }

    internal class PaymentDetails
    {
        public int PaymentDetailsID { get; set; }
        public string InvoiceNo { get; set; }
        public int PayMode { get; set; }
        public double CashAmount { get; set; }
        public double CardAmount { get; set; }
        public int CardDetailsID { get; set; }
    }

    internal class SalePaymentDetails
    {
        public int SalePaymentDetailsID { get; set; }
        public string InvoiceNo { get; set; }
        public int PayMode { get; set; }
        public double CashAmount { get; set; }
        public double CardAmount { get; set; }
        public CardPaymentDetail CardDetails { get; set; }
    }



    internal class CardPaymentDetail
    {
        public int CardPaymentDetailID { get; set; }
        public string InvoiceNo { get; set; }
        public int CardType { get; set; }
        public double Amount { get; set; }
        public int AuthCode { get; set; }
        public int LastDigit { get; set; }
    }

    public class SaleItem
    {
        public int SaleItemID { get; set; }
        public string InvoiceNo { get; set; }
        public string BarCode { get; set; }
        public double Qty { get; set; }
        public double MRP { get; set; }
        public double BasicAmount { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
        public double BillAmount { get; set; }
        public int SalesmanID { get; set; }
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
