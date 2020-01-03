using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AprajitaRetails.Models.Data.Voyagers
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
        public int ProductPurchaseId { get; set; }

        public string InWardNo { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InWardDate { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PurchaseDate { get; set; }
        public string InvoiceNo { get; set; }

        public double TotalQty { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalBasicAmount { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal ShippingCost { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalTax { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        public string Remarks { get; set; }

        public int SupplierID { get; set; }
        public virtual Supplier Supplier { get; set; }

        public bool IsPaid { get; set; }

        public ICollection<PurchaseItem> PurchaseItems { get; set; }

    }

    public class PurchaseItem
    {
        public int PurchaseItemId { get; set; }

        public int ProductPurchaseId { get; set; }

        //[ForeignKey("ProductItem")]
        public string Barcode { get; set; }// TODO: if not working then link with productitemid

        public decimal Qty { get; set; }
        public Units Unit { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Cost { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TaxAmout { get; set; }

        public int PurchaseTaxTypeId { get; set; }
        public virtual PurchaseTaxType PurchaseTaxType { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CostValue { get; set; }


        //Navigation Properties
        public virtual ProductItem ProductItem { get; set; }
        public virtual ProductPurchase ProductPurchase { get; set; }
    }
    public class PurchaseTaxType
    {
        public int PurchaseTaxTypeId { get; set; }
        public string TaxName { get; set; }
        public TaxType TaxType { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CompositeRate { get; set; }

        //Navigation
        public ICollection<PurchaseItem> PurchaseItems { get; set; }
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

        public Category MainCategory { get; set; }
        public Category ProductCategory { get; set; }
        public Category ProductType { get; set; }


        public decimal MRP { get; set; }
        public decimal TaxRate { get; set; }    // TODO:Need to Review in final Edition
        public decimal Cost { get; set; }

        public Sizes Size { get; set; }
        public Units Units { get; set; }

        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }


    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsPrimaryCategory { get; set; }
        public bool IsSecondaryCategory { get; set; }
    }
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string BCode { get; set; }
    }

    public class Stock
    {
        public int StockID { set; get; }
        public int ProductItemId { set; get; }
        public double Quantity { set; get; }
        public double SaleQty { get; set; }
        public double PurchaseQty { get; set; }
        public Units Units { get; set; }

        public virtual ProductItem ProductItem { get; set; }
    }

}