using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using AprajitaRetailsService.RESTAPI.Utils;
using AprajitaRetailsService.RESTAPI.Data;
using AprajitaRetailsService.RESTAPI.View;

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

        public static async Task UploadBill(VoyagerBillInfo bill)
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri(DataConstant.API_URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            try
            {
                var url = await AddBill(bill);
                LogEvent.WriteEvent($"Invoice Upload with url : {url}");
            }
            catch (Exception e)
            {

                LogEvent.WriteEvent("UploadBill: " + e.Message);
            }
        }

        public static async Task<Uri> AddBill(VoyagerBillInfo bill)
        {

            HttpResponseMessage response = await client.PostAsJsonAsync("api/VoyagerBills", bill);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
                LogEvent.WriteEvent("Invocie is saved: " + bill.InvoiceNo);
            return response.Headers.Location;
        }

        public static async Task Upload(VoygerBill vBill)
        {
            //TODO: Here implement or add function to convert payment mode to PayModes unit. so less amount of data will be uploaded
            VoyagerBillInfo bill = new VoyagerBillInfo()
            {
                 Amount=(decimal) vBill.bill.BillAmount, BillDate=vBill.bill.BillTime??DateTime.Now,
                 ImportDate= DateTime.Now,InvoiceNo=vBill.bill.BillNumber, IsUsed=false,PayModes=0

            };

            await UploadBill(bill);

        }

    }
}
