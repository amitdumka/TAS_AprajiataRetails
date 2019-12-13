using AprajitaRetailsService.RESTAPI.Utils;
using AprajitaRetailsService.RESTAPI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AprajitaRetailsService.RESTAPI.RemoteAPI;
using AprajitaRetailsService.RESTAPI.Data;

namespace AprajitaRetailsService.RESTAPI.ServiceWorker
{
    public class ServiceAction : IDisposable
    {
        private Task t = null;
        public void InsertInvoiceXML(string filename) { t = Task.Run(() => ProcessInvoiceXML(filename)); }

        private void ProcessInvoiceXML(string invoiceXMLFile)
        {
            /// Read Invoice.XML and store in object of VoygerBill
            using (VoygerXMLReader xmlReader = new VoygerXMLReader())
            {

                VoygerBill voygerBill = xmlReader.ReadInvoiceXML(invoiceXMLFile);

                if (voygerBill != null)
                {
                    InsertBillData(voygerBill);
                    //TODO:Call WebAPI and Store on RemoteDB
                    //await new WebAPI().Upload(voygerBill);
                }
                else
                {
                    LogEvent.Warning("voygerBill Readed, and it is empty");

                }
                LogEvent.WriteEvent("ProcessInvoiceXml is ended.#" + Watcher.NoOfEvent);
                Watcher.NoOfEvent = 0;
                if (voygerBill != null) voygerBill.Dispose();

                Dispose();
                xmlReader.Dispose();
            }


        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                disposedValue = true;
            }
        }
        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        public void InsertBillData(VoygerBill voygerBill)
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
                LogEvent.WriteEvent("VoyBill is added with BillId: [" + bill.VoyBillId + "]and BillNo: [" + bill.BillNumber + "]");
            }
        }


    }
}
