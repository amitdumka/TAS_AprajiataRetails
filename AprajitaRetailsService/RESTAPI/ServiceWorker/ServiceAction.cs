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
    public class ServiceAction
    {
        private static Task t = null;
        public static void InsertInvoiceXML(string filename) { t = Task.Run(() => ProcessInvoiceXML(filename)); }
        private static void ProcessInvoiceXML(string invoiceXMLFile)
        {
            /// Read Invoice.XML and store in object of VoygerBill
            VoygerBill voygerBill = VoygerXMLReader.ReadInvoiceXML(invoiceXMLFile);
            if (voygerBill != null)
            {
                //LogEvent.WriteEvent( "voygerBill Readed and have data _with_EF6" );
                LocalData.InsertBillData(voygerBill);
                //TODO:Call WebAPI and Store on RemoteDB
                t = Task.Run(() => RemoteAPI.WebAPI.Upload(voygerBill));
               

            }
            else
            {
                LogEvent.WriteEvent("voygerBill Readed, and it is empty");
                //TODO: Raise Event  and store in database for futher intervention.
            }
            LogEvent.WriteEvent("ProcessInvoiceXml is ended.#" + Watcher.NoOfEvent);
            Watcher.NoOfEvent = 0;
            LogEvent.WriteEvent("NoofEvent:" + Watcher.NoOfEvent);
        }
    }
}
