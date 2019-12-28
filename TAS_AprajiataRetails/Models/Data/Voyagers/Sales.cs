using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAS_AprajiataRetails.Models.Data.Voyagers
{

    public class SaleInvoice
    {
        public int SaleInvoiceID { get; set; }

        public int CustomerId { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OnDate { get; set; }

        public string InvoiceNo { get; set; }

        public int TotalItems { get; set; }

        public decimal TotalQty { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalBillAmount { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalDiscountAmount { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal RoundOffAmount { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalTaxAmount { get; set; }

        public virtual SalePaymentDetail PaymentDetail { get; set; }

        public virtual ICollection<SaleItem> SaleItems { get; set; }
    }

    public class Salesman
    {
        public int SalesmanId { set; get; }
        public string SMCode { get; set; }
        public string SalesmanName { get; set; }
        public virtual ICollection<SaleItem> SaleItems { get; set; }
    }


    public class SaleItem
    {
        public int SaleItemID { get; set; }

        public int SaleInvoiceID { get; set; }

        public string BarCode { get; set; }
        public decimal Qty { get; set; }
        public Units Units { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MRP { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal BasicAmount { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Discount { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TaxAmount { get; set; }

        public int SaleTaxTypeId { get; set; }
        public virtual SaleTaxType SaleTaxType { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal BillAmount { get; set; }
        // SaleTax options and it will Done

        public int SalesmanID { get; set; }

        public virtual SaleInvoice SaleInvoice { get; set; }
        public virtual Salesman Salesman { get; set; }
    }

    public class SaleTaxType
    {
        public int SaleTaxTypeId { get; set; }
        public string TaxName { get; set; }
        public TaxType TaxType { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CompositeRate { get; set; }

        //Navigation
        public ICollection<SaleItem> SaleItems { get; set; }
    }

    public class SalePaymentDetail
    {
        [ForeignKey("SaleInvoice")]
        public int SalePaymentDetailId { get; set; }
              
        //public int SaleInvoiceId { get; set; }
        
        public SalePayMode PayMode { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashAmount { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CardAmount { get; set; }

        public virtual CardPaymentDetail CardDetails { get; set; }
       
        public virtual SaleInvoice SaleInvoice { get; set; }

    }



    public class CardPaymentDetail
    {
        [ForeignKey("SalePaymentDetail")]
        public int CardPaymentDetailId { get; set; }


        public int SaleInvoiceId { get; set; } // FK of SalesInvoice

        public int CardType { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public int AuthCode { get; set; }
        public int LastDigit { get; set; }

        public virtual SalePaymentDetail SalePaymentDetail { get; set; }
    }




}