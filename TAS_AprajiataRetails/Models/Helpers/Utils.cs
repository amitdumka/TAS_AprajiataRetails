using System;
using System.Linq;
using System.Data.Entity;
using TAS_AprajiataRetails.Models.Data;
using System.Collections.Generic;
using NLog.Fluent;

namespace TAS_AprajiataRetails.Models.Helpers
{
    // Utils Section 
    public class Utils
    {
        public static void CashInHandCorrectionForMonth(DateTime forDate)
        {
            using (AprajitaRetailsContext db = new AprajitaRetailsContext())
            {
                //if (forDate == null) forDate = DateTime.Today;
                IEnumerable<CashInHand> cashs = db.CashInHands.Where(c => DbFunctions.TruncateTime(c.CIHDate).Value.Month == DbFunctions.TruncateTime(forDate).Value.Month).OrderBy(c => c.CIHDate);
                decimal cBal = 0;

                if (cashs != null && cashs.Count() > 0)
                {
                    cBal = cashs.First().OpenningBalance;

                    foreach (var cash in cashs)
                    {
                        cash.OpenningBalance = cBal;

                        cash.ClosingBalance = cash.OpenningBalance + cash.CashIn - cash.CashOut;
                        cBal = cash.ClosingBalance;

                        db.Entry(cash).State = EntityState.Modified;

                    }
                    try
                    {
                        Log.Info("No of CashInhand Changes :"+ db.SaveChanges());
                    }
                    catch (Exception)
                    {

                        Log.Info("CashInHand Correction failed");
                    }
                   
                }
            }

            

            
        }


        public static void ProcessOpenningClosingBalance(AprajitaRetailsContext db, DateTime date, bool isclosing = false, bool saveit = false)
        {
            if (!isclosing)
            {
                CashInHand today;
                today = db.CashInHands.Where(c => DbFunctions.TruncateTime(c.CIHDate) == DbFunctions.TruncateTime(date)).FirstOrDefault();
                //TODO:One Day Back
                DateTime yDate = date.AddDays(-1);
                CashInHand yesterday = db.CashInHands.Where(c => DbFunctions.TruncateTime(c.CIHDate) == DbFunctions.TruncateTime(yDate)).FirstOrDefault();

                if (today == null)
                {
                    today = new CashInHand() { CashIn = 0, CashOut = 0, CIHDate = date, ClosingBalance = 0, OpenningBalance = 0 };

                    if (yesterday != null)
                    {
                        yesterday.ClosingBalance = yesterday.OpenningBalance + yesterday.CashIn - yesterday.CashOut;
                        today.ClosingBalance = today.OpenningBalance = yesterday.ClosingBalance;
                        db.CashInHands.Add(today);
                        if (saveit) db.SaveChanges();
                    }
                    else
                    {
                        if (db.CashInHands.Count() > 0)
                            throw new Exception();
                        //TODO: if yesterday one or day back data not present handel this
                        else
                        {
                            today.ClosingBalance = today.OpenningBalance = 0;
                            db.CashInHands.Add(today);
                            if (saveit) db.SaveChanges();
                        }
                    }

                }
                else
                {
                    if (yesterday != null)
                    {
                        today.ClosingBalance = today.OpenningBalance = yesterday.ClosingBalance;
                        db.Entry(today).State = EntityState.Modified;
                        if (saveit) db.SaveChanges();
                    }
                    else
                    {
                        if (db.CashInHands.Count() > 1)
                            throw new Exception();
                        //TODO: if yesterday one or day back data not present handel this
                        else
                        {
                            today.ClosingBalance = today.OpenningBalance = 0;
                            db.Entry(today).State = EntityState.Modified;
                            if (saveit) db.SaveChanges();
                        }
                    }
                }


            }
            else
            {
                //ClosingBalance;
                CashInHand today;
                today = db.CashInHands.Where(c => DbFunctions.TruncateTime(c.CIHDate) == DbFunctions.TruncateTime(date)).FirstOrDefault();
                if (today != null)
                {
                    if (today.ClosingBalance != today.OpenningBalance + today.CashIn - today.CashOut)
                    {
                        today.ClosingBalance = today.OpenningBalance + today.CashIn - today.CashOut;
                        db.Entry(today).State = EntityState.Modified;
                        if (saveit) db.SaveChanges();
                    }

                }
                else
                {
                    throw new Exception();
                }
            }

        }
        public static void ProcessOpenningClosingBankBalance(AprajitaRetailsContext db, DateTime date, bool isclosing = false, bool saveit = false)
        {
            if (!isclosing)
            {
                CashInBank today;
                today = db.CashInBanks.Where(c => DbFunctions.TruncateTime(c.CIBDate) == DbFunctions.TruncateTime(date)).FirstOrDefault();
                DateTime yDate = date.AddDays(-1);
                CashInBank yesterday = db.CashInBanks.Where(c => DbFunctions.TruncateTime(c.CIBDate) == DbFunctions.TruncateTime(yDate)).FirstOrDefault();

                if (today == null)
                {
                    today = new CashInBank() { CashIn = 0, CashOut = 0, CIBDate = date, ClosingBalance = 0, OpenningBalance = 0 };

                    if (yesterday != null)
                    {
                        yesterday.ClosingBalance = yesterday.OpenningBalance + yesterday.CashIn - yesterday.CashOut;
                        today.ClosingBalance = today.OpenningBalance = yesterday.ClosingBalance;
                        db.CashInBanks.Add(today);
                        if (saveit) db.SaveChanges();
                    }
                    else
                    {
                        if (db.CashInBanks.Count() > 0)
                            throw new Exception();
                        else
                        {
                            today.ClosingBalance = today.OpenningBalance = 0;
                            db.CashInBanks.Add(today);
                            if (saveit) db.SaveChanges();
                        }
                    }

                }
                else
                {
                    if (yesterday != null)
                    {
                        today.ClosingBalance = today.OpenningBalance = yesterday.ClosingBalance;
                        db.Entry(today).State = EntityState.Modified;
                        if (saveit) db.SaveChanges();
                    }
                    else
                    {
                        if (db.CashInBanks.Count() > 1)
                            throw new Exception();
                        else
                        {
                            today.ClosingBalance = today.OpenningBalance = 0;
                            db.Entry(today).State = EntityState.Modified;
                            if (saveit) db.SaveChanges();
                        }
                    }
                }





            }
            else
            {
                //ClosingBalance;
                CashInBank today;
                today = db.CashInBanks.Where(c => DbFunctions.TruncateTime(c.CIBDate) == DbFunctions.TruncateTime(date)).FirstOrDefault();

                if (today != null)
                {
                    if (today.ClosingBalance != today.OpenningBalance + today.CashIn - today.CashOut)
                    {
                        today.ClosingBalance = today.OpenningBalance + today.CashIn - today.CashOut;
                        db.Entry(today).State = EntityState.Modified;
                        if (saveit) db.SaveChanges();
                    }

                }
                else
                {
                    //TODO: Implement this on urgent basis
                    throw new Exception();

                }
            }

        }

