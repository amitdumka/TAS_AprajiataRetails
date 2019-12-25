using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAS_AprajiataRetails.Models.Data.Voyagers;

namespace TAS_AprajiataRetails.Ops.TAS
{
    /// <summary>
    /// This class help to manage voyager data in perfect and correct manger
    /// </summary>
    public class InventoryManger
    {
        private int GetBrandID(VoyagerContext db, string code)
        {
            int ids = (int?)db.Brands.Where(c => c.BCode == code).FirstOrDefault().BrandId ?? -1;
            return ids;
        }

        private int GetSupplierIdOrAdd(VoyagerContext db, string sup)
        {
            int ids = (int?)db.Suppliers.Where(c => c.SuppilerName == sup).FirstOrDefault().SupplierID ?? -1;
            if (ids > 0) return ids;
            else if (ids == -1)
            {
                Supplier supplier = new Supplier
                {
                    SuppilerName = sup,
                    Warehouse = sup
                };
                db.Suppliers.Add(supplier);
                db.SaveChanges();
                return supplier.SupplierID;
            }
            else return 1;// Suspense Supplier
        }

        #region Purchase

        // Converting purchase items to stock 

        public void CreateProductItem(ImportPurchase purchase)
        {

            using (VoyagerContext db = new VoyagerContext())
            {
                int barc = db.ProductItems.Where(c => c.Barcode == purchase.Barcode).Count();
                if (barc > 0)
                {
                    ProductItem item = new ProductItem
                    {
                        Barcode = purchase.Barcode,
                        Cost = purchase.Cost,
                        MRP = purchase.MRP,
                        StyleCode = purchase.StyleCode,
                        ProductName = purchase.ProductName,
                        ItemDesc = purchase.ItemDesc,
                        SupplierId = GetSupplierIdOrAdd(db, purchase.SupplierName)
                    };
                    //spliting ProductName
                    string[] PN = purchase.ProductName.Split('/');

                    // Apparel / Work / Blazers
                    if (PN[0] == "Apparel") item.Categorys = ProductCategorys.ReadyMade;
                    else if (PN[0] == "Suiting" || PN[0] == "Shirting") item.Categorys = ProductCategorys.Fabric;
                    else item.Categorys = ProductCategorys.Others; //TODO: For time being

                    Category cat1 = new Category { CategoryName = PN[0], IsPrimaryCategory = true };
                    Category cat2 = new Category { CategoryName = PN[1], IsSecondaryCategory = true };
                    Category cat3 = new Category { CategoryName = PN[2] };
                    // Add or Update 


                    //Adding BrandName, 
                    if (purchase.StyleCode.StartsWith("U"))
                    {
                        //USPAit    
                        item.BrandId = GetBrandID(db, "USPA");

                    }
                    else if (purchase.StyleCode.StartsWith("AR"))
                    {
                        //Arvind RTW
                        item.BrandId = GetBrandID(db, "RTW");
                    }
                    else if (purchase.StyleCode.StartsWith("A"))
                    {
                        //Arrow
                        item.BrandId = GetBrandID(db, "ARW");
                    }
                    else if (purchase.StyleCode.StartsWith("FM"))
                    {
                        //FM
                        item.BrandId = GetBrandID(db, "FM");
                    }
                    else
                    {
                        // Arvind Store
                        item.BrandId = GetBrandID(db, "AS");
                    }

                    //Adding Suppliers

                }
                else
                {
                    //Allready added.
                }

            }


        }

        public void CreateStockItem(ImportPurchase purchase, int pItemId)
        {
            using (VoyagerContext db = new VoyagerContext())
            {
                Stock stcks = db.Stocks.Where(c => c.ProductItemId == pItemId).FirstOrDefault();
                if (stcks != null)
                {
                    stcks.PurchaseQty += purchase.Quantity;
                    stcks.Quantity += purchase.Quantity;
                    db.Entry(stcks).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    Stock stock = new Stock
                    {
                        PurchaseQty = purchase.Quantity,
                        Quantity = purchase.Quantity,
                        ProductItemId = pItemId,
                        SaleQty = 0
                    };
                    db.Stocks.Add(stock);
                }
                db.SaveChanges();

            }

        }

        public void CreatePurchaseInWard(ImportPurchase purchase)
        {
            using (VoyagerContext db = new VoyagerContext())
            {
                
            }
        }
        #endregion

        #region Sale

        #endregion

        #region Stocks

        #endregion

    }

    //Table for processing imported data
    class PurchaseProcess
    {
        public int PurchaseProcessId { get; set; }
        public string RefId { get; set; }
        public bool IsStockCreated { get; set; }
        public bool IsItemCreated { get; set; }
        public bool IsPurchaseEntry { get; set; }
        public bool IsAccoutingEntry { get; set; }
    }

}