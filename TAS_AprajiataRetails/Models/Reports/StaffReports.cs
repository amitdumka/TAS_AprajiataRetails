using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TAS_AprajiataRetails.Models.Data;

namespace TAS_AprajiataRetails.Models.Reports
{
    public class StaffPayments
    {
        public int EmpId { get; set; }
        public decimal SalaryPayment { get; set; }
        public decimal AdavcePayment { get; set; }
        public decimal AdvaceReciepts { get; set; }
        public decimal CurrentMonthSalary { get; set; }
        public decimal NetSalary { get; set; }
    }

    public class StaffReports
    {
        public StaffPayments StaffPayments(int EmpId, DateTime onDate, decimal curSalary)
        {
            using (AprajitaRetailsContext db= new AprajitaRetailsContext())
            {
                StaffPayments sPay = new StaffPayments();
                sPay.AdavcePayment = db.StaffAdvancePayments.Where(c =>c.EmployeeId==EmpId && DbFunctions.TruncateTime(c.PaymentDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);
                sPay.AdvaceReciepts=db.StaffAdvanceReceipts.Where(c => c.EmployeeId == EmpId && DbFunctions.TruncateTime(c.ReceiptDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);
                sPay.SalaryPayment=db.Salaries.Where(c => c.EmployeeId == EmpId && DbFunctions.TruncateTime(c.PaymentDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);
                sPay.CurrentMonthSalary = curSalary;
                sPay.NetSalary = curSalary - (sPay.SalaryPayment + sPay.AdavcePayment - sPay.AdvaceReciepts);

                return sPay;
            }

        }

        public List<StaffPayments> AllStaffPayments(DateTime onDate)
        {
            using (AprajitaRetailsContext db = new AprajitaRetailsContext())
            {
                var emp = db.Employees.Where(c => c.IsWorking).Select(c => c.EmployeeId);
                List<StaffPayments> lists = new List<StaffPayments>();
                foreach (var EmpId in emp)
                {
                    StaffPayments sPay = new StaffPayments();
                    sPay.AdavcePayment = db.StaffAdvancePayments.Where(c => c.EmployeeId == EmpId && DbFunctions.TruncateTime(c.PaymentDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);
                    sPay.AdvaceReciepts = db.StaffAdvanceReceipts.Where(c => c.EmployeeId == EmpId && DbFunctions.TruncateTime(c.ReceiptDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);
                    sPay.SalaryPayment = db.Salaries.Where(c => c.EmployeeId == EmpId && DbFunctions.TruncateTime(c.PaymentDate).Value.Month == DbFunctions.TruncateTime(onDate).Value.Month).Sum(c => c.Amount);
                    sPay.CurrentMonthSalary = 0;//TODO: aad option here
                    sPay.NetSalary = sPay.CurrentMonthSalary - (sPay.SalaryPayment + sPay.AdavcePayment - sPay.AdvaceReciepts);

                    lists.Add(sPay);
                }
                

                return lists;
            }

        }

        public void CalculateSalary()
        {
            // use current salary and payslip
        }
    }
}