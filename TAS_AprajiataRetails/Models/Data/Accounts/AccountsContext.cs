using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TAS_AprajiataRetails.Models.Data.Accounts
{
    public class AccountsContext : DbContext
    {
        public AccountsContext() : base("ARAccounts")
        {
            Database.SetInitializer<AccountsContext>(new CreateDatabaseIfNotExists<AccountsContext>());
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<AccountsContext, Migrations.Configuration>());
        }
        public DbSet<LedgerMaster> Masters { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<LedgerEntry> LedgerEntries { get; set; }
    }


    //Tables 
    public enum LedgerType { Credit, Debit, Income, Expenses, Assests, Bank, Loan, Purchase, Sale, Vendor, Customer }
    public class LedgerMaster
    {
        public int LedgerMasterId { get; set; }
        //[ForeignKey("Party")]
        public int PartyId { get; set; }
        public virtual Party Party { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime CreatingDate { get; set; }
        [Display(Name = "Ledger Type")]
        public LedgerType LedgerType { get; set; }


    }
    public class Party
    {

        public int PartyId { get; set; }
        public string PartyName { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "On Date")]
        public DateTime OpenningDate { get; set; }
        [Display(Name = "Openning Balance")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal OpenningBalance { get; set; }
        public string Address { get; set; }
        public string PANNo { get; set; }
        public string GSTNo { get; set; }
        [Display(Name = "Ledger Type")]
        public LedgerType LedgerType { get; set; }
        public virtual ICollection<LedgerEntry> Entries { get; set; }

    }
    public class LedgerEntry
    {
        public int LedgerEntryId { get; set; }
        public int PartyId { get; set; }
        public virtual Party PartyName { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime EntryDate { get; set; }
        public string Particulars { get; set; }
        [Display(Name = "Amount In")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal AmountIn { get; set; }
        [Display(Name = "Amount Out")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal AmountOut { get; set; }
        [Display(Name = "Balance")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Balance { get; set; }
    }

    public class DebitNote
    {
        public int DebitNoteId { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime EntryDate { get; set; }
        public int PartyId { get; set; }
        public Party PartyName { get; set; }
        public string Particulars { get; set; }
        [Display(Name = "Debit Amount")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
    }
    public class CreditNote {
        public int CreditNoteId { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime EntryDate { get; set; }
        public int PartyId { get; set; }
        public Party PartyName { get; set; }
        public string Particulars { get; set; }
        [Display(Name = "Credit Amount")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
    }

    public class Purchase
    {
        public int PurchaseId { get; set; }
        public DateTime EntryDate { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Particulars { get; set; }
        public decimal BasicAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class PurchaseInWard
    {
        public int PurchaseInWardId { get; set; }

        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
        public DateTime InWardDate { get; set; }
        public string InWardRefernce { get; set; }
        public string Remarks { get; set; }
    }

    public class GoodsReturn { 
        public DateTime EntryDate { get; set; }
        public string Particulars { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Remarks { get; set; }
    }
    public class PurchaseReturn: GoodsReturn
    {
        public int PurchaseReturnId { get; set; }
    }
    public class DefectiveGoodsReturn : GoodsReturn
    {
        public int DefectiveGoodsReturnId { get; set; }

    }

}