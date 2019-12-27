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
        private List<Category> GetCategory(VoyagerContext db, string pCat, string sCat, string tCat)
        {
            List<Category> CatIdList = new List<Category>();
            Category Cid = db.Categories.Where(c => c.CategoryName == pCat && c.IsPrimaryCategory).FirstOrDefault();
            if (Cid == null)
            {
                Category cat1 = new Category { CategoryName = pCat, IsPrimaryCategory = true };
                db.Categories.Add(cat1);
                db.SaveChanges();
                CatIdList.Add(cat1);

            }
            else if (Cid!=null)
            {
                CatIdList.Add(Cid);
            }
            else { }

            //id = 0;
            Category Cid2 = db.Categories.Where(c => c.CategoryName == sCat && c.IsSecondaryCategory).FirstOrDefault();
            if (Cid2 == null)
            {
                Category cat2 = new Category { CategoryName = sCat, IsSecondaryCategory = true };
                db.Categories.Add(cat2);
                db.SaveChanges();
                CatIdList.Add(cat2);

            }
            else if (Cid2 != null)
            {
                CatIdList.Add(Cid2);
            }
            else { }



            Category Cid3 = db.Categories.Where(c => c.CategoryName == tCat && c.IsPrimaryCategory && !c.IsSecondaryCategory).FirstOrDefault();
            if (Cid3 == null)
            {
                Category cat3 = new Category { CategoryName = tCat };
                db.Categories.Add(cat3);
                db.SaveChanges();
                CatIdList.Add(cat3);

            }
            else if (Cid3 != null)
            {
                CatIdList.Add(Cid3);
            }
            else { }
            

            return CatIdList;




        }

        private List<int> GetCategoryId(VoyagerContext db, string pCat, string sCat, string tCat)
        {
            List<int> CatIdList = new List<int>();
            int id = (int?)db.Categories.Where(c => c.CategoryName == pCat && c.IsPrimaryCategory).FirstOrDefault().CategoryId ?? -1;
            if (id == -1)
            {
                Category cat1 = new Category { CategoryName = pCat, IsPrimaryCategory = true };
                db.Categories.Add(cat1);
                db.SaveChanges();
                CatIdList.Add(cat1.CategoryId);

            }
            else if (id > 0)
            {
                CatIdList.Add(id);
            }
            else { }

            id = 0;
            id = (int?)db.Categories.Where(c => c.CategoryName == sCat && c.IsSecondaryCategory).FirstOrDefault().CategoryId ?? -1;
            if (id == -1)
            {
                Category cat2 = new Category { CategoryName = sCat, IsSecondaryCategory = true };
                db.Categories.Add(cat2);
                db.SaveChanges();
                CatIdList.Add(cat2.CategoryId);

            }
            else if (id > 0)
            {
                CatIdList.Add(id);
            }
            else { }
            id = 0;


            id = (int?)db.Categories.Where(c => c.CategoryName == tCat && c.IsPrimaryCategory && !c.IsSecondaryCategory).FirstOrDefault().CategoryId ?? -1;
            if (id == -1)
            {
                Category cat3 = new Category { CategoryName = tCat };
                db.Categories.Add(cat3);
                db.SaveChanges();
                CatIdList.Add(cat3.CategoryId);

            }
            else if (id > 0)
            {
                CatIdList.Add(id);
            }
            else { }
            id = 0;

            return CatIdList;




        }

        private int GetBrandID(VoyagerContext db, string code)
        {
            int ids = (int?)db.Brands.Where(c => c.BCode == code).FirstOrDefault().BrandId ?? -1;
            return ids;
        }
        private int GetBrand(VoyagerContext db, string StyleCode)
        {
            if (StyleCode.StartsWith("U"))
            {
                //USPAit    
                return GetBrandID(db, "USPA");

            }
            else if (StyleCode.StartsWith("AR"))
            {
                //Arvind RTW
                return GetBrandID(db, "RTW");
            }
            else if (StyleCode.StartsWith("A"))
            {
                //Arrow
                return GetBrandID(db, "ARW");
            }
            else if (StyleCode.StartsWith("FM"))
            {
                //FM
                return GetBrandID(db, "FM");
            }
            else
            {
                // Arvind Store
                return GetBrandID(db, "AS");
            }

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

        public int ProcessPurchaseInward(DateTime inDate)
        {
            using (VoyagerContext db = new VoyagerContext())
            {
                int ctr = 0;
                var data = db.ImportPurchases.Where(c => c.IsDataConsumed == false && c.GRNDate == inDate);
                if (data != null && data.Count() > 0)
                {
                    foreach (var item in data)
                    {

                        int pid = CreateProductItem(item);
                        if (pid != -999)
                            CreateStockItem(item, pid);
                        ctr++;
                    }
                }
                return ctr;


            }//end of using
        }


        public int CreateProductItem(ImportPurchase purchase)
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
                        //SupplierId = GetSupplierIdOrAdd(db, purchase.SupplierName),

                        BrandId = GetBrand(db, purchase.StyleCode)

                    };

                    //spliting ProductName
                    string[] PN = purchase.ProductName.Split('/');

                    // Apparel / Work / Blazers
                    if (PN[0] == "Apparel") item.Categorys = ProductCategorys.ReadyMade;
                    else if (PN[0] == "Suiting" || PN[0] == "Shirting") item.Categorys = ProductCategorys.Fabric;
                    else item.Categorys = ProductCategorys.Others; //TODO: For time being

                    List<Category> catIds = GetCategory(db, PN[0], PN[1], PN[2]);
                    item.MainCategory = catIds[0];
                    item.ProductCategory = catIds[1];
                    item.ProductType = catIds[2];

                    db.ProductItems.Add(item);
                    db.SaveChanges();
                    return item.ProductItemId;


                }
                else
                {
                    return -999;//TODO: Handel this options
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