        public static void CreateCashInHand(AprajitaRetailsContext db, DateTime date, decimal inAmt, decimal outAmt, bool saveit = false)
        {


            //One Day Back
            DateTime yDate = date.AddDays(-1);
            CashInHand yesterday = db.CashInHands.Where(c => c.CIHDate == yDate).FirstOrDefault();
            CashInHand today = new CashInHand() { CashIn = inAmt, CashOut = outAmt, CIHDate = date, ClosingBalance = 0, OpenningBalance = 0 };

            if (yesterday != null)
            {
                yesterday.ClosingBalance = yesterday.OpenningBalance + yesterday.CashIn - yesterday.CashOut;
                today.ClosingBalance = today.OpenningBalance = yesterday.ClosingBalance;
                db.CashInHands.Add(today);
                if (saveit) db.SaveChanges();
            }
            else
            {
                if (db.CashInHands.Count() > 0)
                    throw new Exception();
                //TODO: if yesterday one or day back data not present handel this
                else
                {
                    today.ClosingBalance = today.OpenningBalance = 0;
                    db.CashInHands.Add(today);
                    if (saveit) db.SaveChanges();
                }
            }


        }
        public static void CreateCashInBank(AprajitaRetailsContext db, DateTime date, decimal inAmt, decimal outAmt, bool saveit = false)
        {


            CashInBank today;

            DateTime yDate = date.AddDays(-1);
            CashInBank yesterday = db.CashInBanks.Where(c => c.CIBDate == yDate).FirstOrDefault();


            today = new CashInBank() { CashIn = 0, CashOut = 0, CIBDate = date, ClosingBalance = 0, OpenningBalance = 0 };

            if (yesterday != null)
            {
                yesterday.ClosingBalance = yesterday.OpenningBalance + yesterday.CashIn - yesterday.CashOut;
                today.ClosingBalance = today.OpenningBalance = yesterday.ClosingBalance;
                db.CashInBanks.Add(today);
                if (saveit) db.SaveChanges();
            }
            else
            {
                if (db.CashInBanks.Count() > 0)
                    throw new Exception();
                else
                {
                    today.ClosingBalance = today.OpenningBalance = 0;
                    db.CashInBanks.Add(today);
                    if (saveit) db.SaveChanges();
                }
            }





        }


