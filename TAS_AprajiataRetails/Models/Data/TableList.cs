using System;
using System.Linq;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAS_AprajiataRetails.Models.Data
{
    //TODO: Add Support for Mix Payment
    public enum PayModes { Cash, Card, RTGS, NEFT, IMPS, Wallets, Cheques, DemandDraft, Points, Others, Coupons, MixPayments };
    public enum PaymentModes { Cash, Card, RTGS, NEFT, IMPS, Wallets, Cheques, DemandDraft, Others };
    public enum AttUnits { Present, Absent, HalfDay, Sunday };
    public enum SalaryComponet { NetSalary, LastPcs, WOWBill, SundaySalary, Incentive, Others }
    public enum BankPayModes { Cash, Card, Cheques, RTGS, NEFT, IMPS, Wallets, Others }

    // Daily Sale  and Cash Management
    public class TranscationMode
    {
        [Display(Name = "Mode")]
        public int TranscationModeId { get; set; }
        //[Index(IsUnique = true)]
        [Display(Name = "Transcation Mode")]
        public string Transcation { get; set; }

        public virtual ICollection<CashReceipt> CashReceipts { get; set; }
        public virtual ICollection<CashPayment> CashPayments { get; set; }
        //Modes Name  write Seed 
        // Amit Kumar , Mukesh, HomeExp, OtherHomeExpenses,CashInOut
    }
    public class CashReceipt
    {
        public int CashReceiptId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Receipt Date")]
        public DateTime InwardDate { get; set; }

        [Display(Name = "Mode")]
        public int TranscationModeId { get; set; }
        public TranscationMode Mode { get; set; }

        [Display(Name = "Receipt From"), Required]
        public string ReceiptFrom { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        [Display(Name = "Receipt No")]
        public string SlipNo { get; set; }

    }
    public class CashPayment
    {
        public int CashPaymentId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Mode")]
        public int TranscationModeId { get; set; }
        public TranscationMode Mode { get; set; }

        [Display(Name = "Paid To"), Required]
        public string PaidTo { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        [Display(Name = "Reciept No")]
        public string SlipNo { get; set; }

    }
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
    public class DuesList
    {
        public int DuesListId { get; set; }
        public decimal Amount { get; set; }
        [Display(Name = "Is Paid")]
        public bool IsRecovered { get; set; }
        public int DailySaleId { get; set; }
        public virtual DailySale DailySale { get; set; }
    }

    //Payroll
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Display(Name = "Staff Name")]
        public string StaffName { get; set; }

        [Display(Name = "Mobile No"), Phone]
        public string MobileNo { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Joining Date")]
        public DateTime JoiningDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Leaving Date")]
        public DateTime? LeavingDate { get; set; }

        [Display(Name = "Is Working")]
        public bool IsWorking { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<SalaryPayment> SalaryPayments { get; set; }
        public ICollection<StaffAdvancePayment> AdvancePayments { get; set; }
        public ICollection<StaffAdvanceReceipt> AdvanceReceipts { get; set; }
        public ICollection<PettyCashExpense> CashExpenses { get; set; }
        public ICollection<Expense> Expenses { get; set; }

    }

    public class Attendance
    {
        public int AttendanceId { get; set; }

        [Display(Name = "Staff Name")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Attendance Date")]
        public DateTime AttDate { get; set; }

        [Display(Name = "Entry Time")]
        public string EntryTime { get; set; }

        public AttUnits Status { get; set; }
        public string Remarks { get; set; }

    }

    public class SalaryPayment
    {
        public int SalaryPaymentId { get; set; }

        [Display(Name = "Staff Name")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Display(Name = "Salary/Year")]
        public string SalaryMonth { get; set; }

        [Display(Name = "Payment Reason")]
        public SalaryComponet SalaryComponet { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PayModes PayMode { get; set; }

        public string Details { get; set; }


    }

    public class StaffAdvancePayment
    {
        public int StaffAdvancePaymentId { get; set; }

        [Display(Name = "Staff Name")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PayModes PayMode { get; set; }

        public string Details { get; set; }
    }
    public class StaffAdvanceReceipt
    {
        public int StaffAdvanceReceiptId { get; set; }

        [Display(Name = "Staff Name")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Receipt Date")]
        public DateTime ReceiptDate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PayModes PayMode { get; set; }
        public string Details { get; set; }
    }

    //Banking Section

    public class BankDeposit
    {
        public int BankDepositId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Deposit Date")]
        public DateTime DepoDate { get; set; }

        public int AccountNumberId { get; set; }
        public AccountNumber Account { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public BankPayModes PayMode { get; set; }

        [Display(Name = "Transcation Details")]
        public string Details { get; set; }
        public string Remarks { get; set; }
    }
    public class Bank
    {
        public int BankId { get; set; }
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        public ICollection<AccountNumber> Accounts { get; set; }
    }
    public class AccountNumber
    {
        public int AccountNumberId { get; set; }

        [Display(Name = "Bank Name")]
        public int BankId { get; set; }
        public Bank Bank { get; set; }

        [Display(Name = "Account Number")]
        public string Account { get; set; }

        public ICollection<BankDeposit> Deposits { get; set; }
        public ICollection<BankWithdrawal> Withdrawals { get; set; }
    }

    public class BankWithdrawal
    {
        public int BankWithdrawalId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Withdrawal Date")]
        public DateTime DepoDate { get; set; }

        public int AccountNumberId { get; set; }
        public AccountNumber Account { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Cheque Details")]
        public string ChequeNo { get; set; }
        [Display(Name = "Signed By")]
        public string SignedBy { get; set; }
        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }
        [Display(Name = "Self/Named")]
        public string InNameOf { get; set; }
    }

    public class EndOfDay
    {
        public int EndOfDayId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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

    //Suspenses
    public class SuspenseAccount
    {
        public int SuspenseAccountId { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime EntryDate { get; set; }
        [Display(Name = "ReferanceDetails")]
        public string ReferanceDetails { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        [Display(Name = "In Amount(+)")]
        public decimal InAmount { get; set; }
        [Display(Name = "Out Amount(-)")]
        public decimal OutAmount { get; set; }
        [Display(Name = "Is Cleared")]
        public bool IsCleared { get; set; }
        [Display(Name = "Cleared Details")]
        public string ClearedDetails { get; set; }
        [Display(Name = "Review By")]
        public string ReviewBy { get; set; }
    }

    //Expenses
    public class Expense
    {
        public int ExpenseId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expense Date")]
        public DateTime ExpDate { get; set; }

        public string Particulars { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PaymentModes PayMode { get; set; }

        [Display(Name = "Payment Details")]
        public string PaymentDetails { get; set; }

        [Display(Name = "Paid By")]
        public int EmployeeId { get; set; }
        public virtual Employee PaidBy { get; set; }

        [Display(Name = "Paid To")]
        public string PaidTo { get; set; }

        public string Remarks { get; set; }

    }

    public class PettyCashExpense
    {
        public int PettyCashExpenseId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expense Date")]
        public DateTime ExpDate { get; set; }

        [Display(Name = "Expense Details")]
        public string Particulars { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Paid By")]
        public int EmployeeId { get; set; }
        public virtual Employee PaidBy { get; set; }


        [Display(Name = "Paid To")]
        public string PaidTo { get; set; }

        [Display(Name = "Remarks/Details")]
        public string Remarks { get; set; }

    }




    public class Payment
    {
        public int PaymentId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date")]
        public DateTime PayDate { get; set; }

        [Display(Name = "Payment Party")]
        public string PaymentPartry { get; set; }
        [Display(Name = "Payment Mode")]
        public PaymentModes PayMode { get; set; }
        [Display(Name = "Payment Details")]
        public string PaymentDetails { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        [Display(Name = "Payment Slip No")]
        public string PaymentSlipNo { get; set; }

        public string Remarks { get; set; }

    }
    public class Receipt
    {
        public int ReceiptId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        [Display(Name = "Expense Date")]
        public DateTime RecieptDate { get; set; }

        [Display(Name = "Receipt From ")]
        public string ReceiptFrom { get; set; }


        [Display(Name = "Payment Mode")]
        public PaymentModes PayMode { get; set; }
        [Display(Name = "Receipt Details ")]
        public string ReceiptDetails { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        [Display(Name = "Receipt Slip No ")]
        public string RecieptSlipNo { get; set; }
        public string Remarks { get; set; }

    }
}
