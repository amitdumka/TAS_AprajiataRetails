using System.Collections.Generic;

public enum Genders { Male, Female, TransGender }
public enum Units { Meters, Nos, Pcs, Packets }
public enum TaxType {  GST, SGST, CGST, IGST, VAT }

public enum SalePayMode { Cash, Card, Mix }//TODO: check update based on data present

public enum Sizes { S, M, L, XL, XXL, XXXL, T28, T30, T32, T34, T36, T38, T40, T41, T42, T44, T46, T48 }

public enum ProductCategorys { Fabric, ReadyMade, Accessiories, Tailoring, Trims, PromoItems, Coupons, GiftVouchers, Others }

public enum CardModes { DebitCard, CreditCard, AmexCard }

public enum CardTypes { Visa, MasterCard, Mastro, Amex, Dinners, Rupay, }

public enum LedgerType { Credit, Debit, Income, Expenses, Assests, Bank, Loan, Purchase, Sale, Vendor, Customer }

public enum VPayModes { CA, DC, CC, Mix, Wal, CRD, OTH }


// Aprajita Retails Context

public enum PayModes { Cash, Card, RTGS, NEFT, IMPS, Wallets, Cheques, DemandDraft, Points, Others, Coupons, MixPayments };
public enum PaymentModes { Cash, Card, RTGS, NEFT, IMPS, Wallets, Cheques, DemandDraft, Others };
public enum AttUnits { Present, Absent, HalfDay, Sunday };
public enum SalaryComponet { NetSalary, LastPcs, WOWBill, SundaySalary, Incentive, Others }
public enum BankPayModes { Cash, Card, Cheques, RTGS, NEFT, IMPS, Wallets, Others }



public enum UploadTypes { Purchase, SaleRegister, SaleItemWise, InWard, Customer }

public class UploadType
{

    public static List<string> Types = new List<string> { "Purchase", "SaleItemWise", "SaleRegister", "InWard", "Customer" };
}