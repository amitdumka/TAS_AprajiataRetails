using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TAS_AprajiataRetails.Models.Data;
using TAS_AprajiataRetails.Models.Views;

namespace TAS_AprajiataRetails.Models.Helpers
{
    public class Reports
    {
        public static TailoringReport GetTailoringReport()
        {
            using (AprajitaRetailsContext db = new AprajitaRetailsContext())
            {
                return  new TailoringReport()
                {
                    TodayBooking = (int?)db.Bookings.Where(c => DbFunctions.TruncateTime(c.BookingDate) == DbFunctions.TruncateTime(DateTime.Today)).Count() ?? 0,
                    TodayUnit = (int?)db.Bookings.Where(c => DbFunctions.TruncateTime(c.BookingDate) == DbFunctions.TruncateTime(DateTime.Today)).Sum(c => (int?)c.TotalQty) ?? 0,

                    MonthlyBooking = (int?)db.Bookings.Where(c => DbFunctions.TruncateTime(c.BookingDate).Value.Month == DbFunctions.TruncateTime(DateTime.Today).Value.Month).Count() ?? 0,
                    MonthlyUnit = (int?)db.Bookings.Where(c => DbFunctions.TruncateTime(c.BookingDate).Value.Month == DbFunctions.TruncateTime(DateTime.Today).Value.Month).Sum(c => (int?)c.TotalQty) ?? 0,
                    YearlyBooking = (int?)db.Bookings.Where(c => DbFunctions.TruncateTime(c.BookingDate).Value.Month == DbFunctions.TruncateTime(DateTime.Today).Value.Month).Count() ?? 0,
                    YearlyUnit = (int?)db.Bookings.Where(c => DbFunctions.TruncateTime(c.BookingDate).Value.Month == DbFunctions.TruncateTime(DateTime.Today).Value.Month).Sum(c => (int?)c.TotalQty) ?? 0,
                    
                    TodaySale= (decimal?)db.Deliveries.Where(c => DbFunctions.TruncateTime(c.DeliveryDate) == DbFunctions.TruncateTime(DateTime.Today)).Sum(c=>(decimal?)c.Amount) ?? 0,
                    YearlySale = (decimal?)db.Deliveries.Where(c => DbFunctions.TruncateTime(c.DeliveryDate).Value.Year == DbFunctions.TruncateTime(DateTime.Today).Value.Year).Sum(c => (decimal?)c.Amount) ?? 0,
                    MonthlySale = (decimal?)db.Deliveries.Where(c => DbFunctions.TruncateTime(c.DeliveryDate).Value.Month == DbFunctions.TruncateTime(DateTime.Today).Value.Month).Sum(c => (decimal?)c.Amount) ?? 0,
                };
               
            }
         
              
        }

        public static DailySaleReport GetSaleRecord()
        {
            using (AprajitaRetailsContext db = new AprajitaRetailsContext())
            {
                DailySaleReport record = new DailySaleReport();
                record.DailySale = (decimal?)db.DailySales.Where(C => DbFunctions.TruncateTime(C.SaleDate) == DbFunctions.TruncateTime(DateTime.Today)).Sum(c => (decimal?)c.Amount) ?? 0;
                record.MonthlySale = (decimal?)db.DailySales.Where(C => DbFunctions.TruncateTime(C.SaleDate).Value.Month == DbFunctions.TruncateTime(DateTime.Today).Value.Month).Sum(c => (decimal?)c.Amount) ?? 0;
                record.YearlySale = (decimal?)db.DailySales.Where(C => DbFunctions.TruncateTime(C.SaleDate).Value.Year == DbFunctions.TruncateTime(DateTime.Today).Value.Year).Sum(c => (decimal?)c.Amount) ?? 0;

                return record;
            }

        }

        public static void GetAccoutingRecord()
        {

        }


        public static void GetEmpInfo()
        {
            using (AprajitaRetailsContext db = new AprajitaRetailsContext())
            {
                //List<EmployeeInfo> EmpInfoList = new List<EmployeeInfo>();
                //var emps= db.Employees.Where(c=>DbFunctions.TruncateTime( c.AttDate).Value.Month== DbFunctions.TruncateTime(DateTime.Today).Value.Month);
                //foreach (var emp in emps)
                //{
                //    EmployeeInfo info = new EmployeeInfo()
                //    {
                //        Name=emp.Employee.StaffName
                //    };

                //}


            }
        }
    }


}