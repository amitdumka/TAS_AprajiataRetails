using System;
using System.ComponentModel.DataAnnotations;
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


    public class TailoringReport
    {
        public decimal TodaySale { get; set; }
        public decimal MonthlySale { get; set; }
        public decimal YearlySale { get; set; }
        public decimal QuaterlySale { get; set; }

        public decimal TodayBooking { get; set; }
        public decimal TodayUnit { get; set; }

        public decimal MonthlyBooking { get; set; }
        public decimal MonthlyUnit { get; set; }

        public decimal YearlyBooking { get; set; }
        public decimal YearlyUnit { get; set; }
    }

    public class DailySaleReport
    {

        [Display(Name = "Today")]
        public decimal DailySale { get; set; }
        [Display(Name = "Monthly")]
        public decimal MonthlySale { get; set; }
        [Display(Name = "Yearly")]
        public decimal YearlySale { get; set; }
        [Display(Name = "Weekly")]
        public decimal WeeklySale { get; set; }
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
        public decimal TotalSale { get; set; }
        [Display(Name = "No Of Bills")]
        public int NoOfBills { get; set; }

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
        public decimal TodaySale { get; set; }
        [Display(Name = "Card Sale")]
        public decimal TodayCardSale { get; set; }
        [Display(Name = "OtherMode Sale")]
        public decimal TodayOtherSale { get; set; }
        [Display(Name = "Manual Sale")]
        public decimal TodayManualSale { get; set; }

        [Display(Name = "Tailoring Delivery Sale")]
        public decimal TodayTailoringSale { get; set; }
        [Display(Name = "Tailoring Booking ")]
        public int TodayTailoringBooking { get; set; }
        [Display(Name = "Total Unit")]
        public int TodayTotalUnit { get; set; }

        [Display(Name = "Total Expenses")]
        public decimal TodayTotalExpenses { get; set; }
        [Display(Name = "Total Payments")]
        public decimal TotalPayments { get; set; }
        [Display(Name = "Total Receipts")]
        public decimal TotalReceipts { get; set; }

        [Display(Name = "Cash In Hand")]
        public decimal TodayCashInHand { get; set; }


    }
}
