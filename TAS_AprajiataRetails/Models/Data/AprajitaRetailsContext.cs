using System.Data.Entity;

namespace TAS_AprajiataRetails.Models.Data
{
    // DBContext Sections
    public class AprajitaRetailsContext : DbContext
    {
        public AprajitaRetailsContext() : base("Amitkumar")
        {
            Database.SetInitializer<AprajitaRetailsContext>(new CreateDatabaseIfNotExists<AprajitaRetailsContext>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<AprajitaRetailsContext, Migrations.Configuration>());
        }


        //Version 2
        public DbSet<DailySale> DailySales { get; set; } //OK
        public DbSet<CashInHand> CashInHands { get; set; } //OK
        public DbSet<CashInBank> CashInBanks { get; set; } //OK
        public DbSet<Salesman> Salesmen { get; set; } //OK
        public DbSet<DuesList> DuesLists { get; set; } //OK

        public DbSet<TranscationMode> TranscationModes { get; set; } //OK
        public DbSet<CashPayment> CashPayments { get; set; } //OK
        public DbSet<CashReceipt> CashReceipts { get; set; } //OK

        //Payroll
        public DbSet<Attendance> Attendances { get; set; } //OK
        public DbSet<Employee> Employees { get; set; } //OK
        public DbSet<SalaryPayment> Salaries { get; set; }//SalaryPayments
        public DbSet<StaffAdvancePayment> StaffAdvancePayments { get; set; } //OK
        public DbSet<StaffAdvanceReceipt> StaffAdvanceReceipts { get; set; } //OK

        public DbSet<CurrentSalary> CurrentSalaries { get; set; } //OK
        public DbSet<PaySlip> PaySlips { get; set; } //OK

        //Banking
        public DbSet<Bank> Banks { get; set; } //OK
        public DbSet<AccountNumber> BankAccounts { get; set; } //AccountNumbers
        public DbSet<BankDeposit> BankDeposits { get; set; } //OK
        public DbSet<BankWithdrawal> Withdrawals { get; set; }//BankWdithdrawals

             

        //Expenses
        public DbSet<Expense> Expenses { get; set; } //OK
        public DbSet<PettyCashExpense> CashExpenses { get; set; } //PettyCashExpenses   
        public DbSet<Payment> Payments { get; set; } //OK
        public DbSet<Receipt> Receipts { get; set; } //OK

        //Suspense
        public DbSet<SuspenseAccount> Suspenses { get; set; }// SuspensesAccounts


        //Tailoring

        public DbSet<TailorAttendance> TailorAttendances { get; set; } //OK
        public DbSet<TailoringEmployee> Tailors { get; set; }//TailoringEmployees
        public DbSet<TailoringSalaryPayment> TailoringSalaries { get; set; }//TailoringSalaryPayments
        public DbSet<TailoringStaffAdvancePayment> TailoringStaffAdvancePayments { get; set; } //OK
        public DbSet<TailoringStaffAdvanceReceipt> TailoringStaffAdvanceReceipts { get; set; } //OK

        public DbSet<TalioringBooking> Bookings { get; set; }//TailoringBookings
        public DbSet<TalioringDelivery> Deliveries { get; set; }//TailoringDeliveries


        //End of Day
        public DbSet<EndOfDay>EndOfDays { get; set; }


        //Others

        public DbSet<DueRecoverd>Recoverds { get; set; }//DuesRecovereds
        public DbSet<ChequesLog>Cheques { get; set; }//ChequesLogs

        public DbSet<MonthEnd> MonthEnds { get; set; } //OK
    }
}