using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TAS_AprajiataRetails.Models.Data;

namespace TAS_AprajiataRetails.Controllers
{
    [Authorize]
    public class AttendancesController : Controller
    {

        static int CountDays(DayOfWeek day, DateTime start, DateTime end)
        {
            TimeSpan ts = end - start;                       // Total duration
            int count = (int) Math.Floor (ts.TotalDays / 7);   // Number of whole weeks
            int remainder = (int) ( ts.TotalDays % 7 );         // Number of remaining days
            int sinceLastDay = (int) ( end.DayOfWeek - day );   // Number of days since last [day]
            if ( sinceLastDay < 0 )
                sinceLastDay += 7;         // Adjust for negative days since last [day]

            // If the days in excess of an even week are greater than or equal to the number days since the last [day], then count this one, too.
            if ( remainder >= sinceLastDay )
                count++;

            return count;
        }
        static int CountDays(DayOfWeek day, DateTime curMnt)
        {
            DateTime start = new DateTime (curMnt.Year, curMnt.Month, 1);
            DateTime end = new DateTime (curMnt.Year, curMnt.Month, DateTime.DaysInMonth (curMnt.Year, curMnt.Month));
            TimeSpan ts = end - start;                       // Total duration
            int count = (int) Math.Floor (ts.TotalDays / 7);   // Number of whole weeks
            int remainder = (int) ( ts.TotalDays % 7 );         // Number of remaining days
            int sinceLastDay = (int) ( end.DayOfWeek - day );   // Number of days since last [day]
            if ( sinceLastDay < 0 )
                sinceLastDay += 7;         // Adjust for negative days since last [day]

            // If the days in excess of an even week are greater than or equal to the number days since the last [day], then count this one, too.
            if ( remainder >= sinceLastDay )
                count++;

            return count;
        }

        private AprajitaRetailsContext db = new AprajitaRetailsContext ();

        // GET: Attendances
        public ActionResult Index()
        {
            var attendances = db.Attendances.Include (a => a.Employee).Where (c => DbFunctions.TruncateTime (c.AttDate) == DbFunctions.TruncateTime (DateTime.Today)).OrderByDescending (c => c.AttDate);
            return View (attendances.ToList ());
        }

        public ActionResult EmpDetails(int? id)
        {
            if ( id == null )
            {
                return new HttpStatusCodeResult (HttpStatusCode.BadRequest);
            }
            //Attendance attendance = db.Attendances.Include (c => c.Employee).Where (c => c.AttendanceId == id).FirstOrDefault ();

            var empid = db.Attendances.Find (id).EmployeeId;
            var attList = db.Attendances.Include (c => c.Employee)
                .Where (c => c.EmployeeId == empid && DbFunctions.TruncateTime (c.AttDate).Value.Month == DbFunctions.TruncateTime (DateTime.Today).Value.Month)
                .OrderBy (c => c.AttDate);

            var p = attList.Where (c => c.Status == AttUnits.Present).Count ();
            var a = attList.Where (c => c.Status == AttUnits.Absent).Count ();
            int noofdays = DateTime.DaysInMonth (DateTime.Today.Year, DateTime.Today.Month);
            int noofsunday = CountDays (DayOfWeek.Sunday, DateTime.Today);
            int sunPresent = attList.Where (c => c.Status == AttUnits.Sunday).Count ();
            int halfDays= attList.Where (c => c.Status == AttUnits.HalfDay).Count ();
            int totalAtt = p + sunPresent + ( halfDays / 2 );

            ViewBag.Present = p;
            ViewBag.Absent = a;
            ViewBag.WorkingDays = noofdays;
            ViewBag.Sundays = noofsunday;
            ViewBag.SundayPresent = sunPresent;
            ViewBag.HalfDays = halfDays;
            ViewBag.Total = totalAtt;


            if ( attList == null )
            {
                return HttpNotFound ();
            }
            ViewBag.EmpName = attList.First ().Employee.StaffName;
            return PartialView (attList);
        }

        // GET: Attendances/Details/5
        public ActionResult Details(int? id)
        {
            if ( id == null )
            {
                return new HttpStatusCodeResult (HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find (id);
            if ( attendance == null )
            {
                return HttpNotFound ();
            }
            return PartialView (attendance);
        }

        // GET: Attendances/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList (db.Employees, "EmployeeId", "StaffName");
            return PartialView ();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind (Include = "AttendanceId,EmployeeId,AttDate,EntryTime,Status,Remarks")] Attendance attendance)
        {
            if ( ModelState.IsValid )
            {
                db.Attendances.Add (attendance);
                db.SaveChanges ();
                return RedirectToAction ("Index");
            }

            ViewBag.EmployeeId = new SelectList (db.Employees, "EmployeeId", "StaffName", attendance.EmployeeId);
            return View (attendance);
        }

        // GET: Attendances/Edit/5
        public ActionResult Edit(int? id)
        {
            if ( id == null )
            {
                return new HttpStatusCodeResult (HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find (id);
            if ( attendance == null )
            {
                return HttpNotFound ();
            }
            ViewBag.EmployeeId = new SelectList (db.Employees, "EmployeeId", "StaffName", attendance.EmployeeId);
            return PartialView (attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind (Include = "AttendanceId,EmployeeId,AttDate,EntryTime,Status,Remarks")] Attendance attendance)
        {
            if ( ModelState.IsValid )
            {
                db.Entry (attendance).State = EntityState.Modified;
                db.SaveChanges ();
                return RedirectToAction ("Index");
            }
            ViewBag.EmployeeId = new SelectList (db.Employees, "EmployeeId", "StaffName", attendance.EmployeeId);
            return View (attendance);
        }

        // GET: Attendances/Delete/5
        public ActionResult Delete(int? id)
        {
            if ( id == null )
            {
                return new HttpStatusCodeResult (HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find (id);
            if ( attendance == null )
            {
                return HttpNotFound ();
            }
            return PartialView (attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance attendance = db.Attendances.Find (id);
            db.Attendances.Remove (attendance);
            db.SaveChanges ();
            return RedirectToAction ("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if ( disposing )
            {
                db.Dispose ();
            }
            base.Dispose (disposing);
        }
    }
}
