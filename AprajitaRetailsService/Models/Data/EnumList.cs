using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AprajitaRetailsService.Models.Data
{
    public enum SaleInvoiceTypes
    {
        Regular = 1, Manual = 2, SalesReturn = 3, Tailoring = 4, Others = 5
    }
    public enum SalePayMode { Cash = 1, Card = 2, Mix = 3 }
    public enum TaxType { Vat = 0, Gst = 1, SGST = 2, CGST = 3, IGST = 4 }
    public class CardType
    {
        public static readonly int Visa = 1;
        public static readonly int MasterCard = 2;
        public static readonly int Mastro = 3;
        public static readonly int Amex = 4;
        public static readonly int Dinners = 5;
        public static readonly int Rupay = 6;
    }
    public class TranscationType
    {
        public static readonly int Cash = 7;
        public static readonly int Cheque = 1;
        public static readonly int RTGS = 2;
        public static readonly int NEFT = 3;
        public static readonly int IMPS = 4;
        public static readonly int UPI = 5;
        public static readonly int PaymentAPP = 6;
        public static readonly int BankTransfer = 8;
        public static readonly int Others = 9;

        public static List<string> ToList()
        {
            List<string> list = new List<string>();
            Type t = typeof(TranscationType);

            foreach (FieldInfo p in t.GetFields())
            {
                list.Add(p.Name);
            }
            return list;
        }
    }

    public class PathList
    {
        public const string InvoiceXMLPath = "C:\\Capillary";
        public const string TabletSaleXMLPath = "D:\\VoyagerRetail\\TabletSale";
        public const string InvoiceXMLFile = "invoice.xml";
        public const string TabletSaleXMLFile = "TabletBill.XML";
        public const string TailoringHubXMLPath = "D:\\VoyagerRetail\\TailoringHub";
        public const string DataBasePath = @"D:\AprajitaRetails\Databases";
    }
}
