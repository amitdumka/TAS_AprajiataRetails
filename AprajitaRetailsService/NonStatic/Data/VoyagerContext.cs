using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprajitaRetailsService.RESTAPI.Data
{
    public class VoyagerContext : DbContext
    {
        public VoyagerContext() : base("VoyagerDB")
        {
        }

        public virtual DbSet<InsertDataLog> InsertDataLogs { get; set; }
        public virtual DbSet<LineItem> LineItems { get; set; }
        public virtual DbSet<VoyBill> VoyBills { get; set; }
        public virtual DbSet<VPaymentMode> VPaymentModes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InsertDataLog>()
                .Property(e => e.CreatedDate)
                .IsFixedLength();
        }
    }

    public partial class InsertDataLog
    {
        public int InsertDataLogId { get; set; }

        [Required]
        public string BillNumber { get; set; }

        public int VoyBillId { get; set; }
       

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] CreatedDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime InsertedOn { get; set; }

        public int? SaleInvoiceId { get; set; }

        public int? DailySaleId { get; set; }

        public string Remark { get; set; }
        public virtual ICollection< VoyBill> VoyBill { get; set; }

    }


    public class VPaymentMode
    {
        public int VPaymentModeID { get; set; }

        public int VoyBillId { get; set; }

        [StringLength(100)]
        public string PaymentMode { get; set; }

        [StringLength(100)]
        public string PaymentValue { get; set; }
        [StringLength(200)]
        public string Notes { get; set; }
        public virtual VoyBill VoyBill { get; set; }
    }



    public class VoyBill
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoyBillId { get; set; }

        [Required]
        [StringLength(100)]
        public string BillType { get; set; }

        [Required]
        [StringLength(100)]
        public string BillNumber { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime? BillTime { get; set; }

        public double BillAmount { get; set; }

        public double BillGrossAmount { get; set; }

        public double BillDiscount { get; set; }

        [StringLength(100)]
        public string CustomerName { get; set; }

        [StringLength(100)]
        public string CustomerMobile { get; set; }

        [StringLength(20)]
        public string StoreID { get; set; }

        public virtual ICollection<VPaymentMode> VPaymentModes { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }
        public virtual InsertDataLog InsertDataLogs { get; set; }
    }

    public class LineItem
    {
        public int LineItemID { get; set; }

        public int VoyBillId { get; set; }
        [StringLength(100)]
        public string LineType { get; set; }

        public int Serial { get; set; }

        [StringLength(100)]
        public string ItemCode { get; set; }

        public double Qty { get; set; }

        public double Rate { get; set; }

        public double Value { get; set; }

        public double DiscountValue { get; set; }

        public double Amount { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
        public virtual VoyBill VoyBill { get; set; }
    }

}
