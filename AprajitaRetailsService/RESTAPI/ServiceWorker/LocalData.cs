using AprajitaRetailsService.RESTAPI.Data;
using AprajitaRetailsService.RESTAPI.Utils;
using AprajitaRetailsService.RESTAPI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprajitaRetailsService.RESTAPI.ServiceWorker
{
    class LocalData
    {
        public  void InsertBillData(VoygerBill voygerBill)
        {
            if (voygerBill != null)
            {
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
                
                // TODO:Need to Check line below here
                var v = from vyb in voyDatabase.VoyBills
                        where vyb.BillNumber == bill.BillNumber && vyb.BillTime == bill.BillTime
                        select new { vyb.VoyBillId };

                if (v.Count() > 0)
                {
                    LogEvent.WriteEvent($"Invoice all ready Present in file {v.FirstOrDefault().VoyBillId},{bill.BillNumber}");
                    return;
                }
                else { LogEvent.WriteEvent($"Invoice is new with bill no {bill.BillNumber}"); }

                voyDatabase.VoyBills.Add(bill);
                if (voyDatabase.SaveChanges() > 0)
                {
                    LogEvent.WriteEvent($"Bill with ID: {bill.VoyBillId} is saved!!! ");
                }
                else
                {
                    LogEvent.WriteEvent("bill is not Saved");
                }
                foreach (LineItem item in lineItemList)
                {
                    item.VoyBillId = bill.VoyBillId;
                    voyDatabase.LineItems.Add(item);
                }
             //   LogEvent.WriteEvent("Inserted LinesItem");
                foreach (VPaymentMode item in paymentList)
                {
                    item.VoyBillId = bill.VoyBillId;
                    voyDatabase.VPaymentModes.Add(item);
                }
              //  LogEvent.WriteEvent("Inserted payments");
                InsertDataLog dataLog = new InsertDataLog()
                {
                    BillNumber = bill.BillNumber,
                    Remark = "First Step",
                    VoyBillId = bill.VoyBillId,
                };
                LogEvent.WriteEvent("Inserted Datalog,payments,LinesItem,payments");
                voyDatabase.InsertDataLogs.Add(dataLog);

                voyDatabase.SaveChanges();
                LogEvent.WriteEvent("VoyBill is added with BillId: [" + bill.VoyBillId + "]and BillNo: [" + bill.BillNumber+"]");
            }
        }
    }
}
