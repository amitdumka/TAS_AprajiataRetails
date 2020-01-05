using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AprajitaRetails.Models.Data.Voyagers;

namespace AprajitaRetailsOps.TAS
{
    /// <summary>
    /// This class help to manage voyager data in perfect and correct manger
    /// </summary>
    public class InventoryManger
    {

        #region HelperFunctions
        public void UpdateHSNCode(VoyagerContext db, string HSNCode, int itemCode) { }

        public int GetSalesmanId(VoyagerContext db, string salesman)
        {
            try
            {
                var id = db.Salesmen.Where(c => c.SalesmanName == salesman).FirstOrDefault().SalesmanId;
                if (id > 0)
                {
                    return id;
                }
                else
                {
                    Salesman sm = new Salesman { SalesmanName = salesman };
                    db.Salesmen.Add(sm); db.SaveChanges();
                    return sm.SalesmanId;
                }
            }
            catch (Exception)
            {

                Salesman sm = new Salesman { SalesmanName = salesman };
                db.Salesmen.Add(sm); db.SaveChanges();
                return sm.SalesmanId;
            }

        }

        public int GetCustomerId(VoyagerContext db, ImportSaleItemWise item) { return 1; }

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
            else if (Cid != null)
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



            Category Cid3 = db.Categories.Where(c => c.CategoryName == tCat && !c.IsPrimaryCategory && !c.IsSecondaryCategory).FirstOrDefault();
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
            //TODO: Null Object is found
            //TODO: create if not exsits
            try
            {
                int ids = (int?)db.Brands.Where(c => c.BCode == code).FirstOrDefault().BrandId ?? -1;
                return ids;
            }
            catch (Exception ex)
            {
                Brand brand = new Brand
                {
                    BCode = code,
                    BrandName = code
                };
                db.Brands.Add(brand);
                db.SaveChanges();
                return brand.BrandId;
            }

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
            try
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
            catch (Exception)
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

        }

        #endregion

        #region Purchase

        // Converting purchase items to stock 

        public int ProcessPurchaseInward(DateTime inDate)
        {
            using (VoyagerContext db = new VoyagerContext())
            {
                int ctr = 0;
                var data = db.ImportPurchases.Where(c => c.IsDataConsumed == false && DbFunctions.TruncateTime(c.GRNDate) == DbFunctions.TruncateTime(inDate)).OrderBy(c => c.InvoiceNo).ToList();

                if (data != null && data.Count() > 0)
                {
                    ProductPurchase PurchasedProduct = null;

                    foreach (var item in data)
                    {
                        int pid = CreateProductItem(db, item);
                        if (pid != -999)
                            CreateStockItem(db, item, pid);
                        PurchasedProduct = CreatePurchaseInWard(db, item, PurchasedProduct);
                        PurchasedProduct.PurchaseItems.Add(CreatePurchaseItem(db, item, pid));
                        item.IsDataConsumed = true;
                        db.Entry(item).State = EntityState.Modified;
                        ctr++;
                    }
                    if (PurchasedProduct != null)
                        db.ProductPurchases.Add(PurchasedProduct);
                    db.SaveChanges();
                }
                return ctr;


            }//end of using
        }


        public int CreateProductItem(VoyagerContext db, ImportPurchase purchase)
        {
            int barc = db.ProductItems.Where(c => c.Barcode == purchase.Barcode).Count();

            if (barc <= 0)
            {
                ProductItem item = new ProductItem
                {
                    Barcode = purchase.Barcode,
                    Cost = purchase.Cost,
                    MRP = purchase.MRP,
                    StyleCode = purchase.StyleCode,
                    ProductName = purchase.ProductName,
                    ItemDesc = purchase.ItemDesc,
                    BrandId = GetBrand(db, purchase.StyleCode),


                };

                //spliting ProductName
                string[] PN = purchase.ProductName.Split('/');

                // Apparel / Work / Blazers
                if (PN[0] == "Apparel")
                {
                    item.Units = Units.Pcs;
                    item.Categorys = ProductCategorys.ReadyMade;
                }
                else if (PN[0] == "Suiting" || PN[0] == "Shirting")
                {
                    item.Units = Units.Meters;
                    item.Categorys = ProductCategorys.Fabric;
                }
                else
                {
                    item.Units = Units.Nos;
                    item.Categorys = ProductCategorys.Others; //TODO: For time being
                }

                List<Category> catIds = GetCategory(db, PN[0], PN[1], PN[2]);

                item.MainCategory = catIds[0];
                item.ProductCategory = catIds[1];
                item.ProductType = catIds[2];

                db.ProductItems.Add(item);
                db.SaveChanges();
                return item.ProductItemId;


            }
            else if (barc > 0)
            {
                barc = db.ProductItems.Where(c => c.Barcode == purchase.Barcode).First().ProductItemId;

                return barc;
                // Already Added 
            }
            else
            {
                return -999;//TODO: Handel this options
                            // See ever here come. 
            }
        }

