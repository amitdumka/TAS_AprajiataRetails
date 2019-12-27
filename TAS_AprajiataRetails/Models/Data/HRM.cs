using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TAS_AprajiataRetails.Models.Data
{
    public class HRM
    {
    }

    //Payroll
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Display(Name = "Staff Name")]
        public string StaffName { get; set; }

        [Display(Name = "Mobile No"), Phone]
        public string MobileNo { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Joining Date")]
        public DateTime JoiningDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Leaving Date")]
        public DateTime? LeavingDate { get; set; }

        [Display(Name = "Is Working")]
        public bool IsWorking { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<SalaryPayment> SalaryPayments { get; set; }
        public ICollection<StaffAdvancePayment> AdvancePayments { get; set; }
        public ICollection<StaffAdvanceReceipt> AdvanceReceipts { get; set; }
        public ICollection<PettyCashExpense> CashExpenses { get; set; }
        public ICollection<Expense> Expenses { get; set; }

    }

    public class Attendance
    {
        public int AttendanceId { get; set; }

        [Display(Name = "Staff Name")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Attendance Date")]
        public DateTime AttDate { get; set; }

        [Display(Name = "Entry Time")]
        public string EntryTime { get; set; }

        public AttUnits Status { get; set; }
        public string Remarks { get; set; }

    }

    public class CurrentSalary
    {
        //TODO: Think some thing others also 
        public int CurrentSalaryId { get; set; }
        public int EmployeeId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal SundaySalary { get; set; }
        public decimal LPRate { get; set; }
        public decimal IncentiveRate { get; set; }
        public decimal IncentiveTarget { get; set; }
        public decimal WOWBillRate { get; set; }
        public decimal WOWBillTarget { get; set; }
        public int NoOfWorkingDays { get; set; }
        public bool IsSundayBillable { get; set; }

        public DateTime EffectiveDate { get; set; }
        public DateTime CloseDate { get; set; }
        public bool IsEffective { get; set; }
        public virtual Employee Employee { get; set; }
    }

    public class PaySlip
    {
        public int PaySlipId { get; set; }
        public DateTime OnDate { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public int EmployeeId { get; set; }
        public decimal BasicSalary { get; set; }
        public int NoOfDaysPresent { get; set; }
        public decimal TotalSale { get; set; }
        public decimal SaleIncentive { get; set; }
        public decimal WOWBillAmount { get; set; }
        public decimal WOWBillIncentive { get; set; }
        public decimal LastPcsAmount { get; set; }
        public decimal LastPCsIncentive { get; set; }
        public decimal OthersIncentive { get; set; }

        public decimal GrossSalary { get; set; }
        
        public decimal StandardDeductions { get; set; }
        public decimal TDSDeductions { get; set; }
        public decimal PFDeductions { get; set; }
        public decimal AdvanceDeducations { get; set; }
        public decimal OtherDeductions { get; set; }

        public string Remarks { get; set; }

        public virtual Employee Employee { get; set; }


    }

}