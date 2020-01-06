namespace AprajitaRetails.Areas.TAS.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AprajitaRetails.Areas.TAS.Models.Data.AprajitaRetailsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AprajitaRetails.Areas.TAS.Models.Data.AprajitaRetailsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }


    //internal sealed class Configuration1 : DbMigrationsConfiguration<AprajitaRetails.Areas.TAS.Models.ApplicationDbContext>
    //{
    //    public Configuration1()
    //    {
    //        AutomaticMigrationsEnabled = true;
    //    }

    //    protected override void Seed(AprajitaRetails.Areas.TAS.Models.ApplicationDbContext context)
    //    {
    //        //  This method will be called after migrating to the latest version.

    //        //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
    //        //  to avoid creating duplicate seed data.
    //    }
    //}
}
