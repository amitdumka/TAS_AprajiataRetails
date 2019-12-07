using System;
using System.Data.Entity;
using System.Linq;
using TAS_AprajiataRetails.Models.Data;
using TAS_AprajiataRetails.Models.Views;

namespace TAS_AprajiataRetails.Models.Helpers
{
    public class Reports
    {
        public static void  GetTailoringReport()
        {

        }

        public static DailySaleReport GetSaleRecord()
        {
            using (AprajitaRetailsContext db = new AprajitaRetailsContext())
            {
                DailySaleReport record = new DailySaleReport();
                record.DailySale = (decimal?) db.DailySales.Where(C => DbFunctions.TruncateTime(C.SaleDate) == DbFunctions.TruncateTime(DateTime.Today)).Sum(c => c.Amount)??0;
                record.MonthlySale= (decimal?)db.DailySales.Where(C => DbFunctions.TruncateTime(C.SaleDate).Value.Month == DbFunctions.TruncateTime(DateTime.Today).Value.Month).Sum(c => c.Amount) ?? 0;
                record.WeeklySale = (decimal?)db.DailySales.Where(C => DbFunctions.TruncateTime(C.SaleDate).Value.DayOfWeek == DbFunctions.TruncateTime(DateTime.Today).Value.DayOfWeek).Sum(c => c.Amount) ?? 0;
                record.YearlySale= (decimal?)db.DailySales.Where(C => DbFunctions.TruncateTime(C.SaleDate).Value.Year == DbFunctions.TruncateTime(DateTime.Today).Value.Year).Sum(c => c.Amount) ?? 0;
                record.QuaterlySale = (decimal?)db.DailySales.Where(C => DbFunctions.TruncateTime(C.SaleDate).Value.Year == DbFunctions.TruncateTime(DateTime.Today).Value.Year).Sum(c => c.Amount) ?? 0;
                return record;
            }
            
        }

        public static void GetAccoutingRecord()
        {

        }
    }


}