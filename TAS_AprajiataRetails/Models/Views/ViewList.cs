namespace TAS_AprajiataRetails.Models.Views
{
    public class ManaulSaleReport
    {
        public double DailySale { get; set; }
        public double MonthlySale { get; set; }
        public double YearlySale { get; set; }
        public double PendingSale { get; set; }
        public double SaleAdjustest { get; set; }
        public double TotalFixedSale { get; set; }
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

    public class DailySaleReport
    {
        public double DailySale { get; set; }
        public double MonthlySale { get; set; }
        public double YearlySale { get; set; }
        public double WeeklySale { get; set; }
        public double QuaterlySale { get; set; }

    }
}
