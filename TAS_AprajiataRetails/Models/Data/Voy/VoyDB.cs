using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TAS_AprajiataRetails.Models.Data.Voy
{
    public class VoyDB:DbContext
    {
        public VoyDB() : base("VoyImportDB")
        {
            Database.SetInitializer<VoyDB>(new CreateDatabaseIfNotExists<VoyDB>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<VoyDB, Migrations.VoyConfiguration>());
        }

        public DbSet<VoyagerBill> Bills { get; set; }

    }

    public enum PayModes { CA, DC, CC, Mix,Wal, CRD, OTH}

    public class VoyagerBill
    {
        public int VoyagerBillId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime BillDate { get; set; }
        public decimal Amount { get; set; }
        public PayModes PayModes { get; set; }
        public DateTime ImportDate { get; set; }
        public bool IsUsed { get; set; }
    }
}