namespace TAS_AprajiataRetails.Migrations
{
    using System;
    using System.Collections.Generic;
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
            context.Salesmen.AddOrUpdate(new Salesman() { SalesmanId = 1, SalesmanName = "Sanjeev Mishra" });
            context.Salesmen.AddOrUpdate(new Salesman() { SalesmanId = 2, SalesmanName = "Mukesh Mandal" });
            context.Salesmen.AddOrUpdate(new Salesman() { SalesmanId = 3, SalesmanName = "Manager" });
            var modes = new List<TranscationMode>() {
                new TranscationMode(){Transcation="Home Expenses",TranscationModeId=1 },
                new TranscationMode(){Transcation="Ohter Home Expenses" ,TranscationModeId=2},
                new TranscationMode(){Transcation="Mukesh(HomeStaff)" ,TranscationModeId=3},
                new TranscationMode(){Transcation="Amit Kumar" ,TranscationModeId=4},
                new TranscationMode(){Transcation="Amit Kumar Expenses" ,TranscationModeId=5},
                new TranscationMode(){Transcation="CashIn" ,TranscationModeId=6},
                new TranscationMode(){Transcation="CashOut" ,TranscationModeId=7},
                new TranscationMode(){Transcation="Regular" ,TranscationModeId=8},
            };
            modes.ForEach(s => context.TranscationModes.AddOrUpdate(s));

            var banks = new List<Bank>() {
                new Bank(){BankId= 1, BankName="SBI"},
                new Bank(){BankId= 2, BankName="ICICI"},
                new Bank(){BankId= 3, BankName="Bandhan Bank"},
                new Bank(){BankId=4, BankName="PNB"},
                new Bank(){BankId= 5, BankName="BOB"},
                new Bank(){ BankId=6, BankName="Axis"},
                new Bank(){BankId= 7, BankName="HDFC"},
            };
            banks.ForEach(s => context.Banks.AddOrUpdate(s));
            context.SaveChanges();
        }
    }

    //public class AprajitaRetailsInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<AprajitaRetailsContext>
    //{
    //    protected override void Seed(AprajitaRetailsContext context)
    //    {
    //        context.Salesmen.AddOrUpdate(new Salesman() { SalesmanName = "Sanjeev Mishra" });
    //        context.Salesmen.AddOrUpdate(new Salesman() { SalesmanName = "Mukesh Mandal" });
    //        context.Salesmen.AddOrUpdate(new Salesman() { SalesmanName = "Manager" });
    //        var modes = new List<TranscationMode>() {
    //            new TranscationMode(){Transcation="Home Expenses"},
    //            new TranscationMode(){Transcation="Ohter Home Expenses" },
    //            new TranscationMode(){Transcation="Mukesh(HomeStaff)" },
    //            new TranscationMode(){Transcation="Amit Kumar" },
    //            new TranscationMode(){Transcation="Amit Kumar Expenses" },
    //            new TranscationMode(){Transcation="CashIn" },
    //            new TranscationMode(){Transcation="CashOut" },
    //        };
    //        modes.ForEach(s => context.TranscationModes.AddOrUpdate(s));

    //        var banks = new List<Bank>() {
    //            new Bank(){ BankName="SBI"},
    //            new Bank(){  BankName="ICICI"},
    //            new Bank(){  BankName="Bandhan Bank"},
    //            new Bank(){  BankName="PNB"},
    //            new Bank(){  BankName="BOB"},
    //            new Bank(){  BankName="Axis"},
    //            new Bank(){  BankName="HDFC"},
    //        };
    //        banks.ForEach(s => context.Banks.AddOrUpdate(s));

    //        context.SaveChanges();
    //    }
    //}
}