        public void CreateStockItem(VoyagerContext db, ImportPurchase purchase, int pItemId)
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
                    SaleQty = 0,
                    Units = db.ProductItems.Find(pItemId).Units
                };
                db.Stocks.Add(stock);
            }
            db.SaveChanges();
        }

        public ProductPurchase CreatePurchaseInWard(VoyagerContext db, ImportPurchase purchase, ProductPurchase product)
        {
            if (product != null)
            {
                if (purchase.InvoiceNo == product.InvoiceNo)
                {
                    product.TotalAmount += (purchase.CostValue + purchase.TaxAmt);
                    product.ShippingCost += 0;//TODO: add option for adding shipping cost for fabric
                    product.TotalBasicAmount += purchase.CostValue;
                    product.TotalTax += purchase.TaxAmt;
                    product.TotalQty += purchase.Quantity;


                }
                else
                {
                    db.ProductPurchases.Add(product);
                    db.SaveChanges();

                    product = new ProductPurchase
                    {
                        InvoiceNo = purchase.InvoiceNo,
                        InWardDate = purchase.GRNDate,
                        InWardNo = purchase.GRNNo,
                        IsPaid = false,
                        PurchaseDate = purchase.InvoiceDate,
                        ShippingCost = 0,//TODO: add option for adding shipping cost for fabric
                        TotalBasicAmount = purchase.CostValue,
                        TotalTax = purchase.TaxAmt,
                        TotalQty = purchase.Quantity,
                        TotalAmount = purchase.CostValue + purchase.TaxAmt,// TODO: Check for actual DATA. 
                        Remarks = "",
                        SupplierID = GetSupplierIdOrAdd(db, purchase.SupplierName),


                    };
                    product.PurchaseItems = new List<PurchaseItem>();



                }

            }
            else
            {
                product = new ProductPurchase
                {
                    InvoiceNo = purchase.InvoiceNo,
                    InWardDate = purchase.GRNDate,
                    InWardNo = purchase.GRNNo,
                    IsPaid = false,
                    PurchaseDate = purchase.InvoiceDate,
                    ShippingCost = 0,//TODO: add option for adding shipping cost for fabric
                    TotalBasicAmount = purchase.CostValue,
                    TotalTax = purchase.TaxAmt,
                    TotalQty = purchase.Quantity,
                    TotalAmount = purchase.CostValue + purchase.TaxAmt,// TODO: Check for actual DATA. 
                    Remarks = "",
                    SupplierID = GetSupplierIdOrAdd(db, purchase.SupplierName)

                };
                product.PurchaseItems = new List<PurchaseItem>();

            }
            return product;
        }

        public PurchaseItem CreatePurchaseItem(VoyagerContext db, ImportPurchase purchase, int productId)
        {
            PurchaseItem item = new PurchaseItem
            {
                Barcode = purchase.Barcode,
                Cost = purchase.Cost,
                CostValue = purchase.CostValue,
                Qty = purchase.Quantity,
                TaxAmout = purchase.TaxAmt,
                Unit = db.ProductItems.Find(productId).Units,
                PurchaseTaxTypeId = 1,// TODO: Calculate option needed.
                ProductItemId = productId
            };
            return item;
        }


        #endregion

        #region Sale
        public int CreateSaleEntry(DateTime onDate)
        {
            using (VoyagerContext db = new VoyagerContext())
            {
                int ctr = 0;
                bool isVat = false;
                if(onDate < new DateTime(2017, 7, 1))
                {
                    isVat = true;
                }
                SaleInvoice saleInvoice = null;
                var data = db.ImportSaleItemWises.Where(c => c.IsDataConsumed == false && DbFunctions.TruncateTime(c.InvoiceDate) == DbFunctions.TruncateTime(onDate)).OrderBy(c => c.InvoiceNo).ToList();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        saleInvoice = CreateSaleInvoice(db, item, saleInvoice);
                        saleInvoice.SaleItems.Add(CreateSaleItem(db, item ));
                        ctr++;
                    }
                    if (saleInvoice != null)
                    {
                        db.SaleInvoices.Add(saleInvoice);
                        db.SaveChanges();
                    }
                    return ctr;

                }
                else
                {
                    return ctr;
                }

            }


        }

        //Invoice No	Invoice Date	Invoice Type	
        //Brand Name	Product Name	Item Desc	
        //HSN Code	BAR CODE	Style Code	Quantity	
        //MRP	Discount Amt	Basic Amt	Tax Amt	SGST Amt	CGST Amt	Line Total	Round Off	
        //Bill Amt	Payment Mode	SalesMan Name	


        private SalePaymentDetail CreatePaymentDetails(VoyagerContext db, ImportSaleItemWise item)
        {

            if (item.PaymentType == null || item.PaymentType == "")
            {
                return null;
            }
            else if (item.PaymentType == "CAS")
            {
                //cash Payment
                SalePaymentDetail payment = new SalePaymentDetail
                {
                    CashAmount = item.BillAmnt,
                    PayMode = SalePayMode.Cash
                };

                return payment;
            }
            else if (item.PaymentType == "CRD")
            {
                SalePaymentDetail payment = new SalePaymentDetail
                {
                    CardAmount = item.BillAmnt,
                    PayMode = SalePayMode.Card
                };
                //Mix Payment
                return payment;
            }
            else if (item.PaymentType == "MIX")
            {
                SalePaymentDetail payment = new SalePaymentDetail
                {
                    MixAmount = item.BillAmnt,
                    PayMode = SalePayMode.Mix
                };
                //CASH
                return payment;
            }
            else if (item.PaymentType == "SR")
            {
                SalePaymentDetail payment = new SalePaymentDetail
                {
                    CashAmount = item.BillAmnt,
                    PayMode = SalePayMode.SR
                };
                return payment;
            }
            else return null;


        }

        private SaleInvoice CreateSaleInvoice(VoyagerContext db, ImportSaleItemWise item, SaleInvoice invoice)
        {
            if (invoice != null)
            {
                if (invoice.InvoiceNo == item.InvoiceNo)
                {
                    // invoice.InvoiceNo = item.InvoiceNo;
                    //invoice.OnDate = item.InvoiceDate;
                    invoice.TotalDiscountAmount += item.Discount;
                    invoice.TotalBillAmount += item.LineTotal;
                    invoice.TotalItems += 1;//TODO: Check for count
                    invoice.TotalQty += item.Quantity;
                    invoice.RoundOffAmount += item.RoundOff;
                    invoice.TotalTaxAmount += item.SGST; //TODO: Check

                    invoice.PaymentDetail = CreatePaymentDetails(db, item);
                    invoice.CustomerId = GetCustomerId(db, item);

                }
                else
                {
                    db.SaleInvoices.Add(invoice);
                    db.SaveChanges();

                    invoice = new SaleInvoice
                    {
                        InvoiceNo = item.InvoiceNo,
                        OnDate = item.InvoiceDate,
                        TotalDiscountAmount = item.Discount,
                        TotalBillAmount = item.LineTotal,
                        TotalItems = 1,//TODO: Check for count
                        TotalQty = item.Quantity,
                        RoundOffAmount = item.RoundOff,
                        TotalTaxAmount = item.SGST, //TODO: Check
                        PaymentDetail = CreatePaymentDetails(db, item),
                        CustomerId = GetCustomerId(db, item),
                        SaleItems = new List<SaleItem>()



                    };
                }

            }
            else
            {
                invoice = new SaleInvoice
                {
                    InvoiceNo = item.InvoiceNo,
                    OnDate = item.InvoiceDate,
                    TotalDiscountAmount = item.Discount,
                    TotalBillAmount = item.LineTotal,
                    TotalItems = 1,//TODO: Check for count
                    TotalQty = item.Quantity,
                    RoundOffAmount = item.RoundOff,
                    TotalTaxAmount = item.SGST, //TODO: Check
                    PaymentDetail = CreatePaymentDetails(db, item),
                    CustomerId = GetCustomerId(db, item),
                    SaleItems = new List<SaleItem>()
                };
            }

            return invoice;


        }

        private  SaleItem CreateSaleItem(VoyagerContext db, ImportSaleItemWise item)
        {
            var pi = db.ProductItems.Where(c => c.Barcode == item.Barcode).Select(c => new { c.ProductItemId, c.Units }).FirstOrDefault();

            SaleItem saleItem = new SaleItem
            {
                BarCode = item.Barcode,
                MRP = item.MRP,
                BasicAmount = item.BasicRate,
                Discount = item.Discount,
                Qty = item.Quantity,
                TaxAmount = item.SGST,
                BillAmount = item.LineTotal,
                Units = pi.Units,
                ProductItemId = pi.ProductItemId,
                SalesmanId = GetSalesmanId(db, item.Saleman),
                SaleTaxTypeId = CreateSaleTax(db, item)

            };
            SalePurchaseManager.UpDateStock(db, pi.ProductItemId, item.Quantity, false);// TODO: Check for this working
            return saleItem;
        }

        private int CreateSaleTax(VoyagerContext db, ImportSaleItemWise item, bool isIGST = false)
        {
            if (item.Tax != 0 && item.SGST != 0)
            {
                //GST Bill
            }
            else if (item.Tax == 0 && item.SGST == 0)
            {
                //TODO: Tax implementation
            }
            else
            {

            }

            return 1;
        }
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


    public class SalePurchaseManager
    {
        /// <summary>
        /// UpDate Stock when Sale or Purchase Happen
        /// </summary>
        /// <param name="db"></param>
        /// <param name="ItemCode"></param>
        /// <param name="Qty"></param>
        /// <param name="IsPurchased"></param>
        /// <returns></returns>
        public static bool UpDateStock(VoyagerContext db, int ItemCode, double Qty, bool IsPurchased)
        {
            var stock = db.Stocks.Where(c => c.ProductItemId == ItemCode).FirstOrDefault();

            if (stock != null)
            {
                if (IsPurchased)
                {
                    // Purchase Stock;
                    stock.PurchaseQty += Qty;
                    stock.Quantity += Qty;

                }
                else
                {
                    //Sale Stock.
                    stock.SaleQty += Qty;
                    stock.Quantity -= Qty;
                }
                db.Entry(stock).State = System.Data.Entity.EntityState.Modified;

                return true;
            }
            else
                return false;

        }




    }

    public class VatSalePurchase
    {
        public int GetSalesmanId(VoyagerContext db, string salesman)
        {
            try
            {
                var id = db.Salesmen.Where(c => c.SalesmanName == salesman).FirstOrDefault().SalesmanId;
                if (id > 0)
                {
                    return id;
                }
                else
                {
                    Salesman sm = new Salesman { SalesmanName = salesman };
                    db.Salesmen.Add(sm); db.SaveChanges();
                    return sm.SalesmanId;
                }
            }
            catch (Exception)
            {

                Salesman sm = new Salesman { SalesmanName = salesman };
                db.Salesmen.Add(sm); db.SaveChanges();
                return sm.SalesmanId;
            }

        }

        public int GetCustomerId(VoyagerContext db, ImportSaleItemWise item) { return 1; }

        private SalePaymentDetail CreatePaymentDetails(VoyagerContext db, ImportSaleItemWise item)
        {

            if (item.PaymentType == null || item.PaymentType == "")
            {
                return null;
            }
            else if (item.PaymentType == "CAS")
            {
                //cash Payment
                SalePaymentDetail payment = new SalePaymentDetail
                {
                    CashAmount = item.BillAmnt,
                    PayMode = SalePayMode.Cash
                };

                return payment;
            }
            else if (item.PaymentType == "CRD")
            {
                SalePaymentDetail payment = new SalePaymentDetail
                {
                    CardAmount = item.BillAmnt,
                    PayMode = SalePayMode.Card
                };
                //Mix Payment
                return payment;
            }
            else if (item.PaymentType == "MIX")
            {
                SalePaymentDetail payment = new SalePaymentDetail
                {
                    MixAmount = item.BillAmnt,
                    PayMode = SalePayMode.Mix
                };
                //CASH
                return payment;
            }
            else if (item.PaymentType == "SR")
            {
                SalePaymentDetail payment = new SalePaymentDetail
                {
                    CashAmount = item.BillAmnt,
                    PayMode = SalePayMode.SR
                };
                return payment;
            }
            else return null;


        }

        private SaleInvoice CreateSaleInvoice(VoyagerContext db, ImportSaleItemWise item, SaleInvoice invoice)
        {
            if (invoice != null)
            {
                if (invoice.InvoiceNo == item.InvoiceNo)
                {
                    // invoice.InvoiceNo = item.InvoiceNo;
                    //invoice.OnDate = item.InvoiceDate;
                    invoice.TotalDiscountAmount += item.Discount;
                    invoice.TotalBillAmount += item.LineTotal;
                    invoice.TotalItems += 1;//TODO: Check for count
                    invoice.TotalQty += item.Quantity;
                    invoice.RoundOffAmount += item.RoundOff;
                    invoice.TotalTaxAmount += item.SGST; //TODO: Check

                    invoice.PaymentDetail = CreatePaymentDetails(db, item);
                    invoice.CustomerId = GetCustomerId(db, item);

                }
                else
                {
                    db.SaleInvoices.Add(invoice);
                    db.SaveChanges();

                    invoice = new SaleInvoice
                    {
                        InvoiceNo = item.InvoiceNo,
                        OnDate = item.InvoiceDate,
                        TotalDiscountAmount = item.Discount,
                        TotalBillAmount = item.LineTotal,
                        TotalItems = 1,//TODO: Check for count
                        TotalQty = item.Quantity,
                        RoundOffAmount = item.RoundOff,
                        TotalTaxAmount = item.SGST, //TODO: Check
                        PaymentDetail = CreatePaymentDetails(db, item),
                        CustomerId = GetCustomerId(db, item),
                        SaleItems = new List<SaleItem>()



                    };
                }

            }
            else
            {
                invoice = new SaleInvoice
                {
                    InvoiceNo = item.InvoiceNo,
                    OnDate = item.InvoiceDate,
                    TotalDiscountAmount = item.Discount,
                    TotalBillAmount = item.LineTotal,
                    TotalItems = 1,//TODO: Check for count
                    TotalQty = item.Quantity,
                    RoundOffAmount = item.RoundOff,
                    TotalTaxAmount = item.SGST, //TODO: Check
                    PaymentDetail = CreatePaymentDetails(db, item),
                    CustomerId = GetCustomerId(db, item),
                    SaleItems = new List<SaleItem>()
                };
            }

            return invoice;


        }

        private SaleItem CreateSaleItem(VoyagerContext db, ImportSaleItemWise item)
        {
            var pi = db.ProductItems.Where(c => c.Barcode == item.Barcode).Select(c => new { c.ProductItemId, c.Units }).FirstOrDefault();

            SaleItem saleItem = new SaleItem
            {
                BarCode = item.Barcode,
                MRP = item.MRP,
                BasicAmount = item.BasicRate,
                Discount = item.Discount,
                Qty = item.Quantity,
                TaxAmount = item.SGST,
                BillAmount = item.LineTotal,
                Units = pi.Units,
                ProductItemId = pi.ProductItemId,
                SalesmanId = GetSalesmanId(db, item.Saleman),
                SaleTaxTypeId = CreateSaleTax(db, item)

            };
            SalePurchaseManager.UpDateStock(db, pi.ProductItemId, item.Quantity, false);// TODO: Check for this working
            return saleItem;
        }

        private int CreateSaleTax(VoyagerContext db, ImportSaleItemWise item, bool isIGST = false)
        {
            if (item.Tax != 0 && item.SGST != 0)
            {
                //GST Bill
            }
            else if (item.Tax == 0 && item.SGST == 0)
            {
                //TODO: Tax implementation
            }
            else
            {

            }

            return 1;
        }

    }
}