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
    [Authorize]
    public class ItemOrderController : ApiController
    {
        private ArtsDBEntities db = new ArtsDBEntities();

        // GET: api/ItemOrder
        public IQueryable<tbl_ItemOrder> Gettbl_ItemOrder()
        {
            return db.tbl_ItemOrder;
        }

        // GET: api/ItemOrder/5
        [ResponseType(typeof(tbl_ItemOrder))]
        public IHttpActionResult Gettbl_ItemOrder(int id)
        {
            tbl_ItemOrder tbl_ItemOrder = db.tbl_ItemOrder.Find(id);
            if (tbl_ItemOrder == null)
            {
                return NotFound();
            }

            return Ok(tbl_ItemOrder);
        }

        [ResponseType(typeof(tbl_ItemOrder))]
        [Route("api/ItemOrder/{id}/Item")]
        public IHttpActionResult Gettbl_ItemOrder_by_Item(int id)
        {
            List<tbl_ItemOrder> tbl_ItemOrder = db.tbl_ItemOrder.Where(t => t.tbl_Item_ItemId == id).ToList();
            if (tbl_ItemOrder == null)
            {
                return NotFound();
            }

            return Ok(tbl_ItemOrder);
        }

        [ResponseType(typeof(tbl_ItemOrder))]
        [Route("api/ItemOrder/{id}/User")]
        public IHttpActionResult Gettbl_ItemOrder_by_User(int id)
        {
            List<tbl_ItemOrder> tbl_ItemOrder = db.tbl_ItemOrder.Where(t => t.tbl_User_UserId == id).ToList();
            if (tbl_ItemOrder == null)
            {
                return NotFound();
            }

            return Ok(tbl_ItemOrder);
        }

        // PUT: api/ItemOrder/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_ItemOrder(int id, tbl_ItemOrder tbl_ItemOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_ItemOrder.OrderId)
            {
                return BadRequest();
            }

            db.Entry(tbl_ItemOrder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_ItemOrderExists(id))
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

        // POST: api/ItemOrder
        [ResponseType(typeof(tbl_ItemOrder))]
        public IHttpActionResult Posttbl_ItemOrder(tbl_ItemOrder tbl_ItemOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_ItemOrder.Add(tbl_ItemOrder);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tbl_ItemOrderExists(tbl_ItemOrder.tbl_Item_ItemId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbl_ItemOrder.tbl_Item_ItemId }, tbl_ItemOrder);
        }

        // DELETE: api/ItemOrder/5
        [ResponseType(typeof(tbl_ItemOrder))]
        public IHttpActionResult Deletetbl_ItemOrder(int id)
        {
            tbl_ItemOrder tbl_ItemOrder = db.tbl_ItemOrder.Find(id);
            if (tbl_ItemOrder == null)
            {
                return NotFound();
            }

            db.tbl_ItemOrder.Remove(tbl_ItemOrder);
            db.SaveChanges();

            return Ok(tbl_ItemOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_ItemOrderExists(int id)
        {
            return db.tbl_ItemOrder.Count(e => e.tbl_Item_ItemId == id) > 0;
        }
    }
}