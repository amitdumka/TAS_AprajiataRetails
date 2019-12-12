using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CHClinic.Models.Data
{
    public enum Genders { Male, Female, Transgender }
    public enum BloodGroups { APositive, BPositive, OPositive, ANegative, BNegative, ONegative, ABPositive, ABNegative }
    public enum Unit { Pcs, Lts, Ml, Gms, Pcks }
    public enum PayMode { Cash, Card, BankTransfer, Cheque, DD }

    public class Person
    {

        public int PersonId { get; set; }

        [Display(Name = "Registration No")]
        public string OPDRegistrationID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime? DateofRecord { get; set; }

        [Required(ErrorMessage = "FirstName is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public Genders Gender { get; set; }
        public int Age { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "City Name is required."), StringLength(50, ErrorMessage = "City Name cannot ne longer than 50 characters.")]
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        [Required(ErrorMessage = "MobileNo is required.")]
        [StringLength(14, MinimumLength = 10, ErrorMessage = "Mobile No cannot be longer than 14 and shorter than 10 characters.")]
        public string MobileNo { get; set; }

        public string Occupation { get; set; }
        public string Religion { get; set; }

        public virtual History PatHistory { get; set; }
        public virtual Complaint PatComplaint { get; set; }
        public virtual PhyicalExamination Examination { get; set; }
        public virtual Generalities PatGeneralities { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public Person()
        {
            Visits = new HashSet<Visit>();
            Appointments = new HashSet<Appointment>();
        }


        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }

    public class State
    {
        public int StateId { get; set; }
        [Display(Name = "State")]
        [Required(ErrorMessage = "State Name is required."), StringLength(50, ErrorMessage = "State Name cannot ne longer than 50 characters.")]
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<City> Cities { set; get; }
    }
    public class City
    {
        public int CityId { get; set; }
        [Required(ErrorMessage = "City Name is required."), StringLength(50, ErrorMessage = "City Name cannot ne longer than 50 characters."), Display(Name = "City")]
        public string CityName { get; set; }
        public int StateId { get; set; }
        public virtual State States { set; get; }
    }
    public class Country
    {
        public int CountryId { get; set; }
        [Required(ErrorMessage = "Country Name is required."), StringLength(50, ErrorMessage = "Country Name cannot ne longer than 50 characters."), Display(Name = "Country")]
        public string CountryName { get; set; }
        public virtual ICollection<State> States { get; set; }
    }
    // First Visit of Person
    public class BloodGroup
    {
        public int BloodGroupId { get; set; }
        public string Blood { get; set; }
        public string RH { get; set; }
    }
    public class History
    {
        [Key]
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public string Accomodation { get; set; }
        public string Addications { get; set; }
        public string AnyMed { get; set; }
        public string BirthPlace { get; set; }
        public string ChildAges { get; set; }
        public string Diet { get; set; }
        public string Habbit { get; set; }
        public string Hobbies { get; set; }
        public string MaritalStatus { get; set; }
        public string Moutox { get; set; }
        public int NoOfChild { get; set; }
        public string Obes { get; set; }
        public string RelationWithFamily { get; set; }
        public string SexualHistory { get; set; }
        public string Sterlization { get; set; }
        public string Vaccine { get; set; }

        public virtual Person Person { get; set; }
    }
    public class Complaint
    {
        [Key]
        [ForeignKey("Person")]
        public int PersonId { get; set; }
       
        [Display(Name = "History Complain")]
        public string HistoryComplain { get; set; }
        [Display(Name = "Matarnal Side")]
        public string MatarnalSide { get; set; }
        [Display(Name = "Own Side")]
        public string OwnSide { get; set; }
        [Display(Name = "Paternal Complian")]
        public string PaternalSide { get; set; }
        [Display(Name = "Past Complian")]
        public string PastComplian { get; set; }
        [Display(Name = "Present Complian")]
        public string PresentComplain { get; set; }

        public virtual Person Person { get; set; }
    }
    public class PhyicalExamination
    {
        [Key]
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public string Anemia { get; set; }
        public string Apperance { get; set; }

        [Display(Name = "Blood Pressure")]
        public string BP { get; set; }
        public string Built { get; set; }
        public string Clubbing { get; set; }
        public string Cynosis { get; set; }
        public string Decubities { get; set; }
        public string Facies { get; set; }
        public string Jaundance { get; set; }
        public string LymphNode { get; set; }
        public string Neck { get; set; }
        public string Nutri { get; set; }
        public string Oedema { get; set; }
        public string Pigmentation { get; set; }
        public string Pluse { get; set; }
        public string ReportDetails { get; set; }
        public string Respiration { get; set; }

        [Display(Name = "Temeprature")]
        public string Temp { get; set; }

        public virtual Person Person { get; set; }
    }
    public class Generalities
    {
        [Key]
        [ForeignKey("Person")]
        public int PersonId { get; set; }

        public string Appatite { get; set; }
        public string Aversion { get; set; }
        public string Desire { get; set; }
        public string Discharge { get; set; }
        public string Intolerance { get; set; }
        public string Mensutral { get; set; }
        public string Mental { get; set; }
        public string Modalities { get; set; }
        public string Periperation { get; set; }
        public string Salavation { get; set; }
        public string Sentation { get; set; }
        public string Sleep { get; set; }
        public string Stool { get; set; }
        public string Taste { get; set; }
        public string Tendencies { get; set; }
        public string ThermalReaction { get; set; }
        public string Thirst { get; set; }
        public string Urine { get; set; }

        public virtual Person Person { get; set; }
    }
    // Regular Visit
    public class Visit
    {
        public Visit()
        {
            this.PrescribedMeds = new HashSet<PrescribedMed>();
            this.Invoices = new HashSet<Invoice>();
        }

        public int VisitId { get; set; } //PK
        [Required]
        public int PersonId { get; set; } //FK

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Visit Date")]
        public DateTime? VisitDate { get; set; }

        //[Display(Name = "Visit No")]
        //public int VisitNo { get; set; }

        [Display(Name = "Complains")]
        public string Problems { get; set; }

        [Display(Name = "Is Revisit")]
        public bool Revisit { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Next Visit Date")]
        public DateTime? NextVisit { get; set; }

        
        [Display(Name = "Is Billable")]
        public bool Billable { get; set; }

        [Display(Name = "Is Visit Billable")]
        public bool VisitBillable { get; set; }

        [Display(Name = "Visit Charge")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal VisitCharge { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<PrescribedMed> PrescribedMeds { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }


    }
    public class PrescribedMed
    {
        public int PrescribedMedId { get; set; } //PK
        public int VisitId { get; set; } //FK

        [Required]
        [Display(Name = "Medicine Name")]
        public string MedicineName { get; set; }

        public string Description { get; set; }
        public string Power { get; set; }

        [Display(Name = "No Of Time")]
        public string NoOfTime { get; set; }

        [Display(Name = "Drops/Goli")]
        public string Quantity { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Cost { get; set; }
        public string Remarks { get; set; }
              
        public virtual Visit Visit { get; set; }
    }
    public class Invoice
    {
        public int InvoiceId { get; set; } //PK
        public int VisitId { get; set; } //FK

        [Display(Name = "Visit Charge")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal VisitCharge { get; set; }

        [Display(Name = "Medicene Charge")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal MedCharge { get; set; }
        
        [Display(Name = "Other Charges")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal OtherCharges { get; set; }
        
        [Display(Name = "Paid Amount")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Paid { get; set; }
        
        [Display(Name = "UnPaid Amount")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Dues { get; set; }
        public string Remarks { get; set; }
                
        public virtual Visit Visit { get; set; }

    }
    public class DueList
    {
        public int DueListId { get; set; }
        public int VisitId { get; set; }
       
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal ClearedAmount { get; set; }
        public bool IsCleared { get; set; }
        public virtual Visit Visit { get; set; }
    }
    public class Income
    {
        public int IncomeId { get; set; }
        public DateTime? IncomeDate { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

    }
    public class Expense
    {
        public int ExpenseId { get; set; }

        [Display(Name = "Expense Details"), Required(ErrorMessage = "Expense Details is required.")]
        public string Particulars { get; set; }

        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PayMode PayMode { get; set; }

        [Display(Name = "Is Medicine")]
        public bool IsPaidMedicine { get; set; }
        public string Remarks { get; set; }

    }
    public class ProfitLoss
    {
        public int ProfitLossId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public int IsLoss { get; set; }

    }
    public class Medicine
    {
        public int MedicineId { get; set; }

        [Display(Name = "Medicine Name")]
        [Required(ErrorMessage = "Medicine Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Medicine name cannot be longer than 100 characters.")]
        public string MedicineName { get; set; }
        public string Power { set; get; }
        public string Description { get; set; }

        [Display(Name = "Cost Price")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal CostPrice { get; set; }
        
        [Display(Name = "Selling Price")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal SellingPrice { get; set; }
        public virtual ICollection<Medicine> Medicines { get; set; }

        public Medicine()
        {
            this.Medicines = new HashSet<Medicine>();
        }
    }
    public class Stock
    {
        public int StockId { get; set; }
        public int MedicineId { get; set; }
        [Required]
        public double Quantity { get; set; }
        [Required]
        public Unit Unit { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal PurchasePrice { get; set; }
        public virtual Medicine Medicine { get; set; }
    }

    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PersonId { get; set; }
        public DateTime Date { get; set; }
        public DateTime? VisitDate { get; set; }
        public virtual Person Person { get; set; }
    }
}