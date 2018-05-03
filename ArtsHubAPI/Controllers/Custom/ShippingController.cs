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
using DAL;

namespace ArtsHubAPI.Controllers.Custom
{
    public class ShippingController : ApiController
    {
        private ArtsDBEntities db = new ArtsDBEntities();

        // GET: api/Shipping
        public IQueryable<tbl_Shipping> Gettbl_Shipping()
        {
            return db.tbl_Shipping
                    .Include(b => b.tbl_Item)
                    .Include(b => b.tbl_User);
        }

        // GET: api/Shipping/5
        [ResponseType(typeof(tbl_Shipping))]
        public IHttpActionResult Gettbl_Shipping(int id)
        {
            tbl_Shipping tbl_Shipping = db.tbl_Shipping
                            .Include(b => b.tbl_Item)
                            .Include(b => b.tbl_User)
                            .SingleOrDefault(b => b.ShippingId == id);

            if (tbl_Shipping == null)
            {
                return NotFound();
            }

            return Ok(tbl_Shipping);
        }

        // PUT: api/Shipping/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Shipping(int id, tbl_Shipping tbl_Shipping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Shipping.ShippingId)
            {
                return BadRequest();
            }

            db.Entry(tbl_Shipping).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_ShippingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(tbl_Shipping);
        }

        // POST: api/Shipping
        [ResponseType(typeof(tbl_Shipping))]
        public IHttpActionResult Posttbl_Shipping(tbl_Shipping tbl_Shipping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Shipping.Add(tbl_Shipping);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Shipping.ShippingId }, tbl_Shipping);
        }

        // DELETE: api/Shipping/5
        [ResponseType(typeof(tbl_Shipping))]
        public IHttpActionResult Deletetbl_Shipping(int id)
        {
            tbl_Shipping tbl_Shipping = db.tbl_Shipping.Find(id);
            if (tbl_Shipping == null)
            {
                return NotFound();
            }

            db.tbl_Shipping.Remove(tbl_Shipping);
            db.SaveChanges();

            return Ok(tbl_Shipping);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_ShippingExists(int id)
        {
            return db.tbl_Shipping.Count(e => e.ShippingId == id) > 0;
        }
    }
}