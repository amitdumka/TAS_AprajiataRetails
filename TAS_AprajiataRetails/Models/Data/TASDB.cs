using System;
using System.Linq;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAS_AprajiataRetails.Models.Data
{
    //Enum Section

    public enum PayModes { Cash, Card, RTGS, NEFT, IMPS, Wallets, Cheques, DemandDraft, Points, Others, Coupons };

    // Utils Section 
    public class Utils
    {
       

        public static void ProcessOpenningClosingBalance(AprajitaRetailsContext db, DateTime date, bool isclosing = false, bool saveit = false)
        {
            if (!isclosing)
            {
                CashInHand today;
                today = db.CashInHands.Where(c => c.CIHDate == date).FirstOrDefault();
                //One Day Back
                DateTime yDate = date.AddDays(-1);
                CashInHand yesterday = db.CashInHands.Where(c => c.CIHDate == yDate).FirstOrDefault();

                if (today == null)
                {
                    today = new CashInHand() { CashIn = 0, CashOut = 0, CIHDate = date, ClosingBalance = 0, OpenningBalance = 0 };

                    if (yesterday != null)
                    {
                        yesterday.ClosingBalance = yesterday.OpenningBalance + yesterday.CashIn - yesterday.CashOut;
                        today.ClosingBalance = today.OpenningBalance = yesterday.ClosingBalance;
                        db.CashInHands.Add(today);
                        if (saveit) db.SaveChanges();
                    }
                    else
                    {
                        if (db.CashInHands.Count() > 0)
                            throw new Exception();
                        //TODO: if yesterday one or day back data not present handel this
                        else
                        {
                            today.ClosingBalance = today.OpenningBalance = 0;
                            db.CashInHands.Add(today);
                            if (saveit) db.SaveChanges();
                        }
                    }

                }
                else
                {
                    if (yesterday != null)
                    {
                        today.ClosingBalance = today.OpenningBalance = yesterday.ClosingBalance;
                        db.Entry(today).State = EntityState.Modified;
                        if (saveit) db.SaveChanges();
                    }
                    else
                    {
                        if (db.CashInHands.Count() > 1)
                            throw new Exception();
                        //TODO: if yesterday one or day back data not present handel this
                        else
                        {
                            today.ClosingBalance = today.OpenningBalance = 0;
                            db.CashInHands.Add(today);
                            if (saveit) db.SaveChanges();
                        }
                    }
                }


            }
            else
            {
                //ClosingBalance;
                CashInHand today;
                today = db.CashInHands.Where(c => c.CIHDate == date).FirstOrDefault();
                if (today != null)
                {
                    if (today.ClosingBalance != (today.OpenningBalance + today.CashIn - today.CashOut))
                    {
                        today.ClosingBalance = (today.OpenningBalance + today.CashIn - today.CashOut);
                        db.Entry(today).State = EntityState.Modified;
                        if (saveit) db.SaveChanges();
                    }

                }
                else
                {
                    throw new Exception();
                }
            }

        }
        public static void ProcessOpenningClosingBankBalance(AprajitaRetailsContext db, DateTime date, bool isclosing = false, bool saveit = false)
        {
            if (!isclosing)
            {
                CashInBank today;
                today = db.CashInBanks.Where(c => c.CIBDate == date).FirstOrDefault();
                DateTime yDate = date.AddDays(-1);
                CashInBank yesterday = db.CashInBanks.Where(c => c.CIBDate == yDate).FirstOrDefault();
                if (today == null)
                {
                    today = new CashInBank() { CashIn = 0, CashOut = 0, CIBDate = date, ClosingBalance = 0, OpenningBalance = 0 };

                    if (yesterday != null)
                    {
                        yesterday.ClosingBalance = yesterday.OpenningBalance + yesterday.CashIn - yesterday.CashOut;
                        today.ClosingBalance = today.OpenningBalance = yesterday.ClosingBalance;
                        db.CashInBanks.Add(today);
                        if (saveit) db.SaveChanges();
                    }
                    else
                    {
                        if (db.CashInHands.Count() > 0)
                            throw new Exception();
                        else
                        {
                            today.ClosingBalance = today.OpenningBalance = 0;
                            db.CashInBanks.Add(today);
                            if (saveit) db.SaveChanges();
                        }
                    }

                }
                else
                {
                    if (yesterday != null)
                    {
                        today.ClosingBalance = today.OpenningBalance = yesterday.ClosingBalance;
                        db.Entry(today).State = EntityState.Modified;
                        if (saveit) db.SaveChanges();
                    }
                    else
                    {
                        if (db.CashInHands.Count() > 1)
                            throw new Exception();
                        else
                        {
                            today.ClosingBalance = today.OpenningBalance = 0;
                            db.CashInBanks.Add(today);
                            if (saveit) db.SaveChanges();
                        }
                    }
                }





            }
            else
            {
                //ClosingBalance;
                CashInBank today;
                today = db.CashInBanks.Where(c => c.CIBDate == date).FirstOrDefault();

                if (today != null)
                {
                    if (today.ClosingBalance != (today.OpenningBalance + today.CashIn - today.CashOut))
                    {
                        today.ClosingBalance = (today.OpenningBalance + today.CashIn - today.CashOut);
                        db.Entry(today).State = EntityState.Modified;
                        if (saveit) db.SaveChanges();
                    }

                }
                else
                {

                    throw new Exception();

                }
            }

        }

        public static void CreateCashInHand(AprajitaRetailsContext db, DateTime date, decimal inAmt, decimal outAmt, bool saveit = false)
        {


            //One Day Back
            DateTime yDate = date.AddDays(-1);
            CashInHand yesterday = db.CashInHands.Where(c => c.CIHDate == yDate).FirstOrDefault();
            CashInHand today = new CashInHand() { CashIn = inAmt, CashOut = outAmt, CIHDate = date, ClosingBalance = 0, OpenningBalance = 0 };

            if (yesterday != null)
            {
                yesterday.ClosingBalance = yesterday.OpenningBalance + yesterday.CashIn - yesterday.CashOut;             
                today.ClosingBalance = today.OpenningBalance = yesterday.ClosingBalance;
                db.CashInHands.Add(today);
                if (saveit) db.SaveChanges();
            }
            else
            {
                if (db.CashInHands.Count() > 0)
                    throw new Exception();
                //TODO: if yesterday one or day back data not present handel this
                else
                {
                    today.ClosingBalance = today.OpenningBalance = 0;
                    db.CashInHands.Add(today);
                    if (saveit) db.SaveChanges();
                }
            }


        }
        public static void CreateCashInBank(AprajitaRetailsContext db, DateTime date, decimal inAmt, decimal outAmt, bool saveit = false)
        {


            CashInBank today;

            DateTime yDate = date.AddDays(-1);
            CashInBank yesterday = db.CashInBanks.Where(c => c.CIBDate == yDate).FirstOrDefault();


            today = new CashInBank() { CashIn = 0, CashOut = 0, CIBDate = date, ClosingBalance = 0, OpenningBalance = 0 };

            if (yesterday != null)
            {
                yesterday.ClosingBalance = yesterday.OpenningBalance + yesterday.CashIn - yesterday.CashOut;
                today.ClosingBalance = today.OpenningBalance = yesterday.ClosingBalance;
                db.CashInBanks.Add(today);
                if (saveit) db.SaveChanges();
            }
            else
            {
                if (db.CashInBanks.Count() > 0)
                    throw new Exception();
                else
                {
                    today.ClosingBalance = today.OpenningBalance = 0;
                    db.CashInBanks.Add(today);
                    if (saveit) db.SaveChanges();
                }
            }





        }


        public static void UpDateCashInHand(AprajitaRetailsContext db, DateTime dateTime, decimal Amount, bool saveit = false)
        {

            {
                CashInHand cashIn = db.CashInHands.Where(d => d.CIHDate == dateTime).FirstOrDefault();
                if (cashIn != null)
                {
                    cashIn.CashIn += Amount;
                    db.Entry(cashIn).State = EntityState.Modified;
                    if (saveit) db.SaveChanges();
                }
                else
                {
                    CreateCashInHand(db, dateTime, Amount, 0, saveit);
                    // db.CashInHands.Add(new CashInHand() { CIHDate = dateTime, CashIn = Amount, OpenningBalance = 0, ClosingBalance = 0, CashOut = 0 });
                    // if (saveit) db.SaveChanges();
                    // ProcessOpenningClosingBalance(db, dateTime, false, saveit);
                }

            }
        }
        public static void UpDateCashOutHand(AprajitaRetailsContext db, DateTime dateTime, decimal Amount, bool saveit = false)
        {

            {
                CashInHand cashIn = db.CashInHands.Where(d => d.CIHDate == dateTime).FirstOrDefault();
                if (cashIn != null)
                {
                    cashIn.CashOut += Amount;
                    db.Entry(cashIn).State = EntityState.Modified;
                    if (saveit) db.SaveChanges();
                }
                else
                {

                    //db.CashInHands.Add(new CashInHand() { CIHDate = dateTime, CashIn = 0, OpenningBalance = 0, ClosingBalance = 0, CashOut = Amount });
                    //if (saveit) db.SaveChanges();
                    CreateCashInHand(db, dateTime, 0, Amount, saveit);
                }

            }
        }
        public static void UpDateCashInBank(AprajitaRetailsContext db, DateTime dateTime, decimal Amount, bool saveit = false)
        {

            {
                CashInBank cashIn = db.CashInBanks.Where(d => d.CIBDate == dateTime).FirstOrDefault();
                if (cashIn != null)
                {
                    cashIn.CashIn += Amount;
                    db.SaveChanges();
                }
                else
                {
                    // db.CashInBanks.Add(new CashInBank() { CIBDate = dateTime, CashIn = 0, OpenningBalance = 0, ClosingBalance = 0, CashOut = Amount });
                    // if (saveit) db.SaveChanges();
                    CreateCashInBank(db, dateTime, Amount, 0, saveit);

                }

            }
        }
        public static void UpDateCashOutBank(AprajitaRetailsContext db, DateTime dateTime, decimal Amount, bool saveit = false)
        {

            {
                CashInBank cashIn = db.CashInBanks.Where(d => d.CIBDate == dateTime).FirstOrDefault();
                if (cashIn != null)
                {
                    cashIn.CashOut += Amount;
                    db.SaveChanges();
                }
                else
                {
                    //  db.CashInBanks.Add(new CashInBank() { CIBDate = dateTime, CashIn = 0, OpenningBalance = 0, ClosingBalance = 0, CashOut = Amount });
                    //if (saveit) db.SaveChanges();
                    CreateCashInBank(db, dateTime, 0, Amount, saveit);

                }

            }
        }
    }

    // Tables
    public class EndOfDay
    {
        public int EndOfDayId { get; set; }

        [Display(Name = "EOD Date")]
        public DateTime EOD_Date { get; set; }
        public float Shirting { get; set; }
        public float Suiting { get; set; }
        public int USPA { get; set; }
        [Display(Name = "FM/Arrow/Others")]
        public int FM_Arrow { get; set; }
        [Display(Name = "Arvind RTW")]
        public int RWT { get; set; }
        [Display(Name = "Accessories")]
        public int Access { get; set; }
        public int Tailoring { get; set; }
    }

    //TODO: Remove this
    //public class PayMode
    //{
    //    public int PayModeId { get; set; }
    //    public string PayModeName { set; get; }
    //}
    public class CashInHand
    {
        public int CashInHandId { get; set; }
        [Index(IsUnique = true)]
        [Display(Name = "Cash-in-hand Date")]
        public DateTime CIHDate { get; set; }
        [Display(Name = "Openning Balance")]
        public decimal OpenningBalance { get; set; }
        [Display(Name = "ClosingBalance")]
        public decimal ClosingBalance { get; set; }
        [Display(Name = "Cash-In Amount")]
        public decimal CashIn { get; set; }
        [Display(Name = "Cash-Out Amount")]
        public decimal CashOut { get; set; }
    }
    public class CashInBank
    {
        public int CashInBankId { get; set; }
        [Display(Name = "Cash-in-Bank Date")]
        [Index(IsUnique = true)]
        public DateTime CIBDate { get; set; }
        [Display(Name = "Openning Balance")]
        public decimal OpenningBalance { get; set; }
        [Display(Name = "ClosingBalance")]
        public decimal ClosingBalance { get; set; }
        public decimal CashIn { get; set; }
        [Display(Name = "Cash-Out Amount")]
        public decimal CashOut { get; set; }
    }
    public class CashInward
    {
        public int CashInwardId { get; set; }
        [Display(Name = "Inward Date")]
        public DateTime InwardDate { get; set; }
        [Display(Name = "Reciept From"), Required]
        public string RecieptFrom { get; set; }
        public double Amount { get; set; }
        [Display(Name = "Reciept No")]
        public string SlipNo { get; set; }
    }
    public class HomeExpense
    {
        public int HomeExpenseId { get; set; }
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Paid To")]
        public string PaidTo { get; set; }
        public double Amount { get; set; }
        [Display(Name = "Slip No")]
        public string SlipNo { get; set; }
    }
    public class OtherHomeExpense
    {
        public int OtherHomeExpenseId { get; set; }
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Paid To")]
        public string PaidTo { get; set; }
        public double Amount { get; set; }
        [Display(Name = "Slip No")]
        public string SlipNo { get; set; }
        public string Remarks { get; set; }
    }
    public class AmitKumarExpense
    {
        public int AmitKumarExpenseId { get; set; }
        [Display(Name = "Payment Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Paid To")]
        public string PaidTo { get; set; }
        public double Amount { get; set; }
        [Display(Name = "Slip No")]
        public string SlipNo { get; set; }
    }
    public class DailySale
    {
        public int DailySaleId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Sale Date")]
        public DateTime SaleDate { get; set; }

        [Display(Name = "Invoice No")]
        public string InvNo { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PayModes PayMode { get; set; }

        [Display(Name = "Cash Amount")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashAmount { get; set; }

        [ForeignKey("Salesman")]
        public int SalesmanId { get; set; }
        public virtual Salesman Salesman { get; set; }

        [Display(Name = "Is Due")]
        public bool IsDue { get; set; }

        [Display(Name = "Is Manual Bill")]
        public bool IsManualBill { get; set; }

        [Display(Name = "Is Tailoring Bill")]
        public bool IsTailoringBill { get; set; }
        public string Remarks { get; set; }

        //public virtual DuesList DuesList { get; set; }

    }

    public class Salesman
    {
        public int SalesmanId { get; set; }
        [Display(Name = "Salesman")]
        public string SalesmanName { get; set; }

        public virtual ICollection<DailySale> DailySales { get; set; }
    }
    public class Expenses
    {
        public int ExpensesId { get; set; }
        [Display(Name = "Expense Date")]
        public DateTime ExpDate { get; set; }
        public string Particulars { get; set; }
        public double Amount { get; set; }
        [Display(Name = "Payment Mode")]
        public PayModes PayMode { get; set; }
        [Display(Name = "Payment Details")]
        public string PaymentDetails { get; set; }
        [Display(Name = "Paid By")]
        public string PaidBy { get; set; }
        [Display(Name = "Paid To")]
        public string PaidTo { get; set; }
        public string Remarks { get; set; }

    }
    public class BankDeposit
    {
        public int BankDepositId { get; set; }
        public DateTime DepoDate { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public double Amount { get; set; }
        public string PayMode { get; set; }
        public string Details { get; set; }
        public string Remarks { get; set; }
    }
    public class Bank
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
    }
    public class AccountNumber
    {
        public int AccountNumberId { get; set; }
        public int BankId { get; set; }
        public string Account { get; set; }
    }
    public class TalioringBooking
    {
        public int TalioringBookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public string CustName { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string BookingSlipNo { get; set; }
        public double TotalAmount { get; set; }
        public int TotalQty { get; set; }
        public int ShirtQty { get; set; }
        public double ShirtPrice { get; set; }
        public int PantQty { get; set; }
        public double PantPrice { get; set; }
        public int CoatQty { get; set; }
        public int KurtaQty { get; set; }
        public int BundiQty { get; set; }
        public int OthersQty { get; set; }
        public double CoatPrice { get; set; }
        public double BundiPrice { get; set; }
        public double OthersPrice { get; set; }
        public double KurtaPrice { get; set; }

    }
    public class TalioringDelivery
    {
        public int TalioringDeliveryId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string InvNo { get; set; }
        public string Remarks { get; set; }
        public double Amount { get; set; }

    }
    public class Payments
    {
        public int PaymentsId { get; set; }
        public DateTime PayDate { get; set; }
        public string PaymentParties { get; set; }
        public string PaymentDetails { get; set; }
        public string Remarks { get; set; }
        public double Amount { get; set; }
        public string PaymentSlipNo { get; set; }
        public string PayMode { get; set; }

    }
    public class Recipets
    {
        public int RecipetsId { get; set; }
        public DateTime RecieptDate { get; set; }
        public string RecieptFrom { get; set; }
        public string RecieptDetails { get; set; }
        public string Remarks { get; set; }
        public double Amount { get; set; }
        public string RecieptSlipNo { get; set; }
        public string PayMode { get; set; }
    }
    public class Attendences
    {
        public int AttendencesId { get; set; }
        public string StaffName { get; set; }
        public string Remarks { get; set; }
        public DateTime AttDate { get; set; }
        public string EntryTime { get; set; }
        public double AttUnit { get; set; }

    }
    public class DailySaleReport
    {
        public double DailySale { get; set; }
        public double MonthlySale { get; set; }
        public double YearlySale { get; set; }
        public double WeeklySale { get; set; }
        public double QuaterlySale { get; set; }

    }
    public class ManaulSaleReport
    {
        public double DailySale { get; set; }
        public double MonthlySale { get; set; }
        public double YearlySale { get; set; }
        public double PendingSale { get; set; }
        public double SaleAdjustest { get; set; }
        public double TotalFixedSale { get; set; }
    }
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Display(Name = "Staff Name")]
        public string StaffName { get; set; }
        [Display(Name = "Mobile No")]
        public string Mobileno { get; set; }
        [Display(Name = "Joining Date")]
        public DateTime JoiningDate { get; set; }
        [Display(Name = "Leaving Date")]
        public DateTime LeavingDate { get; set; }
        [Display(Name = "Is Working")]
        public bool IsWorking { get; set; }

    }
    public class SalaryPayment
    {
        public int SalaryPaymentId { get; set; }
        [Display(Name = "OpenningBalance")]
        public string StaffName { get; set; }
        [Display(Name = "OpenningBalance")]
        public string SalaryMonth { get; set; }
        [Display(Name = "OpenningBalance")]
        public DateTime PaymentDate { get; set; }
        public double Amount { get; set; }
        [Display(Name = "OpenningBalance")]
        public PayModes PayMode { get; set; }
        public string Details { get; set; }


    }
    public class AdvancePayment
    {
        public int AdvancePaymentId { get; set; }
        public string StaffName { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Amount { get; set; }
        public string PayMode { get; set; }
        public string Details { get; set; }
    }
    public class AdvanceReceipt
    {
        public int AdvanceReceiptId { get; set; }
        public string StaffName { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Amount { get; set; }
        public string PayMode { get; set; }
        public string Details { get; set; }
    }
    public class TailoringReport
    {
        public double TodaySale { get; set; }
        public double MonthlySale { get; set; }
        public double YearlySale { get; set; }
        public double QuaterlySale { get; set; }
        public double TodayBooking { get; set; }
        public double TodayUnit { get; set; }
        public double MonthlyBooking { get; set; }
        public double MonthlyUnit { get; set; }
        public double YearlyBooking { get; set; }
        public double YearlyUnit { get; set; }
    }
    public class DuesList
    {
        public int DuesListId { get; set; }
        public decimal Amount { get; set; }
        [Display(Name ="Is Paid")]
        public bool IsRecovered { get; set; }
        public int DailySaleId { get; set; }
        public virtual DailySale DailySale { get; set; }
    }
    // DBContext Sections
    public class AprajitaRetailsContext : DbContext
    {
        public AprajitaRetailsContext() : base("AprajitaRetails")
        {
            Database.SetInitializer<AprajitaRetailsContext>(new CreateDatabaseIfNotExists<AprajitaRetailsContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AprajitaRetailsContext, Migrations.Configuration>());
        }

        public DbSet<DailySale> DailySales { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<BankDeposit> BankDeposits { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Recipets> Recipets { get; set; }
        public DbSet<TalioringBooking> TalioringBookings { get; set; }
        public DbSet<TalioringDelivery> TalioringDeliveries { get; set; }
        public DbSet<Attendences> Attendences { get; set; }
        public DbSet<Employee> Employees { get; set; } // Changed from Orignail
        public DbSet<AdvancePayment> AdvancePayments { get; set; }
        public DbSet<AdvanceReceipt> AdvanceReceipts { get; set; }
        public DbSet<SalaryPayment> SalaryPayments { get; set; }
        public DbSet<HomeExpense> HomeExpenses { get; set; }
        public DbSet<OtherHomeExpense> OtherHomeExpenses { get; set; }
        public DbSet<AmitKumarExpense> AmitKumarExpenses { get; set; }
        public DbSet<CashInward> CashInwards { get; set; }
        public DbSet<CashInHand> CashInHands { get; set; }
        public DbSet<CashInBank> CashInBanks { get; set; }
        public DbSet<Bank> Banks { get; set; }
        //public DbSet<PayMode> PayModes { get; set; }// Changed from Orignail

        //Version 2

        public DbSet<Salesman> Salesmen { get; set; }
        public DbSet<DuesList> DuesLists { get; set; }
    }
}