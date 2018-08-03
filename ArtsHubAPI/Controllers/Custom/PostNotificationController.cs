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
    public class PostNotificationController : ApiController
    {
        private ArtsDBEntities db = new ArtsDBEntities();

        // GET: api/PostNotification
        public IQueryable<tbl_PostNotification> Gettbl_PostNotification()
        {
            return db.tbl_PostNotification;
        }

        // GET: api/PostNotification/5
        [ResponseType(typeof(tbl_PostNotification))]
        public IHttpActionResult Gettbl_PostNotification(int id)
        {
            tbl_PostNotification tbl_PostNotification = db.tbl_PostNotification.Find(id);
            if (tbl_PostNotification == null)
            {
                return NotFound();
            }

            return Ok(tbl_PostNotification);
        }

        // GET: api/PostNotification/5
        [ResponseType(typeof(tbl_PostNotification))]
        [Route("api/PostNotification/{id}/Artist")]
        public IHttpActionResult Gettbl_PostNotification_by_Artist(int id)
        {
            List<tbl_PostNotification> tbl_PostNotification = db.tbl_PostNotification.Where(t => t.ArtistId == id).ToList();
            if (tbl_PostNotification == null)
            {
                return NotFound();
            }

            return Ok(tbl_PostNotification);
        }

        // GET: api/PostNotification/5
        [ResponseType(typeof(tbl_PostNotification))]
        [Route("api/PostNotification/{id}/User")]
        public IHttpActionResult Gettbl_PostNotification_by_User(int id)
        {
            List<tbl_PostNotification> tbl_PostNotification = db.tbl_PostNotification.Where(t => t.UserId == id).ToList();
            if (tbl_PostNotification == null)
            {
                return NotFound();
            }

            return Ok(tbl_PostNotification);
        }

        // PUT: api/PostNotification/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_PostNotification(int id, tbl_PostNotification tbl_PostNotification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_PostNotification.NotificationId)
            {
                return BadRequest();
            }

            db.Entry(tbl_PostNotification).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_PostNotificationExists(id))
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

        // POST: api/PostNotification
        [ResponseType(typeof(tbl_PostNotification))]
        public IHttpActionResult Posttbl_PostNotification(tbl_PostNotification tbl_PostNotification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_PostNotification.Add(tbl_PostNotification);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tbl_PostNotificationExists(tbl_PostNotification.NotificationId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbl_PostNotification.NotificationId }, tbl_PostNotification);
        }

        // DELETE: api/PostNotification/5
        [ResponseType(typeof(tbl_PostNotification))]
        public IHttpActionResult Deletetbl_PostNotification(int id)
        {
            tbl_PostNotification tbl_PostNotification = db.tbl_PostNotification.Find(id);
            if (tbl_PostNotification == null)
            {
                return NotFound();
            }

            db.tbl_PostNotification.Remove(tbl_PostNotification);
            db.SaveChanges();

            return Ok(tbl_PostNotification);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_PostNotificationExists(int id)
        {
            return db.tbl_PostNotification.Count(e => e.NotificationId == id) > 0;
        }
    }
}