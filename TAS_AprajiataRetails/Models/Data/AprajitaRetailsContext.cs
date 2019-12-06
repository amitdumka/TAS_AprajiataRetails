using System.Data.Entity;

namespace TAS_AprajiataRetails.Models.Data
{
    // DBContext Sections
    public class AprajitaRetailsContext : DbContext
    {
        public AprajitaRetailsContext() : base("AprajitaRetails")
        {
            Database.SetInitializer<AprajitaRetailsContext>(new CreateDatabaseIfNotExists<AprajitaRetailsContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AprajitaRetailsContext, Migrations.Configuration>());
        }

       
        //public DbSet<Payments> Payments { get; set; }
        //public DbSet<Recipets> Recipets { get; set; }
        //public DbSet<TalioringBooking> TalioringBookings { get; set; }
        //public DbSet<TalioringDelivery> TalioringDeliveries { get; set; }
       
        ////Version 2
        public DbSet<DailySale> DailySales { get; set; }
        public DbSet<CashInHand> CashInHands { get; set; }
        public DbSet<CashInBank> CashInBanks { get; set; }
        public DbSet<Salesman> Salesmen { get; set; }
        public DbSet<DuesList> DuesLists { get; set; }

        public DbSet<TranscationMode> TranscationModes { get; set; }
        public DbSet<CashPayment> CashPayments { get; set; }
        public DbSet<CashReceipt> CashReceipts { get; set; }

        //Payroll
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<SalaryPayment> Salaries { get; set; }
        public DbSet<StaffAdvancePayment> StaffAdvancePayments { get; set; }
        public DbSet<StaffAdvanceReceipt> StaffAdvanceReceipts { get; set; }

        //Banking
        public DbSet<Bank> Banks { get; set; }
        public DbSet<AccountNumber> BankAccounts { get; set; }
        public DbSet<BankDeposit> BankDeposits { get; set; }
        public DbSet<BankWithdrawal> Withdrawals { get; set; }


        //Others
        public DbSet<EndOfDay> EndOfDays { get; set; }

        //Expenses
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<PettyCashExpense> CashExpenses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        //Suspense
        public DbSet<SuspenseAccount> Suspenses { get; set; }

    }
}