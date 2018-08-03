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
    public class ItemNotificationController : ApiController
    {
        private ArtsDBEntities db = new ArtsDBEntities();

        // GET: api/ItemNotification
        public IQueryable<tbl_ItemNotification> Gettbl_ItemNotification()
        {
            return db.tbl_ItemNotification;
        }

        // GET: api/ItemNotification/5
        [ResponseType(typeof(tbl_ItemNotification))]
        public IHttpActionResult Gettbl_ItemNotification(int id)
        {
            tbl_ItemNotification tbl_ItemNotification = db.tbl_ItemNotification.Find(id);
            if (tbl_ItemNotification == null)
            {
                return NotFound();
            }

            return Ok(tbl_ItemNotification);
        }

        // GET: api/ItemNotification/5
        [Route("api/ItemNotification/{id}/Buyer")]
        [ResponseType(typeof(tbl_ItemNotification))]
        public IHttpActionResult Gettbl_ItemNotification_by_Buyer(int id)
        {
            List<tbl_ItemNotification> tbl_ItemNotification = db.tbl_ItemNotification.Where(t => t.BuyerId == id).ToList();
            if (tbl_ItemNotification == null)
            {
                return NotFound();
            }

            return Ok(tbl_ItemNotification);
        }

        // GET: api/ItemNotification/5
        [Route("api/ItemNotification/{id}/Seller")]
        [ResponseType(typeof(tbl_ItemNotification))]
        public IHttpActionResult Gettbl_ItemNotification_by_Seller(int id)
        {
            List<tbl_ItemNotification> tbl_ItemNotification = db.tbl_ItemNotification.Where(t => t.SellerId == id).ToList();
            if (tbl_ItemNotification == null)
            {
                return NotFound();
            }

            return Ok(tbl_ItemNotification);
        }

        // PUT: api/ItemNotification/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_ItemNotification(int id, tbl_ItemNotification tbl_ItemNotification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_ItemNotification.NotificationId)
            {
                return BadRequest();
            }

            db.Entry(tbl_ItemNotification).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_ItemNotificationExists(id))
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

        //// POST: api/ItemNotification
        //[ResponseType(typeof(tbl_ItemNotification))]
        //public IHttpActionResult Posttbl_ItemNotification(tbl_ItemNotification tbl_ItemNotification)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.tbl_ItemNotification.Add(tbl_ItemNotification);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (tbl_ItemNotificationExists(tbl_ItemNotification.NotificationId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = tbl_ItemNotification.NotificationId }, tbl_ItemNotification);
        //}

        // DELETE: api/ItemNotification/5
        [ResponseType(typeof(tbl_ItemNotification))]
        public IHttpActionResult Deletetbl_ItemNotification(int id)
        {
            tbl_ItemNotification tbl_ItemNotification = db.tbl_ItemNotification.Find(id);
            if (tbl_ItemNotification == null)
            {
                return NotFound();
            }

            db.tbl_ItemNotification.Remove(tbl_ItemNotification);
            db.SaveChanges();

            return Ok(tbl_ItemNotification);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_ItemNotificationExists(int id)
        {
            return db.tbl_ItemNotification.Count(e => e.NotificationId == id) > 0;
        }
    }
}