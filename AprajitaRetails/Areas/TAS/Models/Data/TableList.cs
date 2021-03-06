﻿using System;
using System.Linq;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.TAS.Models.Data
{
    //TODO: Add Support for Mix Payment
    
    
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


    //Income 
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

        [Display(Name = "Is Sale Return")]
        public bool IsSaleReturn { get; set; }
        public string Remarks { get; set; }

        //public virtual DuesList DuesList { get; set; }

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

    // Expenses
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
    public class DuesList
    {
        public int DuesListId { get; set; }
        public decimal Amount { get; set; }

        [Display(Name = "Is Paid")]
        public bool IsRecovered { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Recovery Date")]
        public DateTime? RecoveryDate { get; set; }
        public int DailySaleId { get; set; }
        public virtual DailySale DailySale { get; set; }

        public bool IsPartialRecovery { get; set; }

        public virtual ICollection<DueRecoverd> Recoverds { get; set; }
    }

    // Daily Sale  and Cash Management


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

        [Display(Name = "CashInHand")]
        public decimal InHand
        {
            get
            {
                return OpenningBalance + CashIn - CashOut;
            }
        }
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

        [Display(Name = "CashInBank")]
        public decimal InHand
        {
            get
            {
                return OpenningBalance + CashIn - CashOut;
            }
        }
    }



    public class Salesman
    {
        public int SalesmanId { get; set; }
        [Display(Name = "Salesman")]
        public string SalesmanName { get; set; }

        public virtual ICollection<DailySale> DailySales { get; set; }
    }

    //Banking Section
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


    public class EndOfDay
    {
        public int EndOfDayId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "EOD Date")]
        [Index(IsUnique = true)]
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
        [Display(Name = "Cash at Store")]
        public decimal CashInHand { get; set; }
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

    public class ChequesLog
    {
        public int ChequesLogId { get; set; }
        [Display(Name ="Bank Name")]
        public string BankName { get; set; }
        [Display(Name = "Account No")]
        public string AccountNumber { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Cheque Date")]
        public DateTime? ChequesDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Deposit Date")]
        public DateTime? DepositDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Cleared Date")]
        public DateTime? ClearedDate { get; set; }
        [Display(Name = "By")]
        public string IssuedBy { get; set; }
        [Display(Name = "To")]
        public string IssuedTo { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        [Display(Name = "PDC")]
        public bool IsPDC { get; set; }
        [Display(Name = "By Aprajita Retails")]
        public bool IsIssuedByAprajitaRetails { get; set; }
        [Display(Name = "To Aprajita Retails")]
        public bool IsDepositedOnAprajitaRetails { get; set; }
        public string Remarks { get; set; }
    }


    public class DueRecoverd
    {
        public int DueRecoverdId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Recovery Date")]
        public DateTime PaidDate { get; set; }

        public int DuesListId { get; set; }
        public virtual DuesList DuesList {get;set;}
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal AmountPaid { get; set; }
        [Display(Name ="Is Partial Payment")]
        public bool IsPartialPayment { get; set; }
        public PaymentModes Modes { get; set; }
        public string Remarks { get; set; }

    }


    public class MonthEnd
    {
        //Table Data
        public int MonthEndId { get; set; }
        public DateTime EntryDate { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }

        //Sale Info

        public double TotalBill { get; set; }
        public double TotalFabric { get; set; }
        public double TotalRMZ { get; set; }
        public double TotalAccess { get; set; }
        public double TotalOthers { get; set; }

        public decimal TotalAmountFabric { get; set; }
        public decimal TotalAmountRMZ { get; set; }
        public decimal TotalAmountAccess { get; set; }
        public decimal TotalAmountOthers { get; set; }


        public decimal TotalSaleIncome { get; set; } //Done
        public decimal TotalTailoringIncome { get; set; } //Done
        public decimal TotalOtherIncome { get; set; }

        public decimal TotalInward { get; set; }
        public decimal TotalInwardByAmitKumar { get; set; }
        public decimal TotalInwardOthers { get; set; }

        public decimal TotalDues { get; set; }//DOne
        public decimal TotalDuesOfMonth { get; set; }//Done
        public decimal TotalDuesRecovered { get; set; }//Check for Corrert table is requried

        public decimal TotalExpenses { get; set; } //Check it 

        public decimal TotalOnBookExpenes { get; set; }
        public decimal TotalCashExpenses { get; set; }//Check it

        public decimal TotalSalary { get; set; }//Done
        public decimal TotalTailoringExpenses { get; set; }//Done
        public decimal TotalTrimsAndOtherExpenses { get; set; }

        public decimal TotalHomeExpenses { get; set; }//Check 
        public decimal TotalOtherHomeExpenses { get; set; }//Check

        public decimal TotalOtherExpenses { get; set; }

        public decimal TotalPayments { get; set; }//done
        public decimal TotalRecipts { get; set; }//done

    }




    //TODO: List
    //TODO: Dues Recovery options
    //TODO: Tailoring 
    //TODO: Sales return policy update and check 
    //TODO: Purchase of Items/Assets
    //TODO: Arvind Payments
    //TODO: Purchase Invoice Entry

}
