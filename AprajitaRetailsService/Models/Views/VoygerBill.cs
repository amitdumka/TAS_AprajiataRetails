using AprajitaRetailsService.Models.Data;
using System.Collections.Generic;

//using AprajitaRetailsDB.DataBase.Voyager;
//using System.Collections.Generic;
//using System.Linq;


//using AprajitaRetailMonitor.SeviceWorker.Common;
//using AprajitaRetailsDB.DataBase.Voyager;
//using AprajitaRetailsDB.DataTypes;
//using System;
//using System.Data;

//using AprajitaRetails.Utils;
//using CyberN.Utility;
//using System.ServiceProcess;

namespace AprajitaRetailsService.Models.Views
{
    public class VoygerBill
    {
        public VoyBill bill;
        public List<VPaymentMode> payModes;
        public List<LineItem> lineItems;

        public VoygerBill()
        {
            bill = new VoyBill();
            lineItems = new List<LineItem>();
            payModes = new List<VPaymentMode>();
        }

        public void AddBillDetails(VoyBill voyBill)
        {
            bill = voyBill;
        }

        public void AddLineItem(LineItem items)
        {
            lineItems.Add(items);
        }

        public void AddPaymentMode(VPaymentMode vPaymentMode)
        {
            payModes.Add(vPaymentMode);
        }
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

