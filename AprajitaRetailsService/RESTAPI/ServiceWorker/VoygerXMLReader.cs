using AprajitaRetailsService.RESTAPI.Data;
using AprajitaRetailsService.RESTAPI.Utils;
using AprajitaRetailsService.RESTAPI.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprajitaRetailsService.RESTAPI.ServiceWorker
{
    /// <summary>
    /// Its Read XML File and store on to an object
    /// </summary>
    public class VoygerXMLReader
    {

        private static VoygerBill vBill;

        /// <summary>
        /// Read Any XML fileinto DataSet
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        public static DataSet ReadXML(string xmlFile)
        {
            if (xmlFile == "")
            {
                return null;
            }
            else if (!File.Exists(xmlFile))
            {
                return null;
            }
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlFile, XmlReadMode.InferSchema);
            // Return DataSet with data table
            return dataSet;
        }



        /// <summary>
        /// read invoicexml with linq
        /// </summary>
        /// <param name="filename">XML File Invoice.xml </param>
        /// <returns></returns>
        public static VoygerBill ReadInvoiceXML(string filename)
        {
            try
            {
                LogEvent.WriteEvent("ReadInvoiceXML: Started and File is : " + filename);
                DataSet dataSet = ReadXML(filename);

                if (dataSet != null || dataSet.Tables.Count > 0)
                {
                    vBill = new VoygerBill();
                    foreach (DataTable table in dataSet.Tables)
                    {
                        switch (table.TableName)
                        {
                            case VoyTable.T_Bill: ReadBill(table); break;
                            case VoyTable.T_Customer: ReadCustomer(table); break;
                            case VoyTable.T_LineItem: ReadLineItems(table); break;
                            case VoyTable.T_Payments: ReadPaymentDetails(table); break;

                            default:
                                break;
                        }
                    }
                    LogEvent.WriteEvent("returing vbill");
                    return vBill;
                }
                else
                {
                    LogEvent.WriteEvent("returing null");
                    return null; // Error: Incase failed to read or no data present
                }
            }
            catch (Exception e)
            {
                LogEvent.WriteEvent(e.Message);
                Watcher.NoOfEvent--;
                return vBill;
            }
            finally
            {
                Watcher.NoOfEvent--;
            }
        }

        // Read DataTable to Object and verify & process data
        /// <summary>
        /// Read Customer :Datatable to Object
        /// </summary>
        /// <param name="table"></param>
        public static void ReadCustomer(DataTable table)
        {
            vBill.bill.CustomerName = (string)table.Rows[0][VBEle.customername];
            vBill.bill.CustomerMobile = (string)table.Rows[0][VBEle.mobile];
        }

        /// <summary>
        /// Read Line Items details : DataTable to Object
        /// </summary>
        /// <param name="table"></param>
        public static void ReadLineItems(DataTable table)
        {
            LineItem lineItem;
            // int id = 0;
            foreach (var row in table.AsEnumerable())
            {
                lineItem = new LineItem
                {
                    Amount = Double.Parse((string)row[VBEle.amount]),//8
                    Description = (string)row[VBEle.description],//9
                    DiscountValue = Double.Parse((string)row[VBEle.discount_value]),//7
                    ItemCode = (string)row[VBEle.item_code],//3
                    LineType = (string)row[VBEle.line_item_type],//1
                    VoyBillId = -1,
                    Qty = Double.Parse((string)row[VBEle.qty]),//4
                    Rate = Double.Parse((string)row[VBEle.rate]),//5
                    Serial = Int16.Parse((string)row[VBEle.serial]),//2
                    Value = Double.Parse((string)row[VBEle.value])//6
                };

                vBill.AddLineItem(lineItem);
            }
        }

        public static void ReadBill(DataTable table)
        {
            vBill.bill.BillType = (string)table.Rows[0][VBEle.type];//1
            vBill.bill.BillNumber = (string)table.Rows[0][VBEle.bill_number];//2
            vBill.bill.BillTime = DateTime.Parse((string)table.Rows[0][VBEle.billing_time]);//3
            vBill.bill.StoreID = (string)table.Rows[0][VBEle.bill_store_id];//4
            vBill.bill.BillAmount = Double.Parse((string)table.Rows[0][VBEle.bill_amount]);//5
            vBill.bill.BillGrossAmount = Double.Parse((string)table.Rows[0][VBEle.bill_gross_amount]);//6
            vBill.bill.BillDiscount = Double.Parse((string)table.Rows[0][VBEle.bill_discount]);//7
            //vBill.bill.ID = -1;
        }

        /// <summary>
        /// PaymentDetails : DataTable To Object
        /// </summary>
        /// <param name="table"></param>
        public static int ReadPaymentDetails(DataTable table)
        {
            VPaymentMode vPayMode;
            int id = 1;
            foreach (var row in table.AsEnumerable())
            {
                vPayMode = new VPaymentMode
                {
                    PaymentMode = (string)row[VBEle.mode],
                    PaymentValue = (string)row[VBEle.value]
                };
                vBill.AddPaymentMode(vPayMode);
            }
            return id;
        }





    }
}
