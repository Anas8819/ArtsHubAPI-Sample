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
    public class ItemController : ApiController
    {
        private ArtsDBEntities db = new ArtsDBEntities();

        // GET: api/Item
        public IQueryable<tbl_Item> Gettbl_Item()
        {
            return db.tbl_Item
                .Include(b => b.tbl_ItemOrder)
                .Include(b => b.tbl_Shipping)
                .Include(b => b.tbl_Category);
        }

        // GET: api/Item/5
        [ResponseType(typeof(tbl_Item))]
        public IHttpActionResult Gettbl_Item(int id)
        {
            tbl_Item tbl_Item = db.tbl_Item.Include(b => b.tbl_ItemOrder)
                    .Include(b => b.tbl_Shipping)
                    .Include(b => b.tbl_ItemDetail)
                    .Include(b => b.tbl_Category)
                    .FirstOrDefault(b => b.ItemId == id);

            if (tbl_Item == null)
            {
                return NotFound();
            }

            return Ok(tbl_Item);
        }

        // PUT: api/Item/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Item(int id, tbl_Item tbl_Item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Item.ItemId)
            {
                return BadRequest();
            }

            db.Entry(tbl_Item).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(tbl_Item);
        }

        // POST: api/Item
        [ResponseType(typeof(tbl_Item))]
        public IHttpActionResult Posttbl_Item(tbl_Item tbl_Item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Item.Add(tbl_Item);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Item.ItemId }, tbl_Item);
        }

        // DELETE: api/Item/5
        [ResponseType(typeof(tbl_Item))]
        public IHttpActionResult Deletetbl_Item(int id)
        {
            tbl_Item tbl_Item = db.tbl_Item.Find(id);
            if (tbl_Item == null)
            {
                return NotFound();
            }

            db.tbl_Item.Remove(tbl_Item);
            db.SaveChanges();

            return Ok(tbl_Item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_ItemExists(int id)
        {
            return db.tbl_Item.Count(e => e.ItemId == id) > 0;
        }
    }
}