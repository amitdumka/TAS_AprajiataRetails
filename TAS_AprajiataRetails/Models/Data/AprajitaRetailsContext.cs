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

        //public DbSet<Expenses> Expenses { get; set; }
        //public DbSet<BankDeposit> BankDeposits { get; set; }
        //public DbSet<Payments> Payments { get; set; }
        //public DbSet<Recipets> Recipets { get; set; }
        //public DbSet<TalioringBooking> TalioringBookings { get; set; }
        //public DbSet<TalioringDelivery> TalioringDeliveries { get; set; }
        //public DbSet<Attendences> Attendences { get; set; }
        //public DbSet<Employee> Employees { get; set; } // Changed from Orignail
        //public DbSet<AdvancePayment> AdvancePayments { get; set; }
        //public DbSet<AdvanceReceipt> AdvanceReceipts { get; set; }
        //public DbSet<SalaryPayment> SalaryPayments { get; set; }
        //public DbSet<HomeExpense> HomeExpenses { get; set; }
        //public DbSet<OtherHomeExpense> OtherHomeExpenses { get; set; }
        //public DbSet<AmitKumarExpense> AmitKumarExpenses { get; set; }
        //public DbSet<CashInward> CashInwards { get; set; }
        //public DbSet<Bank> Banks { get; set; }
        ////public DbSet<PayMode> PayModes { get; set; }// Changed from Orignail

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
    }
}