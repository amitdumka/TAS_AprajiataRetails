using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TAS_AprajiataRetails.Models.Data;

namespace TAS_AprajiataRetails.Models.Views
{
    public class ManaulSaleReport
    {
        public decimal DailySale { get; set; }
        public decimal MonthlySale { get; set; }
        public decimal YearlySale { get; set; }
        public decimal PendingSale { get; set; }
        public decimal SaleAdjustest { get; set; }
        public decimal TotalFixedSale { get; set; }
    }


    public class MasterViewReport
    {
        public DailySaleReport SaleReport { get; set; }
        public TailoringReport TailoringReport { get; set; }
        public List<EmployeeInfo> EmpInfoList { get; set; }
       // public ManaulSaleReport ManaulSale { get; set; }
        //public List<EmpStatus> PresentEmp { get; set; }
        public AccountsInfo AccountsInfo { get; set; }

    }


    public class TailoringReport
    {
        [Display(Name ="Today")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TodaySale { get; set; }
        [Display(Name = "Montly")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MonthlySale { get; set; }
        [Display(Name = "Yearly")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal YearlySale { get; set; }
        //public decimal QuaterlySale { get; set; }

        [Display(Name = "Booking")]
        public decimal TodayBooking { get; set; }
        [Display(Name = "Items")]
        public decimal TodayUnit { get; set; }
        [Display(Name = "Booking")]
        public decimal MonthlyBooking { get; set; }
        [Display(Name = "Items")]
        public decimal MonthlyUnit { get; set; }
        [Display(Name = "Booking")]
        public decimal YearlyBooking { get; set; }
        [Display(Name = "Itemst")]
        public decimal YearlyUnit { get; set; }
    }

    public class DailySaleReport
    {

        [Display(Name = "Today")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal DailySale { get; set; }

        [Display(Name = "Monthly")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MonthlySale { get; set; }
        
        [DataType(DataType.Currency), Column(TypeName = "money")]
        [Display(Name = "Yearly")]
        public decimal YearlySale { get; set; }
        
        [DataType(DataType.Currency), Column(TypeName = "money")]
        [Display(Name = "Weekly")]
        public decimal WeeklySale { get; set; }
        
        [DataType(DataType.Currency), Column(TypeName = "money")]
        [Display(Name = "Quarterly")]
        public decimal QuaterlySale { get; set; }

    }

    public class EmployeeInfo
    {
        [Display(Name = "Staff Name")]
        public string Name { get; set; }

        [Display(Name = "Present Today")]
        public string Present { get; set; }
        
        [Display(Name = "No of Days Present")]
        public double PresentDays { get; set; }
        
        [Display(Name = "No of Days Absent")]
        public double AbsentDays { get; set; }
        
        [Display(Name = "Ratio Of Attenadance")]
        public double Ratio { get; set; }
        
        [Display(Name = "Current Month Sale")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalSale { get; set; }
        
        [Display(Name = "No Of Bills")]
        public int NoOfBills { get; set; }

    }


    public class EmpStatus
    {
        public string StaffName { get; set;}
        public bool IsPresent { get; set; }
    }

    public class EndofDayDetails
    {
        // public EndOfDay EndofDay { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "DSR Date")]
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

        [Display(Name = "Total Sale")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TodaySale { get; set; }
        [Display(Name = "Card Sale")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TodayCardSale { get; set; }
        [Display(Name = "OtherMode Sale")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TodayOtherSale { get; set; }
        [Display(Name = "Manual Sale")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TodayManualSale { get; set; }

        [Display(Name = "Tailoring Delivery Sale")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TodayTailoringSale { get; set; }
        [Display(Name = "Tailoring Booking ")]
        public int TodayTailoringBooking { get; set; }
        [Display(Name = "Total Unit")]
        public int TodayTotalUnit { get; set; }

        [Display(Name = "Total Expenses")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TodayTotalExpenses { get; set; }
        [Display(Name = "Total Payments")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalPayments { get; set; }
        [Display(Name = "Total Receipts")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalReceipts { get; set; }

        [Display(Name = "Cash In Hand")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TodayCashInHand { get; set; }


    }

    public class AccountsInfo
    {
        [Display(Name ="Cash-In-Hand")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashInHand { get; set; }
        [Display(Name = "Openning Balance")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal OpenningBal { get; set; }
        [Display(Name = "Income")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashIn { get; set; }
        [Display(Name = "Expenses")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashOut { get; set; }
        [Display(Name = "Cash Payments")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalCashPayments { get; set; }
        [Display(Name = "Bank Balance")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashInBank { get; set; }
        [Display(Name = "Deposited")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashToBank { get; set;}
        [Display(Name = "Withdrawal")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashFromBank { get; set; }
    }


    public class CashBook
    {
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name ="Date")]
        public DateTime EDate { get; set; }
        public string Particulars { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashIn { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashOut { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashBalance { get; set; }
    }
}
