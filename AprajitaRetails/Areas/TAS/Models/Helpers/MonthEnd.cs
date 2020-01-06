using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AprajitaRetails.Areas.TAS.Models.Data;

namespace AprajitaRetails.Areas.TAS.Models.Helpers
{
    //TODO: Create Table Class for MonthEnd Data

    

    public class MonthEndProcesser
    {
        public void ProcessMonthEnd(DateTime onDate)
        {
            MonthEnd monthEnd = null;
            using (AprajitaRetailsContext db = new AprajitaRetailsContext())
            {
                monthEnd = CalculateTotalIncome(db, onDate);
                monthEnd = CalculateTotalExpenses(db, onDate, monthEnd);
                monthEnd= CalculateSaleData(db, onDate, monthEnd);
                monthEnd = CalculateSaleFinData(db, onDate, monthEnd);
                monthEnd.EntryDate = DateTime.Today;


            }
        }
        private MonthEnd CalculateTotalIncome(AprajitaRetailsContext db, DateTime onDate)
        {
            MonthEnd monthEnd = new MonthEnd();
            monthEnd.TotalSaleIncome = db.DailySales.Where(c => DbFunctions.TruncateTime(c.SaleDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);
            monthEnd.TotalTailoringIncome = db.DailySales.Where(c => c.IsTailoringBill && DbFunctions.TruncateTime(c.SaleDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);
            monthEnd.TotalOtherIncome = 0; //TODO: Ohter Income group will be dealt with proper entry.
            monthEnd.TotalRecipts = db.Receipts.Where(c => DbFunctions.TruncateTime(c.RecieptDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);

            return monthEnd;
        }
        private MonthEnd CalculateTotalExpenses(AprajitaRetailsContext db, DateTime onDate, MonthEnd mEnd)
        {
            mEnd.TotalSalary = db.Salaries.Where(c => DbFunctions.TruncateTime(c.PaymentDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);
            mEnd.TotalTailoringExpenses = db.TailoringSalaries.Where(c => DbFunctions.TruncateTime(c.PaymentDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);
            mEnd.TotalCashExpenses = db.CashExpenses.Where(c => DbFunctions.TruncateTime(c.ExpDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);

            mEnd.TotalHomeExpenses = db.CashPayments.Where(c => DbFunctions.TruncateTime(c.PaymentDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);
            mEnd.TotalPayments = db.Payments.Where(c => DbFunctions.TruncateTime(c.PayDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);

            mEnd.TotalDuesOfMonth = db.DuesLists.Include(c => c.DailySale).Where(c => c.IsRecovered == false && DbFunctions.TruncateTime(c.DailySale.SaleDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);
            mEnd.TotalDues = db.DuesLists.Where(c => !c.IsRecovered).Sum(c => c.Amount);
            mEnd.TotalDuesRecovered = db.DuesLists.Where(c => DbFunctions.TruncateTime(c.RecoveryDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);

            return mEnd;

        }

        public MonthEnd CalculateSaleData(AprajitaRetailsContext db, DateTime onDate, MonthEnd mEnd)
        {
            var endofdays = db.EndOfDays.Where(c => DbFunctions.TruncateTime(c.EOD_Date).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month);
            if (endofdays != null)
            {
                mEnd.TotalFabric = endofdays.Sum(c => c.Shirting) + endofdays.Sum(c => c.Suiting);
                mEnd.TotalAccess = endofdays.Sum(c => c.Access);
                mEnd.TotalRMZ = endofdays.Sum(c => c.FM_Arrow) + endofdays.Sum(c => c.RWT) + endofdays.Sum(c => c.USPA);
                mEnd.TotalOthers = 0;


            }
            return mEnd;
        }

        public MonthEnd CalculateSaleFinData(AprajitaRetailsContext db, DateTime onDate, MonthEnd mEnd)
        {

            mEnd.TotalAmountAccess =mEnd.TotalAmountFabric = mEnd.TotalAmountOthers = mEnd.TotalAmountRMZ = 0;
            //TODO: to be in version 2 & 3 onwards
            return mEnd;
        }

        private void UpdateMonthEnd(AprajitaRetailsContext db, MonthEnd mEnd, bool upDate)
        {
            //TODO: Save MonthEnd data to database.
        }

    }


}