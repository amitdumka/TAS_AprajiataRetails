using System.Data.Entity;

namespace TAS_AprajiataRetails.Models.Data
{
    // DBContext Sections
    public class AprajitaRetailsContext : DbContext
    {
        public AprajitaRetailsContext() : base("AprajitaRetails")
        {
            Database.SetInitializer<AprajitaRetailsContext>(new CreateDatabaseIfNotExists<AprajitaRetailsContext>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<AprajitaRetailsContext, Migrations.Configuration>());
        }


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

        public DbSet<CurrentSalary> CurrentSalaries { get; set; }
        public DbSet<PaySlip> PaySlips { get; set; }

        //Banking
        public DbSet<Bank> Banks { get; set; }
        public DbSet<AccountNumber> BankAccounts { get; set; }
        public DbSet<BankDeposit> BankDeposits { get; set; }
        public DbSet<BankWithdrawal> Withdrawals { get; set; }

             

        //Expenses
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<PettyCashExpense> CashExpenses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        //Suspense
        public DbSet<SuspenseAccount> Suspenses { get; set; }


        //Tailoring

        public DbSet<TailorAttendance> TailorAttendances { get; set; }
        public DbSet<TailoringEmployee> Tailors { get; set; }
        public DbSet<TailoringSalaryPayment> TailoringSalaries { get; set; }
        public DbSet<TailoringStaffAdvancePayment> TailoringStaffAdvancePayments { get; set; }
        public DbSet<TailoringStaffAdvanceReceipt> TailoringStaffAdvanceReceipts { get; set; }

        public DbSet<TalioringBooking> Bookings { get; set; }
        public DbSet<TalioringDelivery> Deliveries { get; set; }


        //End of Day
        public DbSet<EndOfDay>EndOfDays { get; set; }


        //Others

        public DbSet<DueRecoverd>Recoverds { get; set; }
        public DbSet<ChequesLog>Cheques { get; set; }

        public DbSet<MonthEnd> MonthEnds { get; set; }
    }
}