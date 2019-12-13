using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using AprajitaRetailsService.RESTAPI.Utils;
using AprajitaRetailsService.RESTAPI.Data;
using AprajitaRetailsService.RESTAPI.View;
using System.Linq;

namespace AprajitaRetailsService.RESTAPI.RemoteAPI
{
    public class WebAPI
    {
        private static HttpClient client = new HttpClient();

        public static async Task<int> AddBills(List<VoyagerBillInfo> bills)
        {
            int ctr = 0;
            foreach (var item in bills)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/VoyagerBills", item);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                    ctr++;
            }

            return ctr;
        }


        public static async Task UploadBills(List<VoyagerBillInfo> bills)
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri(DataConstant.API_URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            try
            {
                var unit = await AddBills(bills);
                LogEvent.WriteEvent($"No of Invoices Added : {unit}");
            }
            catch (Exception e)
            {

                LogEvent.WriteEvent("UploadBills: " + e.Message);
            }

        }

        private async Task UploadBill(VoyagerBillInfo bill)
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri(DataConstant.API_URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            LogEvent.WriteEvent($"Invoice Uploader with bill {bill.InvoiceNo} with Base address {client.BaseAddress.AbsoluteUri} .");

            try
            {
                var url = await AddBill(bill);
                LogEvent.WriteEvent($"Invoice Upload with url : {url}");
            }
            catch (Exception e)
            {

                LogEvent.WriteEvent("UploadBill: " + e.Message);
                LogEvent.WriteEvent("UpLoadBillError:" + e.StackTrace);
            }
        }

        private async Task<Uri> AddBill(VoyagerBillInfo bill)
        {

            HttpResponseMessage response = await client.PostAsJsonAsync("api/VoyagerBills", bill);
            LogEvent.WriteEvent("Header:" + response.Headers.ToString());
            LogEvent.WriteEvent("Content:"+response.Content.ToString());
            LogEvent.WriteEvent("Status Code:" + response.StatusCode.ToString());

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                LogEvent.WriteEvent("Invocie is saved: " + bill.InvoiceNo);
                using (VoyagerContext db= new VoyagerContext())
                {
                    InsertDataLog log= db.InsertDataLogs.Where(c => c.BillNumber == bill.InvoiceNo).FirstOrDefault();
                    log.Remark = response.Content.ToString();
                    db.Entry(log).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    
                    

                }
            }
            return response.Headers.Location;
        }

        public async Task Upload(VoygerBill vBill)
        {
            //TODO: Here implement or add function to convert payment mode to PayModes unit. so less amount of data will be uploaded
            VoyagerBillInfo bill = new VoyagerBillInfo()
            {
                Amount = (decimal)vBill.bill.BillAmount,
                BillDate = vBill.bill.BillTime ?? DateTime.Now,
                ImportDate = DateTime.Now,
                InvoiceNo = vBill.bill.BillNumber,
                IsUsed = false,
                PayModes = 0

            };
            LogEvent.WriteEvent($"UploadFunctionStated with: {bill.VoyagerBillId}");

            await UploadBill(bill);

        }





    }
}
