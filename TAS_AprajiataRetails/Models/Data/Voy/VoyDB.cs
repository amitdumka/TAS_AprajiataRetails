using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TAS_AprajiataRetails.Models.Data.Voy
{
    public class VoyDB : DbContext
    {
        public VoyDB() : base("VoyImportDB")
        {
            Database.SetInitializer<VoyDB>(new CreateDatabaseIfNotExists<VoyDB>());
           // Database.SetInitializer(new MigrateDatabaseToLatestVersion<VoyDB, Migrations.VoyConfiguration>());
        }

        public DbSet<VoyagerBill> Bills { get; set; }

    }

   

    public class VoyagerBill
    {
        public int VoyagerBillId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime BillDate { get; set; }
        public decimal Amount { get; set; }
        public VPayModes PayModes { get; set; }
        public DateTime ImportDate { get; set; }
        public bool IsUsed { get; set; }
    }

    public class LineItem
    {
        public int LineItemId { get; set; }

        public int VoyBillId { get; set; }

        public string LineType { get; set; }

        public int Serial { get; set; }

        public string ItemCode { get; set; }

        public double Qty { get; set; }

        public decimal Rate { get; set; }

        public decimal Value { get; set; }

        public decimal DiscountValue { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }
    }
    public class SaleBill {
        private int _ID;

        private string _BillType;

        private string _BillNumber;

        private System.Nullable<System.DateTime> _BillTime;

        private double _BillAmount;

        private double _BillGrossAmount;

        private double _BillDiscount;

        private string _CustomerName;

        private string _CustomerMobile;

        private string _StoreID;
    }
    public class PaymentDetails {
        private int _ID;

        private int _VoyBillId;

        private string _PaymentMode;

        private string _PaymentValue;

        private string _Notes;
    }







}