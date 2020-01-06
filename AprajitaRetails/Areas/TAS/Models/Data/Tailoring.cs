using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.TAS.Models.Data
{
    //Tailoring
    public class TailoringEmployee
    {
        public int TailoringEmployeeId { get; set; }

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

        public ICollection<TailorAttendance> Attendances { get; set; }
        public ICollection<TailoringSalaryPayment> SalaryPayments { get; set; }
        public ICollection<TailoringStaffAdvancePayment> AdvancePayments { get; set; }
        public ICollection<TailoringStaffAdvanceReceipt> AdvanceReceipts { get; set; }
    }

    public class TailorAttendance
    {
        public int TailorAttendanceId { get; set; }

        [Display(Name = "Staff Name")]
        public int TailoringEmployeeId { get; set; }

        public TailoringEmployee Employee { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Attendance Date")]
        public DateTime AttDate { get; set; }

        [Display(Name = "Entry Time")]
        public string EntryTime { get; set; }

        public AttUnits Status { get; set; }
        public string Remarks { get; set; }
    }

    public class TailoringSalaryPayment
    {
        public int TailoringSalaryPaymentId { get; set; }

        [Display(Name = "Tailor Name")]
        public int TailoringEmployeeId { get; set; }

        public TailoringEmployee Employee { get; set; }

        [Display(Name = "Salary/Year")]
        public string SalaryMonth { get; set; }

        [Display(Name = "Payment Reason")]
        public SalaryComponet SalaryComponet { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PayModes PayMode { get; set; }

        public string Details { get; set; }
    }

    public class TailoringStaffAdvancePayment
    {
        public int TailoringStaffAdvancePaymentId { get; set; }

        [Display(Name = "Tailor Name")]
        public int TailoringEmployeeId { get; set; }

        public TailoringEmployee Employee { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PayModes PayMode { get; set; }

        public string Details { get; set; }
    }

    public class TailoringStaffAdvanceReceipt
    {
        public int TailoringStaffAdvanceReceiptId { get; set; }

        [Display(Name = "Staff Name")]
        public int TailoringEmployeeId { get; set; }

        public TailoringEmployee Employee { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Receipt Date")]
        public DateTime ReceiptDate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PayModes PayMode { get; set; }

        public string Details { get; set; }
    }

    public class TalioringDelivery
    {
        public int TalioringDeliveryId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }

        public int TalioringBookingId { get; set; }
        public TalioringBooking Booking { get; set; }

        [Display(Name = "Voy Inv No")]
        public string InvNo { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public string Remarks { get; set; }
    }

    public class TalioringBooking
    {
        public int TalioringBookingId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }

        [Display(Name = "Customer Name")]
        public string CustName { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Trail Date")]
        public DateTime TryDate { get; set; }

        [Display(Name = "Booking Slip")]
        public string BookingSlipNo { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Total Qty")]
        public int TotalQty { get; set; }

        [Display(Name = "Shirt Qty")]
        public int ShirtQty { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Shirt Price")]
        public decimal ShirtPrice { get; set; }

        [Display(Name = "Pant Qty")]
        public int PantQty { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Pant Price")]
        public decimal PantPrice { get; set; }

        [Display(Name = "Coat Qty")]
        public int CoatQty { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Coat Price")]
        public decimal CoatPrice { get; set; }

        [Display(Name = "Kurta Qty")]
        public int KurtaQty { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Kurta Price")]
        public decimal KurtaPrice { get; set; }

        [Display(Name = "Bundi Qty")]
        public int BundiQty { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Bundi Price")]
        public decimal BundiPrice { get; set; }

        [Display(Name = "Others Qty")]
        public int Others { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Others Price")]
        public decimal OthersPrice { get; set; }

        [DefaultValue(false)]
        public bool IsDelivered { get; set; }

        public virtual ICollection<TalioringDelivery> Deliveries { get; set; }
    }
}