using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TAS_AprajiataRetails.Models.Data.Voy;

namespace TAS_AprajiataRetails.Controllers
{
    public class VoyagerBillsController : ApiController
    {
        private VoyDB db = new VoyDB();

        // GET: api/VoyagerBills
        public IQueryable<VoyagerBill> GetBills()
        {
            return db.Bills;
        }

        // GET: api/VoyagerBills/5
        [ResponseType(typeof(VoyagerBill))]
        public IHttpActionResult GetVoyagerBill(int id)
        {
            VoyagerBill voyagerBill = db.Bills.Find(id);
            if (voyagerBill == null)
            {
                return NotFound();
            }

            return Ok(voyagerBill);
        }

        // PUT: api/VoyagerBills/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVoyagerBill(int id, VoyagerBill voyagerBill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != voyagerBill.VoyagerBillId)
            {
                return BadRequest();
            }

            db.Entry(voyagerBill).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoyagerBillExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/VoyagerBills
        [ResponseType(typeof(VoyagerBill))]
        public IHttpActionResult PostVoyagerBill(VoyagerBill voyagerBill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bills.Add(voyagerBill);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = voyagerBill.VoyagerBillId }, voyagerBill);
        }

        // DELETE: api/VoyagerBills/5
        [ResponseType(typeof(VoyagerBill))]
        public IHttpActionResult DeleteVoyagerBill(int id)
        {
            VoyagerBill voyagerBill = db.Bills.Find(id);
            if (voyagerBill == null)
            {
                return NotFound();
            }

            db.Bills.Remove(voyagerBill);
            db.SaveChanges();

            return Ok(voyagerBill);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VoyagerBillExists(int id)
        {
            return db.Bills.Count(e => e.VoyagerBillId == id) > 0;
        }
    }
}