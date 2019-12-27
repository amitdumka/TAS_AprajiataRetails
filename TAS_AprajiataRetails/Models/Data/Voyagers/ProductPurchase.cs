using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TAS_AprajiataRetails.Models.Data.Voyagers
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string SuppilerName { get; set; }
        public string Warehouse { get; set; }
        public ICollection<ProductPurchase> ProductPurchases { get; set; }
    }

    public class ProductPurchase
    {
        public int ProductPurchaseID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string InvoiceNo { get; set; }
        public decimal TotalQty { get; set; }
        public decimal TotalBasicAmount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalAmount { get; set; }
        public string Remarks { get; set; }
        public int SupplierID { get; set; }
        public virtual Supplier Supplier { get; set; }
        public bool IsPaid { get; set; }
        public virtual PurchaseInwardsRegister PurchaseInwardsRegister { get; set; }

    }

    public class PurchaseInwardsRegister
    {
        public int PurchaseInwardsRegisterId { get; set; }
        public string GRNNo { get; set; }
        public DateTime GRNDate { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int ProductPurchaseId { get; set; }
        public virtual ProductPurchase ProductPurchase { get; set; }

    }
    public class PurchaseItem
    {
        public int PurchaseItemId { get; set; }
        public int ProductPurchaseId { get; set; }
        [ForeignKey("ProductItem")]
        public string Barcode { get; set; }
        public decimal Qty { get; set; }
        public decimal Cost { get; set; }
        public decimal TaxAmout { get; set; }
        public int PurchaseTaxTypeId { get; set; }
        public virtual PurchaseTaxType PurchaseTaxType { get; set; }
        public decimal CostValue { get; set; }

        public virtual ProductItem ProductItem { get; set; }

        public virtual ProductPurchase ProductPurchase { get; set; }
    }
    public class PurchaseTaxType
    {
        public int PurchaseTaxTypeId { get; set; }
        public string TaxName { get; set; }
        public TaxType TaxType { get; set; }
        public decimal CompositeRate { get; set; }
    }

    public class ProductItem
    {
        public int ProductItemId { set; get; }
        public string Barcode { get; set; }

        public int BrandId { get; set; }
        public virtual Brand BrandName { get; set; }

        public string StyleCode { get; set; }
        public string ProductName { get; set; }
        public string ItemDesc { get; set; }

        public ProductCategorys Categorys { get; set; }

        public int MainCategory { get; set; }
        public int ProductCategory { get; set; }
        public int ProductType { get; set; }


        public decimal MRP { get; set; }
        public decimal TaxRate { get; set; }    // TODO:Need to Review in final Edition
        public decimal Cost { get; set; }

        public Sizes Size { get; set; }

        public virtual ICollection<ProductPurchase> ProductPurchases { get; set; }
        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }

        
    }

}