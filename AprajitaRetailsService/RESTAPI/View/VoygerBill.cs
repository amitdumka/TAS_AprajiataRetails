using AprajitaRetailsService.RESTAPI.Data;
using System;
using System.Collections.Generic;

namespace AprajitaRetailsService.RESTAPI.View
{
    public class VoygerBill:IDisposable
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    bill = null;
                    lineItems.Clear();
                    payModes.Clear();
                    lineItems = null;
                    payModes = null;

                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~VoygerBill()
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
