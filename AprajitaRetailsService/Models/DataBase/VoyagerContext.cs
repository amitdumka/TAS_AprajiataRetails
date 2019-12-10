using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AprajitaRetailsService.Models.Data;

namespace AprajitaRetailsService.Models.DataBase
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
}