        public static void UpDateCashInHand(AprajitaRetailsContext db, DateTime dateTime, decimal Amount, bool saveit = false)
        {

            {
                CashInHand cashIn = db.CashInHands.Where(d => d.CIHDate == dateTime).FirstOrDefault();
                if (cashIn != null)
                {
                    cashIn.CashIn += Amount;
                    db.Entry(cashIn).State = EntityState.Modified;
                    if (saveit) db.SaveChanges();
                }
                else
                {
                    CreateCashInHand(db, dateTime, Amount, 0, saveit);
                    // db.CashInHands.Add(new CashInHand() { CIHDate = dateTime, CashIn = Amount, OpenningBalance = 0, ClosingBalance = 0, CashOut = 0 });
                    // if (saveit) db.SaveChanges();
                    // ProcessOpenningClosingBalance(db, dateTime, false, saveit);
                }

            }
        }
        public static void UpDateCashOutHand(AprajitaRetailsContext db, DateTime dateTime, decimal Amount, bool saveit = false)
        {

            {
                CashInHand cashIn = db.CashInHands.Where(d => d.CIHDate == dateTime).FirstOrDefault();
                if (cashIn != null)
                {
                    cashIn.CashOut += Amount;
                    db.Entry(cashIn).State = EntityState.Modified;
                    if (saveit) db.SaveChanges();
                }
                else
                {

                    //db.CashInHands.Add(new CashInHand() { CIHDate = dateTime, CashIn = 0, OpenningBalance = 0, ClosingBalance = 0, CashOut = Amount });
                    //if (saveit) db.SaveChanges();
                    CreateCashInHand(db, dateTime, 0, Amount, saveit);
                }

            }
        }
        public static void UpDateCashInBank(AprajitaRetailsContext db, DateTime dateTime, decimal Amount, bool saveit = false)
        {

            {
                CashInBank cashIn = db.CashInBanks.Where(d => d.CIBDate == dateTime).FirstOrDefault();
                if (cashIn != null)
                {
                    cashIn.CashIn += Amount;
                    db.SaveChanges();
                }
                else
                {
                    // db.CashInBanks.Add(new CashInBank() { CIBDate = dateTime, CashIn = 0, OpenningBalance = 0, ClosingBalance = 0, CashOut = Amount });
                    // if (saveit) db.SaveChanges();
                    CreateCashInBank(db, dateTime, Amount, 0, saveit);

                }

            }
        }
        public static void UpDateCashOutBank(AprajitaRetailsContext db, DateTime dateTime, decimal Amount, bool saveit = false)
        {

            {
                CashInBank cashIn = db.CashInBanks.Where(d => d.CIBDate == dateTime).FirstOrDefault();
                if (cashIn != null)
                {
                    cashIn.CashOut += Amount;
                    db.SaveChanges();
                }
                else
                {
                    //  db.CashInBanks.Add(new CashInBank() { CIBDate = dateTime, CashIn = 0, OpenningBalance = 0, ClosingBalance = 0, CashOut = Amount });
                    //if (saveit) db.SaveChanges();
                    CreateCashInBank(db, dateTime, 0, Amount, saveit);

                }

            }
        }
        public static void UpdateSuspenseAccount(AprajitaRetailsContext db, DateTime dateTime, decimal Amount, bool isOut, string referanceDetails, bool saveit = false)
        {
            //TODO: Implement This  Suspense Account
        }


        public static void JobOpeningClosingBalance()
        {
            using (AprajitaRetailsContext db = new TAS_AprajiataRetails.Models.Data.AprajitaRetailsContext())
            {
                Utils.ProcessOpenningClosingBalance(db, DateTime.Today, false, true);
                Utils.ProcessOpenningClosingBalance(db, DateTime.Today, true, true);

                Utils.ProcessOpenningClosingBankBalance(db, DateTime.Today, false, true);
                Utils.ProcessOpenningClosingBankBalance(db, DateTime.Today, true, true);
            }
        }
    }




}