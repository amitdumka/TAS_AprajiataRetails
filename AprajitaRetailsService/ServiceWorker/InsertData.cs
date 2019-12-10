using AprajitaRetailsService.Models.Data;
using AprajitaRetailsService.Models.DataBase;
using AprajitaRetailsService.Models.Helpers;
using System.Collections.Generic;
//using AprajitaRetailsDB.DataBase.Voyager;
//using System.Collections.Generic;
using System.Data;
//using System.Linq;


//using AprajitaRetailMonitor.SeviceWorker.Common;
//using AprajitaRetailsDB.DataBase.Voyager;
//using AprajitaRetailsDB.DataTypes;
//using System;
//using System.Data;
using System.Linq;

//using AprajitaRetails.Utils;
//using CyberN.Utility;
//using System.ServiceProcess;

namespace AprajitaRetailsService.Models.Views
{
    public class InsertData
    {
        #region EF6

        /// <summary>
        /// using EF 6
        /// </summary>
        /// <param name="voygerBill"></param>
        public static void InsertBillData(VoygerBill voygerBill)
        {
            LogEvent.WriteEvent("insert bill data _with_EF6");
            if (voygerBill != null)
            {
                LogEvent.WriteEvent(" voy bil is not null");
                if (voygerBill.bill.BillType != null)
                {
                    LogEvent.WriteEvent("bill tye: " + voygerBill.bill.BillType);
                }
            }
            VoyBill bill = voygerBill.bill;

            List<LineItem> lineItemList = voygerBill.lineItems;
            List<VPaymentMode> paymentList = voygerBill.payModes;

            VoyagerContext voyDatabase;

            using (voyDatabase = new VoyagerContext())
            {
                LogEvent.WriteEvent("Voyager DB is connected");
                var v = from vyb in voyDatabase.VoyBills
                        where vyb.BillNumber == bill.BillNumber && vyb.BillTime == bill.BillTime
                        select new { vyb.VoyBillId };

                if (v.Count() > 0)
                {
                    LogEvent.WriteEvent("Invoice all ready Present in file");
                    return;
                }

                voyDatabase.VoyBills.Add(bill);
                if (voyDatabase.SaveChanges() > 0)
                {
                    LogEvent.WriteEvent("Bill is saved!!!");
                }
                else
                {
                    LogEvent.WriteEvent("bill is not Saved");
                }
                foreach (LineItem item in lineItemList)
                {
                    item.VoyBillId = bill.VoyBillId;
                    voyDatabase.LineItems.Add(item);//.InsertOnSubmit( item );
                }
                LogEvent.WriteEvent("Inserted LinesItem");
                foreach (VPaymentMode item in paymentList)
                {
                    item.VoyBillId = bill.VoyBillId;
                    voyDatabase.VPaymentModes.Add(item);
                }
                LogEvent.WriteEvent("Inserted payments");
                InsertDataLog dataLog = new InsertDataLog()
                {
                    BillNumber = bill.BillNumber,
                    Remark = "First Step",
                    VoyBillId = bill.VoyBillId,
                };
                LogEvent.WriteEvent("Inserted Datalog");
                voyDatabase.InsertDataLogs.Add(dataLog);

                voyDatabase.SaveChanges();
                LogEvent.WriteEvent("VoyBill is added with BillId: " + bill.VoyBillId + "and BillNo: " + bill.BillNumber);
            }
        }

        #endregion EF6
    }

    

      









}





//public class VoygerXMLToLinqDB
//{
//    private readonly DataTable vbTable;
//    //private static readonly Clients client = CurrentClient.LoggedClient;

//    public VoygerXMLToLinqDB( )
//    {
//        vbTable=new DataTable( "VoyBill" );
//    }
//}

