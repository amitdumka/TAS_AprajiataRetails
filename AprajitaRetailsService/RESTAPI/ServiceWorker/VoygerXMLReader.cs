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
    public class VoygerXMLReader:IDisposable
    {

        private  VoygerBill vBill;

        /// <summary>
        /// Read Any XML fileinto DataSet
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        private static DataSet ReadXML(string xmlFile)
        {
            if (xmlFile == "")
            {
                return null;
            }
            else if (!File.Exists(xmlFile))
            {
                return null;
            }
            using (DataSet dataSet =new DataSet())
            {
                dataSet.ReadXml(xmlFile, XmlReadMode.InferSchema);
                return dataSet;
            }
           
           
        }

        public static bool IsFileExist(string xmlFile)
        {
            if (xmlFile == "")
            {
                return false;
            }
            else if (!File.Exists(xmlFile))
            {
                return false;
            }
            else return true;
        }


        /// <summary>
        /// read invoicexml with linq
        /// </summary>
        /// <param name="filename">XML File Invoice.xml </param>
        /// <returns></returns>
        public  VoygerBill ReadInvoiceXML(string filename)
        {
            try
            {
                LogEvent.WriteEvent("ReadInvoiceXML: Started and File is : " + filename);

                if (IsFileExist(filename))
                {
                    using (TextReader tr = new StreamReader(filename))
                    {
                        DataSet dataSet = new DataSet();
                        
                        dataSet.ReadXml(tr, XmlReadMode.InferSchema);
                        
                        if (dataSet.Tables.Count > 0)
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
                            LogEvent.WriteEvent("returing vbill with No: " + vBill.bill.BillNumber);
                            dataSet.Reset();
                            dataSet.Clear();
                            dataSet.Dispose();
                            tr.Close();
                            tr.Dispose();
                            return vBill;
                        }
                        else
                        {
                            LogEvent.Warning("It doesnt have any VoyBill");
                            dataSet.Reset();
                            dataSet.Clear();
                            dataSet.Dispose();
                            return null; // Error: Incase failed to read or no data present
                        }

                    }

                }
                else
                {
                    LogEvent.Warning("It doesnt have any VoyBill and path doesnt exist");
                    return null; // Error: Incase failed to read or no data present
                }
              
                               
            }
            catch (Exception e)
            {
                LogEvent.Error("Error/Exception: " + e.Message);
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
        private  void ReadCustomer(DataTable table)
        {
            vBill.bill.CustomerName = (string)table.Rows[0][VBEle.customername];
            vBill.bill.CustomerMobile = (string)table.Rows[0][VBEle.mobile];
        }

        /// <summary>
        /// Read Line Items details : DataTable to Object
        /// </summary>
        /// <param name="table"></param>
        private  void ReadLineItems(DataTable table)
        {
            LineItem lineItem;
           
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

        private  void ReadBill(DataTable table)
        {
            vBill.bill.BillType = (string)table.Rows[0][VBEle.type];//1
            vBill.bill.BillNumber = (string)table.Rows[0][VBEle.bill_number];//2
            vBill.bill.BillTime = DateTime.Parse((string)table.Rows[0][VBEle.billing_time]);//3
            vBill.bill.StoreID = (string)table.Rows[0][VBEle.bill_store_id];//4
            vBill.bill.BillAmount = Double.Parse((string)table.Rows[0][VBEle.bill_amount]);//5
            vBill.bill.BillGrossAmount = Double.Parse((string)table.Rows[0][VBEle.bill_gross_amount]);//6
            vBill.bill.BillDiscount = Double.Parse((string)table.Rows[0][VBEle.bill_discount]);//7
            
        }

        /// <summary>
        /// PaymentDetails : DataTable To Object
        /// </summary>
        /// <param name="table"></param>
        private  int ReadPaymentDetails(DataTable table)
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    vBill = null;
                }

                
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~VoygerXMLReader()
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
