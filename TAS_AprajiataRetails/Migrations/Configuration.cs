namespace TAS_AprajiataRetails.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TAS_AprajiataRetails.Models.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<TAS_AprajiataRetails.Models.Data.AprajitaRetailsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

        }

        protected override void Seed(TAS_AprajiataRetails.Models.Data.AprajitaRetailsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //new Salesman() {SalesmanName="Manager" };
            context.Salesmen.AddOrUpdate(new Salesman() { SalesmanName = "Sanjeev Mishra" });
            context.Salesmen.AddOrUpdate(new Salesman() { SalesmanName = "Mukesh Mandal" });
            context.Salesmen.AddOrUpdate(new Salesman() { SalesmanName = "Manager" });

            //context.SaveChanges();
        }
    }

    public class AprajitaRetailsInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<AprajitaRetailsContext>
    {
        protected override void Seed(AprajitaRetailsContext context)
        {
            context.Salesmen.Add(new Salesman() { SalesmanName = "Sanjeev Mishra" });
            context.Salesmen.Add(new Salesman() { SalesmanName = "Mukesh Mandal" });
            context.Salesmen.Add(new Salesman() { SalesmanName = "Manager" });

            context.SaveChanges();
        }
    }
}