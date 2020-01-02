using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TAS_AprajiataRetails.Models.Data;

namespace TAS_AprajiataRetails.Models.Helpers
{
    //TODO: Create Table Class for MonthEnd Data

    public class MonthEnd
    {
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

    public class MonthEndProcesser
    {
        public void ProcessMonthEnd(DateTime onDate)
        {
            MonthEnd monthEnd = null;
            using (AprajitaRetailsContext db = new AprajitaRetailsContext())
            {
                monthEnd = CalculateTotalIncome(db, onDate);
                monthEnd = CalculateTotalExpenses(db, onDate, monthEnd);


            }
        }
        private MonthEnd CalculateTotalIncome(AprajitaRetailsContext db, DateTime onDate)
        {
            MonthEnd monthEnd = new MonthEnd();
            monthEnd.TotalSaleIncome = db.DailySales.Where(c => DbFunctions.TruncateTime(c.SaleDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);
            monthEnd.TotalTailoringIncome = db.DailySales.Where(c => c.IsTailoringBill && DbFunctions.TruncateTime(c.SaleDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);
            monthEnd.TotalOtherIncome = 0; //TODO: Ohter Income group will be dealt with proper entry.
            monthEnd.TotalRecipts=db.Receipts.Where(c => DbFunctions.TruncateTime(c.RecieptDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);

            return monthEnd;
        }
        private MonthEnd CalculateTotalExpenses(AprajitaRetailsContext db, DateTime onDate, MonthEnd mEnd)
        {
            mEnd.TotalSalary = db.Salaries.Where(c => DbFunctions.TruncateTime(c.PaymentDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);
            mEnd.TotalTailoringExpenses=db.TailoringSalaries.Where(c => DbFunctions.TruncateTime(c.PaymentDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);
            mEnd.TotalCashExpenses=db.CashExpenses.Where(c => DbFunctions.TruncateTime(c.ExpDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);
     
            mEnd.TotalHomeExpenses=db.CashPayments.Where(c => DbFunctions.TruncateTime(c.PaymentDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);
            mEnd.TotalPayments=db.Payments.Where(c => DbFunctions.TruncateTime(c.PayDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);

            mEnd.TotalDuesOfMonth=db.DuesLists.Include(c=>c.DailySale).Where(c =>c.IsRecovered==false && DbFunctions.TruncateTime(c.DailySale.SaleDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);
            mEnd.TotalDues = db.DuesLists.Where(c => !c.IsRecovered).Sum(c => c.Amount);
            mEnd.TotalDuesRecovered=db.DuesLists.Where(c => DbFunctions.TruncateTime(c.RecoveryDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);

            return mEnd;
        
        }

        private void UpdateMonthEnd(AprajitaRetailsContext db, MonthEnd mEnd, bool upDate)
        {
            //TODO: Save MonthEnd data to database.
        }

    }


}