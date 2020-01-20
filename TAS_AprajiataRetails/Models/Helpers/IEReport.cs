//using NLog.Fluent;
using System;
using System.Data.Entity;
using System.Linq;
using TAS_AprajiataRetails.Models.Data;
using TAS_AprajiataRetails.Models.Views;

namespace TAS_AprajiataRetails.Models.Helpers
{
    public class IEReport
    {
        public IncomeExpensesReport GetWeeklyReport(AprajitaRetailsContext db)
        {



            var start = DateTime.Now.StartOfWeek();
            var end = DateTime.Now.EndOfWeek();

            IncomeExpensesReport ierData = new IncomeExpensesReport
            {
                OnDate = DateTime.Today,
                IncomeExpensesReportId = 1,

                //Income
                TotalSale = (decimal?) db.DailySales.Where(c => c.SaleDate.Date >= start.Date && c.SaleDate.Date <= end.Date && !c.IsManualBill && !c.IsTailoringBill).Sum(c => c.Amount)??0,
                TotalManualSale = (decimal?) db.DailySales.Where(c => c.SaleDate.Date >= start.Date && c.SaleDate.Date <= end.Date && c.IsManualBill && !c.IsTailoringBill).Sum(c => c.Amount)??0,
                TotalTailoringSale = (decimal?) db.DailySales.Where(c => c.SaleDate.Date >= start.Date && c.SaleDate.Date <= end.Date && c.IsTailoringBill).Sum(c => c.Amount)??0,
                TotalCashRecipts = (decimal?) db.CashReceipts.Where(c => c.InwardDate.Date >= start.Date && c.InwardDate.Date <= end.Date).Sum(c => c.Amount)??0,
                TotalRecipts = (decimal?) db.Receipts.Where(c => c.RecieptDate.Date >= start.Date && c.RecieptDate.Date <= end.Date).Sum(c => c.Amount)??0,
                TotalOtherIncome = 0,

                //Expenses

                TotalExpenses = (decimal?) db.Expenses.Where(c => c.ExpDate.Date >= start.Date && c.ExpDate.Date <= end.Date).Sum(c => c.Amount)??0 + (decimal?)db.CashExpenses.Where(c => c.ExpDate.Date >= start.Date && c.ExpDate.Date <= end.Date).Sum(c => c.Amount)??0,
                TotalOthersExpenses = 0,
                TotalPayments = (decimal?) db.Payments.Where(c => c.PayDate.Date >= start.Date && c.PayDate.Date <= end.Date).Sum(c => c.Amount)??0,
                TotalStaffPayments = (decimal?) db.Salaries.Where(c => c.PaymentDate.Date >= start.Date && c.PaymentDate.Date <= end.Date).Sum(c => c.Amount)??0 +
                                   (decimal?)db.StaffAdvancePayments.Where(c => c.PaymentDate.Date >= start.Date && c.PaymentDate.Date <= end.Date).Sum(c => c.Amount)??0,
                TotalTailoringPayments = (decimal?) db.TailoringSalaries.Where(c => c.PaymentDate.Date >= start.Date && c.PaymentDate.Date <= end.Date).Sum(c => c.Amount)??0 +
                                       (decimal?)db.TailoringStaffAdvancePayments.Where(c => c.PaymentDate.Date >= start.Date && c.PaymentDate.Date <= end.Date).Sum(c => c.Amount)??0,

                TotalCashPayments = (decimal?) db.CashPayments.Where(c => c.PaymentDate.Date >= start.Date && c.PaymentDate.Date <= end.Date && c.Mode.Transcation != "Home Expenses").Sum(c => c.Amount)??0,
                TotalHomeExpenses = (decimal?) db.CashPayments.Where(c => c.PaymentDate.Date >= start.Date && c.PaymentDate.Date <= end.Date && c.Mode.Transcation == "Home Expenses").Sum(c => c.Amount)??0,

                //Dues

                TotalDues = (decimal?) db.DuesLists.Include(c => c.DailySale).Where(c => c.DailySale.SaleDate.Date >= start.Date && c.DailySale.SaleDate.Date <= end.Date).Sum(c => c.Amount)??0,
                TotalRecovery = (decimal?) db.Recoverds.Where(c => c.PaidDate.Date >= start.Date && c.PaidDate.Date <= end.Date).Sum(c => c.AmountPaid)??0

            };

            ierData.TotalPendingDues = ierData.TotalDues - ierData.TotalRecovery;


            return ierData;


        }
        public IncomeExpensesReport GetMonthlyReport(AprajitaRetailsContext db, DateTime onDate)
        {
            IncomeExpensesReport ierData = new IncomeExpensesReport
            {
                OnDate = onDate,
                IncomeExpensesReportId = 1,

                //Income
                TotalSale = (decimal?) db.DailySales.Where(c => c.SaleDate.Month == onDate.Month && !c.IsManualBill && !c.IsTailoringBill).Sum(c => c.Amount)??0,
                TotalManualSale = (decimal?) db.DailySales.Where(c => c.SaleDate.Month == onDate.Month && c.IsManualBill && !c.IsTailoringBill).Sum(c => c.Amount)??0,
                TotalTailoringSale = (decimal?) db.DailySales.Where(c => c.SaleDate.Month == onDate.Month && c.IsTailoringBill).Sum(c => c.Amount)??0,
                TotalCashRecipts = (decimal?) db.CashReceipts.Where(c => c.InwardDate.Month == onDate.Month).Sum(c => c.Amount)??0,
                TotalRecipts = (decimal?) db.Receipts.Where(c => c.RecieptDate.Month == onDate.Month).Sum(c => c.Amount)??0,
                TotalOtherIncome = 0,

                //Expenses

                TotalExpenses = (decimal?) db.Expenses.Where(c => c.ExpDate.Month == onDate.Month).Sum(c => c.Amount)??0 + (decimal?) db.CashExpenses.Where(c => c.ExpDate.Month == onDate.Month).Sum(c => c.Amount)??0,
                TotalOthersExpenses = 0,
                TotalPayments = (decimal?) db.Payments.Where(c => c.PayDate.Month == onDate.Month).Sum(c => c.Amount)??0,
                TotalStaffPayments = (decimal?) db.Salaries.Where(c => c.PaymentDate.Month == onDate.Month).Sum(c => c.Amount)??0 + (decimal?) db.StaffAdvancePayments.Where(c => c.PaymentDate.Month == onDate.Month).Sum(c => c.Amount)??0,
                TotalTailoringPayments = (decimal?) db.TailoringSalaries.Where(c => c.PaymentDate.Month == onDate.Month).Sum(c => c.Amount)??0 + (decimal?) db.TailoringStaffAdvancePayments.Where(c => c.PaymentDate.Month == onDate.Month).Sum(c => c.Amount)??0,
                TotalCashPayments = (decimal?) db.CashPayments.Where(c => c.PaymentDate.Month == onDate.Month && c.Mode.Transcation != "Home Expenses").Sum(c => c.Amount)??0,
                TotalHomeExpenses = (decimal?) db.CashPayments.Where(c => c.PaymentDate.Month == onDate.Month && c.Mode.Transcation == "Home Expenses").Sum(c => c.Amount)??0,

                //Dues

                TotalDues = (decimal?) db.DuesLists.Include(c => c.DailySale).Where(c => c.DailySale.SaleDate.Month == onDate.Month).Sum(c => c.Amount)??0,
                TotalRecovery = (decimal?) db.Recoverds.Where(c => c.PaidDate.Month == onDate.Month).Sum(c => c.AmountPaid)??0

            };

            ierData.TotalPendingDues = ierData.TotalDues - ierData.TotalRecovery;


            return ierData;
        }
        public IncomeExpensesReport GetYearlyReport(AprajitaRetailsContext db, DateTime onDate)
        {
            IncomeExpensesReport ierData = new IncomeExpensesReport
            {
                OnDate = onDate,
                IncomeExpensesReportId = 1,

                //Income
                TotalSale = (decimal?) db.DailySales.Where(c => c.SaleDate.Year == onDate.Year && !c.IsManualBill && !c.IsTailoringBill).Sum(c => c.Amount)??0,
                TotalManualSale = (decimal?) db.DailySales.Where(c => c.SaleDate.Year == onDate.Year && c.IsManualBill && !c.IsTailoringBill).Sum(c => c.Amount)??0,
                TotalTailoringSale = (decimal?) db.DailySales.Where(c => c.SaleDate.Year == onDate.Year && c.IsTailoringBill).Sum(c => c.Amount)??0,
                TotalCashRecipts = (decimal?) db.CashReceipts.Where(c => c.InwardDate.Year == onDate.Year).Sum(c => c.Amount)??0,
                TotalRecipts = (decimal?) db.Receipts.Where(c => c.RecieptDate.Year == onDate.Year).Sum(c => c.Amount)??0,
                TotalOtherIncome = 0,

                //Expenses

                TotalExpenses = (decimal?) db.Expenses.Where(c => c.ExpDate.Year == onDate.Year).Sum(c => c.Amount)??0 + (decimal?) db.CashExpenses.Where(c => c.ExpDate.Year == onDate.Year).Sum(c => c.Amount)??0,
                TotalOthersExpenses = 0,
                TotalPayments = (decimal?) db.Payments.Where(c => c.PayDate.Year == onDate.Year).Sum(c => c.Amount)??0,
                TotalStaffPayments = (decimal?) db.Salaries.Where(c => c.PaymentDate.Year == onDate.Year).Sum(c => c.Amount)??0 + (decimal?) db.StaffAdvancePayments.Where(c => c.PaymentDate.Year == onDate.Year).Sum(c => c.Amount)??0,
                TotalTailoringPayments = (decimal?) db.TailoringSalaries.Where(c => c.PaymentDate.Year == onDate.Year).Sum(c => c.Amount)??0 + (decimal?) db.TailoringStaffAdvancePayments.Where(c => c.PaymentDate.Year == onDate.Year).Sum(c => c.Amount)??0,
                TotalCashPayments = (decimal?) db.CashPayments.Where(c => c.PaymentDate.Year == onDate.Year && c.Mode.Transcation != "Home Expenses").Sum(c => c.Amount)??0,
                TotalHomeExpenses = (decimal?) db.CashPayments.Where(c => c.PaymentDate.Year == onDate.Year && c.Mode.Transcation == "Home Expenses").Sum(c => c.Amount)??0,

                //Dues

                TotalDues = (decimal?) db.DuesLists.Include(c => c.DailySale).Where(c => c.DailySale.SaleDate.Year == onDate.Year).Sum(c => c.Amount)??0,
                TotalRecovery = (decimal?) db.Recoverds.Where(c => c.PaidDate.Year == onDate.Year).Sum(c => c.AmountPaid)??0

            };

            ierData.TotalPendingDues = ierData.TotalDues - ierData.TotalRecovery;


            return ierData;
        }
        public IncomeExpensesReport GetDailyReport(AprajitaRetailsContext db, DateTime onDate)
        {

            IncomeExpensesReport ierData = new IncomeExpensesReport
            {
                OnDate = onDate,
                IncomeExpensesReportId = 1,

                //Income
                TotalSale = (decimal?) db.DailySales.Where(c => DbFunctions.TruncateTime(c.SaleDate) == DbFunctions.TruncateTime(onDate) && !c.IsManualBill && !c.IsTailoringBill).Sum(c => c.Amount)??0,
                
                TotalManualSale = (decimal?) db.DailySales.Where(c => DbFunctions.TruncateTime(c.SaleDate) ==  DbFunctions.TruncateTime(onDate) && c.IsManualBill && !c.IsTailoringBill).Sum(c => c.Amount)??0,
                TotalTailoringSale = (decimal?) db.DailySales.Where(c => DbFunctions.TruncateTime(c.SaleDate) ==  DbFunctions.TruncateTime(onDate) && c.IsTailoringBill).Sum(c => c.Amount)??0,
                TotalCashRecipts = (decimal?) db.CashReceipts.Where(c => DbFunctions.TruncateTime(c.InwardDate) ==  DbFunctions.TruncateTime(onDate)).Sum(c => c.Amount)??0,
                TotalRecipts = (decimal?) db.Receipts.Where(c => DbFunctions.TruncateTime(c.RecieptDate) ==  DbFunctions.TruncateTime(onDate)).Sum(c => c.Amount)??0,
                TotalOtherIncome = 0,

                //Expenses

                TotalExpenses = (decimal?) db.Expenses.Where(c => DbFunctions.TruncateTime(c.ExpDate) ==  DbFunctions.TruncateTime(onDate)).Sum(c => c.Amount)??0 + (decimal?) db.CashExpenses.Where(c => DbFunctions.TruncateTime(c.ExpDate) ==  DbFunctions.TruncateTime(onDate)).Sum(c => c.Amount)??0,
                TotalOthersExpenses = 0,
                TotalPayments = (decimal?) db.Payments.Where(c => DbFunctions.TruncateTime(c.PayDate) ==  DbFunctions.TruncateTime(onDate)).Sum(c => c.Amount)??0,
                TotalStaffPayments = (decimal?) db.Salaries.Where(c => DbFunctions.TruncateTime(c.PaymentDate) ==  DbFunctions.TruncateTime(onDate)).Sum(c => c.Amount)??0 + (decimal?) db.StaffAdvancePayments.Where(c => DbFunctions.TruncateTime(c.PaymentDate) ==  DbFunctions.TruncateTime(onDate)).Sum(c => c.Amount)??0,
                TotalTailoringPayments = (decimal?) db.TailoringSalaries.Where(c => DbFunctions.TruncateTime(c.PaymentDate) ==  DbFunctions.TruncateTime(onDate)).Sum(c => c.Amount)??0 + (decimal?) db.TailoringStaffAdvancePayments.Where(c => DbFunctions.TruncateTime(c.PaymentDate) ==  DbFunctions.TruncateTime(onDate)).Sum(c => c.Amount)??0,
                TotalCashPayments = (decimal?) db.CashPayments.Where(c => DbFunctions.TruncateTime(c.PaymentDate) ==  DbFunctions.TruncateTime(onDate) && c.Mode.Transcation != "Home Expenses").Sum(c => c.Amount)??0,
                TotalHomeExpenses = (decimal?) db.CashPayments.Where(c => DbFunctions.TruncateTime(c.PaymentDate) ==  DbFunctions.TruncateTime(onDate) && c.Mode.Transcation == "Home Expenses").Sum(c => c.Amount)??0,

                //Dues

                TotalDues = (decimal?) db.DuesLists.Include(c => c.DailySale).Where(c => DbFunctions.TruncateTime(c.DailySale.SaleDate) == DbFunctions.TruncateTime(onDate)).Sum(c => c.Amount)??0,
                TotalRecovery = (decimal?) db.Recoverds.Where(c => DbFunctions.TruncateTime(c.PaidDate) == DbFunctions.TruncateTime(onDate)).Sum(c => c.AmountPaid)??0

            };

            ierData.TotalPendingDues = ierData.TotalDues - ierData.TotalRecovery;



            return ierData;

        }
    }
}