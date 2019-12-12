using AprajitaRetailsService.RESTAPI.Utils;
using AprajitaRetailsService.RESTAPI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AprajitaRetailsService.RESTAPI.RemoteAPI;

namespace AprajitaRetailsService.RESTAPI.ServiceWorker
{
    public class ServiceAction:IDisposable
    {
        private Task t = null;
        public  void InsertInvoiceXML(string filename) { t = Task.Run(() => ProcessInvoiceXML(filename)); }
        
        private static void ProcessInvoiceXML(string invoiceXMLFile)
        {
            /// Read Invoice.XML and store in object of VoygerBill
           // VoygerBill voygerBill = new VoygerXMLReader().ReadInvoiceXML(invoiceXMLFile);

            using (VoygerBill voygerBill = new VoygerXMLReader().ReadInvoiceXML(invoiceXMLFile))
            {
                if (voygerBill != null)
                {

                    new LocalData().InsertBillData(voygerBill);
                    //TODO:Call WebAPI and Store on RemoteDB
                    // t = Task.Run(() => RemoteAPI.WebAPI.Upload(voygerBill));


                }
                else
                {
                    LogEvent.Warning("voygerBill Readed, and it is empty");
                    //TODO: Raise Event  and store in database for futher intervention.
                }


                voygerBill.Dispose();
                

                LogEvent.WriteEvent("ProcessInvoiceXml is ended.#" + Watcher.NoOfEvent);
                Watcher.NoOfEvent = 0;
                
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

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ServiceAction()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion


    }
}
